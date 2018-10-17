using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using System.Text;
using System.Threading;

public class MapTile
{
    public int mainPath; //place upon the main path 
    public int creationOrder; //order in which the tile was placed 
    public TileType type; // type of tile enum
    public int variation;
    public Coords pos;
}

public enum TileType
{
    Entry, Exit, Tile, Gap, Jump, Crumble, Turret, Door, Switch, Moving
};

public class Coords
{ //holds x and y coordinates
    public int x, y;
    public Coords(int xPos, int yPos)
    {
        x = xPos;
        y = yPos;
    }
}


public class ProcGen : MonoBehaviour
{

    public int tiles; //the amount of tiles to place
    int baseTiles;
    public float chance;
    float baseDirChance; //chance that path changes direction
    public enum DIR { N, E, S, W };
    DIR dir;
    int posX, posY;
    public int levelSize;
    public MapTile[,] lvl;
    public float obsRate = 0.5f; //chacne that a tile will spawn
    public Coords entryCoords, exitCoords;
    public Coords[] moveList;

    // Use this for initialization
    void Start()
    {
        MapTile[,] lvldone = createLevel(levelSize, tiles, chance);
        printLevel(1, lvldone); //print the main path
        printLevel(0, lvldone); //print the level
    }

    public MapTile[,] createLevel(int levelSize, int tiles, float chance)
    {
        posX = levelSize / 2;
        posY = levelSize / 2;
        baseTiles = tiles;
        baseDirChance = chance; //set the base dir from main chance (IMPORTANT)
        lvl = setupLevel(levelSize, tiles, chance, lvl, obsRate); //create a level
        int attempt = 1; //attempts at making a solvable level

        bool levelMade = pathfindTest(moveList, lvl, baseTiles); //test making a level
        bool doors = addDoors(lvl);


        while (!levelMade) //keep trying to make levels if one fails, so far works 100%
        {
            attempt++;
            Debug.Log("generating attempt " + attempt);
            lvl = setupLevel(levelSize, baseTiles, 0.6f, lvl, obsRate);
            Debug.Log("testing path");
            levelMade = pathfindTest(moveList, lvl, baseTiles);
            doors = addDoors(lvl);
        }
        Debug.Log("path found on attempt " + attempt);

        return lvl;
    }





    // Update is called once per frame
    void Update()
    {

    }

    void printLevel(int var, MapTile[,] level)
    {
        string output = "";
        int range = 0;
        foreach (var MapTile in level)
        {
            if (MapTile != null)
            {
                switch (var)
                {
                    case 0: //tile type
                        output += (int)MapTile.type;
                        break;
                    case 1: //main path
                        if (MapTile.mainPath > 0)
                        {
                            output += MapTile.mainPath;
                        }
                        else
                        {
                            output += "X";
                        }
                        break;

                    default:
                        break;
                }

            }
            else
            {
                output += "_";
            }
            range += 1;
            if (range % levelSize == 0)
            {
                Debug.Log(output);
                output = "";
            }
        }
    }


    void findTopLeft(MapTile[,] level)
    {
        //find top left tile for entry point
        for (int x = 0; x < levelSize; x++)
        {
            for (int y = 0; y < levelSize; y++)
            {
                if (level[x, y] != null)
                {
                    level[x, y].type = TileType.Entry;
                    entryCoords = new Coords(level[x, y].pos.x, level[x, y].pos.y);
                    return;
                }
            }
        }
    }

    void findBottomRight(MapTile[,] level)
    {
        //find bottom left for exit point
        for (int x = levelSize - 1; x >= 0; x--)
        {
            for (int y = levelSize - 1; y >= 0; y--)
            {
                if (level[x, y] != null)
                {
                    level[x, y].type = TileType.Exit;
                    exitCoords = new Coords(level[x, y].pos.x, level[x, y].pos.y);
                    return;
                }
            }
        }
    }

    TileType randomType()
    { //returns a random tile, will not return moving
        TileType output = new TileType();
        float rand = UnityEngine.Random.value * 10;
        if (rand >= 2.0 && rand <= 6.0)
        {
            output = (TileType)(int)rand;
        }
        else
        {
            output = randomType();
        }
        return output;
    }


    void populate(MapTile[,] level, float obsRate)
    { //adds tiles to the map. Uses the obsRate float as the chance a tile is placed (can be altered to change the difficulty)

        //checking for cross - this should be used for turrets
        for (int y = 0; y < levelSize; y++)
        {
            for (int x = 0; x < levelSize; x++)
            {
                if (level[x, y] != null && level[x, y + 1] != null && level[x + 1, y] != null && level[x, y - 1] != null && level[x - 1, y] != null)
                {
                    //if there is a cross make the centre a turret
                    if ((level[x, y].type == TileType.Tile && level[x, y + 1].type == TileType.Tile && level[x + 1, y].type == TileType.Tile && level[x, y - 1].type == TileType.Tile && level[x - 1, y].type == TileType.Tile))
                    {
                        float rnJesus = UnityEngine.Random.value;
                        if (rnJesus > obsRate)
                        {
                            // level[x, y].type = TileType.Turret; //randomize this line?
                            level[x, y].type = TileType.Turret;
                        }

                    }
                }
            }
        }
        /*
        //check for square (moving platforms)
        for (int y = 0; y < levelSize; y++)
        {
            for (int x = 0; x < levelSize; x++)
            {
                if (level[x, y] != null && level[x, y + 1] != null && level[x + 1, y] != null && level[x + 1, y + 1] != null)
                {
                    //if there is a square make the top right a moving platform
                    if ((level[x, y].type == TileType.Tile && level[x, y + 1].type == TileType.Tile && level[x + 1, y].type == TileType.Tile && level[x + 1, y + 1].type == TileType.Tile ))
                    {
                        float rnJesus = Random.value;
                        if (rnJesus > obsRate)
                        {
                            // level[x, y].type = TileType.Turret; //randomize this line?
                            level[x, y].type = TileType.Moving;
                            level[x, y].variation = 0;
                            level[x, y + 1] = null;
                            level[x + 1, y] = null;
                            level[x + 1, y + 1] = null;

                        }

                    }
                }
            }
        } */

        //vertical 3 * 1 object placement
        for (int y = 0; y < levelSize; y++)
        {
            for (int x = 0; x < levelSize; x++)
            {
                if (level[x, y] != null && level[x - 1, y] != null && level[x + 1, y] != null)
                {
                    //if there are 3 tiles in a row, make the middle one a gap
                    if (level[x, y].type == TileType.Tile && level[x - 1, y].type == TileType.Tile && level[x + 1, y].type == TileType.Tile)
                    {
                        float rnJesus = UnityEngine.Random.value;
                        if (rnJesus > obsRate)
                        {
                            //level[x, y].type = TileType.Gap; //randomize this line?
                            level[x, y].type = randomType();
                        }
                    }
                }
            }
        }
        //horizontal 3 * 1 object placement
        for (int y = 0; y < levelSize; y++)
        {
            for (int x = 0; x < levelSize; x++)
            {
                if (level[x, y] != null && level[x, y - 1] != null && level[x, y + 1] != null)
                {
                    //if there are 3 tiles in a row, make the middle one a gap
                    if (level[x, y].type == TileType.Tile && level[x, y - 1].type == TileType.Tile && level[x, y + 1].type == TileType.Tile)
                    {
                        float rnJesus = UnityEngine.Random.value;
                        if (rnJesus > obsRate)
                        {
                            //level[x, y].type = TileType.Gap; //randomize this line?
                            level[x, y].type = randomType();
                        }
                    }
                }
            }
        }
        //2 * 1 placement vertical
        for (int y = 0; y < levelSize; y++)
        {
            for (int x = 0; x < levelSize; x++)
            {
                if (level[x, y] != null && level[x - 1, y] != null)
                {
                    //if there is a cross make the centre a turret
                    if (level[x, y].type == TileType.Tile && level[x - 1, y].type == TileType.Tile)
                    {
                        float rnJesus = UnityEngine.Random.value;
                        if (rnJesus > obsRate)
                        {
                            //   level[x, y].type = TileType.Crumble; //randomize this line?
                            level[x, y].type = randomType();
                        }
                    }
                }
            }
        }
        //2 * 1 placement horizontal
        for (int y = 0; y < levelSize; y++)
        {
            for (int x = 0; x < levelSize; x++)
            {
                if (level[x, y] != null && level[x, y - 1] != null)
                {
                    //if there is a cross make the centre a turret
                    if (level[x, y].type == TileType.Tile && level[x, y - 1].type == TileType.Tile)
                    {
                        float rnJesus = UnityEngine.Random.value;
                        if (rnJesus > obsRate)
                        {
                            //level[x, y].type = TileType.Crumble; //randomize this line?
                            level[x, y].type = randomType();
                        }
                    }
                }
            }
        }
    }



    void layTile(DIR dir, TileType type, MapTile[,] level)
    { //lay a tile in a given direction, if not null then skip over that position in the current direction
        if (dir == DIR.E)
        {
            if (level[posX, posY] == null)
            {
                level[posX, posY] = new MapTile();
                level[posX, posY].type = type;
                level[posX, posY].creationOrder = tiles;
                level[posX, posY].pos = new Coords(posX, posY);
                tiles -= 1;
            }
            posX += 1;
        }
        if (dir == DIR.W)
        {
            if (level[posX, posY] == null)
            {
                level[posX, posY] = new MapTile();
                level[posX, posY].type = type;
                level[posX, posY].creationOrder = tiles;
                level[posX, posY].pos = new Coords(posX, posY);
                tiles -= 1;
            }
            posX -= 1;
        }
        if (dir == DIR.N)
        {
            if (level[posX, posY] == null)
            {
                level[posX, posY] = new MapTile();
                level[posX, posY].type = type;
                level[posX, posY].creationOrder = tiles;
                level[posX, posY].pos = new Coords(posX, posY);
                tiles -= 1;
            }
            posY -= 1;
        }
        if (dir == DIR.S)
        {
            if (level[posX, posY] == null)
            {
                level[posX, posY] = new MapTile();
                level[posX, posY].type = type;
                level[posX, posY].creationOrder = tiles;
                level[posX, posY].pos = new Coords(posX, posY);
                tiles -= 1;
            }
            posY += 1;
        }

    }

    DIR rndDir()
    { //generate a random direction for tiles to be placed in
        float x = UnityEngine.Random.value;
        if (x >= 0 && x <= 0.24)
        {
            return DIR.E;
        }
        if (x >= 0.25f && x <= 0.49f)
        {
            return DIR.N;
        }
        if (x >= 0.50f && x <= 0.74f)
        {
            return DIR.S;
        }
        if (x >= 0.75f && x <= 1.00f)
        {
            return DIR.W;
        }
        else
        {
            Debug.Log("new dir outside range; returning N");
            return DIR.N;
        }
    }

    void generate(float chance, MapTile[,] level)
    { //create a tile in a random direction dependant on the chance set within the inspector
        {
            float rng = UnityEngine.Random.value;
            if (rng < chance)
            {
                layTile(dir, TileType.Tile, level);
                chance -= 0.10f;
            }
            else
            {
                dir = rndDir();
                layTile(dir, TileType.Tile, level);
                chance = baseDirChance;     //does this need changing to chance's initial value?
            }
        }
    }

    public MapTile[,] setupLevel(int lvlSize, int numTiles, float dirChance, MapTile[,] level, float obsRate)
    {
        levelSize = lvlSize;
        posX = levelSize / 2;
        posY = levelSize / 2;
        tiles = numTiles;
        level = new MapTile[levelSize, levelSize];
        chance = dirChance;

        //randomize initial direction and place the first tile (for debug)
        dir = rndDir();

        //create subsequent tiles
        while (tiles > 0)
        {
            generate(chance, level);

            //Debug.Log(tiles);
        }
        //create entry and exit points
        findTopLeft(level);
        findBottomRight(level);


        if (tiles == 0)
        {
            //place tiles into the array
            populate(level, obsRate);
            //print the array to console
            //printLevel();
            tiles = -5;
        }
        return level;
    }

    //==================================================================================================
    //=================Pathfinding======================================================================

    public class Location
    {
        public int X;
        public int Y;
        public int F;
        public int G;
        public int H;
        public Location Parent;
    }

    public string[] levelToString(MapTile[,] level)
    {
        int x = level.GetLength(0), y = level.GetLength(1);
        string[] output = new string[x];
        for (int zx = 79; zx >= 0; zx--)
        {
            for (int zy = 0; zy < y; zy++)
            {
                if (level[zx, zy] == null)
                {
                    output[zx] += "X";
                }
                else if (level[zx, zy].type == TileType.Exit)
                {
                    output[zx] += "B";
                }
                else if (level[zx, zy].type == TileType.Entry)
                {
                    output[zx] += "A";
                }
                else
                {
                    output[zx] += "_";
                }
            }
        }
        return output;
    }


    public Location getTileLocation(MapTile[,] level, TileType tile)
    {
        Location output = null;
        int x = level.GetLength(0), y = level.GetLength(1);
        for (int zx = 0; zx < x; zx++)
        {
            for (int zy = 0; zy < x; zy++)
            {
                if (level[zy, zx] != null)
                {
                    if (level[zy, zx].type == tile)
                    {
                        output = new Location { X = zx, Y = zy };
                    }
                }
            }
        }
        if (output == null)
        {
            Debug.Log("no " + tile + " found");
        }
        else
            Debug.Log(tile + " found at " + output.X + "," + output.Y);
        return output;
    }

    public bool pathfindTest(Coords[] moves, MapTile[,] lvl, int baseTiles)
    {
        //create moveList
        Coords[] moveList = new Coords[baseTiles]; //should be dynamic, will have to create more variables

        //convert level from MapTile[,] to string        
        string[] map = levelToString(lvl);

        //this could return pathfinding between two points if these were passed as parameters...
        Location current = null;
        var start = getTileLocation(lvl, TileType.Entry); //set starting location from entry tile
        var target = getTileLocation(lvl, TileType.Exit); //set target location from exit tile 
        var openList = new List<Location>();    //create open list (unvisited)
        var closedList = new List<Location>();  //create closed list (visited)
        int g = 0;  //G Score

        //add start point to the open list
        openList.Add(start);

        while (openList.Count > 0)
        {
            //find lowest f score location
            var lowest = openList.Min(l => l.F);
            current = openList.First(l => l.F == lowest);

            //add current lcoation to the closed list
            closedList.Add(current);

            //remove current location from the open list
            openList.Remove(current);

            //target has been reached if in the closed list
            if (closedList.FirstOrDefault(l => l.X == target.X && l.Y == target.Y) != null)
                break;

            var adjacentSquares = GetWalkableAdjacentSquares(current.X, current.Y, map);
            g++;

            foreach (var adjacentSquare in adjacentSquares)
            {
                //ignore adjacent if already in the closed list
                if (closedList.FirstOrDefault(l => l.X == adjacentSquare.X
                        && l.Y == adjacentSquare.Y) != null)
                    continue;

                // if it's not in the open list...
                if (openList.FirstOrDefault(l => l.X == adjacentSquare.X
                        && l.Y == adjacentSquare.Y) == null)
                {
                    //compute g, h, and f, then set the parent location
                    adjacentSquare.G = g;
                    adjacentSquare.H = ComputeHScore(adjacentSquare.X, adjacentSquare.Y, target.X, target.Y);
                    adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                    adjacentSquare.Parent = current;

                    //add to open list
                    openList.Insert(0, adjacentSquare);
                }
                else
                {
                    // test if using the current G score makes the adjacent square's F score
                    // lower, if yes update the parent because it means it's a better path
                    if (g + adjacentSquare.H < adjacentSquare.F)
                    {
                        adjacentSquare.G = g;
                        adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                        adjacentSquare.Parent = current;
                    }
                }
            }
        }

        //save path (will be reversed) into coords array
        int mlx = 0;
        while (current != null)
        {
            moveList[mlx] = new Coords(current.X, current.Y);
            mlx++;
            current = current.Parent;
        }

        //retrieving move list
        bool hasEntry = false, hasExit = false;
        foreach (Coords c in moveList)
        {
            if (c != null)
            {
                if (c.x == start.X && c.y == start.Y)
                {
                    hasEntry = true;
                }
                else if (c.y == target.Y && c.x == target.X)
                {
                    hasExit = true;
                }
                Debug.Log("(" + c.x + "," + c.y + ")");

            }
        }
        //return false if no path found else apply path to level
        if (!(hasEntry && hasExit))
        {
            return false;
        }
        else
        {
            int x = 1;
            foreach (Coords c in moveList)
            {
                if (c != null)
                {
                    foreach (MapTile t in lvl)
                    {
                        if (t != null)
                        {
                            if (c.x == t.pos.y && c.y == t.pos.x)
                            {
                                t.mainPath = x;
                                x++;
                            }
                        }
                    }
                }
            }
            //if a single tile has a mainPath value > 0, return true
            foreach (MapTile t in lvl)
            {
                if (t != null)
                {
                    if (t.mainPath > 0)
                    {
                        return true;
                    }
                }
            }
            //return false if no path has been found
            return false;
        }
    }

    static List<Location> GetWalkableAdjacentSquares(int x, int y, string[] map)
    {
        var proposedLocations = new List<Location>()
        {
            new Location { X = x, Y = y - 1 },
            new Location { X = x, Y = y + 1 },
            new Location { X = x - 1, Y = y },
            new Location { X = x + 1, Y = y },
        };

        return proposedLocations.Where(l => map[l.Y][l.X] == '_' || map[l.Y][l.X] == 'B').ToList();
    }

    static int ComputeHScore(int x, int y, int targetX, int targetY)
    {
        return Math.Abs(targetX - x) + Math.Abs(targetY - y);
    }

    public bool addDoors(MapTile[,] level)
    {
        bool placed = false;
        int max = 0;
        foreach(MapTile t in lvl)
        {
            if (t != null)
            {
                if (t.mainPath > max)
                {
                    max = t.mainPath;
                }
            }
        }
        foreach (MapTile t in lvl)
        {
            if (t != null)
            {
                if (t.mainPath >= ((max / 3) * 2) && t.mainPath < (((max / 3) * 2) + 10) && t.type == TileType.Tile)
                {
                    t.type = TileType.Door;
                    placed = true;
                    Debug.Log("Door placed at (" + t.pos.x + ", " + t.pos.y + ")");
                    break;
                }
            }
        }
        if(!placed)
        {
            Debug.Log("no door placed");
            return false;
        }
        else
        {
            placed = false;
            foreach (MapTile t in lvl)
            {
                if (t != null)
                {
                    if (t.mainPath >= (max / 3) && t.mainPath < ((max / 3) + 10) && t.type == TileType.Tile)
                    {
                        t.type = TileType.Switch;
                        placed = true;
                        Debug.Log("Switch placed at (" + t.pos.x + ", " + t.pos.y + ")");
                        return true;
                    }
                }
            }
        }
        if(!placed)
        {
            Debug.Log("no switch placed");
        }
        return false;
    }





}
