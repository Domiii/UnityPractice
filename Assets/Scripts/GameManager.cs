using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public float maxTimeScale = 20;

	public void IncreaseGameSpeed(float amount = 0.2f) {
		Time.timeScale = Mathf.Clamp(Time.timeScale + amount, 0, maxTimeScale);
	}

	public void DecreaseGameSpeed(float amount = 0.2f) {
		Time.timeScale = Mathf.Clamp(Time.timeScale - amount, 0, maxTimeScale);
	}
}
