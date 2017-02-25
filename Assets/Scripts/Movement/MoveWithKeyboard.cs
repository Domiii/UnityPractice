/**
 * Assign a CollisionCounter (ideally placed in "feet") to get a read on the ground colliders!
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(HasSpeed))]
public class MoveWithKeyboard : MonoBehaviour
{
	public float jumpStrength = 9;
	public CollisionCounter groundCollisionCounter;

	HasSpeed hasSpeed;

	void Start ()
	{
		hasSpeed = GetComponent<HasSpeed> ();
	}

	void Update ()
	{
		var body = GetComponent<Rigidbody> ();
		var v = body.velocity; 
		v.x = Input.GetAxis ("Horizontal") * hasSpeed.speed;
		v.z = Input.GetAxis ("Vertical") * hasSpeed.speed;
   
		if (Input.GetKeyDown (KeyCode.Space) && CanJump) {
			v.y = jumpStrength;
		}
   
		body.velocity = v;
	}

	public bool CanJump {
		get { return groundCollisionCounter == null || !groundCollisionCounter.HasAnyCollisions; }
	}
}
