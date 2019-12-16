using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;


public class WS_World : MonoBehaviour
{
    // World Variables
    private WS_Tile[][] tiles               = null;     // tiles container
    private List<WS_BaseEvent> eventPool    = new List<WS_BaseEvent>();
    public static List<WS_Trait> allTraits     = new List<WS_Trait>();
    private WS_WorldGenerator worldGenerator   = null;     // world generator, which fills blank tiles with geographical data

    private List<River> rivers              = null;     // river container, River class definition at the bottom of this script
    public List<WS_Nation> nations             = null;     // river container, River class definition at the bottom of this script

    public static int sizeX                 = 200;      // number of columns in the map
    public static int sizeY                 = 100;      // number of rows in the map
    private int eventIt                     = 0;
    private int tileXIt                     = 0;
    private int tileYIt                     = 0;
    private int longCounter                 = 0;

    
    // Rendering  Variables
    [HideInInspector] public float lowestPoint          = 0.0f;         // all these variables are used for reference when drawing 
    [HideInInspector] public float highestPoint         = 0.0f;
    [HideInInspector] public float lowestTemperature    = 200.0f;
    [HideInInspector] public float highestTemperature   = -200.0f;
    [HideInInspector] public float maxLandErosion       = 0.0f;
    [HideInInspector] public float maxSeaErosion        = 0.0f;
    [HideInInspector] public float maxRiverStrength     = 0.0f;
    [HideInInspector] public float maxHabitability      = -200.0f;
    [HideInInspector] public float minHabitability      = 200.0f;
    [HideInInspector] public static float maxGrowth            = -20000.0f;
    [HideInInspector] public static float minGrowth            = 20000.0f;


    // Utility variables
    public static float frameMult = 100.0f;
    private float frameTime = (1.0f / 30.0f) * 1000.0f;
    private Stopwatch timer = new Stopwatch();
    [HideInInspector] public Vector2 realSize = new Vector2();
    bool render = false;

    // which information should be displayed in the map drawing
    public Texture2D hexTex = null;
    [HideInInspector] public Texture2D output = null;
    [HideInInspector] private SpriteRenderer spriteRenderer = null;
    [HideInInspector] public Color32[] pixels = null;

    public void addNation(WS_Nation nation)
    {
        nations.Add(nation);
    }

    public WS_Tile GetTile(Vector2Int position)                        // returns a tile from the map with its array position
    {
        return tiles[position.x][position.y];
    }

    public List<River> getRivers()                                  // returns river list
    {
        return rivers;
    }

    void Start()
    {
        tiles           = new WS_Tile[sizeX][];                    // initialize world
        rivers          = new List<River>();
        nations         = new List<WS_Nation>();
        worldGenerator  = new WS_WorldGenerator();
        

        // EVENTS (order is relevant!)

        // Population
        eventPool.Add(new FoodGenerationEvent());
        eventPool.Add(new FoodConsumptionEvent());
        eventPool.Add(new MigrationEvent());
        eventPool.Add(new ColonizationEvent());
        eventPool.Add(new RuralMigrationsEvent());


        // Culture
        eventPool.Add(new CultureStrengthEvent());
        eventPool.Add(new RulingCultureChangeEvent());
        //eventPool.Add(new SyncreticAssimilationEvent());


        // TRAITS (order is not relevant)

        allTraits.Add(new SurvivalistsTrait());
        allTraits.Add(new ResilientTrait());
        allTraits.Add(new UnadaptableTrait());
        allTraits.Add(new SybaritesTrait());

        allTraits.Add(new UrbanitesTrait());
        allTraits.Add(new CityDwellersTrait());
        allTraits.Add(new VillagePeopleTrait());
        allTraits.Add(new CountrymenTrait());

        allTraits.Add(new MasterFarmersTrait());
        allTraits.Add(new AgriculturalFocusedTrait());
        allTraits.Add(new NeglectedFarmsTrait());
        allTraits.Add(new IncompetentFarmersTrait());

        allTraits.Add(new FarAndBeyondTrait());
        allTraits.Add(new ExpansionistsTrait());
        allTraits.Add(new ShortHorizonsTrait());
        allTraits.Add(new NothingLiketHomeTrait());

        allTraits.Add(new BreedLikeRabbitsTrait());
        allTraits.Add(new HighNatalityTrait());
        allTraits.Add(new LowNatalityTrait());
        allTraits.Add(new DwindlingPopulationTrait());

        allTraits.Add(new HealthyTrait());
        allTraits.Add(new DurableTrait());
        allTraits.Add(new HighMortalityTrait());
        allTraits.Add(new DecayingHealthTrait());

        allTraits.Add(new SyncreticTrait());
        allTraits.Add(new WelcomingTrait());
        

        for (int i = 0; i < sizeX; i++)
        {
            tiles[i] = new WS_Tile[sizeY];                             // initialize data

            for (int j = 0; j < sizeY; j++)
            {
                Vector2 position = new Vector2(i * hexTex.width * 0.84f, j * hexTex.height * 0.75f);          // set tranform position for the tile

                if (j % 2 == 1)                                                 // apply a position offset if odd row
                    position.x += 0.43f * hexTex.width;
                
                tiles[i][j] = new WS_Tile();
                tiles[i][j].utility = new WS_TileUtility();
                tiles[i][j].tileRenderer = new WS_TileRenderer();
                
                // set pointers to newly created tile
                tiles[i][j].utility.setPosition(new Vector2Int(i, j));
                tiles[i][j].utility.setWorld(this);

                tiles[i][j].tileRenderer.setTile(tiles[i][j]);
                tiles[i][j].tileRenderer.setRenderPos(new Vector2Int(Mathf.CeilToInt(position.x), Mathf.CeilToInt(position.y)));
                tiles[i][j].tileRenderer.setWorld(this);
            }
        }

        
        for (int r = 1; r <= 3; r++)
            for (int i = 0; i < sizeX; i++)
                for (int j = 0; j < sizeY; j++)
                    tiles[i][j].Neighbors(r);                           // Pre-load all tiles' neighbors

        worldGenerator.Generate(this);                                              // execute world generation process
        worldGenerator.PopulateWorld(100);

        foreach (WS_Nation nation in nations)
        {
            foreach (WS_Tile tile in nation.nationTiles)
            {
                if (tile.Population() <= 0.0f)
                {
                    nation.nationTiles.Remove(tile);
                    tile.nation = null;
                    return;
                }
                else
                    tile.updateData();
            }

            nation.UpdateData();
        }

        worldGenerator.CulturalizeWorld();

        foreach (WS_Nation nation in nations)
        {
            foreach (WS_Tile tile in nation.nationTiles)
                tile.updateData();

            nation.UpdateData();
        }


        output = new Texture2D(Mathf.CeilToInt(hexTex.width * sizeX * 0.84f), Mathf.CeilToInt(hexTex.height * sizeY * 0.75f), TextureFormat.RGBA32, false);
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = Sprite.Create(output, new Rect(0, 0, output.width, output.height), new Vector2(0.5f, 0.5f));
        pixels = hexTex.GetPixels32(0);

        Color32 clearColor = new Color(0.0f, 0.2f, 0.6f);
        Color32[] resetColorArray = output.GetPixels32();

        for (int i = 0; i < resetColorArray.Length; i++)
        {
            resetColorArray[i] = clearColor;
        }

        output.SetPixels32(resetColorArray);
        output.Apply();

        realSize = new Vector2(output.width / 100.0f, output.height / 100.0f);
        transform.Translate(new Vector3(realSize.x / 2.0f, realSize.y / 2.0f, 0.0f));
    }

    private void Update()
    {
        timer.Start();
        while (true)
        {
            for (; eventIt < eventPool.Count; eventIt++)
            {
                for (; tileXIt < sizeX; tileXIt++)
                {
                    for (; tileYIt < sizeY; tileYIt++)
                    {
                        if (timer.ElapsedMilliseconds > frameTime)
                        {
                            timer.Reset();
                            return;
                        }
                        else if (tiles[tileXIt][tileYIt].Population() > 0.0f)
                            eventPool[eventIt].Execute(tiles[tileXIt][tileYIt]);

                        if(tiles[tileXIt][tileYIt].tileRenderer.Render())
                            render = true;
                    }
                    tileYIt = 0;
                }
                tileXIt = 0;
            }

            if (render)
            {
                output.Apply();
                render = false;
            }

            maxGrowth = -20000.0f;
            minGrowth = 20000.0f;

            foreach (WS_Nation nation in nations)
            {
                for (int i = 0; i < nation.nationTiles.Count; i++)
                    nation.nationTiles[i].updateData();

                nation.UpdateData();
            }

            eventIt = 0;
            longCounter++;

            if(longCounter > 12)
            {
                LongUpdate();
                longCounter = 0;
            }
        }
    }
    
    void LongUpdate()
    {
        foreach (WS_Nation nation in nations)
            nation.RecalculateAffinities();
    }
}
