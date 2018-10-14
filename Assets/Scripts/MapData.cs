using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData {

	private MapTile[,] mapDataArray;
	public MapTile[,] MapDataArray { get {return mapDataArray;} }

	private GameObject[,] mapObjArray;
	public GameObject[,] MapObjArray {
		// TODO: Test is this can prevent changing the array items - or how best to do it
		set	{
			mapObjArray = value;
			recordFeatures();
		}
		get { return mapObjArray; }
	}

	

    private int mapWidth;
	public int MapWidth { get {return mapWidth;} }
	private int mapDepth;
	public int MapDepth { get {return mapDepth;} }
	

	public ArrayList MapObjArray2D = new ArrayList();
	public ArrayList MainPath = new ArrayList();
	public ArrayList Floors = new ArrayList();
	public ArrayList Entries = new ArrayList();
	public ArrayList Exits = new ArrayList();
	public ArrayList Turrets = new ArrayList();
	public ArrayList Jumps = new ArrayList();
	public ArrayList Doors = new ArrayList();
	public ArrayList Switches = new ArrayList();
	public ArrayList Movers = new ArrayList();
	public ArrayList Crumblers = new ArrayList();
	
	
	


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


	
	private void recordFeatures() {

		for(int j=0; j<mapObjArray.GetLength(0); j++) {
			
			for(int k=0; k<mapObjArray.GetLength(1); k++) {

				if(mapObjArray[j,k] == null) { continue; };

				TileBase mapObjScript = (TileBase) mapObjArray[j,k].GetComponent(typeof(TileBase));
				// record array indices directly into game object
				mapObjScript.ArrayIndices[0] = j;
				mapObjScript.ArrayIndices[1] = k;
				mapObjScript.TileType = mapDataArray[j,k].type;

				// record references to special objects in special arrays
				MapObjArray2D.Add(mapObjArray[j,k]);
				if(mapDataArray[j,k].type == TileType.Tile) { Floors.Add(mapObjArray[j,k]); };
				if(mapDataArray[j,k].type == TileType.Entry) { Entries.Add(mapObjArray[j,k]); };
				if(mapDataArray[j,k].type == TileType.Exit) { Exits.Add(mapObjArray[j,k]); };
				if(mapDataArray[j,k].type == TileType.Moving) { Movers.Add(mapObjArray[j,k]); };
				if(mapDataArray[j,k].type == TileType.Door) { Doors.Add(mapObjArray[j,k]); };
				if(mapDataArray[j,k].type == TileType.Switch) { Switches.Add(mapObjArray[j,k]); };
				if(mapDataArray[j,k].type == TileType.Jump) { Jumps.Add(mapObjArray[j,k]); };
				if(mapDataArray[j,k].type == TileType.Crumble) { Crumblers.Add(mapObjArray[j,k]); };
				if(mapDataArray[j,k].type == TileType.Turret) { Turrets.Add(mapObjArray[j,k]); };

				// TODO: check if part of main path
				// TODO: slot in appropriate spot in main path
				
			}
			
		}

	}


}
