using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterMovement : MonoBehaviour {
	private static float GROUND_ACCELERATION = 10000.0f;
	private static float MAX_GROUND_VELOCITY = 5.0f;

	public Vector3 characterFacingVelocity = Vector3.zero;
	public Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		
	}
	
	// Update is called once per frame
	void Update () {
		var xInput = Input.GetAxis("Horizontal");
        var zInput = Input.GetAxis("Vertical");

		var jump = Input.GetButtonDown("Jump");

		var xAcceleration = xInput * Time.deltaTime * GROUND_ACCELERATION * (MAX_GROUND_VELOCITY - Math.Abs(rb.velocity.x))/MAX_GROUND_VELOCITY;
		var zAcceleration = zInput * Time.deltaTime * GROUND_ACCELERATION * (MAX_GROUND_VELOCITY - Math.Abs(rb.velocity.z))/MAX_GROUND_VELOCITY;
		rb.AddForce(xAcceleration, 0, zAcceleration);

		// if (Math.Abs(rb.velocity.x) < MAX_GROUND_VELOCITY) {
		// 	rb.AddForce(x, 0, 0);
		// }

		// if (Math.Abs(rb.velocity.z) < MAX_GROUND_VELOCITY) {
		// 	rb.AddForce(0, 0, z);
		// }

		var velocity_planar = rb.velocity;
		//todo change this to be maybe a small factor of the y so the
		// character follows their velocity a little during a jump!
		velocity_planar.y = rb.velocity.y * 0.3f; 

		if (velocity_planar.magnitude > 0.1) {
			// transform.rotation=Quaternion.LookRotation(velocity_planar);
		}

		if (jump) {
			Debug.Log("Jumping");
			rb.AddForce(0.0f, 400.0f, 0.0f);
		}
	}
}
