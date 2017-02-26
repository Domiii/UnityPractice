/**
 * Assign a CollisionCounter (ideally placed in "feet") to get a read on the ground colliders!
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(HasSpeed))]
public class KeyboardMove : MonoBehaviour
{
	public float jumpStrength = 9;

	public CollisionCounter groundCollisionCounter;

	HasSpeed hasSpeed;
	Rigidbody body;

	void Start ()
	{
		hasSpeed = GetComponent<HasSpeed> ();
		body = GetComponent<Rigidbody> ();
	}

	void FixedUpdate ()
	{
		var v = body.velocity; 
		v.x = Input.GetAxis ("Horizontal") * hasSpeed.speed;
		v.z = Input.GetAxis ("Vertical") * hasSpeed.speed;
   
		if (Input.GetAxisRaw("Jump") != 0 && CanJump) {
			v.y = jumpStrength;
		}
   
		body.velocity = v;
	}

	public bool CanJump {
		get { return groundCollisionCounter == null || groundCollisionCounter.HasAnyCollisions || !body.useGravity; }
	}
}
