using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTransitioner : MonoBehaviour {

    public enum MapTransitionType { MainPathFirst, RippleFromCentre };
    public enum MapTransitionDirection { On, Off }; // TODO: This should be defined in the global class
    

    // visibile in developer GUI
    public float transitionSeparationDelay;
    public float tileSeparationDelay;
    public MapTransitionType mapTransitionOnType;
    public MapTransitionType mapTransitionOffType;
	
    
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
    



    //public LevelTransitioner Instance { get { return m_Instance; } };
    //private LevelTransitioner m_Instance;
    

	void Awake()
	{
		//m_Instance = this;
	}

	void OnDestroy()
	{
		//m_Instance = null;
	}

	// Use this for initialization
	void Start () {
		

        // iterate through objects in scene and store links to those marked as tiles for use
        floorTiles = GameObject.FindGameObjectsWithTag("Floor Tile");
        floorTiles = GameObject.FindGameObjectsWithTag("Entry Tile");
        floorTiles = GameObject.FindGameObjectsWithTag("Exit Tile");
        floorTiles = GameObject.FindGameObjectsWithTag("Obstruction Tile");

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









    public void StartTransitioning(MapData currentMapData, MapTile[,] newMapDataArray) {

        MapData newMapData = new MapData(newMapDataArray);
        newMapData.MapObjArray = InstantiateNewMapTiles(newMapData.MapDataArray);

        StartCoroutine( InitMapTransitions(currentMapData, newMapData) );

    }
    public void StartTransitioning(MapTile[,] newMapDataArray) {

        MapData newMapData = new MapData(newMapDataArray);
        StartTransitioning(newMapData, MapTransitionDirection.On);

    }
    public void StartTransitioning(MapData mapData, MapTransitionDirection mapTransitionDirection) {

        InitMapTransition(mapData, mapTransitionDirection);

    }




    private IEnumerator InitMapTransitions(MapData currentMapData, MapData newMapData) {

        // start animating the existing level off
        // TODO: Needs to handle not starting with an existing level (or maybe they're called directly with startTransition?)
        InitMapTransition(currentMapData, MapTransitionDirection.Off);

        yield return new WaitForSeconds(transitionSeparationDelay);

        // start animating new level on
        // TODO: Needs to handle not having a new level to bring in
        InitMapTransition(newMapData, MapTransitionDirection.On);

    }

    
    private void InitMapTransition(MapData mapData, MapTransitionDirection mapTransitionDirection) {

        MapTransitionType mapTransitionType; 

        if(mapTransitionDirection == MapTransitionDirection.On) {
            mapTransitionType = mapTransitionOnType;
            // TODO: overwrite this is it's specified in code directly (needs to be passed throught he functions)
        } else {
            mapTransitionType = mapTransitionOffType;
            // TODO: overwrite this is it's specified in code directly (needs to be passed throught he functions)
        }
        
        switch(mapTransitionType)
        {
            case MapTransitionType.MainPathFirst:
                StartCoroutine( RunMainPathFirstTransition(mapData, mapTransitionDirection) );
                break;

            case MapTransitionType.RippleFromCentre:
                StartCoroutine( RunRippleFromCentreTransition(mapData, mapTransitionDirection) );
                break;

            default:
                // do something
                break;
        }



    }


    private GameObject[,] InstantiateNewMapTiles(MapTile[,] newMapDataArray) {

        GameObject[,] newMapObjectArray = new GameObject[newMapDataArray.GetLength(0), newMapDataArray.GetLength(1)];

        // iterate through array and instantiate all objects
        for(int j=0; j<newMapDataArray.GetLength(0); j++) {

            for(int k=0; k<newMapDataArray.GetLength(1); k++) {

                if(newMapDataArray[j,k] != null) {
                    
                    newMapObjectArray[j,k] = CreateMapTile(newMapDataArray[j,k].tileType); // TODO: This variable now shouldn't be lowercase
                
                }

            }

        }

        return newMapObjectArray;

    }



    private GameObject CreateMapTile(GlobalVar.TileType tileType) {

        GameObject newTileObject;

        switch(tileType)
        {
            case GlobalVar.TileType.Entry:
                newTileObject = Instantiate(entryTiles[0]);
                break;

            case GlobalVar.TileType.Tile:
                newTileObject = Instantiate(floorTiles[0]);
                break;

            case GlobalVar.TileType.Exit:
                newTileObject = Instantiate(exitTiles[0]);
                break;

            case GlobalVar.TileType.Gap:
                newTileObject = Instantiate(gapTiles[0]);
                break;

            case GlobalVar.TileType.Jump:
                newTileObject = Instantiate(jumpTiles[0]);
                break;

            case GlobalVar.TileType.Crumble:
                newTileObject = Instantiate(crumbleTiles[0]);
                break;

            case GlobalVar.TileType.Door:
                newTileObject = Instantiate(doorTiles[0]);
                break;

            case GlobalVar.TileType.Switch:
                newTileObject = Instantiate(switchTiles[0]);
                break;

            case GlobalVar.TileType.Turret:
                newTileObject = Instantiate(turretTiles[0]);
                break;

            case GlobalVar.TileType.Moving:
                newTileObject = Instantiate(movingTiles[0]);
                break;

            default:
                // TODO: Default should probably be a normal floor so that the game is still playable - but maybe something that visually represents and error so devs notice? (or maybe just log it)
                Debug.Log("Error in CreateMapTile function");
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


    private IEnumerator RunMainPathFirstTransition(MapData mapData, MapTransitionDirection mapTransitionDirection) {
        Debug.Log("Running RunMainPathFirstTransition");

        // Show the first item

        // pause
        yield return new WaitForSeconds(tileSeparationDelay);

        // etc

    }

    private IEnumerator RunRippleFromCentreTransition(MapData mapData, MapTransitionDirection mapTransitionDirection) {
        Debug.Log("Running RunRippleFromCentreTransition");

        // Show the first item

        // pause
        yield return new WaitForSeconds(tileSeparationDelay);

        // etc

    }






}


