using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile
{
    public int mainPath; //order in which the tile was placed (NO WAY OF DETECTING MAIN PATH YET)
    public int tileType; //type of tile int
    public TileType type; // type of tile enum
}

public enum TileType
{
    Entry, Exit, Tile, Gap, Jump, Crumble, Door, Switch, Turret, Moving
};


public class ProcGen : MonoBehaviour {

    public int tiles; //the amount of tiles to place
    public float chance = 0.90f;
    public enum DIR { N, E, S, W };
    public int posX, posY = 20;
    public BoxCollider2D _collider;
    DIR dir;
    public int levelSize = 80;
    public MapTile[,] level = new MapTile[80, 80];
    public float tileChance = 0.5f;

    // Use this for initialization
    void Start ()
    {
        dir = DIR.E;
        //generate (tiles, rand);
        _collider = GetComponent<BoxCollider2D> ();

        createLevel();
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
                output += MapTile.tileType;
                //output += MapTile.mainPath;
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
                    level[x, y].tileType = (int)level[x, y].type;
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
                    level[x, y].tileType = (int)level[x, y].type;
                    return;
                }
            }
        }
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
                            level[x, y].type = TileType.Turret; //randomize this line?
                            level[x, y].tileType = (int)level[x, y].type;
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
                            level[x, y].type = TileType.Gap; //randomize this line?
                            level[x, y].tileType = (int)level[x, y].type;
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
                            level[x, y].type = TileType.Gap; //randomize this line?
                            level[x, y].tileType = (int)level[x, y].type;
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
                            level[x, y].type = TileType.Crumble; //randomize this line?
                            level[x, y].tileType = (int)level[x, y].type;
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
                            level[x, y].type = TileType.Crumble; //randomize this line?
                            level[x, y].tileType = (int)level[x, y].type;
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
                level[posX, posY].tileType = (int)level[posX, posY].type;
                level[posX, posY].mainPath = tiles;
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
                level[posX, posY].tileType = (int)level[posX, posY].type;
                level[posX, posY].mainPath = tiles;
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
                level[posX, posY].tileType = (int)level[posX, posY].type;
                level[posX, posY].mainPath = tiles;
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
                level[posX, posY].tileType = (int)level[posX, posY].type;
                level[posX, posY].mainPath = tiles;
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

    void createLevel()
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
            populate();
            //print the array to console
            printLevel();
            tiles = -5;
        }
    }

}
