using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCtrl : MonoBehaviour {

    private Animator anim;
    public bool doorUnlock;
    public bool exitEntered;
    public bool exitEnterAvailable;

    private GameObject gameController;

    // Use this for initialization
    void Start ()
    {
        gameController = GameObject.Find("Game Ctrl");
        anim = GetComponent<Animator>();
        doorUnlock = false;
        exitEntered = false;
        exitEnterAvailable = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
           if (Input.GetKeyDown(KeyCode.P))
            {
                anim.SetTrigger("Unlock");
                exitEnterAvailable = true;
            }
            
    }

    void OnTriggerEnter (Collider col)
    {
        if (col.tag == "Player" ) {
            doorUnlock = true;
            anim.SetTrigger("Active");

            Debug.Log("end level");

            var controllerComponent = (GameController) gameController.GetComponent(typeof(GameController));
            controllerComponent.restartLevel();
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            anim.SetTrigger("Unlock");
        }
    }
}
