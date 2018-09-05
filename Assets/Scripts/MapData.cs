using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData {

	private MapTile[,] mapDataArray;
	public MapTile[,] MapDataArray { get {return mapDataArray;} }

	private GameObject[,] mapObjArray;
	public GameObject[,] MapObjArray {
		set	{ mapObjArray = value; }		// TODO: Test is this can prevent changing the array items - or how best to do it
		get { return mapObjArray; }
	}

	private GameObject[] mainPath;
	public GameObject[] MainPath { get {return mainPath;} }

	private GameObject[] branches;
	public GameObject[] Branches { get {return branches;} }
	


	public MapData(MapTile[,] newGeneratedMapArray) {

		mapDataArray = newGeneratedMapArray;

		// populate main path


		// populate branches


		// populate other tile types

	}


}
