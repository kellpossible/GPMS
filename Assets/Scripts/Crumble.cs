﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crumble : MonoBehaviour
{
    private Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
  
          anim.SetTrigger("Touch");
            //anim.SetBool("Unlock", true);


            //Debug.Log("player actived the activator");
        }
    }

}
