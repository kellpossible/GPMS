using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterMovement : MonoBehaviour {
	private static float LATERAL_FORCE_ON_GROUND = 120000.0f;
	private static float LATERAL_FORCE_IN_AIR = 10000.0f;
	private static float JUMP_FORCE = 8000.0f;
	private static float MAX_GROUND_VELOCITY = 5.0f;
	private static float MAX_Z_GROUNDED_VELOCITY = 0.01f;
	private static float ROTATION_START_THRESHOLD_VELOCITY = 0.1f;

	public Vector3 characterFacingVelocity = Vector3.zero;
	public Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		
	}
	
	/// This override currently just handles the collision with the 
	/// ground
	private void OnCollisionEnter(Collision collision) {
		if(collision.contacts.Length > 0)
		{
			ContactPoint contact = collision.contacts[0];
			if(Vector3.Dot(contact.normal, Vector3.up) > 0.5)
			{
				//collision was from below
			}
		}
	}

	private bool OnGround() {
		return Math.Abs(rb.velocity.y) < MAX_Z_GROUNDED_VELOCITY;
	}
	
	// Update is called once per frame
	void Update () {
		var xInput = Input.GetAxis("Horizontal");
        var zInput = Input.GetAxis("Vertical");

		var jump = Input.GetButtonDown("Jump");

		Vector3 lateralForceDirection = new Vector3(xInput, 0.0f, zInput);
		Vector3 lateralForce = lateralForceDirection.normalized;
		
		float lateralForceMagnitude = LATERAL_FORCE_IN_AIR;

		if (this.OnGround()) {
			lateralForceMagnitude = LATERAL_FORCE_ON_GROUND;

			if (jump) {
				Debug.Log("Jumping");
				rb.AddForce(0.0f, JUMP_FORCE, 0.0f);
			}
		}

		lateralForce = lateralForce * lateralForceMagnitude * Time.deltaTime;

		rb.AddForce(lateralForce);

		var velocity_planar = rb.velocity;
		//todo change this to be maybe a small factor of the y so the
		// character follows their velocity a little during a jump!
		// velocity_planar.y = rb.velocity.y * 0.3f; 
		velocity_planar.y = 0.0f;

		var playerMesh = GameObject.Find("PlayerMesh");
		

		if (velocity_planar.magnitude > ROTATION_START_THRESHOLD_VELOCITY) {
			transform.rotation=Quaternion.LookRotation(velocity_planar);
			playerMesh.transform.Rotate(new Vector3(1, 0, 0), 60.0f * velocity_planar.magnitude * Time.deltaTime);
		} else {
			
		}

		
	}
}
