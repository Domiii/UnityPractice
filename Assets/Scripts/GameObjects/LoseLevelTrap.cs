using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class LoseLevelTrap : MonoBehaviour {
  void OnTriggerEnter(Collider other) {
    var triggerPlayer = other.GetComponent<Player> ();
    if (triggerPlayer != null) {
      // player entered! (-> reset scene)
      print("You lose! :(");

      Scene scene = SceneManager.GetActiveScene(); 
      SceneManager.LoadScene(scene.name);
    }
  }
}