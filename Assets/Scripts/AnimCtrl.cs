using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCtrl : MonoBehaviour {

    public Animator anim;
    public bool doorUnlock;
    public bool exitEnter;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        doorUnlock = false;
        exitEnter = false;

    }
	
	// Update is called once per frame
	void Update () {
        // FOR TEST ONLY
        /*
        if (Input.GetKeyDown(KeyCode.P))
        {
            anim.SetTrigger("Active");
        }
        */


    }

    void OnTriggerEnter (Collider col) {
        if (col.tag == "Player" ) {
            anim.SetTrigger("Active");

            Debug.Log("player actived the activator");
            doorUnlock = true;
            anim.SetTrigger("Unlock");
            Debug.Log("exit is now unlocked");


        }
    }

    void CheckState ()
    {
        //if (doorUnlock && exitEnter)
        if (doorUnlock)
        {
            Debug.Log("player has reached the exit");
            Debug.Log("Level Completed.");

        }
    }
}
