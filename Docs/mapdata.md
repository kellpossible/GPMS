When a map transition is called, the MapTransitioner class takes the data array and parses it into a number of fields for access during the levels play.
These fields are stored in the MapData class.

Each levels MapData is populated into the GameController script. It can be accessed with the following code:

**This is not yet implemented**

`// create reference to the game controller object (this is an expensive operation and should only be done once (in startup)`

`GameObject gameController = GameObject.Find("Game Ctrl");`

`// create a reference to the script component on the GameController object`

`GameController gameControllerScript = (MapTransitioner) gameController.GetComponent(typeof(GameController));`

`// access mapData`

`gameControllerScript.mapData`

A GameObjects position in the MapData arrays can also be found by accessing the variables directly on it. See below for details.

# Map Data Class #


## MapObjArray[,] ##
This is a 2d array containing references to the GameObjects that make up each tile.
Any empty tile position is null.

## MapWidth ##
The width of the map (x-axis) in world units.

## MapDepth ##
The depth of the map (z-axis) in world units.

## MainPath _(ArrayList)_ ##

**This is not yet implemented**

This will be a list of all tiles that make up the shortest route from the entry point to exit point on the map.

## Entries _(ArrayList)_ ##
A list GameObject references to all entry tiles in the map.

## Exits _(ArrayList)_ ##
A list GameObject references to all exit tiles in the map.

## Turrets _(ArrayList)_ ##
A list GameObject references to all turret tiles in the map.

## Jumps _(ArrayList)_ ##
A list GameObject references to all jump tiles in the map.

## Doors _(ArrayList)_ ##
A list GameObject references to all door tiles in the map.

## Switches _(ArrayList)_ ##
A list GameObject references to all switch tiles in the map.

## MapDataArray[,] ##
Use this to access the original Procedurally generated data (minus surplus padding from the edges of the array).
You shouldn't need to use this as the above fields provide more cleaned data.


# Note on GameObjects #
While the MapData class provides access to GameObjects by iterating through arrays and ArrayLists (and accessing other fields), During gameplay, you may require going the other direction, ie. The player steps on a tile and you want to know where that tile sits in the array and what tiles surround it.
To do this, MapData also populates some information on the tile itself.

This also means taht any GameObject tile must have a script component that is either extends BaseTile.cs or uses it directly.

## Access BaseTile Data ##
`
// Access the classes script on the GameObject

TileBase gameObjectScript = (TileBase) gameObjectTile.GetComponent(typeof(TileBase));

// Use the script to access the public variables on that tile

Debug.Log( gameObjectScript.TileType );
`

## TypeType ##
This field returns the type of tile this GameObject represents.
Type will be from the list of TypeTypes in the publicly accessible enum `TypeType`

## ArrayIndices[2] ##
This array conteains the indices for this objects references in the `MapData.MapDataArray` and the `MapData.MapObjArray` and can be used to find adjacent tile GameObject references.

