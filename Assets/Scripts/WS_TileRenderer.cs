using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WS_TileRenderer
{
    private WS_World world                     = null;                     // pointer to world
    private WS_Tile tile                    = null;                     // pointer to owner tile
    private Color32 renderColor = new Color32();
    private Color32 lastRenderColor = new Color32();
    //private LineRenderer lineRenderer       = null;                     // tile's line renderer
    private AnimationCurve erosionCurve     = new AnimationCurve();     // curve used in erosion render mode
    private AnimationCurve defaultCurve     = new AnimationCurve();     // curve used in river render mode
    private Vector2Int renderPos = new Vector2Int();
    

    private bool wasSelected = false;

    public void setRenderPos(Vector2Int _renderPos)
    {
        renderPos = _renderPos;
    }

    public void setWorld(WS_World _world)              // set world pointer
    {
        world = _world;
    }

    public void setTile(WS_Tile _tile)              // set world pointer
    {
        tile = _tile;
    }

    public bool Render()
    {
        if (tile.seaBody && (WS_RenderPanel.rendermode != WorldRenderMode.GEOGRAPHY || WS_RenderPanel.geoFilter != GeoFilter.BIOME))
            return false;

        switch (WS_RenderPanel.rendermode)
        {
            case WorldRenderMode.GEOGRAPHY:

                switch (WS_RenderPanel.geoFilter)
                {
                    case GeoFilter.ALTITUDE:
                        float relativeAltitudeLand = tile.altitude / world.highestPoint;
                        renderColor = new Color(1.0f, 1.0f - relativeAltitudeLand, 1.0f - relativeAltitudeLand, 1.0f);
                        break;

                    case GeoFilter.TEMPERATURE:       // set color based on temperature, blue to red
                        float relativeTemperature = (tile.avgTemperature - world.lowestTemperature) / (world.highestTemperature - world.lowestTemperature);
                        renderColor = new Color(relativeTemperature, tile.seaBody ? 0.0f : 0.5f, 1.0f - relativeTemperature, 1.0f);
                        break;

                    case GeoFilter.HUMIDITY:          // set color based on humidity, black to blue
                        renderColor = new Color(0.2f, 0.0f, tile.humidity / 100.0f, 1.0f);
                        break;

                    case GeoFilter.EROSION_STRENGTH:  // create lines defining the tile's erosion strength
                        renderColor = new Color(0.3f + (0.7f * (tile.erosionStrength / world.maxLandErosion)), 0.2f, 0.2f);
                        break;

                    case GeoFilter.RIVER_STRENGTH:        // create lines defining rivers
                        if (tile.riverDirection.Count > 0) renderColor = new Color(0.2f, 0.4f, 0.7f + (0.3f * (tile.riverStrength / world.maxRiverStrength)));
                        else renderColor = Color.white;
                        break;

                    case GeoFilter.HABITABILITY:  // set color based on habitability, red to black if negative, black to green if positive
                        if (tile.habitability < 0) renderColor = new Color(tile.habitability / world.minHabitability, 0.0f, 0.0f, 1.0f);
                        else renderColor = new Color(0.0f, tile.habitability / world.maxHabitability, 0.0f, 1.0f);
                        break;

                    case GeoFilter.BIOME:

                        switch (tile.biome)
                        {
                            case Biome.POLAR: renderColor = new Color(0.6f, 0.6f, 1.0f); break;
                            case Biome.TUNDRA: renderColor = new Color(0.6f, 0.6f, 0.6f); break;
                            case Biome.BOREAL_FOREST: renderColor = new Color(0.4f, 1.0f, 0.8f); break;
                            case Biome.ALPINE: renderColor = new Color(1.0f, 0.6f, 0.4f); break;
                            case Biome.ALPINE_SHRUBLAND: renderColor = new Color(0.8f, 0.6f, 0.0f); break;
                            case Biome.ALPINE_FOREST: renderColor = new Color(0.6f, 0.6f, 0.2f); break;
                            case Biome.TEMPERATE_SHRUBLAND: renderColor = new Color(0.5f, 0.4f, 0.0f); break;
                            case Biome.TEMPERATE_GRASSLAND: renderColor = new Color(0.4f, 0.9f, 0.4f); break;
                            case Biome.TEMPERATE_FOREST: renderColor = new Color(0.0f, 0.4f, 0.1f); break;
                            case Biome.WETLANDS: renderColor = new Color(0.0f, 0.7f, 0.4f); break;
                            case Biome.SAVANNAH: renderColor = new Color(1.0f, 0.8f, 0.1f); break;
                            case Biome.TEMPERATE_DESERT: renderColor = new Color(0.9f, 0.7f, 0.4f); break;
                            case Biome.TROPICAL_GRASSLAND: renderColor = new Color(0.6f, 0.8f, 0.1f); break;
                            case Biome.TROPICAL_JUNGLE: renderColor = new Color(0.3f, 0.4f, 0.1f); break;
                            case Biome.ARID_DESERT: renderColor = new Color(1.0f, 0.5f, 0.0f); break;
                            case Biome.WATER: renderColor = new Color(0.0f, 0.2f, 0.6f); break;
                            case Biome.ICE: renderColor = new Color(1.0f, 1.0f, 1.0f); break;
                        }

                        break;

                }


                break;

            case WorldRenderMode.POPULATION:

                switch (WS_RenderPanel.popFilter)
                {
                    case PopFilter.NATION:  // set color based on habitability, red to black if negative, black to green if positive


                        if (tile.nation != null)
                        {
                            float alpha = 1.0f;

                            if (tile.settlement == Settlement.TOWN) alpha = 0.75f;
                            else if (tile.settlement == Settlement.CITY) alpha = 0.5f;

                            Color color = tile.nation.nationColor;
                            color.a = alpha;

                            renderColor = color;
                        }
                        else
                            renderColor = Color.white;

                        break;
                        
                    case PopFilter.GROWTH:

                        if (tile.Population() > 0.0f)
                        {
                            if (tile.lastCycleGrowth > 0.0f)
                                renderColor = new Color(0.0f, tile.lastCycleGrowth / WS_World.maxGrowth, 0.0f, 1.0f);
                            else
                                renderColor = new Color(Mathf.Abs(tile.lastCycleGrowth / WS_World.minGrowth), 0.0f, 0.0f, 1.0f);
                        }
                        else
                            renderColor = Color.white;

                        break;

                    case PopFilter.URBAN_PERCENTILE:

                        if (tile.Population() > 0.0f)
                            renderColor = new Color(0.3f, tile.urbanPercentile, 0.0f, 1.0f);
                        else
                            renderColor = Color.white;
                        break;
                }

                break;

            case WorldRenderMode.CULTURE:


                switch (WS_RenderPanel.culFilter)
                {
                    case CulFilter.CULTURE:  // set color based on habitability, red to black if negative, black to green if positive

                        if (tile.mainCulture != null)
                            renderColor = tile.mainCulture.cultureColor;
                        else
                            renderColor = Color.white;

                        break;
                }
                
                break;
        }

        if (!lastRenderColor.Equals(renderColor) || (wasSelected && WS_TilePanel.selectedTile != tile))
        {
            lastRenderColor = renderColor;

            for (int x = 0; x < world.hexTex.width; x++)
                for (int y = 0; y < world.hexTex.height; y++)
                {
                    if (world.pixels[(y * world.hexTex.height) + x].a > 0)
                        world.output.SetPixel(renderPos.x + x, renderPos.y + y, renderColor);
                }

            wasSelected = false;
            return true;
        }
        else if (WS_TilePanel.selectedTile == tile)
        {
            wasSelected = true;

            for (int x = 0; x < world.hexTex.width; x++)
                for (int y = 0; y < world.hexTex.height; y++)
                {
                    if (world.pixels[(y * world.hexTex.height) + x].a > 0)
                    {
                        if(world.pixels[(y * world.hexTex.height) + x].a > 240)
                            world.output.SetPixel(renderPos.x + x, renderPos.y + y, renderColor);
                        else
                            world.output.SetPixel(renderPos.x + x, renderPos.y + y, Color.black);
                    }
                }

            return true;
        }
        return false;
    }
}




                //case WorldRenderMode.RESOURCE:


                //    if (tile.Resources.Count > 0)
                //    {
                //        WS_Resource resource = null;

                //        if (WS_UIController.resourceFilter == ResourceType.NONE)
                //        {
                //            resource = tile.Resources[0];

                //            foreach (WS_Resource res in tile.Resources)
                //                if (res.amount > resource.amount)
                //                    resource = res;

                //        }
                //        else
                //        {
                //            foreach (WS_Resource res in tile.Resources)
                //                if (res.type == WS_UIController.resourceFilter)
                //                    resource = res;
                //        }

                //        if (resource != null)
                //        {
                //            switch (resource.type)
                //            {
                //                case ResourceType.IRON: renderColor = new Color(0.39f, 0.31f, 0.29f); break;
                //                case ResourceType.COPPER: renderColor = new Color(0.75f, 0.46f, 0.16f); break;
                //                case ResourceType.TIN: renderColor = new Color(0.34f, 0.34f, 0.34f); break;
                //                case ResourceType.LEAD: renderColor = new Color(0.55f, 0.55f, 0.55f); break;
                //                case ResourceType.SILVER: renderColor = new Color(0.75f, 0.75f, 0.75f); break;
                //                case ResourceType.GOLD: renderColor = new Color(0.93f, 0.78f, 0.0f); break;
                //                case ResourceType.URANIUM: renderColor = new Color(0.26f, 0.93f, 0.0f); break;
                //                case ResourceType.WOOD: renderColor = new Color(0.37f, 0.21f, 0.0f); break;
                //                case ResourceType.PASTURES: renderColor = new Color(0.12f, 0.66f, 0.0f); break;
                //                case ResourceType.FISH: renderColor = new Color(0.0f, 0.5f, 0.80f); break;
                //                case ResourceType.HUNT: renderColor = new Color(0.91f, 0.32f, 0.32f); break;
                //                case ResourceType.FURS: renderColor = new Color(0.32f, 0.18f, 0.13f); break;
                //                case ResourceType.SPICES: renderColor = new Color(0.64f, 0.1f, 0.80f); break;
                //                case ResourceType.OPIOIDS: renderColor = new Color(0.1f, 0.8f, 0.75f); break;
                //                case ResourceType.COAL: renderColor = new Color(0.14f, 0.14f, 0.14f); break;
                //                case ResourceType.OIL: renderColor = new Color(0.0f, 0.0f, 0.0f); break;
                //                case ResourceType.GRANITE: renderColor = new Color(1.0f, 0.78f, 0.80f); break;
                //                case ResourceType.CLAY: renderColor = new Color(0.84f, 0.24f, 0.0f); break;
                //                case ResourceType.MARBLE: renderColor = new Color(0.94f, 0.24f, 0.70f); break;
                //                case ResourceType.JADE: renderColor = new Color(0.0f, 0.29f, 0.09f); break;
                //                case ResourceType.SALT: renderColor = new Color(0.88f, 0.88f, 0.88f); break;
                //            }
                //        }
                //        else
                //            renderColor = Color.white;
                //    }
                //    else
                //        renderColor = Color.white;

