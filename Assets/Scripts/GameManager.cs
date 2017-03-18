using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public void IncreaseGameSpeed(float amount = 0.2f) {
		Time.timeScale += amount;
	}

	public void DecreaseGameSpeed(float amount = 0.2f) {
		Time.timeScale -= amount;
	}
}
