using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public class SpeedPickup : MonoBehaviour
{
	public float speedFactor = 2;

	void OnTriggerEnter (Collider other)
	{
		var triggerer = other.GetComponent<HasSpeed> ();
		if (triggerer != null) {
			// someone picked it up!
			triggerer.speed *= speedFactor;
			Destroy (gameObject);
		}
	}
}