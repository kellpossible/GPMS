using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTransitioner : MonoBehaviour {

    // reference to GameController class
    private GameController gameCtrlScript;

    public enum TransitionPattern { Disabled, Scanline, MainPathFirst, RippleFromCentre };
    public enum TransitionDirection { On, Off }; // TODO: This should be defined in the global class
    public enum TileTransitionGrouping { Any, MainPath, Floors, Entries, Exits, Movers, Doors, Switches, Jumps, Crumblers, Turrets };


    ////////////////////////////
    // visibile in developer GUI
    ////////////////////////////
    
    [Space(10)]

    [Header("Transition Off Settings")]

    [Space(20)]


    
    [Header("Off Transition - First Pass")]

    [Tooltip("Delay from start of Off transition to start of this pass (should usually remain 0).")]
    public float OffPass1Delay = 0f;

    [Tooltip("The transition pattern to use when a level animates off.")]
    public TransitionPattern OffPass1TransitionPattern;

    [Tooltip("The transition pattern to use when a level animates off.")]
    public TileTransitionGrouping OffPass1Tiles;

    [Tooltip("The delay between each tile when they animate off.")]
    public float OffPass1TileDelay = 0.01f;

    [Tooltip("Type in the name of the transition you would like to override with.")]
	public string OffPass1AnimationOveride;


    [Header("Off Transition - Second Pass")]

    [Tooltip("Delay from start of last pass until this pass is run.")]
    public float OffPass2Delay = 1.0f;

    [Tooltip("The transition pattern to use when a level animates off.")]
    public TransitionPattern OffPass2TransitionPattern;

    [Tooltip("The transition pattern to use when a level animates off.")]
    public TileTransitionGrouping OffPass2Tiles;

    [Tooltip("The delay between each tile when they animate off.")]
    public float OffPass2TileDelay = 0.01f;

    [Tooltip("Type in the name of the transition you would like to override with.")]
	public string OffPass2AnimationOveride;


    [Header("Off Transition - Third Pass")]

    [Tooltip("Delay from start of last pass until this pass is run.")]
    public float OffPass3Delay = 1.0f;

    [Tooltip("The transition pattern to use when a level animates off.")]
    public TransitionPattern OffPass3TransitionPattern;

    [Tooltip("The transition pattern to use when a level animates off.")]
    public TileTransitionGrouping OffPass3Tiles;

    [Tooltip("The delay between each tile when they animate off.")]
    public float OffPass3TileDelay = 0.01f;

    [Tooltip("Type in the name of the transition you would like to override with.")]
	public string OffPass3AnimationOveride;


    [Header("Off Transition - Fourth Pass")]

    [Tooltip("Delay from start of last pass until this pass is run.")]
    public float OffPass4Delay = 1.0f;

    [Tooltip("The transition pattern to use when a level animates off.")]
    public TransitionPattern OffPass4TransitionPattern;

    [Tooltip("The transition pattern to use when a level animates off.")]
    public TileTransitionGrouping OffPass4Tiles;

    [Tooltip("The delay between each tile when they animate off.")]
    public float OffPass4TileDelay = 0.01f;

    [Tooltip("Type in the name of the transition you would like to override with.")]
	public string OffPass4AnimationOveride;


    [Header("Off Transition - Fifth Pass")]

    [Tooltip("Delay from start of last pass until this pass is run.")]
    public float OffPass5Delay = 1.0f;

    [Tooltip("The transition pattern to use when a level animates off.")]
    public TransitionPattern OffPass5TransitionPattern;

    [Tooltip("The transition pattern to use when a level animates off.")]
    public TileTransitionGrouping OffPass5Tiles;

    [Tooltip("The delay between each tile when they animate off.")]
    public float OffPass5TileDelay = 0.01f;

    [Tooltip("Type in the name of the transition you would like to override with.")]
	public string OffPass5AnimationOveride;



    [Space(20)]

    [Header("Transition On Settings")]
    
    [Tooltip("The delay between starting a level animating off and starting a new one animating on.")]
    public float OnTransitionDelay = 1.0f;

    [Space(20)]



    [Header("On Transition - First Pass")]
    
    [Tooltip("Delay from start of On transition to start of this pass (should usually remain 0).")]
    public float OnPass1Delay = 0f;

    [Tooltip("The transition pattern to use when a level animates on.")]
    public TransitionPattern OnPass1TransitionPattern;

    [Tooltip("The transition pattern to use when a level animates on.")]
    public TileTransitionGrouping OnPass1Tiles;

    [Tooltip("The delay between each tile when they animate on.")]
    public float OnPass1TileDelay = 0.01f;

    [Tooltip("Type in the name of the transition you would like to override with.")]
	public string OnPass1AnimationOveride;


    [Header("On Transition - Second Pass")]

    [Tooltip("Delay from start of last pass until this pass is run.")]
    public float OnPass2Delay = 1.0f;

    [Tooltip("The transition pattern to use when a level animates on.")]
    public TransitionPattern OnPass2TransitionPattern;

    [Tooltip("The transition pattern to use when a level animates on.")]
    public TileTransitionGrouping OnPass2Tiles;

    [Tooltip("The delay between each tile when they animate on.")]
    public float OnPass2TileDelay = 0.01f;

    [Tooltip("Type in the name of the transition you would like to override with.")]
	public string OnPass2AnimationOveride;


    [Header("On Transition - Third Pass")]

    [Tooltip("Delay from start of last pass until this pass is run.")]
    public float OnPass3Delay = 1.0f;

    [Tooltip("The transition pattern to use when a level animates on.")]
    public TransitionPattern OnPass3TransitionPattern;

    [Tooltip("The transition pattern to use when a level animates on.")]
    public TileTransitionGrouping OnPass3Tiles;

    [Tooltip("The delay between each tile when they animate on.")]
    public float OnPass3TileDelay = 0.01f;

    [Tooltip("Type in the name of the transition you would like to override with.")]
	public string OnPass3AnimationOveride;


    [Header("On Transition - Fourth Pass")]

    [Tooltip("Delay from start of last pass until this pass is run.")]
    public float OnPass4Delay = 1.0f;

    [Tooltip("The transition pattern to use when a level animates on.")]
    public TransitionPattern OnPass4TransitionPattern;

    [Tooltip("The transition pattern to use when a level animates on.")]
    public TileTransitionGrouping OnPass4Tiles;

    [Tooltip("The delay between each tile when they animate on.")]
    public float OnPass4TileDelay = 0.01f;

    [Tooltip("Type in the name of the transition you would like to override with.")]
	public string OnPass4AnimationOveride;


    [Header("On Transition - Fifth Pass")]

    [Tooltip("Delay from start of last pass until this pass is run.")]
    public float OnPass5Delay = 1.0f;

    [Tooltip("The transition pattern to use when a level animates on.")]
    public TransitionPattern OnPass5TransitionPattern;

    [Tooltip("The transition pattern to use when a level animates on.")]
    public TileTransitionGrouping OnPass5Tiles;

    [Tooltip("The delay between each tile when they animate on.")]
    public float OnPass5TileDelay = 0.01f;

    [Tooltip("Type in the name of the transition you would like to override with.")]
	public string OnPass5AnimationOveride;

    

    

    
	private MapData upcomingMapData;
    
    private GameObject[] entryTiles;
    private GameObject[] floorTiles;
    private GameObject[] exitTiles;
    private GameObject[] gapTiles;
    private GameObject[] jumpTiles;
    private GameObject[] crumbleTiles;
    private GameObject[] doorTiles;
    private GameObject[] switchTiles;
    private GameObject[] turretTiles;
    private GameObject[] movingTiles;
    




	// Use this for initialization
	void Start () {
		
        // get references to required classes
		GameObject gameCtrl = GameObject.Find("Game Ctrl");
		gameCtrlScript = (GameController) gameCtrl.GetComponent(typeof(GameController));

        findAndHideTemplateMapTiles();



	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// common GUI code goes here
	void OnGui()
	{
		
	}










    private void findAndHideTemplateMapTiles() {
        
        // iterate through objects in scene and store links to those marked as tiles for use
        floorTiles = GameObject.FindGameObjectsWithTag("Floor Tile");
        entryTiles = GameObject.FindGameObjectsWithTag("Entry Tile");
        exitTiles = GameObject.FindGameObjectsWithTag("Exit Tile");
        gapTiles = GameObject.FindGameObjectsWithTag("Gap Tile");
        jumpTiles = GameObject.FindGameObjectsWithTag("Jump Tile");
        crumbleTiles = GameObject.FindGameObjectsWithTag("Crumble Tile");
        doorTiles = GameObject.FindGameObjectsWithTag("Door Tile");
        switchTiles = GameObject.FindGameObjectsWithTag("Switch Tile");
        turretTiles = GameObject.FindGameObjectsWithTag("Turret Tile");
        movingTiles = GameObject.FindGameObjectsWithTag("Moving Tile");

        setAllInactive(floorTiles, "Floor Tiles");
        setAllInactive(entryTiles, "Entry Tiles");
        setAllInactive(exitTiles, "Exit Tiles");
        setAllInactive(gapTiles, "Gap Tiles");
        setAllInactive(jumpTiles, "Jump Tiles");
        setAllInactive(crumbleTiles, "Crumble Tiles");
        setAllInactive(doorTiles, "Door Tiles");
        setAllInactive(switchTiles, "Switch Tiles");
        setAllInactive(turretTiles, "Turret Tiles");
        setAllInactive(movingTiles, "Moving Tiles");


    }

    private void setAllInactive(GameObject[] objectArray, string tagName) {

        if(objectArray.Length <= 0) {
            Debug.Log("---------------------------------------------------");
            Debug.Log("WARNING: No " + tagName + "'s have been found. If these are needed during map creation you will get a runtime error.");
            Debug.Log("Tiles must be included in the SCENE in which they are to be used and given the appropriate tag ("+tagName+"). They will be hidden on initialisation.");
            Debug.Log("---------------------------------------------------");
            return;
        }

        Debug.Log(tagName+" tiles found: " + objectArray.Length);

        for(int k=0; k<objectArray.Length; k++) {
            objectArray[k].SetActive(false);
        }

    }



    public void RunTestTransition() {

        MapTile[,] testMap = new MapTile[20,20];

        for(int j=0; j<20; j++) {

            for(int k=0; k<20; k++) {

                float randomNumber = Random.Range(0.0f, 10.0f);

                if(randomNumber > 6.0f) {
                    testMap[j,k] = new MapTile();
                    testMap[j,k].type = TileType.Tile;
                } else if(randomNumber > 4.0f) {
                    testMap[j,k] = new MapTile();
                    testMap[j,k].type = TileType.Jump;
                } else {
                    // leave spot as null
                }


            }

        }

        Debug.Log("Created Test Map");

        StartTransitioning(testMap);

    }


    /// <summary>
    /// Transitions a map off screen as well as parses new map data and transitions it onto screen.
    /// </summary>
    /// <param name="currentMapData">MapTile object representing the current map to be transitioned off.</param>
    /// <param name="newGeneratedMapArray">New generated map data to be parsed and then transitioned on.</param>
    /// 
    public void StartTransitioning(MapData currentMapData, MapTile[,] newGeneratedMapArray) {
        Debug.Log("StartTransitioning OLD & NEW");

        MapData newMapData = new MapData(newGeneratedMapArray);
        newMapData.MapObjArray = InstantiateNewMapTiles(newMapData.MapDataArray);
        
        StartCoroutine( initMapTransitions(currentMapData, newMapData) );

    }
    /// <summary>
    /// Parses new map data and transitions it onto screen.
    /// </summary>
    /// <param name="newGeneratedMapArray">New generated map data.</param>
    /// 
    public void StartTransitioning(MapTile[,] newGeneratedMapArray) {
        Debug.Log("StartTransitioning NEW - Generated Map Array");

        MapData newMapData = new MapData(newGeneratedMapArray);
        newMapData.MapObjArray = InstantiateNewMapTiles(newMapData.MapDataArray);
    
        StartTransitioning(newMapData, TransitionDirection.On);

    }
    /// <summary>
    /// Accepts an already parsed MapData object and transitions it on or off screen.
    /// </summary>
    /// <param name="mapData">MapData object.</param>
    /// <param name="mapTransitionDirection">Specifies transition direction.</param>
    /// 
    public void StartTransitioning(MapData mapData, TransitionDirection mapTransitionDirection) {
        Debug.Log("StartTransitioning NEW - Object Array");


        StartCoroutine( initMapTransition(mapData, mapTransitionDirection) );

    }











/// <summary>
    /// Instantiate new MapTiles into the scene (inactive - to be activated and animated later).
    /// </summary>
    /// <param name="newGeneratedMapArray">Unparsed generated map data.</param>
    /// <returns></returns>
    /// 
    private GameObject[,] InstantiateNewMapTiles(MapTile[,] newGeneratedMapArray) {
        Debug.Log("InstantiateNewMapTiles");

        GameObject[,] newMapObjectArray = new GameObject[newGeneratedMapArray.GetLength(0), newGeneratedMapArray.GetLength(1)];

        // iterate through array and instantiate all objects
        for(int j=0; j<newGeneratedMapArray.GetLength(0); j++) {

            for(int k=0; k<newGeneratedMapArray.GetLength(1); k++) {

                if(newGeneratedMapArray[j,k] == null) { continue; }
                if(newGeneratedMapArray[j,k].type == null) { continue; }
                    
                newMapObjectArray[j,k] = CreateMapTile(newGeneratedMapArray[j,k].type); // TODO: This variable now shouldn't be lowercase

            }

        }

        return newMapObjectArray;

    }


    /// <summary>
    /// Accepts a TileType and instantiates the corresponding GameObject.
    /// </summary>
    /// <param name="tileType">TileType of the map tile to be instantiated.</param>
    /// <returns>GameObject corresponding to the instantiated map tile.</returns>
    ///
    private GameObject CreateMapTile(TileType tileType) {

        GameObject newTileObject;

        switch(tileType)
        {
            case TileType.Entry:
                newTileObject = Instantiate(entryTiles[0]);
                break;

            case TileType.Tile:
                newTileObject = Instantiate(floorTiles[0]);
                break;

            case TileType.Exit:
                newTileObject = Instantiate(exitTiles[0]);
                break;

            // case TileType.Gap:
            //     newTileObject = Instantiate(floorTiles[0]);
            //     break;

            case TileType.Jump:
                System.Random rnd = new System.Random();
                int variation = rnd.Next(0, 2); 
                newTileObject = Instantiate(jumpTiles[variation]);
                break;

            // case TileType.Crumble:
            //     newTileObject = Instantiate(crumbleTiles[0]);
            //     break;

            // case TileType.Door:
            //     newTileObject = Instantiate(doorTiles[0]);
            //     break;

            case TileType.Switch:
                newTileObject = Instantiate(switchTiles[0]);
                break;

            case TileType.Turret:
                newTileObject = Instantiate(turretTiles[0]);
                break;

            // case TileType.Moving:
            //     newTileObject = Instantiate(movingTiles[0]);
            //     break;

            default:
                // TODO: Default should probably be a normal floor so that the game is still playable - but maybe something that visually represents and error so devs notice? (or maybe just log it)
                Debug.Log("CreateMapTile function: Unsupported TileType (using floor tile instead)");
                newTileObject = Instantiate(floorTiles[0]);
                break;

        }

        return newTileObject;

    }











    /// <summary>
    /// Accepts 2 MapData objects, transitions the first off screen and the second onto screen.
    /// </summary>
    /// <param name="currentMapData">MapData of map currently on screen to transition off.</param>
    /// <param name="newMapData">MapData of map to transition onto screen</param>
    /// <returns></returns>
    ///
    private IEnumerator initMapTransitions(MapData currentMapData, MapData newMapData) {
        Debug.Log("InitMapTransitions - part 1");

        // get references to required classes
		gameCtrlScript.LevelAvailable = false;
        // this is made true again during final transition method

        // start animating the existing level off
        // TODO: Needs to handle not starting with an existing level (or maybe they're called directly with startTransition?)
        StartCoroutine( initMapTransition(currentMapData, TransitionDirection.Off) );

        yield return new WaitForSeconds(OnTransitionDelay);

        Debug.Log("InitMapTransitions - part 2 (after yield)");

        // start animating new level on
        // TODO: Needs to handle not having a new level to bring in
        StartCoroutine( initMapTransition(newMapData, TransitionDirection.On) );

    }
    /// <summary>
    /// Initialise the transition of the passed in MapData in the specified direction.
    /// </summary>
    /// <param name="mapData">MapData object of map to transition.</param>
    /// <param name="mapTransitionDirection">Direction of transition.</param>
    /// 
    private IEnumerator initMapTransition(MapData mapData, TransitionDirection mapTransitionDirection) {
        Debug.Log("InitMapTransition");


        // set up pass variables
        TransitionPattern[] mapTransitionTypes;
        TileTransitionGrouping[] tileGroupings;
        float[] tileDelays;
        string[] animationOverides;
        float[] nextPassDelays;

        ArrayList tileObjects;

        
        if(mapTransitionDirection == TransitionDirection.Off) {

            // populate pass variables
            // TODO: Make all these off passes
            mapTransitionTypes = new TransitionPattern[] { OffPass1TransitionPattern, OffPass2TransitionPattern, OffPass3TransitionPattern, OffPass4TransitionPattern, OffPass5TransitionPattern };
            tileGroupings = new TileTransitionGrouping[] { OffPass1Tiles, OffPass2Tiles, OffPass3Tiles, OffPass4Tiles, OffPass5Tiles };
            tileDelays = new float[] { OffPass1TileDelay, OffPass2TileDelay, OffPass3TileDelay, OffPass4TileDelay, OffPass5TileDelay };
            animationOverides = new string[] { OffPass1AnimationOveride, OffPass2AnimationOveride, OffPass3AnimationOveride, OffPass4AnimationOveride, OffPass5AnimationOveride };
            nextPassDelays = new float[] { OffPass1Delay, OffPass2Delay, OffPass3Delay, OffPass4Delay, OffPass5Delay };

        } else {

            // store mapData for pushing into the game object when ready
            upcomingMapData = mapData;

            positionAllTiles(mapData);

            // populate pass variables
            mapTransitionTypes = new TransitionPattern[] { OnPass1TransitionPattern, OnPass2TransitionPattern, OnPass3TransitionPattern, OnPass4TransitionPattern, OnPass5TransitionPattern };
            tileGroupings = new TileTransitionGrouping[] { OnPass1Tiles, OnPass2Tiles, OnPass3Tiles, OnPass4Tiles, OnPass5Tiles };
            tileDelays = new float[] { OnPass1TileDelay, OnPass2TileDelay, OnPass3TileDelay, OnPass4TileDelay, OnPass5TileDelay };
            animationOverides = new string[] { OnPass1AnimationOveride, OnPass2AnimationOveride, OnPass3AnimationOveride, OnPass4AnimationOveride, OnPass5AnimationOveride };
            nextPassDelays = new float[] { OnPass1Delay, OnPass2Delay, OnPass3Delay, OnPass4Delay, OnPass5Delay };
        }

        
        


        


        // start loop for passes
        for( int k=0; k<mapTransitionTypes.Length; k++ ) {

            // Get the gameObjects related to this pass
            switch(tileGroupings[k])
            {
                case TileTransitionGrouping.Any:
                    tileObjects = mapData.MapObjArray2D;
                    break;

                case TileTransitionGrouping.MainPath:
                    tileObjects = mapData.MainPath;
                    break;

                case TileTransitionGrouping.Floors:
                    tileObjects = mapData.Floors;
                    break;

                case TileTransitionGrouping.Entries:
                    tileObjects = mapData.Entries;
                    break;

                case TileTransitionGrouping.Exits:
                    tileObjects = mapData.Exits;
                    break;

                case TileTransitionGrouping.Movers:
                    tileObjects = mapData.Movers;
                    break;

                case TileTransitionGrouping.Doors:
                    tileObjects = mapData.Doors;
                    break;

                case TileTransitionGrouping.Switches:
                    tileObjects = mapData.Switches;
                    break;

                case TileTransitionGrouping.Jumps:
                    tileObjects = mapData.Jumps;
                    break;

                case TileTransitionGrouping.Crumblers:
                    tileObjects = mapData.Crumblers;
                    break;

                case TileTransitionGrouping.Turrets:
                    tileObjects = mapData.Turrets;
                    break;

                default:
                    tileObjects = mapData.MapObjArray2D;
                    break;
            }


        
            Debug.Log(mapTransitionDirection + " / " + mapTransitionTypes[k]);
            


            switch(mapTransitionTypes[k])
            {
                case TransitionPattern.Disabled:
                    runAllAtOnceTransition(tileObjects, mapTransitionDirection);
                    break;

                case TransitionPattern.Scanline:
                    StartCoroutine( runScanlineTransition(tileObjects, mapTransitionDirection, tileDelays[k]) );
                    break;

                case TransitionPattern.MainPathFirst:
                    StartCoroutine( runMainPathFirstTransition(tileObjects, mapTransitionDirection, tileDelays[k]) );
                    break;

                case TransitionPattern.RippleFromCentre:
                    runRippleFromCentreTransition(tileObjects, mapTransitionDirection, tileDelays[k]);
                    break;

                default:
                    Debug.Log("mapTransitionType Issue");
                    break;
            }



            if( k < mapTransitionTypes.Length-1 ) {
                // It's not the last pass, check how to move forward

                if (    mapTransitionTypes[k+1] == TransitionPattern.Disabled ||
                        tileGroupings[k] == TileTransitionGrouping.Any    
                ) {
                    // if the next pass is disabled, or the current pass did all tiles, then no need for subsequent passes
                    break;

                } else {
                    // otherwise, pause before the next pass
                    yield return new WaitForSeconds( nextPassDelays[k+1] );
                }
                
            }


        }


        // all the passes are started (meaning the last pass was just started)
        // TODO: wait enought time for the last pass to be over then finalize level
        finalizeLevel(mapTransitionDirection);


    }


    


    private void positionAllTiles(MapData mapData) {

        for(int j=0; j<mapData.MapObjArray.GetLength(0); j++) {
            for(int k=0; k<mapData.MapObjArray.GetLength(1); k++) {

                GameObject tile = mapData.MapObjArray[j,k];
                if(tile == null) { continue; }

                // TODO: This should be calculated in MapData (but it would restrict all map tiles to same size)
                // and would need to be done after MapObjArray is populated. It would need to iterate over the list, find a floor, and set the size to that.
                // Alternatively, could be done quicker in MapData start by finding the actual floor tile int he scene rather than the array.
                // for now it's here.
                Vector3 size = tile.GetComponent<Renderer>().bounds.size;

                // TODO: the offset doesn't seem visually accurate
                Vector3 mapOffset = new Vector3(-mapData.MapWidth/2, 0, -mapData.MapDepth/2);

                Vector3 tilePosition = new Vector3(size.x*j, 0, size.z*k);
                tile.transform.position = mapOffset + tilePosition;

                // Vector3 tileScale = new Vector3(0, 0.3f, 1);
                // tile.transform.localScale = tileScale;

            }
        } 

    }








    /**
     * 
     * Map Transition Functions
     * 
     */







    private void runAllAtOnceTransition(ArrayList tileObjects, TransitionDirection mapTransitionDirection) {

        for(int k=0; k<tileObjects.Count; k++) {

            GameObject tile = (GameObject) tileObjects[k];

            switch(mapTransitionDirection) {

                case TransitionDirection.On:
                    startTileOnTransition(tile);
                    break;
                
                case TransitionDirection.Off:
                    startTileOffTransition(tile);
                    break;
                
                default:
                    Debug.Log("RunAllAtOnceTransition: Error in called mapTransition Direction");
                    break;

            }

        }


    }







    private IEnumerator runScanlineTransition(ArrayList tileObjects, TransitionDirection mapTransitionDirection, float tileSeparationDelay) {
        Debug.Log("Running RunScanlineTransition");

        float deltaTimeConsumed = 0.0f;


        // TODO: Try removing - This is just put in to try and solve animation on selection bug
        yield return new WaitForSeconds(0.5f);



        // now start them animating on at the appropriate time
        // position all tiles first so their positions can be checked easily
        for(int k=0; k<tileObjects.Count; k++) {

            GameObject tile = (GameObject) tileObjects[k];

            switch(mapTransitionDirection) {

                case TransitionDirection.On:
                    startTileOnTransition(tile);
                    break;
                
                case TransitionDirection.Off:
                    startTileOffTransition(tile);
                    break;
                
                default:
                    Debug.Log("RunScanlineTransition: Error in called mapTransition Direction");
                    break;

            }
            
            // pause for a frame if as many tiles as should have fit in the previous frames time have been initiated (according to the tileDelay set)
            deltaTimeConsumed += tileSeparationDelay;
            if(deltaTimeConsumed >= Time.deltaTime) {
                deltaTimeConsumed = 0.0f;
                yield return new WaitForSeconds(tileSeparationDelay);
            }

        }


    }


    

    private IEnumerator runMainPathFirstTransition(ArrayList tileObjects, TransitionDirection mapTransitionDirection, float tileSeparationDelay) {
        Debug.Log("Running RunMainPathFirstTransition");

        // Show the first item

        // pause
        yield return new WaitForSeconds(OnPass1TileDelay);




    }

    
    private void runRippleFromCentreTransition(ArrayList tileObjects, TransitionDirection mapTransitionDirection, float tileSeparationDelay) {
        Debug.Log("Running RunRippleFromCentreTransition");
    
        Vector3 point = new Vector3(0,0,0);
        StartCoroutine( runRippleFromPointTransition(tileObjects, mapTransitionDirection, tileSeparationDelay, point) );
    }

    




    private IEnumerator runRippleFromPointTransition(ArrayList tileObjects, TransitionDirection mapTransitionDirection, float tileSeparationDelay, Vector3 point) {

        float deltaTimeConsumed = 0.0f;
        float radius = 0.0f;
        float maxRadius = 0;

        

        // now start them animating on at the appropriate time
        while (radius < maxRadius) {

            // NOTE: this is rather inefficient as it goes through the whole array for every increase in radius
            for(int k=0; k<tileObjects.Count; k++) {

                GameObject tile = (GameObject) tileObjects[k];

                if(tile == null) { continue; }
                if(tile.activeSelf == true) { continue; }

                // if the tile is within the current radius
                if( Vector3.Distance(point, tile.transform.position) <= radius ) {
                    startTileOnTransition(tile);
                }

            }

            // initiation as many tiles as should have fit in the previous frames time (according to the tileDelay set)
            deltaTimeConsumed += OnPass1TileDelay;
            if(deltaTimeConsumed >= Time.deltaTime) {
                deltaTimeConsumed = 0.0f;
                yield return new WaitForSeconds(OnPass1TileDelay);
            }

            // TODO: Easing would be nice
            radius++;
        }
        

    }






    private void startTileOnTransition(GameObject tile) {

        // if tile is already turned on in a previous pass, then abort
        if(tile.activeSelf == true) {  return; };

        if( OnPass1AnimationOveride != null &&
            OnPass1AnimationOveride != "" &&
            OnPass1AnimationOveride != " "
            ) {

            tile.GetComponent<TileBase>().TransitionOn(OnPass1AnimationOveride);

        } else {
            tile.GetComponent<TileBase>().TransitionOn();

        }
    }

    private void startTileOffTransition(GameObject tile) {

        if( tile == null    ||
            tile.GetComponent<TileBase>().OnDeletionPath == true
            ) {

            // if the tile is already deleted or if it's already animating and will delete itself, then abort.
            return;

        }

        
        tile.GetComponent<TileBase>().OnDeletionPath = true;


        if( OffPass1AnimationOveride != null &&
            OffPass1AnimationOveride != "" &&
            OffPass1AnimationOveride != " "
            ) {

            tile.GetComponent<TileBase>().TransitionOff(OffPass1AnimationOveride);

        } else {
            tile.GetComponent<TileBase>().TransitionOff();

        }
    }











    private void finalizeLevel(TransitionDirection mapTransitionDirection) {

        Debug.Log("finalizeLevel: Transitioning "+mapTransitionDirection);
        
        if(mapTransitionDirection == TransitionDirection.On) {
            // replace old mapData with newly created mapData
            Debug.Log("finalizeLevel: upcomingMapData: "+upcomingMapData);
            gameCtrlScript.MapData = upcomingMapData;
            Debug.Log("finalizeLevel: gameCtrlScript.MapData: "+gameCtrlScript.MapData);
            //upcomingMapData = null;
            //Debug.Log("finalizeLevel: gameCtrlScript.MapData: "+gameCtrlScript.MapData);

            // set the flag to say the level can be played
            gameCtrlScript.LevelAvailable = true;
        }

    }




    /*
    Utilities
     */


    private float GetMax(float first, float second)
    {
        return first > second ? first : second;
    }





}


