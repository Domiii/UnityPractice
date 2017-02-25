using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class UnitCollisionTest : MonoBehaviour {
  void OnTriggerEnter(Collider other) {
    var unit = other.gameObject.GetComponent<Unit>();
    if (player != null) {
      print ("Unit entered: " + unit.name);
    }
  }
  
  void OnTriggerEnter(Collider other) {
    var unit = other.gameObject.GetComponent<Unit>();
    if (player != null) {
      print ("Unit exited: " + unit.name);
    }
  }
}