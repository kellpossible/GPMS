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

    private int mapWidth;
	public int MapWidth { get {return mapWidth;} }
	private int mapDepth;
	public int MapDepth { get {return mapDepth;} }
	


	public MapData(MapTile[,] newGeneratedMapArray) {

		// old version (copy in the whole array)
		//mapDataArray = newGeneratedMapArray;

		// new version (trim off any unecessary tiles from borders of array)
		int jMin = newGeneratedMapArray.GetLength(0)+1;
		int jMax = -1;
		int kMin = newGeneratedMapArray.GetLength(1)+1;
		int kMax = -1;
		for(int j=0; j<newGeneratedMapArray.GetLength(0); j++) {
			for(int k=0; k<newGeneratedMapArray.GetLength(1); k++) {

				if(newGeneratedMapArray[j,k] != null) {

					if(j<jMin){
						jMin = j;
					}
					if(k<kMin){
						kMin = k;
					}
					if(j>jMax){
						jMax = j;
					}
					if(k>kMax){
						kMax = k;
					}

				}
				
			}
		}
		// create splice of array
		mapWidth = jMax-jMin+1;
		mapDepth = kMax-kMin+1;
		mapDataArray = new MapTile[mapWidth,mapDepth];
		int jNew = 0;
		for(int j=jMin; j<=jMax; j++) {
			
			int kNew = 0;
			for(int k=kMin; k<=kMax; k++) {

				if(newGeneratedMapArray[j,k] != null) {
					mapDataArray[jNew,kNew] = newGeneratedMapArray[j,k];
				}
				
				kNew++;
			}
			jNew++;
		}


		

		

		// populate main path


		// populate branches


		// populate other tile types

	}


}
