using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Feet are currently only used for objects that are supposed to be added to platforms.
/// </summary>
public class Feet : MonoBehaviour {
	public Transform owner;

	void Start() {
		if (!owner) {
			owner = transform;
		}
	}
}
