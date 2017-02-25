using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AnyCollisionTest : MonoBehaviour {
  void OnCollisionEnter(Collision other) {
    print ("Enter: " + other.gameObject.name);
  }
   
  void OnCollisionExit(Collision other) {
    print ("Exit: " + other.gameObject.name);
  }
}