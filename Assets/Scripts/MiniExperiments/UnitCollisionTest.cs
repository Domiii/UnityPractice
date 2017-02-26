using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public class UnitCollisionTest : MonoBehaviour
{
	void OnTriggerEnter (Collider other)
	{
		var triggerer = other.gameObject.GetComponent<Unit> ();
		if (triggerer != null) {
			print ("Unit entered: " + triggerer.name);
		}
	}

	void OnTriggerExit (Collider other)
	{
		var triggerer = other.gameObject.GetComponent<Unit> ();
		if (triggerer != null) {
			print ("Unit exited: " + triggerer.name);
		}
	}
}