using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public class NoGravityZone : MonoBehaviour
{
	Collider coll;
	bool originalGravity;
	float originalDrag;

	void Start ()
	{
		coll = GetComponent<Collider> ();
		coll.isTrigger = true;
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.attachedRigidbody) {
			originalGravity = other.attachedRigidbody.useGravity;
			originalDrag = other.attachedRigidbody.drag;

			other.attachedRigidbody.useGravity = false;
			other.attachedRigidbody.drag = 1;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.attachedRigidbody) {
			other.attachedRigidbody.useGravity = originalGravity;
			other.attachedRigidbody.drag = originalDrag;
		}
	}
}
