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
    public float chance;
    public float baseDirChance; //chance that path changes direction
    public enum DIR { N, E, S, W };
    public int posX, posY;
    DIR dir;
    public int levelSize;
    public MapTile[,] level;
    public MapTile[,] lvl;
    public float tileChance = 0.5f; //chacne that a tile will spawn
    public Coords entryCoords, exitCoords;
    public Coords[] moveList;

    // Use this for initialization
    void Start()
    {
        baseDirChance = chance; //set the base dir from main chance (IMPORTANT)
        lvl = createLevel(levelSize, tiles, chance); //create a level
        int attempt = 1; //attempts at making a solvable level

        bool levelMade = pathfindTest(moveList, lvl); //test making a level

        while (!levelMade) //keep trying to make levels if one fails, so far works 100%
        {
            attempt++;
            Debug.Log("generating attempt " + attempt);
            lvl = createLevel(levelSize, 60, 0.6f);
            Debug.Log("testing path");
            levelMade = pathfindTest(moveList, lvl);
        }
        Debug.Log("path found on attempt " + attempt);

        printLevel(1); //print the main path
        printLevel(0); //print the level

    }





    // Update is called once per frame
    void Update()
    {

    }

    void printLevel(int var)
    {
        string output = "";
        int range = 0;
        foreach (var MapTile in level)
        {
            if (MapTile != null)
            {
                switch (var) { 
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


    void findTopLeft()
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

    void findBottomRight()
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


    void populate()
    { //adds tiles to the map. Uses the tileChance float as the chance a tile is placed (can be altered to change the difficulty)

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
                        if (rnJesus > tileChance)
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
                        if (rnJesus > tileChance)
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
                        if (rnJesus > tileChance)
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
                        if (rnJesus > tileChance)
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
                        if (rnJesus > tileChance)
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
                        if (rnJesus > tileChance)
                        {
                            //level[x, y].type = TileType.Crumble; //randomize this line?
                            level[x, y].type = randomType();
                        }
                    }
                }
            }
        }
    }



    void layTile(DIR dir, TileType type)
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

    void generate(float chance)
    { //create a tile in a random direction dependant on the chance set within the inspector
        {
            float rng = UnityEngine.Random.value;
            if (rng < chance)
            {
                layTile(dir, TileType.Tile);
                chance -= 0.10f;
            }
            else
            {
                dir = rndDir();
                layTile(dir, TileType.Tile);
                chance = baseDirChance;     //does this need changing to chance's initial value?
            }
        }
    }

    public MapTile[,] createLevel(int lvlSize, int numTiles, float dirChance)
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
            generate(chance);

            //Debug.Log(tiles);
        }
        //create entry and exit points
        findTopLeft();
        findBottomRight();


        if (tiles == 0)
        {
            //place tiles into the array
            populate();
            //print the array to console
            //printLevel();
            tiles = -5;
        }
        return level;
    }

    //==================================================================================================
    //=================Pathfinding======================================================================

    public Coords checkTile(int dir, Coords pos)
    {//returns coordinates of an available move or null
        switch (dir)
        {
            case 0:
                if (level[pos.x - 1, pos.y] != null)
                {
                    Coords output = new Coords(pos.x - 1, pos.y);
                    return output;
                }
                break;
            case 1:
                if (level[pos.x, pos.y + 1] != null)
                {
                    Coords output = new Coords(pos.x, pos.y + 1);
                    return output;
                }
                break;
            case 2:
                if (level[pos.x + 1, pos.y] != null)
                {
                    Coords output = new Coords(pos.x + 1, pos.y);
                    return output;
                }
                break;
            case 3:
                if (level[pos.x, pos.y - 1] != null)
                {
                    Coords output = new Coords(pos.x, pos.y - 1);
                    return output;
                }
                break;
            default: return null;
        }
        return null;
    }


    public Coords[] checkMoves(Coords tile)
    {
        //returns an array of coordinates of available moves up to length 4
        //if null no moves
        Coords[] allMoves = new Coords[4];
        int j = 1;
        for (int i = 0; i < 4; i++)
        {
            allMoves[i] = checkTile(i, tile);
            if (allMoves[i] != null)
            {
                j += 1;
            }
        }
        Coords[] moves = new Coords[j];
        j = 1;
        moves[0] = tile;
        for (int i = 0; i < 4; i++)
        {
            if (allMoves[i] != null)
            {
                moves[j] = allMoves[i];
                j += 1;
            }
        }
        if (moves[0] != null)
        {
            return moves;
        }
        else return null;
    }

    public bool isExit(Coords pos)
    {
        if (pos.x == exitCoords.x && pos.y == exitCoords.y)
        {
            return true;
        }
        else
            return false;
    }

    public bool isSame(Coords i, Coords j)
    {
        if (i.x == j.x && i.y == j.y)
        {
            return true;
        }
        else return false;
    }

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
        string[] output = new string[80];
        int x = level.GetLength(0), y = level.GetLength(1);
        for (int zx = 79; zx >= 0; zx--)
        {
            for (int zy = 0; zy < y; zy++)
            {
                if (level[zx, zy] == null)
                {
                    output[zx] += "X";
                }
                else if(level[zx,zy].type == TileType.Exit)
                {
                    output[zx] += "B";
                }
                else if(level[zx,zy].type == TileType.Entry)
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

    public bool pathfindTest(Coords[] moves, MapTile[,] lvl)
    {

        Coords[] moveList = new Coords[60];

        //convert map 
        /*string[] map = new string[]
        {
            "+------+",
            "|      |",
            "|A X   |",
            "|XXX   |",
            "|   X  |",
            "| B    |",
            "|      |",
            "+------+",
        }; */
        
        string[] map = levelToString(level);

        /*
        foreach (var line in map)
        {
            Debug.Log(line);
        } */

        
        // algorithm

        Location current = null;
        //var start = new Location { X = 1, Y = 2 }; //get start point
        //var target = new Location { X = 2, Y = 5 }; //get end point
        var start = getTileLocation(level, TileType.Entry);
        var target = getTileLocation(level, TileType.Exit);
        var openList = new List<Location>();
        var closedList = new List<Location>();
        int g = 0;

        // start by adding the original position to the open list
        openList.Add(start);

        while (openList.Count > 0)
        {
            // get the square with the lowest F score
            var lowest = openList.Min(l => l.F);
            current = openList.First(l => l.F == lowest);

            // add the current square to the closed list
            closedList.Add(current);

            // show current square on the map
            //(current.X, current.Y);
            //Console.Write('.');
            //Console.SetCursorPosition(current.X, current.Y);
            //System.Threading.Thread.Sleep(100);

            // remove it from the open list
            openList.Remove(current);

            // if we added the destination to the closed list, we've found a path
            if (closedList.FirstOrDefault(l => l.X == target.X && l.Y == target.Y) != null)
                break;

            var adjacentSquares = GetWalkableAdjacentSquares(current.X, current.Y, map);
            g++;

            foreach (var adjacentSquare in adjacentSquares)
            {
                // if this adjacent square is already in the closed list, ignore it
                if (closedList.FirstOrDefault(l => l.X == adjacentSquare.X
                        && l.Y == adjacentSquare.Y) != null)
                    continue;

                // if it's not in the open list...
                if (openList.FirstOrDefault(l => l.X == adjacentSquare.X
                        && l.Y == adjacentSquare.Y) == null)
                {
                    // compute its score, set the parent
                    adjacentSquare.G = g;
                    adjacentSquare.H = ComputeHScore(adjacentSquare.X, adjacentSquare.Y, target.X, target.Y);
                    adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                    adjacentSquare.Parent = current;

                    // and add it to the open list
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

        // assume path was found; let's show it
        int mlx = 0;
        while (current != null)
        {
            //Console.SetCursorPosition(current.X, current.Y);
            //Console.Write('_');
            //Console.SetCursorPosition(current.X, current.Y);
            moveList[mlx] = new Coords(current.X, current.Y);
            mlx++;
            current = current.Parent;
            //System.Threading.Thread.Sleep(100);
        }

        // end pathfinding
        // retrieving move list
        bool hasEntry = false, hasExit = false;
        foreach (Coords c in moveList)
        {
            if (c != null)
            {
                if(c.x == start.X && c.y == start.Y)
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
        //return false if path found else apply path to level
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
                            if(c.x == t.pos.y && c.y == t.pos.x)
                            {
                                t.mainPath = x;
                                x++;
                            }
                        }
                    }
                }
            }
            foreach(MapTile t in lvl)
            {
                if(t != null)
                {
                    if(t.mainPath > 0)
                    {
                        return true;
                    }
                }
            }
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

    
     
    


}
