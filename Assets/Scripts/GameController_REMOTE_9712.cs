using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
		// get references to required classes
		GameObject mapTransitionerCtrl = GameObject.Find("MapTransition Ctrl");
		MapTransitioner mapTransitioner = (MapTransitioner) mapTransitionerCtrl.GetComponent(typeof(MapTransitioner));
		GameObject procGenCtrl = GameObject.Find("ProcGen Ctrl");
		ProcGen procGen = (ProcGen) procGenCtrl.GetComponent(typeof(ProcGen));

        // Run level generation
        MapTile[,] mapArray = procGen.createLevel(80, 60, 0.60f);  
        // transition level on
        mapTransitioner.StartTransitioning(mapArray); 

        // run a test transition using randomised tiles
        // mapTransitioner.RunTestTransition();
        
    }

    // Update is called once per frame
    void Update () {
		
	}
}
