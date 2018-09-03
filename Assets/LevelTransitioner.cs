using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitioner : MonoBehaviour {
	private LevelTransitioner m_Instance;
  	public LevelTransitioner Instance { get { return m_Instance; } }

	void Awake()
	{
		m_Instance = this;
	}

	void OnDestroy()
	{
		m_Instance = null;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// common GUI code goes here
	void OnGui()
	{
		
	}







	void populate()
    { //put rules here for objects
        for (int i = 0; i < 200; i++)
        {
            for (int j = 0; j < 200; j++)
            {
                if (map[i, j] == '#')
                {
                    if(map[i + 1, j] == '#' && map[i, j + 1] == '#')
                    {
                        Instantiate(turret, new Vector3(i - 100, j - 100), Quaternion.identity);
                        //Debug.Log("turret created at " + i + ", " + j);
                    }
                }
            }
        }
        Debug.Log("populated");
    }



	void layTile(DIR dir, GameObject tile)
    {
        if (dir == DIR.E)
        {
            Instantiate(tile, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
            map[posX, posY] = '#';
            transform.position = new Vector3(transform.position.x + 1, transform.position.y);
            posX += 1;
        }
        if (dir == DIR.W)
        {
            Instantiate(tile, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
            map[posX, posY] = '#';
            transform.position = new Vector3(transform.position.x - 1, transform.position.y);
            posX -= 1;
        }
        if (dir == DIR.N)
        {
            Instantiate(tile, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
            map[posX, posY] = '#';
            transform.position = new Vector3(transform.position.x, transform.position.y - 1);
            posY -= 1;
        }
        if (dir == DIR.S)
        {
            Instantiate(tile, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
            map[posX, posY] = '#';
            transform.position = new Vector3(transform.position.x, transform.position.y + 1);
            posY += 1;
        }
        
    }



}
