using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    Entry, Exit, Tile, Gap, Jump, Crumble, Door, Switch, Turret, Moving
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


public class ProcGen : MonoBehaviour {

    public int tiles; //the amount of tiles to place
    public float chance = 0.90f;
    public enum DIR { N, E, S, W };
    public int posX, posY = 20;
    DIR dir;
    public int levelSize = 80;
    public MapTile[,] level = new MapTile[80, 80];
    public float tileChance = 0.5f;
    private Coords entryCoords, exitCoords;

    // Use this for initialization
    void Start ()
    {
        MapTile[,] lvl = createLevel();
        //Coords[] test = findPath(entryCoords, exitCoords);
    }


	// Update is called once per frame
	void Update ()
    {
        
	}

    void printLevel()
    {
        string output = "";
        int range = 0;
        foreach(var MapTile in level)
        {
            if (MapTile != null)
            {
                output += (int)MapTile.type;
                //output += MapTile.mainPath;
                //output += MapTile.pos.x;
            }
            else
            {
                output += "_";
            }
            range += 1;
            if(range % 80 == 0)
            {
                Debug.Log(output);
                output = "";
            }
        }
    }


    void findTopLeft()
    {
        //find top left tile for entry point
        for (int x = 0; x < 80; x++)
        {
            for (int y = 0; y < 80; y++)
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
        for (int x = 79; x >= 0; x--)
        {
            for (int y = 79; y >= 0; y--)
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
        float rand = Random.value * 10;
        if (rand > 1.0 && rand <= 8.0)
        {
            output = (TileType)(int)rand;
        }
        else
        {
            output = TileType.Jump;
        }
        return output;
    }
    

    void populate()
    { //adds tiles to the map. Uses the tileChance float as the chance a tile is placed (can be altered to change the difficulty)
        
        //checking for cross 
        for (int y = 0; y < 80; y++)
        {
            for (int x = 0; x < 80; x++)
            {
                if (level[x, y] != null && level[x, y + 1] != null && level[x + 1, y] != null && level[x, y - 1] != null && level[x - 1, y] != null)
                {
                    //if there is a cross make the centre a turret
                    if ((level[x, y].type == TileType.Tile && level[x, y + 1].type == TileType.Tile && level[x + 1, y].type == TileType.Tile && level[x, y - 1].type == TileType.Tile && level[x - 1, y].type == TileType.Tile))
                    {
                        float rnJesus = Random.value;
                        if(rnJesus > tileChance)
                        {
                            // level[x, y].type = TileType.Turret; //randomize this line?
                            level[x, y].type = randomType();
                        }
                        
                    }
                }
            }
        } 

        //check for square (moving platforms)
        for (int y = 0; y < 80; y++)
        {
            for (int x = 0; x < 80; x++)
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
        }

        //vertical 3 * 1 object placement
        for (int y = 0; y < 80; y++)
        {
            for (int x = 0; x < 80; x++)
            {
                if (level[x, y] != null && level[x - 1, y] != null && level[x + 1, y] != null)
                {
                    //if there are 3 tiles in a row, make the middle one a gap
                    if (level[x, y].type == TileType.Tile && level[x - 1, y].type == TileType.Tile && level[x + 1, y].type == TileType.Tile)
                    {
                        float rnJesus = Random.value;
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
        for (int y = 0; y < 80; y++)
        {
            for (int x = 0; x < 80; x++)
            {
                if (level[x, y] != null && level[x, y - 1] != null && level[x, y + 1] != null)
                {
                    //if there are 3 tiles in a row, make the middle one a gap
                    if (level[x, y].type == TileType.Tile && level[x, y - 1].type == TileType.Tile && level[x, y + 1].type == TileType.Tile)
                    {
                        float rnJesus = Random.value;
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
        for (int y = 0; y < 80; y++)
        {
            for (int x = 0; x < 80; x++)
            {
                if (level[x, y] != null && level[x - 1, y] != null)
                {
                    //if there is a cross make the centre a turret
                    if (level[x, y].type == TileType.Tile && level[x - 1, y].type == TileType.Tile)
                    {
                        float rnJesus = Random.value;
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
        for (int y = 0; y < 80; y++)
        {
            for (int x = 0; x < 80; x++)
            {
                if (level[x, y] != null && level[x, y - 1] != null)
                {
                    //if there is a cross make the centre a turret
                    if (level[x, y].type == TileType.Tile && level[x, y - 1].type == TileType.Tile)
                    {
                        float rnJesus = Random.value;
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
        float x = Random.value;
        if(x >= 0 && x <= 0.24)
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

    void generate (float chance)
    { //create a tile in a random direction dependant on the chance set within the inspector
        {
            float rng = Random.value;
            if (rng < chance)
            {
                layTile(dir, TileType.Tile);
                chance -= 0.10f;
            }
            else
            {
                dir = rndDir();
                layTile(dir, TileType.Tile);
                chance = 0.90f;
            }   
        }
    }

    public MapTile[,] createLevel()
    {
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
            //populate();
            //print the array to console
            printLevel();
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
                if(level[pos.x - 1, pos.y] != null)
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
        for(int i = 0; i < 4; i++)
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
            if(allMoves[i] != null)
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
        if (pos == exitCoords)
        {
            return true;
        }
        else
            return false;
    }

    /*
    public Coords[][] findPath(Coords start, Coords finish)
    {
        Coords[,][] temp = new Coords[100,100][];
        temp[0,0] = checkMoves(start, (int)DIR.N);

        Debug.Log("moves from start");
        int i = 1 ,j = 1;
        foreach (Coords pos in temp[0,0])
        {
            temp[i,j] = checkMoves(pos, )
        }



        return null;
    }

    */


    public Coords[][] createPairList(MapTile[,] level)
    {
        Coords[][] pairs = new Coords[60][];
        foreach(MapTile tile in level)
        {

        }
        

        return null;
    }


}
