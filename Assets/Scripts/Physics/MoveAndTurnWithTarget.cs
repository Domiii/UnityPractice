using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Step #1: Add an empty object (a "mount") into the same position as target object.
/// Step #2: Then add this component to the "mount" object. Assign target object to target variable.
/// Step #3: Add any objects you want it to follow (e.g. camera) as children to "mount".
/// Done!
/// 
/// The trick: Since the mount is positioned directly on the target, we will always pivot around the target.
/// </summary>
public class MoveAndTurnWithTarget : MonoBehaviour {
	public Rigidbody target;
	public float turnSmoothness = 6;
	//public float followSmoothness = 0.2f;

	Vector3 offset;

	void Start () {
		offset = transform.position - target.transform.position;
	}

	void FixedUpdate () {
		if (target != null) {
			UpdateDirection ();
			UpdatePosition ();
		}
	}

	void UpdateDirection () {
		// slerp = spherical linear interpolation (linear interpolation of a rotation along an ARC)
		// see: https://www.youtube.com/watch?v=uNHIPVOnt-Y
		transform.forward = Vector3.Slerp (transform.forward, target.transform.forward, Time.fixedDeltaTime * turnSmoothness);
	}

	void UpdatePosition () {
		transform.position = target.transform.position + offset;
	}
}
