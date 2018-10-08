using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTransitioner : MonoBehaviour {

    // reference to GameController class
    private GameController gameCtrlScript;

    public enum MapTransitionType { Scanline, MainPathFirst, RippleFromCentre, JumpsLast };
    public enum MapTransitionDirection { On, Off }; // TODO: This should be defined in the global class
    

    // visibile in developer GUI
    public float TransitionSeparationDelay;
    public float TileSeparationDelay;
    public MapTransitionType MapTransitionOnType;
    public MapTransitionType MapTransitionOffType;

    [Tooltip("Type in the name of the transition you would like to override with.")]
	public string tileOnTransitionOveride;

	[Tooltip("Type in the name of the transition you would like to override with.")]
	public string tileOffTransitionOveride;
	
    
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

        

        // TODO: Warn when tile types aren't found. - describe that they must be active (and will be made inactive on launch)


        // iterate through the animation on each prefab type and remember what's available for each one
        // TODO


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



    public void StartTransitioning(MapData currentMapData, MapTile[,] newGeneratedMapArray) {
        Debug.Log("StartTransitioning OLD & NEW");

        MapData newMapData = new MapData(newGeneratedMapArray);
        newMapData.MapObjArray = InstantiateNewMapTiles(newMapData.MapDataArray);
        
        StartCoroutine( InitMapTransitions(currentMapData, newMapData) );

    }
    public void StartTransitioning(MapTile[,] newGeneratedMapArray) {
        Debug.Log("StartTransitioning NEW - Generated Map Array");

        MapData newMapData = new MapData(newGeneratedMapArray);
        newMapData.MapObjArray = InstantiateNewMapTiles(newMapData.MapDataArray);
    
        StartTransitioning(newMapData, MapTransitionDirection.On);

    }
    public void StartTransitioning(MapData mapData, MapTransitionDirection mapTransitionDirection) {
        Debug.Log("StartTransitioning NEW - Object Array");

        InitMapTransition(mapData, mapTransitionDirection);

    }




    private IEnumerator InitMapTransitions(MapData currentMapData, MapData newMapData) {
        Debug.Log("InitMapTransitions - part 1");

        // get references to required classes
		gameCtrlScript.LevelAvailable = false;
        // this is made true again during final transition method

        // start animating the existing level off
        // TODO: Needs to handle not starting with an existing level (or maybe they're called directly with startTransition?)
        InitMapTransition(currentMapData, MapTransitionDirection.Off);

        yield return new WaitForSeconds(TransitionSeparationDelay);

        Debug.Log("InitMapTransitions - part 2 (after yield)");

        // start animating new level on
        // TODO: Needs to handle not having a new level to bring in
        InitMapTransition(newMapData, MapTransitionDirection.On);

    }



    
    private void InitMapTransition(MapData mapData, MapTransitionDirection mapTransitionDirection) {
        Debug.Log("InitMapTransition");

        MapTransitionType mapTransitionType; 

        if(mapTransitionDirection == MapTransitionDirection.On) {
            mapTransitionType = MapTransitionOnType;
            // TODO: overwrite this as it's specified in code directly (needs to be passed through the functions)
        } else {
            mapTransitionType = MapTransitionOffType;
            // TODO: overwrite this is at's specified in code directly (needs to be passed through the functions)
        }

        Debug.Log(mapTransitionDirection + " / " + mapTransitionType);
        
        switch(mapTransitionType)
        {
            case MapTransitionType.Scanline:
                StartCoroutine( RunScanlineTransition(mapData, mapTransitionDirection) );
                break;

            case MapTransitionType.MainPathFirst:
                StartCoroutine( RunMainPathFirstTransition(mapData, mapTransitionDirection) );
                break;

            case MapTransitionType.RippleFromCentre:
                RunRippleFromCentreTransition(mapData, mapTransitionDirection);
                break;

            case MapTransitionType.JumpsLast:
                StartCoroutine( RunJumpsLastTransition(mapData, mapTransitionDirection) );
                break;

            default:
                Debug.Log("mapTransitionType Issue");
                break;
        }



    }


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



    private GameObject CreateMapTile(TileType tileType) {
        //Debug.Log("CreateMapTile: "+tileType);

        GameObject newTileObject;

        //newTileObject = Instantiate(floorTiles[0]);

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








    /**
     * 
     * Map Transition Functions
     * 
     */


    private IEnumerator RunScanlineTransition(MapData mapData, MapTransitionDirection mapTransitionDirection) {
        Debug.Log("Running RunScanlineTransition");

        float deltaTimeConsumed = 0.0f;

        if(mapTransitionDirection == MapTransitionDirection.On) {
            // TODO: this is getting too deep - rethink how transitions are run with map direction present

            // position all tiles first so their positions can be checked easily
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

        // TODO: Try removing - This is just put in to try and solve animation on selection bug
        yield return new WaitForSeconds(0.5f);

        // now start them animating on at the appropriate time
        // position all tiles first so their positions can be checked easily
        for(int j=0; j<mapData.MapObjArray.GetLength(0); j++) {
            for(int k=0; k<mapData.MapObjArray.GetLength(1); k++) {

                GameObject tile = mapData.MapObjArray[j,k];
                if(tile == null) { continue; }

                switch(mapTransitionDirection) {

                    case MapTransitionDirection.On:
                        startTileOnTransition(tile);
                        break;
                    
                    case MapTransitionDirection.Off:
                        startTileOffTransition(tile);
                        break;
                    
                    default:
                        Debug.Log("RunScanlineTransition: Error in called mapTransition Direction");
                        break;

                }
                
                // pause for a frame if as many tiles as should have fit in the previous frames time have been initiated (according to the tileDelay set)
                deltaTimeConsumed += TileSeparationDelay;
                if(deltaTimeConsumed >= Time.deltaTime) {
                    deltaTimeConsumed = 0.0f;
                    yield return new WaitForSeconds(TileSeparationDelay);
                }

            }
        }


        finalizeLevel(mapData, mapTransitionDirection);

    }


    private void startTileOnTransition(GameObject tile) {
        if( tileOnTransitionOveride != null &&
            tileOnTransitionOveride != "" &&
            tileOnTransitionOveride != " "
            ) {

            tile.GetComponent<TileBase>().TransitionOn(tileOnTransitionOveride);

        } else {
            tile.GetComponent<TileBase>().TransitionOn();

        }
    }

    private void startTileOffTransition(GameObject tile) {
        if( tileOnTransitionOveride != null &&
            tileOnTransitionOveride != "" &&
            tileOnTransitionOveride != " "
            ) {

            tile.GetComponent<TileBase>().TransitionOff(tileOnTransitionOveride);

        } else {
            tile.GetComponent<TileBase>().TransitionOff();

        }
    }

    private IEnumerator RunMainPathFirstTransition(MapData mapData, MapTransitionDirection mapTransitionDirection) {
        Debug.Log("Running RunMainPathFirstTransition");

        // Show the first item

        // pause
        yield return new WaitForSeconds(TileSeparationDelay);



        finalizeLevel(mapData, mapTransitionDirection);

    }

    
    private void RunRippleFromCentreTransition(MapData mapData, MapTransitionDirection mapTransitionDirection) {
        Debug.Log("Running RunRippleFromCentreTransition");
    
        Vector3 point = new Vector3(0,0,0);
        StartCoroutine( RunRippleFromPointTransition(mapData, mapTransitionDirection, point) );
    }

    

    private IEnumerator RunRippleFromPointTransition(MapData mapData, MapTransitionDirection mapTransitionDirection, Vector3 point) {

        float deltaTimeConsumed = 0.0f;
        float radius = 0.0f;
        float maxRadius = 0;

        // position all tiles first so their positions can be checked easily
        for(int j=0; j<mapData.MapObjArray.GetLength(0); j++) {
            for(int k=0; k<mapData.MapObjArray.GetLength(1); k++) {

                GameObject tile = mapData.MapObjArray[j,k];
                if(tile == null) { continue; }

                // TODO: This should be calculated in MapData (but it would restrict all map tiles to same size)
                // and would need to be done after MapObjArray is populated. It would need to iterate over the list, find a floor, and set the size to that.
                // Alternatively, could be done quicker in MapData start by finding the actual floor tile int he scene rather than the array.
                // for now it's here.
                Vector3 size = tile.GetComponent<Renderer>().bounds.size;
                // TODO: this shouldn't be calculated in this loop either
                // should also be based off point position or there'll need to be too much excess
                maxRadius = GetMax(mapData.MapWidth,mapData.MapDepth); ///2;

                // TODO: the offset doesn't seem visually accurate
                Vector3 mapOffset = new Vector3(-mapData.MapWidth/2, 0, -mapData.MapDepth/2);

                Vector3 tilePosition = new Vector3(size.x*j, 0, size.z*k);
                tile.transform.position = mapOffset + tilePosition;

            }
        } 

        // now start them animating on at the appropriate time
        while (radius < maxRadius) {

            // NOTE: this is rather inefficient as it goes through the whole array for every increase in radius
            for(int j=0; j<mapData.MapObjArray.GetLength(0); j++) {
                for(int k=0; k<mapData.MapObjArray.GetLength(1); k++) {

                    GameObject tile = mapData.MapObjArray[j,k];
                    if(tile == null) { continue; }
                    if(tile.activeSelf == true) { continue; }

                    // if the tile is within the current radius
                    if( Vector3.Distance(point, tile.transform.position) <= radius ) {
                        startTileOnTransition(tile);
                    }

                }
            }

            // initiation as many tiles as should have fit in the previous frames time (according to the tileDelay set)
            deltaTimeConsumed += TileSeparationDelay;
            if(deltaTimeConsumed >= Time.deltaTime) {
                deltaTimeConsumed = 0.0f;
                yield return new WaitForSeconds(TileSeparationDelay);
            }

            // TODO: Easing would be nice
            radius++;
        }
        

        finalizeLevel(mapData, mapTransitionDirection);

    }






    private IEnumerator RunJumpsLastTransition(MapData mapData, MapTransitionDirection mapTransitionDirection) {
        Debug.Log("Running RunJumpsLastTransition");

        float deltaTimeConsumed = 0.0f;

        // position all tiles first so their positions can be checked easily
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

            }
        } 


        // now start them animating on at the appropriate time
        for(int j=0; j<mapData.MapObjArray.GetLength(0); j++) {
            for(int k=0; k<mapData.MapObjArray.GetLength(1); k++) {

                GameObject tile = mapData.MapObjArray[j,k];
                if(tile == null) { continue; }

                // don't do jumps
                if(mapData.MapDataArray[j,k].type == TileType.Jump) { continue; };
                
                //tile.GetComponent<Animator>().Play("Pop Up");
                tile.SetActive(true);
                
                // initiation as many tiles as should have fit in the previous frames time (according to the tileDelay set)
                deltaTimeConsumed += TileSeparationDelay;
                if(deltaTimeConsumed >= Time.deltaTime) {
                    deltaTimeConsumed = 0.0f;
                    yield return new WaitForSeconds(TileSeparationDelay);
                }

            }
        }


        // pause before doing features tiles
        yield return new WaitForSeconds(1);


        // now pop on the doors
        foreach(GameObject jumpTile in mapData.Jumps) {
            
            Debug.Log("starting jump transition");
            jumpTile.SetActive(true);
            
            // initiation as many tiles as should have fit in the previous frames time (according to the tileDelay set)
            deltaTimeConsumed += TileSeparationDelay;
            if(deltaTimeConsumed >= Time.deltaTime) {
                deltaTimeConsumed = 0.0f;
                yield return new WaitForSeconds(TileSeparationDelay);
            }

        }


        
        
        finalizeLevel(mapData, mapTransitionDirection);


    }











    private void finalizeLevel(MapData mapData, MapTransitionDirection mapTransitionDirection) {
        
        if(mapTransitionDirection == MapTransitionDirection.On) {
            // replace old mapData with newly created mapData
            gameCtrlScript.MapData = mapData;

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


