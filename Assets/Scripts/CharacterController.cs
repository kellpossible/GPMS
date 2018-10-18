using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
	Vector3 previousStartPosition;

	// Use this for initialization
	void Start () {
		// this.gameObject.SetActive(false);
	}

	public void moveToStartPosition(Vector3 position)
	{
		previousStartPosition = position;
		this.transform.position = position;
		CharacterMovement characterMovement = (CharacterMovement) this.GetComponent(typeof(CharacterMovement));
		characterMovement.rb.velocity = new Vector3();
	}
	
	// Update is called once per frame
	void Update () {
		if (this.transform.position.y < -3.0) {
			moveToStartPosition(previousStartPosition);
		}
	}
}
