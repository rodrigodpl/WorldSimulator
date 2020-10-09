using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;


public class WS_World : MonoBehaviour
{
    // World Variables
    private WS_Tile[][] tiles                               = null;     // tiles container
    private List<WS_BaseEvent> eventPool                    = new List<WS_BaseEvent>();
    public static List<WS_Trait> cultureTraits              = new List<WS_Trait>();
    public static List<WS_Trait> religionTraits             = new List<WS_Trait>();
    public static List<WS_Trait> governmentTraits           = new List<WS_Trait>();
    public static List<WS_Disaster> disasters               = new List<WS_Disaster>();
    public static List<WS_Infrastructure> infrastructure    = new List<WS_Infrastructure>();
    public static List<WS_ResourceSource> resources         = new List<WS_ResourceSource>();
    public static List<WS_Tech> techs                       = new List<WS_Tech>();
    private WS_WorldGenerator worldGenerator                = null;     // world generator, which fills blank tiles with geographical data

    public static int sizeX                                 = 200;      // number of columns in the map
    public static int sizeY                                 = 100;      // number of rows in the map
    
    // Rendering  Variables
    [HideInInspector] public float lowestPoint              = 0.0f;         // all these variables are used for reference when drawing 
    [HideInInspector] public float highestPoint             = 0.0f;
    [HideInInspector] public float lowestTemperature        = 200.0f;
    [HideInInspector] public float highestTemperature       = -200.0f;
    [HideInInspector] public float maxLandErosion           = 0.0f;
    [HideInInspector] public float maxSeaErosion            = 0.0f;
    [HideInInspector] public float maxRiverStrength         = 0.0f;
    [HideInInspector] public float maxHabitability          = -200.0f;
    [HideInInspector] public float minHabitability          = 200.0f;
    [HideInInspector] public static float maxGrowth         = -20000.0f;
    [HideInInspector] public static float minGrowth         = 20000.0f;


    // Utility variables
    [HideInInspector] public Vector2 realSize               = new Vector2();
    public Texture2D hexTex                                 = null;
    [HideInInspector] public Texture2D output               = null;
    [HideInInspector] private SpriteRenderer spriteRenderer = null;
    [HideInInspector] public Color32[] pixels               = null;

    public static SimulationSpeed speed                     = SimulationSpeed.NORMAL;
    public static int year                                  = -2000;
    public Stopwatch timer                                  = new Stopwatch();

    public WS_Tile GetTile(Vector2Int position)                        // returns a tile from the map with its array position
    {
        if (position.x < tiles.Length && position.x >= 0 && position.y < tiles[0].Length && position.y >= 0)
            return tiles[position.x][position.y];
        else
            return null;
    }

    void Start()
    {   
        // EVENTS (order is relevant!)

        // Population Events
        eventPool.Add(new PopulationGrowthEvent());
        eventPool.Add(new ColonizationEvent());


        // Culture Events
        eventPool.Add(new CulturalAdoptionEvent());
        eventPool.Add(new CulturalMergeEvent());
        eventPool.Add(new CulturalEvolutionEvent());
        eventPool.Add(new CulturalCollapseEvent());


        // Disaster Events
        eventPool.Add(new DisasterTriggerEvent());
        eventPool.Add(new DisasterEndEvent());
        eventPool.Add(new DisasterSpreadEvent());

            // Disasters
            disasters.Add(new WS_DroughtDisaster());
            disasters.Add(new WS_FloodDisaster());
            disasters.Add(new WS_TsunamiDisaster());
            disasters.Add(new WS_PlagueDisaster());
            disasters.Add(new WS_FireDisaster());
            disasters.Add(new WS_EarthQuakeDisaster());


        // Religion Events
        eventPool.Add(new ReligiousAdoptionEvent());
        eventPool.Add(new ReligiousMergeEvent());
        eventPool.Add(new ReligiousEvolutionEvent());
        eventPool.Add(new ReligiousCollapseEvent());

        // Infrastructure Events
        eventPool.Add(new InfrastructureProductionEvent());

            // Infrastructures
            infrastructure.Add(new WS_SanitationInfrastructure());
            infrastructure.Add(new WS_FoodInfrastructure());
            infrastructure.Add(new WS_HealthcareInfrastructure());
            infrastructure.Add(new WS_UnrestInfrastructure());
            infrastructure.Add(new WS_WarInfrastructure());
            infrastructure.Add(new WS_CultureInfrastructure());
            infrastructure.Add(new WS_ReligionInfrastructure());
            infrastructure.Add(new WS_ConstructionInfrastructure());
            infrastructure.Add(new WS_CommerceInfrastructure());
            infrastructure.Add(new WS_TechnologyInfrastructure());


        // Government Events
        eventPool.Add(new UnrestEvent());
        eventPool.Add(new UprisingEvent());

        // Diplomacy Events
        eventPool.Add(new FindNeighborsEvent());
        eventPool.Add(new OpinionsEvent());
        eventPool.Add(new TreatyProposalEvent());
        eventPool.Add(new TreatyEndedEvent());
        eventPool.Add(new JoinWarEvent());

        // Commerce Events
        eventPool.Add(new WS_ResourceConsumptionEvent());
        eventPool.Add(new WS_CaravanJourneyEvent());

            // Resources
            resources.Add(new WS_ResourceSource(ResourceType.CLAY));
            resources.Add(new WS_ResourceSource(ResourceType.COAL));
            resources.Add(new WS_ResourceSource(ResourceType.COPPER));
            resources.Add(new WS_ResourceSource(ResourceType.FISH));
            resources.Add(new WS_ResourceSource(ResourceType.FURS));
            resources.Add(new WS_ResourceSource(ResourceType.GOLD));
            resources.Add(new WS_ResourceSource(ResourceType.GRANITE));
            resources.Add(new WS_ResourceSource(ResourceType.HUNT));
            resources.Add(new WS_ResourceSource(ResourceType.IRON));
            resources.Add(new WS_ResourceSource(ResourceType.JADE));
            resources.Add(new WS_ResourceSource(ResourceType.MARBLE));
            resources.Add(new WS_ResourceSource(ResourceType.OPIOIDS));
            resources.Add(new WS_ResourceSource(ResourceType.PASTURES));
            resources.Add(new WS_ResourceSource(ResourceType.SALT));
            resources.Add(new WS_ResourceSource(ResourceType.SILVER));
            resources.Add(new WS_ResourceSource(ResourceType.SPICES));
            resources.Add(new WS_ResourceSource(ResourceType.TIN));
            resources.Add(new WS_ResourceSource(ResourceType.WOOD));

        // War Events
        eventPool.Add(new MoveCapitalEvent());
        eventPool.Add(new EndWarEvent());
        eventPool.Add(new ArmyRecruitmentEvent());
        eventPool.Add(new BattleFoughtEvent());

        // Technology
        eventPool.Add(new WS_TechSelection());
        eventPool.Add(new WS_TechResearch());
        eventPool.Add(new WS_TechSpread());

        // Techs 
            techs.Add(new WS_TechWarI());               techs.Add(new WS_TechWarII());              techs.Add(new WS_TechWarIII());             techs.Add(new WS_TechWarIV());
            techs.Add(new WS_TechDefenseI());           techs.Add(new WS_TechDefenseII());          techs.Add(new WS_TechDefenseIII());
            techs.Add(new WS_TechConstructionI());      techs.Add(new WS_TechConstructionII());     techs.Add(new WS_TechConstructionIII());    techs.Add(new WS_TechConstructionIV());
            techs.Add(new WS_TechSanitationI());        techs.Add(new WS_TechSanitationII());       techs.Add(new WS_TechSanitationII());
            techs.Add(new WS_TechCultureI());           techs.Add(new WS_TechCultureII());          techs.Add(new WS_TechCultureIII());         techs.Add(new WS_TechCultureIV());
            techs.Add(new WS_TechExploitationI());      techs.Add(new WS_TechExploitationII());     techs.Add(new WS_TechExploitationIII());
            techs.Add(new WS_TechCommerceI());          techs.Add(new WS_TechCommerceII());         techs.Add(new WS_TechCommerceIII());        techs.Add(new WS_TechCommerceIV());
            techs.Add(new WS_TechAdministrationI());    techs.Add(new WS_TechAdministrationII());   techs.Add(new WS_TechAdministrationIII());
            techs.Add(new WS_TechPopulationI());        techs.Add(new WS_TechPopulationII());       techs.Add(new WS_TechPopulationIII());      techs.Add(new WS_TechPopulationIV());
            techs.Add(new WS_TechHealthcareI());        techs.Add(new WS_TechHealthcareII());       techs.Add(new WS_TechHealthcareIII());
            techs.Add(new WS_TechReligionI());          techs.Add(new WS_TechReligionII());         techs.Add(new WS_TechReligionIII());        techs.Add(new WS_TechReligionIV());


        // TRAITS (order is not relevant)

        // Culture Traits
        cultureTraits.Add(new MasterFarmersTrait());
        cultureTraits.Add(new AgriculturalFocusedTrait());
        cultureTraits.Add(new NeglectedFarmsTrait());
        cultureTraits.Add(new IncompetentFarmersTrait());

        cultureTraits.Add(new FarAndBeyondTrait());
        cultureTraits.Add(new ExpansionistsTrait());
        cultureTraits.Add(new ShortHorizonsTrait());
        cultureTraits.Add(new NothingLikeHomeTrait());

        cultureTraits.Add(new HealthyTrait());
        cultureTraits.Add(new DurableTrait());
        cultureTraits.Add(new HighMortalityTrait());
        cultureTraits.Add(new DecayingHealthTrait());

        cultureTraits.Add(new InfluentialCulTrait());
        cultureTraits.Add(new OutwardnessCulTrait());
        cultureTraits.Add(new InwardnessCulTrait());
        cultureTraits.Add(new IsolationistsCulTrait());

        cultureTraits.Add(new SyncreticCulTrait());
        cultureTraits.Add(new TolerantCulTrait());
        cultureTraits.Add(new IntolerantCulTrait());
        cultureTraits.Add(new RepressiveCulTrait());

        // Religion Traits
        religionTraits.Add(new GoverningChurchTrait());
        religionTraits.Add(new PowerfulPriestsTrait());
        religionTraits.Add(new AgnosticsTrait());
        religionTraits.Add(new AtheistsTrait());

        religionTraits.Add(new MoneyHungryTrait());
        religionTraits.Add(new ChurchDonationsTrait());
        religionTraits.Add(new AltruistsTrait());
        religionTraits.Add(new AsceticsTrait());

        religionTraits.Add(new InfluentialRelTrait());
        religionTraits.Add(new OutwardnessRelTrait());
        religionTraits.Add(new InwardnessRelTrait());
        religionTraits.Add(new IsolationistsRelTrait());

        religionTraits.Add(new SyncreticRelTrait());
        religionTraits.Add(new TolerantRelTrait());
        religionTraits.Add(new IntolerantRelTrait());
        religionTraits.Add(new RepressiveRelTrait());

        // Government Traits
        governmentTraits.Add(new AutocracyTrait());
        governmentTraits.Add(new OligarchyTrait());
        governmentTraits.Add(new RulingCouncilTrait());
        governmentTraits.Add(new DemocracyTrait());

        governmentTraits.Add(new RulerHolderTrait());
        governmentTraits.Add(new NobilityHolderTrait());
        governmentTraits.Add(new ChurchHolderTrait());
        governmentTraits.Add(new PeopleHolderTrait());

        governmentTraits.Add(new CentralizedTrait());
        governmentTraits.Add(new HierarchichalTrait());
        governmentTraits.Add(new DistributedTrait());
        governmentTraits.Add(new LocalControlTrait());

        governmentTraits.Add(new NationalistTrait());
        governmentTraits.Add(new RepressiveTrait());
        governmentTraits.Add(new TolerantTrait());
        governmentTraits.Add(new OpenViewsTrait());

        WS_WordCreator.Init();
        WS_CoatManager.Init();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();

        if (tiles != null)
        {
            for (int tileXIt = 0; tileXIt < sizeX; tileXIt++)
                for (int tileYIt = 0; tileYIt < sizeY; tileYIt++)
                    tiles[tileXIt][tileYIt].tileRenderer.Render();

            output.Apply();
        }
    }

    private void UpdateWorld()
    {

        timer.Start();

        float time = 0.0f;
        switch (speed)
        {
            case SimulationSpeed.SLOW:      time = 2.0f; break;
            case SimulationSpeed.NORMAL:    time = 1.0f; break;
            case SimulationSpeed.FAST:      time = 0.5f; break;
            case SimulationSpeed.FASTEST:   time = 0.1f; break;
        }

        if (time == 0.0f)  // PAUSED
        {
            Invoke("UpdateWorld", 0.1f);
            return;
        }

        maxGrowth = float.MinValue;
        minGrowth = float.MaxValue;

        foreach (WS_BaseEvent Event in eventPool)
        {
            for (int tileXIt = 0; tileXIt < sizeX; tileXIt++)
            {
                for (int tileYIt = 0; tileYIt < sizeY; tileYIt++)
                {
                    if (tiles[tileXIt][tileYIt].population > 0.0f)
                        Event.Execute(tiles[tileXIt][tileYIt]);
                }
            }
        }

        year++;

        Invoke("UpdateWorld", time);
    }
  
    public void InitWorld()
    {
        tiles = new WS_Tile[sizeX][];                    // initialize world
        worldGenerator = new WS_WorldGenerator();

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
                tiles[i][j].name = WS_WordCreator.Create();


                // set pointers to newly created tile
                tiles[i][j].utility.setPosition(new Vector2Int(i, j));
                tiles[i][j].utility.setWorld(this);

                tiles[i][j].tileRenderer.setTile(tiles[i][j]);
                tiles[i][j].tileRenderer.setRenderPos(new Vector2Int(Mathf.CeilToInt(position.x), Mathf.CeilToInt(position.y)));
                tiles[i][j].tileRenderer.setWorld(this);

                for (int inf = 0; inf < (int)InfrastructureType.MAX; inf++)
                    tiles[i][j].infrastructureLevels[inf] = 1;


                for (int res = 0; res < (int)ResourceType.MAX; res++)
                    tiles[i][j].resStacks[res] = new WS_ResourceStack((ResourceType)res);
            }
        }


        for (int r = 1; r <= 3; r++)
            for (int i = 0; i < sizeX; i++)
                for (int j = 0; j < sizeY; j++)
                    tiles[i][j].Neighbors(r);                           // Pre-load all tiles' neighbors


        worldGenerator.Generate(this);                                              // execute world generation process
        worldGenerator.PopulateWorld();


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

        UpdateWorld();
    }
}
