using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
	public Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		
	}
	
	// Update is called once per frame
	void Update () {
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 1000.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 1000.0f;

		rb.AddForce(x, 0, z);
		Debug.Log(rb.velocity);

        // transform.Translate(x, 0, z);
	}
}
