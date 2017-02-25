using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountingTimer : MonoBehaviour {
  public float secondsSinceStart1;
  public float secondsSinceStart2;

  void Update () {
    secondsSinceStart1 += Time.deltaTime;
  }

  void FixedUpdate () {
    secondsSinceStart2 += Time.fixedDeltaTime;
  }
}