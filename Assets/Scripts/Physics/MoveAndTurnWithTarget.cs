using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndTurnWithTarget : MonoBehaviour
{
	public Rigidbody target;
	public float turnSmoothness = 6;
	//public float followSmoothness = 0.2f;

	Vector3 offset;

	void Start ()
	{
		offset = transform.position - target.transform.position;
	}

	void FixedUpdate ()
	{
		if (target != null) {
			UpdateDirection ();
			UpdatePosition ();
		}
	}

	void UpdateDirection ()
	{
		// slerp = spherical linear interpolation (linear interpolation of a rotation along an ARC)
		// see: https://www.youtube.com/watch?v=uNHIPVOnt-Y
		transform.forward = Vector3.Slerp (transform.forward, target.transform.forward, Time.fixedDeltaTime * turnSmoothness);
	}

	void UpdatePosition ()
	{
		transform.position = target.transform.position + offset;
	}
}
