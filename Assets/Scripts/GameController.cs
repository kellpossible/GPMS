﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public MapData MapData;

	ProcGen procGen;
	MapTransitioner mapTransitioner;

	private GameObject playerObject;

	// Use this for initialization
	void Start () {

		// get references to required classes
		GameObject mapTransitionerCtrl = GameObject.Find("MapTransition Ctrl");
		mapTransitioner = (MapTransitioner) mapTransitionerCtrl.GetComponent(typeof(MapTransitioner));
		GameObject procGenCtrl = GameObject.Find("ProcGen Ctrl");
		procGen = (ProcGen) procGenCtrl.GetComponent(typeof(ProcGen));
		playerObject = GameObject.Find("Player");
		playerObject.SetActive(false);
		
		// Run level generation
		// MapTile[,] mapArray = procGen.createLevel(80, 60, 0.60f);  
		// transition level on

		// register an action handler for when the level 
		// becomes available
		
		mapTransitioner.LevelAvailable += delegate {
			placePlayerOnStart(); 
		};

		// mapTransitioner.StartTransitioning(mapArray);

		// run a demo to transition the level every few seconds
		// StartCoroutine( levelTransitionDemo() );
		levelDemo();
	}

	private void placePlayerOnStart() {
		Debug.Log("placing player on start");
		GameObject entryTile = (GameObject) this.MapData.Entries[0];
		var entryPosition = entryTile.transform.position;
		var startPosition = new Vector3(entryPosition.x, 4.0f, entryPosition.z);

		Debug.Log("Moving player to " + startPosition);

		playerObject.SetActive(true);
		CharacterController characterController = (CharacterController) playerObject.GetComponent(typeof(CharacterController));
		characterController.moveToStartPosition(startPosition);
	}


	private void levelDemo() {
		Debug.Log("starting level transition ...");
		MapTile[,] mapArray = procGen.createLevel(80, 60, 0.60f);  
		mapTransitioner.StartTransitioning(MapData, mapArray);
	}

	public void restartLevel()
	{
		playerObject.SetActive(false);
		levelDemo();
	}

	private IEnumerator  levelTransitionDemo() {

		while(true) {

			yield return new WaitForSeconds(10);

			MapTile[,] mapArray = procGen.createLevel(80, 60, 0.60f);  
		
			// run a test transition using randomised tiles
			// MapTile[,] mapArray = mapTransitioner.CreateTestMap();

			// transition level on
			mapTransitioner.StartTransitioning(MapData, mapArray);

		}

	}
	
	
	
	void Update () {
		// This code is just an example of using MapData and LevelAvailable
		// if(LevelAvailable) {
		// 	GameObject entryTile = (GameObject) MapData.Entries[0];
		// 	TileBase gameObjectScript = (TileBase) entryTile.GetComponent(typeof(TileBase));

		// 	int colIndex = gameObjectScript.ArrayIndices[0];
		// 	int rowIndex = gameObjectScript.ArrayIndices[1];

		// 	Debug.Log("The first Entry Tile is at ("+colIndex+","+rowIndex+") in the array.");
		// 	Debug.Log("The first Entry Tile's co-ordinates (via Entries Array) are: " + entryTile.transform.position);
		// 	Debug.Log("The first Entry Tile's co-ordinates (via MapData Array) are: " + ((GameObject) MapData.MapObjArray[colIndex,rowIndex]).transform.position);
		// }

	}


}
