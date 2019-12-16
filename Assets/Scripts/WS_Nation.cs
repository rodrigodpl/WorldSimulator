using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WS_Nation
{
    public List<WS_Tile> nationTiles = null;
    public WS_Tile capital = null;

    public Color nationColor;
    
    public float storedFood             = 0.0f;
    public WS_Culture rulingCulture        = null;

    // updateable variables
    public List<WS_Tile> frontier          = new List<WS_Tile>();
    public List<WS_Tile> border            = new List<WS_Tile>();
    public List<WS_Tile> coast           = new List<WS_Tile>();
    public float population             = 0.0f;
    public float lastCycleGrowth        = 0.0f;
    public float culturalStrength       = 0.0f;

    private List<WS_Affinity> affinities    = new List<WS_Affinity>();

    public List<WS_Culture> foreignCultures = new List<WS_Culture>();
    public List<float> foreignPopulations = new List<float>();


    public WS_Affinity getAffinity(WS_Nation other)
    {
        foreach (WS_Affinity affinity in affinities)
            if (affinity.other == other)
                return affinity;

        WS_Affinity newAffinity = new WS_Affinity();
        newAffinity.CalculateAffinity(this, other);
        affinities.Add(newAffinity);
        return newAffinity;
    }

    public void RecalculateAffinities()
    {
        foreach (WS_Affinity affinity in affinities)
            affinity.CalculateAffinity(this, affinity.other);
    }

    public void UpdateData()
    {
        frontier.Clear();
        border.Clear();
        coast.Clear();

        foreignCultures.Clear();
        foreignPopulations.Clear();

        population = 0.0f;
        storedFood = 0.0f;
        lastCycleGrowth = 0.0f;

        foreach (WS_Tile tile in nationTiles)
        {
            if (tile.isFrontier) frontier.Add(tile);
            if (tile.isBorder) border.Add(tile);
            if (tile.isCoastal) coast.Add(tile);

            population += tile.Population();
            lastCycleGrowth += tile.lastCycleGrowth;
            
            for (int i = 0; i < tile.cultures.Count; i++)
            {
                if (tile.cultures[i] != rulingCulture)
                { 
                    bool insert = true;
                    for (int j = 0; j < foreignCultures.Count; j++)
                        if (tile.cultures[i] == foreignCultures[j])
                        {
                            foreignPopulations[j] += tile.culturePopulations[i];
                            insert = false;
                        }

                    if (insert)
                    {
                        foreignCultures.Add(tile.cultures[i]);
                        foreignPopulations.Add(tile.culturePopulations[i]);
                    }
                }
            }

        }

        lastCycleGrowth /= nationTiles.Count;
    }

    public void SetCulture(WS_Culture culture)
    {
        rulingCulture = culture;

        foreach (WS_Tile tile in nationTiles)
        {
            tile.mainCulture = culture;
            tile.cultures.Clear();
            tile.culturePopulations.Clear();
            tile.cultures.Add(culture);
            tile.culturePopulations.Add(tile.Population());
        }
        
    }

    public void Populate()
    {
        capital = nationTiles[0];
        nationColor = new Color(Random.Range(0.2f, 1.0f), Random.Range(0.2f, 1.0f), Random.Range(0.2f, 1.0f));

        foreach (WS_Tile tile in nationTiles)
        {
            if (tile.habitability > capital.habitability)
                capital = tile;
        }

        foreach (WS_Tile tile in nationTiles)
        {
            tile.nation = this;
            if (tile != capital)
            {
                tile.Populate(Mathf.FloorToInt(Random.Range(0.65f, 1.35f) * WS_WorldGenerator.startingPop));
                tile.urbanPercentile = Random.Range(0.05f, 0.15f);
            }
            else
            {
                tile.Populate(Mathf.FloorToInt(Random.Range(0.65f, 1.35f) * WS_WorldGenerator.startingPop * 3.0f));
                tile.urbanPercentile = Random.Range(0.40f, 0.55f);
            }
        }
    }

}

