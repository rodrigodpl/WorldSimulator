using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Biome
{
    POLAR, TUNDRA, BOREAL_FOREST,
    ALPINE, ALPINE_SHRUBLAND, ALPINE_FOREST,
    TEMPERATE_GRASSLAND, TEMPERATE_FOREST, TEMPERATE_SHRUBLAND, WETLANDS,
    SAVANNAH, TEMPERATE_DESERT, TROPICAL_GRASSLAND, TROPICAL_JUNGLE,
    ARID_DESERT, WATER, ICE, MAX_BIOMES
}

public class River
{
    public List<WS_Tile> path;
    public string name = "";
}

public class WS_WorldGenerator 
{
    //  Utility 
    private WS_World world                  = null;     // pointer to the world container
    private List<Generator> generators      = null;     // generator list, Generator class is at the bottom of the script
    private List<River> rivers              = null;                  // river container
    private int landWS_TileNum              = 0;        // used for land mass calculations

    // Generation main variables
    public int shallowGenerators            = 3;        // 2 - 4        // shallow generators will create islands and archipelagos
    public int continentalGenerators        = 4;        // 2 - 4        // continental generators will create continental masses
    public int alpineGenerators             = 2;        // 2 - 4        // alpine generators will create mountains ranges

    public float landmassPercentage         = 0.35f;    // 30 - 80
    public float altitudeRandomizer         = 500.0f;   // 50 - 500


    // Generation secondary variables (only recommended for advanced users)
    public static bool lockPoles            = true;     // if true, land tiles will very rarely appear near the poles      

    public static int altitudeSmoothing     = 1;        // 1 - 4        // The higher the value, the smoother the altitude will be
    public static int temperatureSmoothing  = 3;        // 1 - 4        // The higher the value, the smoother the temperature will be
    public static int humiditySmoothing     = 1;        // 1 - 4        // The higher the value, the smoother the humidity will be
    public static int erosionSmoothing      = 1;        // 1 - 4        // The higher the value, the smoother the erosion will be

    public static float shallowRestriction  = 0.6f;     // 0.4 - 0.7    // The higher the value, the more fractured and small landmasses will be
    public static float alpineRestriction   = 0.3f;     // 0.4 - 0.7    // The higher the value, the smaller mountain ranges will be


    public static float maxTemperatureMod   = 10.0f;    // 5 - 20       // The higher the value, the more extreme temperature changes will be
    public static float temperatureModNum   = 0.01f;    // 0.005 - 0.2  // The higher the value, the more diverse temperature changes will be

    public static float startingLandTemp    = 35.0f;    // 30 - 40      // The higher the value, the hotter land temperature will be
    public static float startingWaterTemp   = 18.0f;    // 15 - 20      // The higher the value, the hotter ocean temperature will be
    public static float latitudeLandTemp    = 26.0f;    // 22 - 30      // The higher the value, the colder land tiles near the poles will be
    public static float latitudeWaterTemp   = 27.0f;    // 25 - 30      // The higher the value, the colder ocean tiles near the poles will be
    public static float altitudeTempLoss    = 3.0f;     // 2 - 5        // The higher the value, the colder higher tiles will be

    public static float baseHumidity        = 90.0f;    // 80 - 100     // The higher the value, the more humid the whole world will be
    public static float altitudeHumLoss     = 4.0f;     // 2 - 6        // The higher the value, the dryer higher tiles will be
    public static float temperatureHumLoss  = 1.0f;     // 0.5 - 2      // The higher the value, the dryer hotter tiles will be

    public static float altitudePressLoss   = 50.0f;    // 25 - 100     // The higher the value, the lower pressure in higher land tiles
    public static float tempPressLoss       = 50.0f;    // 25 - 100     // The higher the value, the lower pressure in hotter land tiles 
    public static float humidPressLoss      = 50.0f;    // 25 - 100     // The higher the value, the lower pressure in humid land tiles
    public static float waterAltPressLoss   = 100.0f;   // 50 - 200     // The higher the value, the higher pressure in lower sea tiles

    public static float erosionMultiplier   = 2.5f;     // 1 - 5        // The higher the value, the higher the erosion effect
    public static float erosionAltMult      = 4.0f;     // 2 - 6        // The higher the value, the higher the erosion effect on altitude
    public static float erosionHumMult      = 0.07f;    // 0.04 - 0.1   // The higher the value, the higher the erosion effect on altitude

    public static float minRiverNum         = 0.15f;    // 0.05 - 0.25  // The higher the value, the lower chance for fewer rivers to appear
    public static float maxRiverNum         = 0.25f;    // 0.1 - 0.35   // The higher the value, the higher chance for more rivers to appear
    public static float baseRiverImpulse    = 1000.0f;  // 0 - 2000     // The higher the value, the higher chance longer rivers will appear
    public static float baseRiverStrength   = 50.0f;    // 25 -300      // The higher the value, the more effect rivers will have 
    public static float riverHumEffect      = 0.1f;     // 0.05 - 0.3   // The higher the value, the more effect rivers will have on humidity
    public static float riverAltEffect      = 1.5f;     // 0.5 - 2.5    // The higher the value, the more effect rivers will have on altitude

    public static float baseHabitability    = 100.0f;   // 50 - 150     // The higher the value, the more habitable the world will be
    public static float habAltMultiplier    = 5.0f;    // 0.005 - 0.03 // The higher the value, the more uninhabitable higher tiles will be
    public static float habTempMultiplier   = 2.0f;     // 2 - 10       // The higher the value, the more uninhabitable hot or cold tiles will be
    public static float habHumMultiplier    = 1.0f;    // 0.1 - 2.5    // The higher the value, the more uninhabitable dry tiles will be
    public static float habWaterMultiplier  = 20.0f;    // 0 - 50       // The higher the value, the more habitable coastal and river tiles will be


    public int minCultureNum = 20;
    public float advCulturePercentage = 0.35f;
    public float advReligionPercentage = 0.2f;
    public float minHabitability = 70.0f;



    public void PopulateWorld()
    {
        List<WS_Culture> cultures = new List<WS_Culture>();
        List<WS_Religion> religions = new List<WS_Religion>();
        List<WS_Government> governments = new List<WS_Government>();

        while (cultures.Count < minCultureNum)
        {
            for (int i = 0; i < WS_World.sizeX; i++)
                for (int j = 0; j < WS_World.sizeY; j++)
                {
                    WS_Tile tile = world.GetTile(new Vector2Int(i, j));

                    if (!tile.seaBody && tile.habitability >= minHabitability && tile.population == 0.0f)
                    {
                        tile.population += tile.habitability * 50.0f * Random.Range(0.5f, 2.5f);
                    }

                    if (Random.Range(0.0f, 1.0f) < tile.population / 1000000)
                    {
                        tile.culture = new WS_Culture(tile);
                        tile.government = new WS_Government(tile);
                        tile.government.rulingCulture = tile.culture;
                        governments.Add(tile.government);
                        cultures.Add(tile.culture);
                        tile.population *= 1.5f;
                    }

                    if (Random.Range(0.0f, 1.0f) < tile.population / 3000000)
                    {
                        tile.religion = new WS_Religion(tile);
                        religions.Add(tile.religion);
                        tile.population *= 1.5f;
                    }

                    tile.farmers = Mathf.CeilToInt(tile.population / 1000.0f);

                }
        }


        int advCultures = Mathf.FloorToInt(advCulturePercentage * cultures.Count);

        for(int i = 0; i < advCultures; i++)
        { 
            int index = Mathf.FloorToInt(Random.Range(0.0f, cultures.Count - 0.01f));

            if (cultures[index].tribal)
            {
                WS_Culture oldCulture = cultures[index];
                WS_Tile capital = oldCulture.capital;

                WS_Culture newCulture = new WS_Culture(oldCulture, oldCulture.capital);

                oldCulture.capital.culture = newCulture;
                cultures.Add(newCulture);
                cultures.Remove(oldCulture);

                advCultures--;
            }
        }


        int advReligions = Mathf.FloorToInt(advReligionPercentage * religions.Count);

        for (int i = 0; i < advReligions; i++)
        {
            int index = Mathf.FloorToInt(Random.Range(0.0f, religions.Count - 0.01f));

            if (religions[index].tribal)
            {
                WS_Religion oldReligion = religions[index];
                WS_Tile capital = oldReligion.capital;

                WS_Religion newReligion = new WS_Religion(oldReligion, oldReligion.capital);

                oldReligion.capital.religion = newReligion;
                religions.Add(newReligion);
                religions.Remove(oldReligion);

                advReligions--;
            }
        }


        for (int i = 0; i < WS_World.sizeX; i++)
            for (int j = 0; j < WS_World.sizeY; j++)
            {
                WS_Tile tile = world.GetTile(new Vector2Int(i, j));

                if (tile.population > 0.0f)
                {
                    if (tile.culture == null)
                    {
                        WS_Culture nearestCulture = null;
                        WS_Government nearestGovernment = null;
                        int minCulDist = int.MaxValue;
                        int minGovDist = int.MaxValue;

                        foreach (WS_Culture culture in cultures)
                        {
                            int distance = tile.DistanceTo(culture.capital) + Mathf.FloorToInt(Random.Range(-1.4f, 1.4f));

                            if (distance < minCulDist)
                            {
                                minCulDist = distance;
                                nearestCulture = culture;
                            }

                            distance = tile.DistanceTo(culture.capital);

                            if (distance < minGovDist)
                            {
                                minGovDist = distance;
                                nearestGovernment = culture.capital.government;
                            }
                        }

                        tile.culture = nearestCulture;
                        tile.government = nearestGovernment;
                    }

                    if(tile.religion == null)
                    {
                        WS_Religion nearestReligion = null;
                        int minDist = int.MaxValue;

                        foreach (WS_Religion religion in religions)
                        {
                            int distance = tile.DistanceTo(religion.capital) + Mathf.FloorToInt(Random.Range(-1.4f, 1.4f));

                            if (distance < minDist)
                            {
                                minDist = distance;
                                nearestReligion = religion;
                            }
                        }

                        tile.religion = nearestReligion;
                    }
                }
            }

        foreach (WS_Government government in governments)
            government.rulingReligion = government.capital.religion;

    }



    public void Generate(WS_World _world)
    {
        world = _world;  // set world pointer


        //Step 1: Generators

        // Generators are in charge of setting altitude values. Several land generators are created, which will start gathering
        // tiles until the amount of landmass equals the one specified in (landmassPercentage). Then, land generators will start 
        // "stepping", setting altitude values on their gathered tiles. At the end, a single ocean generator is created, which will
        // fill all remaining tiles with water altitude values.

        int generatorNum = (shallowGenerators + continentalGenerators + alpineGenerators);  // amount of generators required

        generators = new List<Generator>();
        rivers = new List<River>();

        for (int i = 0; i < generatorNum;)      // create generators
        {
            Vector2Int random_pos;

            if(lockPoles)
                random_pos = new Vector2Int(Mathf.RoundToInt(Random.Range(0.0f, WS_World.sizeX - 1.0f)), Mathf.RoundToInt(Random.Range(WS_World.sizeY * 0.3f, WS_World.sizeY * 0.7f)));  // set a random position
            else
                random_pos = new Vector2Int(Mathf.RoundToInt(Random.Range(0.0f, WS_World.sizeX - 1.0f)), Mathf.RoundToInt(Random.Range(0.0f, WS_World.sizeY - 1.0f)));  // set a random position


            bool valid = true;
            foreach (Generator generator in generators)
            {
                if (world.GetTile(random_pos) == generator.tilePool[0])    // discard if there is a generator on the tile already
                {
                    valid = false;
                    break;
                }
            }

            if (valid)
            {
                Generator new_generator = new Generator();
                new_generator.tilePool.Add(world.GetTile(random_pos));  // create and initalize a new generator

                // set generator type
                if (i < shallowGenerators)                              new_generator.setType(Generator.GeneratorType.SHALLOW);
                else if (i < shallowGenerators + continentalGenerators) new_generator.setType(Generator.GeneratorType.CONTINENTAL);
                else                                                    new_generator.setType(Generator.GeneratorType.ALPINE);

                // add to list
                generators.Add(new_generator);
                i++;
            }
        }

        landWS_TileNum = Mathf.FloorToInt(WS_World.sizeX * WS_World.sizeY * (landmassPercentage + 0.1f));  // amount of land tiles required
        // NOTE: 0.1 is added to the actual (landmassPercentage) value, as posterior calculations tend to diminish the land amount

        while (landWS_TileNum > 0)   // while more land is still required
        {
            int randomGenerator = Mathf.FloorToInt(Random.Range(0.0f, generators.Count - 0.01f));  // get a random generator
            if (generators[randomGenerator].Gather())       // if successfully has gathered a tile
                landWS_TileNum--;                                  // reduce the amount of land required
        }

        // once all land has been gathered, create an ocean Generator
        OceanGenerator oceanGenerator = new OceanGenerator();

        foreach (Generator generator in generators)
            foreach (WS_Tile tile in generator.tilePool)
                oceanGenerator.tilePool.Add(tile);       // fill it with the borders of land Generator areas
        //NOTE: this allows the generator to start always from coastal tiles and into deeper water



        bool active = true;
        while (active)          // keep going until all land tiles have been set its altitude
        {
            active = false;
            for (int i = 0; i < generatorNum; i++)
            {
                generators[i].Step(altitudeRandomizer);     // on each Step, generators set the altitude of a land tile

                if (generators[i].active)
                    active = true;
            }
        }

        while (oceanGenerator.tilePool.Count > 0)           // then, the ocean generator sets the altitude of water tiles
            oceanGenerator.Step();


        // Step 2: Altitude smoothing 

        // The altitude of each tile is smoothed agains the values of its adjacent neighbors.

        for (int i = 0; i < WS_World.sizeX; i++)
            for (int j = 0; j < WS_World.sizeY; j++)
            {
                WS_Tile tile = world.GetTile(new Vector2Int(i, j));
                float totalAltitude = 0.0f;

                foreach (WS_Tile neighbor in tile.Neighbors())
                    totalAltitude += neighbor.altitude;

                totalAltitude = totalAltitude / 6.0f;                       // totalAltitude: average neighbor altitude
                tile.altitude = (tile.altitude + totalAltitude * altitudeSmoothing) / (altitudeSmoothing + 1.0f);     // average between totalAltitude and tile's altitude
            }


        // Step 3: Temperature modifiers

        // Random land tiles receive a random modifier to temperature, to achieve realisitic and diverse temperatures around
        // the world map. This step tries to emulate the local factors affecting temperature.

        int temperatureModifiers = Mathf.FloorToInt(WS_World.sizeX * WS_World.sizeY * landmassPercentage * temperatureModNum);  // one every one hundred tiles will start a modifier

        while (temperatureModifiers > 0)
        {
            WS_Tile startingWS_Tile = null;

            List<WS_Tile> tilePool = new List<WS_Tile>();
            List<WS_Tile> doneWS_Tiles = new List<WS_Tile>();

            do
            {
                Vector2Int randomPos;
                randomPos = new Vector2Int(Mathf.CeilToInt(Random.Range(0.0f, WS_World.sizeX - 1.0f)), Mathf.CeilToInt(Random.Range(0.0f, WS_World.sizeY - 1.0f)));
                startingWS_Tile = world.GetTile(randomPos);

            } while (startingWS_Tile.altitude < 0.0f);  // select a random valid land tile
                
            float modifier = Random.Range(0.0f, 1.0f) > 0.5f ? maxTemperatureMod : -maxTemperatureMod;   // set cold or hot modifier at random
            int tiles = Mathf.FloorToInt(Random.Range(WS_World.sizeX, WS_World.sizeY));     // set a random amount of affected tiles

            tilePool.Add(startingWS_Tile);             // add the starting tile to the tile pool

            while (tiles > 0 && tilePool.Count > 0)
            {
                int nextWS_Tile = Mathf.CeilToInt(Random.Range(0.0f, tilePool.Count - 1.0f));   // choose a random tile from the pool

                WS_Tile tile = tilePool[nextWS_Tile];

                tile.avgTemperature = 0.0f;
                tile.avgTemperature += Random.Range(0.6f, 1.0f) * modifier;
                tilePool.Remove(tile);                                          // apply the modifier and remove it from the pool

                foreach (WS_Tile neighbor in tile.Neighbors())     // for each neighbor of the current tile
                    if (neighbor.altitude > 0.0f)
                    {
                        if (!doneWS_Tiles.Contains(neighbor))               // if that tile has already been visited by this modifier, discard it
                            tilePool.Add(neighbor);
                    }

                doneWS_Tiles.Add(tile);
                tiles--;                // keep going until all modifier's tiles are done
            }

            temperatureModifiers--;     // keep going until all world's modifiers have been applied
        }


        // Step 4: Temperature

        // Apply a temperature value to each tile based on its latitude, altitude and proximity to the sea. This step tries to emulate 
        // global factors affecting temperature. The combination of the previous step and this one creates a world where temperatures 
        // are hotter near the equator and in continental areas, while still keeping random variations around.

        // NOTE: in order to combine steps 3 and 4, the temperature values obtained in step 4 must be ADDED to the current value.

        for (int i = 0; i < WS_World.sizeX; i++)
            for (int j = 0; j < WS_World.sizeY; j++)
            {
                WS_Tile tile = world.GetTile(new Vector2Int(i, j));

                if (float.IsNaN(tile.avgTemperature))
                    tile.avgTemperature = 0.0f;

                float latitudeMultiplier = Mathf.Abs((WS_World.sizeY / 2.0f) - tile.utility.getPosition().y) / (WS_World.sizeY / 2.0f);

                if(tile.altitude > 0.0f)    tile.avgTemperature += startingLandTemp - (latitudeMultiplier * latitudeLandTemp); // Latitude modifier
                else                        tile.avgTemperature += startingWaterTemp - (latitudeMultiplier * latitudeWaterTemp); // Latitude modifier

                tile.avgTemperature += Random.Range(-5.0f, 5.0f);  // Random modifier


                if (tile.altitude > 0.0f)   // if land tile
                {
                    tile.avgTemperature -= (tile.altitude / 1000.0f) * altitudeTempLoss; // Altitude 

                    bool waterFound = false;

                    for (int r = 1; r <= 3 && !waterFound; r++)
                    {
                        foreach (WS_Tile neighbor in tile.Neighbors(r))
                            if (neighbor.seaBody)
                            {
                                waterFound = true;
                                tile.avgTemperature = (tile.avgTemperature + neighbor.avgTemperature * (4 - r)) / (4 - r + 1); // Land sea attenuation
                                break;
                            }
                    }

                }
                else
                    tile.avgTemperature = (tile.avgTemperature + 17.0f) / 2.0f; // Ocean sea attenuation
            }


        // Step 5: Temperature smoothing + Humidity

        // On this step, temperature values are first smoothed against its neighbors, to then apply humidity values based on
        // the tile's altitude, temperature and proximity to the sea.

        for (int i = 0; i < WS_World.sizeX; i++)
            for (int j = 0; j < WS_World.sizeY; j++)
            {
                // TEMPERATURE SMOOTHING
                WS_Tile tile = world.GetTile(new Vector2Int(i, j));

                float totalTemperature = 0.0f;

                foreach (WS_Tile neighbor in tile.Neighbors())
                    totalTemperature += neighbor.avgTemperature;

                totalTemperature = totalTemperature / 6.0f;   // totalTemperature: average neighbor's temperature
                tile.avgTemperature = (tile.avgTemperature + (totalTemperature * temperatureSmoothing)) / (temperatureSmoothing + 1.0f);  // tilted average between temperature and totalTemperature

                if (tile.avgTemperature < world.lowestTemperature) world.lowestTemperature = tile.avgTemperature;
                if (tile.avgTemperature > world.highestTemperature) world.highestTemperature = tile.avgTemperature;


                // HUMIDITY PASS
                tile.humidity = baseHumidity;  // starting humidity

                if (tile.altitude > 0.0f)  // if land tile
                {
                    // reduce humidity based on temperature
                    if (tile.avgTemperature > 30.0f)        tile.humidity -= temperatureHumLoss * (40.0f + (tile.avgTemperature - 25.0f));
                    else if (tile.avgTemperature > 20.0f)   tile.humidity -= temperatureHumLoss * (20.0f + (tile.avgTemperature - 15.0f));
                    else if (tile.avgTemperature > 10.0f)   tile.humidity -= temperatureHumLoss * (30.0f + (tile.avgTemperature - 5.0f));
                    else                                    tile.humidity -= temperatureHumLoss * (50.0f + tile.avgTemperature);

                    tile.humidity += Random.Range(-10.0f, 10.0f);   // Random modifier
                    tile.humidity -= (tile.altitude / 1000.0f) * altitudeHumLoss;    // Altitude

                    bool waterFound = false;
                    for (int r = 1; r <= 3 && !waterFound; r++)
                    {
                        foreach (WS_Tile neighbor in tile.Neighbors(r))
                            if (neighbor.altitude < 0.0f)
                            {
                                waterFound = true;
                                tile.humidity += 20.0f - (5.0f * (r - 1)); // Sea proximity
                                break;
                            }

                    }
                    
                }

                // cap values to 2 - 98
                if (tile.humidity < 2.0f) tile.humidity = Random.Range(2.0f, 6.0f);
                else if (tile.humidity > 98.0f) tile.humidity = Random.Range(94.0f, 98.0f);
            }


        // Step 6: Humidity smoothing + Air/Water Pressure

        // On this step, humidity values are first smoothed against its neighbors, to then calculate the tile's air pressure based on
        // its altitude, temperature and humidity. Although the variable is named airPressure, in water tiles it should be read
        // Water Pressure, defining how water currents behave.

        for (int i = 0; i < WS_World.sizeX; i++)
            for (int j = 0; j < WS_World.sizeY; j++)
            {
                // HUMIDITY SMOOTHING
                WS_Tile tile = world.GetTile(new Vector2Int(i, j));

                float totalHumidity = 0.0f;

                foreach (WS_Tile neighbor in tile.Neighbors())
                    totalHumidity += neighbor.humidity;

                totalHumidity = totalHumidity / 6.0f; // totalHumidity: average neighbor's humidity
                tile.humidity = (tile.humidity + (totalHumidity * humiditySmoothing)) / (humiditySmoothing + 1.0f);   // average between humidity and totalHumidity

                // AIR PRESSURE PASS
                tile.airPressure = 1000.0f;
                tile.airPressure -= tempPressLoss * ((tile.avgTemperature - 21.0f) / 10.0f); // Temperature

                if (tile.altitude > 0.0f)
                {
                    tile.airPressure -= altitudePressLoss * ((tile.altitude - 1000.0f) / 2000.0f); // Altitude
                    tile.airPressure += humidPressLoss * ((tile.humidity - 50.0f) / 100.0f); // Humidity
                }
                else
                    tile.airPressure -= waterAltPressLoss * (-tile.altitude / 2000.0f); // Altitude
            }

        // Step 7: Wind/Water currents + coastal labeling

        // In this step, the previous values of Air/Water pressure are checked against its neighbors, creating currents towards the
        // neighboring tile with lowest air pressure. As coastal tiles may have been changed due to previous steps, coastal tiles are
        // searched for again and stored in a list for calculations in the step 9: Rivers.

        List<WS_Tile> coastalWS_Tiles = new List<WS_Tile>();

        for (int i = 0; i < WS_World.sizeX; i++)
            for (int j = 0; j < WS_World.sizeY; j++)
            {
                WS_Tile tile = world.GetTile(new Vector2Int(i, j));

                tile.erosionDirection = tile;

                foreach (WS_Tile neighbor in tile.Neighbors())
                {
                    if (tile.altitude > 0.0f && neighbor.altitude > 0.0f)
                    {
                        if (tile.erosionDirection.airPressure > neighbor.airPressure)
                            tile.erosionDirection = neighbor;       // set (erosionDirection) to the neighbor with lower Pressure
                    }
                }


                foreach (WS_Tile neighbor in tile.Neighbors())
                    if (neighbor.altitude < 0.0f && tile.altitude > 0.0f)   // if a tile is land but a neighbor is water
                    {
                        coastalWS_Tiles.Add(tile);     // add it to the coastal tiles list
                        break;
                    }


                if (tile.erosionDirection != tile)      // (erosionStrength) is proportional to the difference in Pressure between tiles
                    tile.erosionStrength = (tile.airPressure - tile.erosionDirection.airPressure) * erosionMultiplier;   
            }

        // Step 8: Erosion Strength smoothing 

        // On this step, (erosionStrength) is smoothed against the values of the tile's neighbors

        for (int i = 0; i < WS_World.sizeX; i++)
            for (int j = 0; j < WS_World.sizeY; j++)
            {
                WS_Tile tile = world.GetTile(new Vector2Int(i, j));

                float totalErosion = 0.0f;
                int count = 0;

                if (float.IsNaN(tile.erosionStrength))
                    tile.erosionStrength = 0.0f;


                foreach (WS_Tile neighbor in tile.Neighbors())   
                {
                    if (!float.IsNaN(neighbor.erosionStrength))
                    {
                        if ((tile.altitude > 0.0f && neighbor.altitude > 0.0f) || (tile.altitude < 0.0f && neighbor.altitude < 0.0f))
                        {       // land tiles will only check against land tiles and water tiles against water tiles.
                            totalErosion += neighbor.erosionStrength;
                            count++;
                        }
                    }
                }

                if (count > 0)
                {
                    totalErosion = totalErosion / count;        // totalErosion: average erosion of valid neighbors
                    tile.erosionStrength = (totalErosion + (tile.erosionStrength * erosionSmoothing)) / (erosionSmoothing + 1.0f);  // average between erosion and totalErosion
                }
            }


        // Step 9: Rivers

        // On this step, rivers are created. In order to do so, first a set of random coastal tiles are selected as river endings, 
        // which will try to grow towards higher tiles, generating the river "backwards". Once the highest tile available has been 
        // reached, the river path is reversed. It will then set the river direction and strength on each of tiles of the river path.
        // (riverStrength) is based on how long is the river, the amount of humidity on the tiles it travels, and the altitude 
        // difference between the start and the end tile.

        int riverAmount = Mathf.FloorToInt(coastalWS_Tiles.Count * Random.Range(minRiverNum, maxRiverNum));
        // there will up to a quarter of coastal tiles which can spawn a river
        
        while (riverAmount > 0)   // while there are remaining rivers to be created
        {
            WS_Tile tile = coastalWS_Tiles[Mathf.FloorToInt(Random.Range(0.0f, coastalWS_Tiles.Count - 0.01f))];

            if (!float.IsNaN(tile.riverStrength))   // select a random coastal tile which does not contain a river already
                continue;

            List<WS_Tile> riverPath = new List<WS_Tile>();
            riverPath.Add(tile);
            
            float riverImpulse = baseRiverImpulse;     // 500 - 2000
            // river impulse allows river to grow towards tiles lower that the current tile a limited amount of times
            // in effect, higher river Impulse will create longer rivers

            while (true)    // while the river can keep growing
            {
                WS_Tile maxAltitudeTile = tile.Neighbors()[0];
                foreach (WS_Tile neighbor in tile.Neighbors())   // for each neighbor
                {
                    if (neighbor.altitude > maxAltitudeTile.altitude && neighbor.altitude > 0.0f && !riverPath.Contains(neighbor))  // choose the higher one
                        maxAltitudeTile = neighbor;
                }

                if (maxAltitudeTile.altitude > tile.altitude - 200.0f && tile.altitude - maxAltitudeTile.altitude < riverImpulse)  // if high enough
                {
                    if (tile.altitude > maxAltitudeTile.altitude)  // if it has required riverImpulse
                    {
                        tile.altitude = maxAltitudeTile.altitude - Random.Range(20.0f, 50.0f);    // lower its own altitude to match

                        if (tile.altitude < 0.0f)                           // check if it has become a sea tile
                            tile.altitude = Random.Range(20.0f, 50.0f);      // and avoid if its the case

                        riverImpulse -= tile.altitude - maxAltitudeTile.altitude;               // lower riverImpulse
                    }
                    riverPath.Add(maxAltitudeTile);     // add the tile to the river Path
                    tile = maxAltitudeTile;

                    if (!float.IsNaN(tile.riverStrength))
                        break;
                }
                else
                    break;

            }

            // once the river can't grow any more

            if (riverPath.Count > 3 || !float.IsNaN(riverPath[riverPath.Count - 1].riverStrength))  // if long enough or affluent to another river
            {
                River new_river = new River();
                riverPath.Reverse();
                new_river.path = riverPath;
                rivers.Add(new_river);   // reverse the path and store the river

                for (int i = 0; i < riverPath.Count; i++)
                {
                    if (float.IsNaN(riverPath[i].riverStrength))    // set strength if not set already
                    {
                        if (i == 0)         // if first tile of the river
                            riverPath[i].riverStrength = baseRiverStrength;   
                        else if (i != 0)    // if any other tile of the tiver
                        {
                            riverPath[i].riverStrength = riverPath[i - 1].riverStrength + (riverPath[i].humidity / 10.0f);
                            riverPath[i].riverStrength += (riverPath[i - 1].altitude - riverPath[i].altitude) / 40.0f;

                            if (riverPath[i].riverStrength > world.maxRiverStrength) world.maxRiverStrength = riverPath[i].riverStrength;
                        }
                    }

                    if (i < riverPath.Count - 1)
                        riverPath[i].riverDirection.Add(riverPath[i + 1]);  // set the river direction in all tiles but last one, as its a water tile
                }

                riverAmount--;   // until all requested rivers have been defined
            }
        }


        // Step 10: River erosion

        // On this steps, rivers will apply water erosion by lowering the altitude and increasing humidity of all the tiles 
        // inside the river's path-

        foreach (River river in rivers)
        {
            foreach (WS_Tile tile in river.path)
            {
                if (tile.altitude > 0.0f)
                {
                    tile.humidity += tile.riverStrength * riverHumEffect;  // add humidity

                    if (tile.altitude - tile.riverStrength * riverAltEffect > 0.0f)   // lower altitude unless the result is lower than 0
                        tile.altitude -= tile.riverStrength * riverAltEffect;

                    foreach (WS_Tile neighbor in tile.Neighbors())   // apply the same effect but reduced to neighboring tiles
                    {
                        neighbor.humidity += tile.riverStrength * (riverHumEffect / 2.0f);

                        if (tile.altitude - tile.riverStrength * (riverAltEffect / 2.0f) > 0.0f)
                            neighbor.altitude -= tile.riverStrength * (riverAltEffect / 2.0f);
                    }
                }

            }

        }

        // Step 11: Currents Erosion

        // On this step, wind erosion will transport altitude and humidity from one tile to its erosion direction tile

        for (int i = 0; i < WS_World.sizeX; i++)
            for (int j = 0; j < WS_World.sizeY; j++)
            {
                WS_Tile tile = world.GetTile(new Vector2Int(i, j));

                if (tile.erosionDirection != tile)
                {
                    tile.erosionDirection.altitude += tile.erosionStrength * erosionAltMult;      // add altitude to erosion direction tile
                    tile.altitude -= tile.erosionStrength * erosionAltMult;                       // remove altitude from itself


                    tile.erosionDirection.humidity += tile.erosionStrength * erosionHumMult;     // add humidity to erosion direction tile

                    if (tile.altitude > 0.0f)
                    {
                        tile.humidity -= tile.erosionStrength * erosionHumMult; // remove humidity only if we are not a water tile
                        if (tile.erosionStrength > world.maxLandErosion) world.maxLandErosion = tile.erosionStrength;
                    }
                    else if (tile.erosionStrength > world.maxSeaErosion) world.maxSeaErosion = tile.erosionStrength;
                }

            }

        // Step 12: Erosion smoothing + Biome

        // Erosion from both rivers and winds is likely to have created coarse altitude and humidity values, so this step starts
        // by applying again a smoothing towards altitude and humidity. Then it will assign the tile's biome based on its altitude, 
        // temperature and humidity.

        for (int i = 0; i < WS_World.sizeX; i++)
            for (int j = 0; j < WS_World.sizeY; j++)
            {
                // EROSION SMOOTHING
                WS_Tile tile = world.GetTile(new Vector2Int(i, j));

                float totalAltitude = 0.0f;
                float totalHumidity = 0.0f;

                foreach (WS_Tile neighbor in tile.Neighbors())
                {
                    totalAltitude += neighbor.altitude;
                    totalHumidity += neighbor.humidity;
                }

                totalAltitude = totalAltitude / 6.0f;
                totalHumidity = totalHumidity / 6.0f;

                tile.altitude = (tile.altitude * 3.0f + totalAltitude) / 4.0f;
                tile.humidity = (tile.humidity + totalHumidity) / 2.0f;


                if (tile.altitude < 0.0f)
                    tile.seaBody = true;

                if (tile.humidity < 2.0f) tile.humidity = Random.Range(2.0f, 6.0f);
                else if (tile.humidity > 98.0f) tile.humidity = Random.Range(94.0f, 98.0f);

                if (tile.altitude < world.lowestPoint) world.lowestPoint = tile.altitude;
                if (tile.altitude > world.highestPoint) world.highestPoint = tile.altitude;


                // BIOME SETTING
                if (!tile.seaBody)   // if land tile
                {
                    if (tile.avgTemperature < 3.0f || tile.altitude > 5500.0f)      tile.biome = Biome.POLAR;
                    else if (tile.altitude > 4000.0f)                               tile.biome = Biome.ALPINE;
                    else if (tile.altitude > 3000.0f && tile.humidity >= 40.0f)     tile.biome = Biome.ALPINE_FOREST;
                    else if (tile.altitude > 3000.0f && tile.humidity < 40.0f)      tile.biome = Biome.ALPINE_SHRUBLAND;
                    else if (tile.avgTemperature < 15.0 && tile.humidity >= 50.0f)  tile.biome = Biome.BOREAL_FOREST;
                    else if (tile.avgTemperature < 15.0 && tile.humidity < 50.0f)   tile.biome = Biome.TUNDRA;
                    else if (tile.avgTemperature < 25.0 && tile.humidity < 40.0f)   tile.biome = Biome.TEMPERATE_SHRUBLAND;
                    else if (tile.avgTemperature < 25.0 && tile.humidity < 55.0f)   tile.biome = Biome.TEMPERATE_GRASSLAND;
                    else if (tile.avgTemperature < 25.0 && tile.humidity < 85.0f)   tile.biome = Biome.TEMPERATE_FOREST;
                    else if (tile.avgTemperature < 25.0 && tile.humidity < 100.0f)  tile.biome = Biome.WETLANDS;
                    else if (tile.avgTemperature < 35.0 && tile.humidity < 40.0f)   tile.biome = Biome.TEMPERATE_DESERT;
                    else if (tile.avgTemperature < 35.0 && tile.humidity < 50.0f)   tile.biome = Biome.SAVANNAH;
                    else if (tile.avgTemperature < 35.0 && tile.humidity < 60.0f)   tile.biome = Biome.TROPICAL_GRASSLAND;
                    else if (tile.avgTemperature < 35.0 && tile.humidity < 100.0f)  tile.biome = Biome.TROPICAL_JUNGLE;
                    else                                                            tile.biome = Biome.ARID_DESERT;
                }
                else
                {
                    if (tile.avgTemperature < 5.0f) tile.biome = Biome.ICE;
                    else tile.biome = Biome.WATER;
                }

            }

        // Step 13: River fixing

        // Due to the changes in altitude applied by the erosion steps, is possible that a tile where a river ends has raised its 
        // altitude and is no longer a sea tile, causing a river ending in a land tile. To avoid this, end tiles of all rivers are 
        // checked. If a river ends in land tile, it will grow until reaching a water tile again.

        foreach (River river in rivers)
        {
            WS_Tile destination = river.path[river.path.Count - 1];   // get the end tile

            if (destination.altitude > 0.0f)    // if land tile now
            {
                foreach (WS_Tile neighbor in destination.Neighbors())  
                    if (neighbor.altitude < 0.0f && river.path.Count > 1)   // look for neighboring water tiles
                    {
                        destination.riverDirection.Add(neighbor);
                        destination.riverStrength = river.path[river.path.Count - 1].riverStrength;
                        river.path.Add(neighbor);   // and grow the river
                        break;
                    }
            }
        }

        // Step 13: Biome smoothing + Resources + Habitability

        // On this steps, first biomes will be smoothed to create somewhat homogeneous areas with the same biome rather than scattered
        // values around the map. To do so, if a tile is surrounded by four neighboring tiles with the same biome between them but
        // different of the starting tile's biome, the tile will change its biome towards that of the neighboring majority. Once finished,
        // random Resources will be set on the tile based on its biome. Then, habitability will be calculated for each tile
        //  based on its altitude, temperature, humidity, proximity to water and rivers, biome and resources.


        for (int i = 0; i < WS_World.sizeX; i++)
            for (int j = 0; j < WS_World.sizeY; j++)
            {
                // BIOME SMOOTHING
                WS_Tile tile = world.GetTile(new Vector2Int(i, j));

                if (!tile.seaBody)
                {
                    int[] biomeCount = new int[(int)Biome.MAX_BIOMES];  // initialize biome counter
                    for (int b = 0; b < (int)Biome.MAX_BIOMES; b++)
                        biomeCount[b] = 0;

                    foreach (WS_Tile neighbor in tile.Neighbors()) // count neighboring biomes
                        biomeCount[(int)neighbor.biome]++;

                    for (int b = 0; b < (int)Biome.MAX_BIOMES; b++)     // if four neighbors have the same biome, change our own
                        if (biomeCount[b] >= 4 && b != (int)tile.biome)
                            tile.biome = (Biome)b;
                }

                if (tile.biome == Biome.WATER || tile.biome == Biome.ICE)  // if a land tile has become a water tile due to this...
                {
                    tile.seaBody = true;
                    if (tile.altitude > 0.0f)
                    {
                        tile.altitude = -tile.altitude;      // invert altitude so its negative

                        bool landFound = false;

                        foreach (WS_Tile neighbor in tile.Neighbors())   // check if its coastal
                            if (neighbor.altitude > 0.0f)
                            {
                                landFound = true;
                                break;
                            }

                        if (!landFound)
                            tile.erosionStrength = 0;   // remove erosion strength unless its coastal
                    }

                    tile.riverStrength = 0;    // remove any river

                }

                // RESOURCES
                if (!tile.seaBody)
                {
                    float max_chance = 0.0f;
                    List<WS_ResourceSource> possibleRes = new List<WS_ResourceSource>();

                    for (int r = 0; r < (int)ResourceType.MAX; r++)
                    {
                        float chance = WS_World.resources[r].Chance(tile);

                        if (chance > 0.0f)
                        {
                            max_chance += chance;
                            possibleRes.Add(WS_World.resources[r]);
                        }
                    }
                    float selector = Random.Range(0.0f, max_chance - 0.01f);

                    foreach (WS_ResourceSource res in possibleRes)
                    {
                        float chance = res.Chance(tile);

                        if (selector < chance)
                        {
                            WS_ResourceSource newRes = new WS_ResourceSource(res.type);
                            newRes.abundance = Random.Range(0.2f, 1.0f);
                            newRes.quality = Random.Range(0.1f, 1.0f);
                            tile.resource = newRes;
                            break;
                        }
                        else
                            selector -= chance;
                    }
                }
            

                // HABITABILITY

                if (tile.seaBody)
                    tile.habitability = -100.0f;       // if water tile, uninhabitable
                else
                {
                    switch (tile.biome)         // biome
                    {
                        case Biome.POLAR: tile.habitability = 50.0f; break;
                        case Biome.TUNDRA: tile.habitability = 70.0f; break;
                        case Biome.BOREAL_FOREST: tile.habitability = 60.0f; break;
                        case Biome.ALPINE: tile.habitability = 60.0f; break;
                        case Biome.ALPINE_SHRUBLAND: tile.habitability = 70.0f; break;
                        case Biome.ALPINE_FOREST: tile.habitability = 70.0f; break;
                        case Biome.TEMPERATE_GRASSLAND: tile.habitability = 120.0f; break;
                        case Biome.TEMPERATE_FOREST: tile.habitability = 80.0f; break;
                        case Biome.WETLANDS: tile.habitability = 120.0f; break;
                        case Biome.SAVANNAH: tile.habitability = 80.0f; break;
                        case Biome.TEMPERATE_DESERT: tile.habitability = 70.0f; break;
                        case Biome.TROPICAL_GRASSLAND: tile.habitability = 100.0f; break;
                        case Biome.TROPICAL_JUNGLE: tile.habitability = 70.0f; break;
                        case Biome.ARID_DESERT: tile.habitability = 50.0f; break;
                    }


                    tile.habitability -= Mathf.Abs(tile.avgTemperature - 21.0f) * habTempMultiplier;    // temperature
                    tile.habitability -= Mathf.Abs(tile.humidity - 70.0f) * habHumMultiplier;           // humidity
                    tile.habitability -= (tile.altitude / 1000.0f) * habAltMultiplier;                  // altitude
                    tile.habitability += 20.0f;


                    if (!float.IsNaN(tile.riverStrength))       // river adjacent
                        tile.habitability += habWaterMultiplier;
                    else
                    {
                        foreach (WS_Tile neighbor in tile.Neighbors())  // river proximity
                            if (!float.IsNaN(neighbor.riverStrength))
                            {
                                tile.habitability += habWaterMultiplier * 0.25f;
                                break;
                            }
                    }

                    bool seaAdjacent = false;
                    foreach (WS_Tile neighbor in tile.Neighbors())     // sea adjacent
                        if (neighbor.seaBody)
                        {
                            tile.habitability += habWaterMultiplier;
                            seaAdjacent = true;
                            break;
                        }

                    if (!seaAdjacent)
                    {
                        foreach (WS_Tile neighbor in tile.Neighbors(2)) // sea proximity
                            if (neighbor.seaBody)
                            {
                                tile.habitability += habWaterMultiplier * 0.25f;
                                break;
                            }
                    }

                    if (tile.habitability > world.maxHabitability) world.maxHabitability = tile.habitability;
                    if (tile.habitability < world.minHabitability) world.minHabitability = tile.habitability;
                }

            }

        // step 14: Real landmass percentage

        // The world generation process design has been focused in generating a real-like factible world rather than to strictly 
        // fulfill world generation user-defined parameters. Therefore, its likely than the real landmass percentage generated 
        // differs slighltly from the value input by the user, with a current error margin of (+-)5%.

        // To properly inform the user of the real amount of landmass generated, land tiles are accounted in this step.

        int landmassCount = 0;
        for (int i = 0; i < WS_World.sizeX; i++)
            for (int j = 0; j < WS_World.sizeY; j++)
            {
                WS_Tile tile = world.GetTile(new Vector2Int(i, j));

                if (!tile.seaBody)
                    landmassCount++;
            }

        landmassPercentage = (float)landmassCount / ((float)WS_World.sizeX * (float)WS_World.sizeY);
    }


    // GENERATORS

    public class Generator   // land Generator
    {
        public enum GeneratorType { NONE, SHALLOW, CONTINENTAL, ALPINE }

        public List<WS_Tile> tilePool  = new List<WS_Tile>();
        public List<WS_Tile> landWS_Tiles = new List<WS_Tile>();
        public bool active          = true;

        float startingAltitude      = 0.0f;
        GeneratorType type          = GeneratorType.NONE;

        public void setType(GeneratorType _type)  // set type and random starting altitude
        {
            type = _type;

            switch (type)
            {
                case GeneratorType.SHALLOW:     startingAltitude = Random.Range(50.0f, 500.0f); break;
                case GeneratorType.CONTINENTAL: startingAltitude = Random.Range(500.0f, 1500.0f); break;
                case GeneratorType.ALPINE:      startingAltitude = Random.Range(2000.0f, 3000.0f); break;
            }
            
        }

        public bool Gather()
        {
            bool repeat = false;   //  if a tile has been discarded by a Shallow Restriction, repeat
            do
            {
                if(type == GeneratorType.ALPINE && Random.Range(0.0f, 1.0f) > alpineRestriction)
                    return false;

                repeat = false;
                int nextWS_Tile = Mathf.FloorToInt(Random.Range(Random.Range(0.0f, tilePool.Count - 1.0f), tilePool.Count - 0.01f));

                if (tilePool.Count == 0)
                    return false;

                WS_Tile tile = tilePool[nextWS_Tile];     // chose a random tile of the pool

                if (lockPoles)
                {
                    if (tile.utility.getPosition().y < Random.Range(0.03f, 0.33f) * WS_World.sizeY || tile.utility.getPosition().y > Random.Range(0.66f,0.96f) * WS_World.sizeY)
                        return false;   // random chance to discard it if too close to the poles
                }

                foreach (WS_Tile neighbor in tile.Neighbors())
                {
                    if (float.IsNaN(neighbor.altitude))     // if neighbor hasnt alreadby been gathered by a generator
                    {
                        neighbor.altitude = -1.0f;          // mark it so other generators dont gather it too
                        tilePool.Add(neighbor);             // add the neighbor to the pool
                    }
                }

                landWS_Tiles.Add(tile);        // gather the tile
                tilePool.Remove(tile);      // remove the tile from the pool
                
                if ((type == GeneratorType.SHALLOW && Random.Range(0.0f, 1.0f) > shallowRestriction))   // Shallow Restriction
                {
                    tile.altitude = -2.0f;      // mark it as a Shallow Restriction
                    repeat = true;              // Shallow generators will randomly discard tiles to create water tiles between its land tiles
                }
            } while (repeat);

            return true;
        }


        public void Step(float altitudeRandomizer)
        {
            if (landWS_Tiles.Count == 0)  // while there's still tiles to be set its altitude
                active = false;
            else
            {
                WS_Tile tile = landWS_Tiles[0];

                float neighborAltitude = 0.0f;
                int count = 0;

                if (tile.altitude == -2.0f)
                    tile.altitude = Random.Range(-400.0f, -100.0f);  // NEVER LESS THAN 500, if Shallow Restricted, create a water tile
                else
                {
                    foreach (WS_Tile neighbor in tile.Neighbors())
                    {
                        if (neighbor.altitude != -1.0f)
                        {
                            neighborAltitude += neighbor.altitude;  // gather altitudes of its neighbors
                            count++;
                        }
                    }

                    if (count > 0)
                    {
                        neighborAltitude /= count;
                        do
                        {   //altitude equal to average of nieghboring tiles + random value
                            tile.altitude = neighborAltitude + Random.Range(-altitudeRandomizer, altitudeRandomizer);
                        } while (tile.altitude <= 0.0f);
                    }
                    else
                        tile.altitude = startingAltitude + Random.Range(-25.0f, 25.0f);  //unless it has no neighbors with set altitude
                }

                landWS_Tiles.Remove(tile);
            }
        }
    }


    public class OceanGenerator
    {
        public List<WS_Tile> tilePool = new List<WS_Tile>();
        public bool active = true;

        float oceanAltitude = -5000.0f;
        float oceanRandomizer = 500.0f;

        public void Step()
        {
            if (tilePool.Count == 0)  // while there's still sea tiles to be set its altitude
                active = false;
            else
            {
                WS_Tile tile = tilePool[0];

                bool coastal = false;
                float neighborAltitude = 0.0f;
                int count = 0;

                foreach (WS_Tile neighbor in tile.Neighbors())
                {
                    if (float.IsNaN(neighbor.altitude))  // if the neighbor is a water tile without altitude set, gather it
                    {
                        neighbor.altitude = 0;
                        tilePool.Add(neighbor);
                    }
                    else
                    {
                        if (neighbor.altitude > 0)    // if the neighbor is a land tile, mark this tile as coastal
                        {
                            coastal = true;
                            break;
                        }
                        else
                        {
                            neighborAltitude += neighbor.altitude;  // if the neighbor is a water tile with set altitude, calculate the average altitude
                            count++;
                        }
                    }
                }

                if (coastal)
                    tile.altitude = Random.Range(-300.0f, 0.0f);   // if its coastal, apply a low depth value
                else
                {       // if not, altitude equal to average of nieghboring tiles + random value
                    neighborAltitude /= count;
                    tile.altitude = ((neighborAltitude + Random.Range(-oceanRandomizer, oceanRandomizer) * 2.0f) + oceanAltitude) / 3.0f;
                }

                tilePool.Remove(tile);
            }
        }
    }

}