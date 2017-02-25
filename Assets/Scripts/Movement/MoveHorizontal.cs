using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class MoveHorizontal : MonoBehaviour
{
	public float SpeedX, SpeedZ;
	Rigidbody body;

	void Start ()
	{
		body = GetComponent<Rigidbody> ();
	}

	void FixedUpdate ()
	{
		body.velocity = new Vector3 (SpeedX, body.velocity.y, SpeedZ);
	}
}
