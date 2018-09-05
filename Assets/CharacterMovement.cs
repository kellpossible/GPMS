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

		var jump = Input.GetButtonDown("Jump");

		rb.AddForce(x, 0, z);

		var velocity_planar = rb.velocity;
		velocity_planar.y = 0.0f;

		if (velocity_planar.magnitude > 0.1)
		{
			transform.rotation=Quaternion.LookRotation(velocity_planar);
		}

		if (jump) {
			Debug.Log("Jumping");
			rb.velocity.Set(rb.velocity.x, 4.0f, rb.velocity.z);
		}
	}
}
