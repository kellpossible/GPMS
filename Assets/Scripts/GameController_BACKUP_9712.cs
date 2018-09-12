using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public MapData MapData;
	public bool LevelAvailable = false;

	// Use this for initialization
	void Start () {
        
		// get references to required classes
		GameObject mapTransitionerCtrl = GameObject.Find("MapTransition Ctrl");
		MapTransitioner mapTransitioner = (MapTransitioner) mapTransitionerCtrl.GetComponent(typeof(MapTransitioner));
		GameObject procGenCtrl = GameObject.Find("ProcGen Ctrl");
		ProcGen procGen = (ProcGen) procGenCtrl.GetComponent(typeof(ProcGen));
<<<<<<< HEAD
		
		// Run level generation
		MapTile[,] mapArray = procGen.createLevel();
		// transition level on
		mapTransitioner.StartTransitioning(mapArray);
		
		// run a test transition using randomised tiles
		// mapTransitioner.RunTestTransition();
		
	}
	
	
	
	void Update () {
=======

        // Run level generation
        MapTile[,] mapArray = procGen.createLevel(80, 60, 0.60f);  
        // transition level on
        mapTransitioner.StartTransitioning(mapArray); 

        // run a test transition using randomised tiles
        // mapTransitioner.RunTestTransition();
        
    }

    // Update is called once per frame
    void Update () {
>>>>>>> ProcGen
		

		// this code is just a test of MapData and LevelAvailable
		// if(LevelAvailable) {
		// 	GameObject entryTile = (GameObject) MapData.Entries[0];
		// 	TileBase gameObjectScript = (TileBase) entryTile.GetComponent(typeof(TileBase));

		// 	int colIndex = gameObjectScript.ArrayIndices[0];
		// 	int rowIndex = gameObjectScript.ArrayIndices[1];

		// 	Debug.Log("The first Entry Tile is at ("+colIndex+","+rowIndex+") in the array.");
		// }

	}


}
