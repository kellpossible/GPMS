using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public void moveToStartPosition(Vector3 position)
	{
		this.transform.position = position;
		CharacterMovement characterMovement = (CharacterMovement) this.GetComponent(typeof(CharacterMovement));
		characterMovement.rb.velocity = new Vector3();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
