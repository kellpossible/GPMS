using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCtrl : MonoBehaviour {

    public GameObject Player;
    public GameObject Button;
    public Animator anim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //if (CompareTag(Player))
		
	}

    void OnTriggerEnter (Collider col)
    {
        if (col.tag == "Player" ) {
          // TODO 
          // button's animation.play
        }
    }
}
