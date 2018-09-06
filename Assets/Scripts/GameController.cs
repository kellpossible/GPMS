using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {

		GameObject mapTransitionerCtrl = GameObject.Find("MapTransition Ctrl");
		MapTransitioner mapTransitioner = (MapTransitioner) mapTransitionerCtrl.GetComponent(typeof(MapTransitioner));
		GameObject procGenCtrl = GameObject.Find("ProcGen Ctrl");
		ProcGen procGen = (ProcGen) procGenCtrl.GetComponent(typeof(ProcGen));
		
		// MapTile[,] mapArray = procGen.createLevel();
		// mapTransitioner.StartTransitioning(mapArray);
		
		mapTransitioner.RunTestTransition();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
