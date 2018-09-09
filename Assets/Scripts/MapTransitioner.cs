﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTransitioner : MonoBehaviour {

    public enum MapTransitionType { Scanline, MainPathFirst, RippleFromCentre };
    public enum MapTransitionDirection { On, Off }; // TODO: This should be defined in the global class
    

    // visibile in developer GUI
    public float TransitionSeparationDelay;
    public float TileSeparationDelay;
    public MapTransitionType MapTransitionOnType;
    public MapTransitionType MapTransitionOffType;
	
    
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
            Debug.Log("WARNING: No " + tagName + " have been found. If these are needed during map creation you will get a runtime error.");
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
            // TODO: overwrite this is it's specified in code directly (needs to be passed throught he functions)
        } else {
            mapTransitionType = MapTransitionOffType;
            // TODO: overwrite this is it's specified in code directly (needs to be passed throught he functions)
        }

        Debug.Log(mapTransitionDirection + ": " + mapTransitionType);
        
        switch(mapTransitionType)
        {
            case MapTransitionType.Scanline:
                StartCoroutine( RunScanlineTransition(mapData, mapTransitionDirection) );
                break;

            case MapTransitionType.MainPathFirst:
                StartCoroutine( RunMainPathFirstTransition(mapData, mapTransitionDirection) );
                break;

            case MapTransitionType.RippleFromCentre:
                StartCoroutine( RunRippleFromCentreTransition(mapData, mapTransitionDirection) );
                break;

            default:
                Debug.Log("mapTransitionType");
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
        Debug.Log("CreateMapTile: "+tileType);

        GameObject newTileObject;

        newTileObject = Instantiate(floorTiles[0]);

        // switch(tileType)
        // {
        //     case TileType.Entry:
        //         newTileObject = Instantiate(entryTiles[0]);
        //         break;

        //     case TileType.Tile:
        //         newTileObject = Instantiate(floorTiles[0]);
        //         break;

        //     case TileType.Exit:
        //         newTileObject = Instantiate(exitTiles[0]);
        //         break;

        //     case TileType.Gap:
        //         newTileObject = Instantiate(gapTiles[0]);
        //         break;

        //     case TileType.Jump:
        //         newTileObject = Instantiate(jumpTiles[0]);
        //         break;

        //     case TileType.Crumble:
        //         newTileObject = Instantiate(crumbleTiles[0]);
        //         break;

        //     case TileType.Door:
        //         newTileObject = Instantiate(doorTiles[0]);
        //         break;

        //     case TileType.Switch:
        //         newTileObject = Instantiate(switchTiles[0]);
        //         break;

        //     case TileType.Turret:
        //         newTileObject = Instantiate(turretTiles[0]);
        //         break;

        //     case TileType.Moving:
        //         newTileObject = Instantiate(movingTiles[0]);
        //         break;

        //     default:
        //         // TODO: Default should probably be a normal floor so that the game is still playable - but maybe something that visually represents and error so devs notice? (or maybe just log it)
        //         Debug.Log("Error in CreateMapTile function");
        //         newTileObject = Instantiate(floorTiles[0]);
        //         break;

        // }

        return newTileObject;

    }








    /**
     * 
     * Map Transition Functions
     * 
     */


    private IEnumerator RunScanlineTransition(MapData mapData, MapTransitionDirection mapTransitionDirection) {
        Debug.Log("Running RunScalineTransition");

        for(int j=0; j<mapData.MapObjArray.GetLength(0); j++) {
            for(int k=0; k<mapData.MapObjArray.GetLength(0); k++) {

                GameObject tile = mapData.MapObjArray[j,k];

                if(tile == null) {
                    // Debug.Log(j + " : "+k);
                    continue;
                }

                // TODO: Calculate mapWidth and height in MapData so it can merely be offset here
                Vector3 size = tile.GetComponent<Renderer>().bounds.size;
                float mapWidth = mapData.MapObjArray.GetLength(0) * size.x;
                float mapDepth = mapData.MapObjArray.GetLength(1) * size.z;

                Vector3 mapOffset = new Vector3(-mapWidth/2, -mapDepth/2, 0);

                Vector3 tilePosition = new Vector3(size.x*j, 0, size.z*k);

                tile.GetComponent<Animator>().Play("Pop Up");
                tile.transform.position = mapOffset + tilePosition;
                //tile.transform.eulerAngles = new Vector3(0, 0, 90);
                tile.SetActive(true);
                
                // pause
                // TODO: This can't go faster than the framerate - thus I need to calculate how much time the last frame took and start as many tiles as should have fit in there
                yield return new WaitForSeconds(TileSeparationDelay);
                

            }
        } 


    }

    private IEnumerator RunMainPathFirstTransition(MapData mapData, MapTransitionDirection mapTransitionDirection) {
        Debug.Log("Running RunMainPathFirstTransition");

        // Show the first item

        // pause
        yield return new WaitForSeconds(TileSeparationDelay);

        // etc

    }

    private IEnumerator RunRippleFromCentreTransition(MapData mapData, MapTransitionDirection mapTransitionDirection) {
        Debug.Log("Running RunRippleFromCentreTransition");

        // Show the first item

        // pause
        yield return new WaitForSeconds(TileSeparationDelay);

        // etc

    }






}

