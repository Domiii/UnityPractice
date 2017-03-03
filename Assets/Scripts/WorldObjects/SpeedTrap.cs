using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SpeedTrap : MonoBehaviour {
  public float speedFactor = 0.5f;

  void OnTriggerEnter(Collider other) {
    var triggerer = other.GetComponent<HasSpeed> ();
    if (triggerer != null) {
      // someone entered!
      triggerer.speed *= speedFactor;
    }
  }

  void OnTriggerExit(Collider other) {
    var triggerer = other.GetComponent<HasSpeed> ();
    if (triggerer != null) {
      // someone exited!
      triggerer.speed /= speedFactor;
    }
  }
}