using UnityEngine;
using System.Collections;

[RequireComponent (typeof(UnitProducer))]
public class UnitSpawnAI : MonoBehaviour {
	UnitProducer unitProducer;
	int nextChoice;
	float lastPurchaseTime;

	float TimeSinceLastPurchase {
		get {
			return Time.time - lastPurchaseTime;
		}
	}

	void Awake () {
		unitProducer = GetComponent<UnitProducer> ();

		// initialize
		lastPurchaseTime = Time.time;
		PredictNextChoice ();
	}

	void Update () {
		// try buying our current target unit of choice
		if (unitProducer.TryBuyUnit (nextChoice)) {
			//Debug.Log("AI bought #" + nextChoice);

			PredictNextChoice ();
			lastPurchaseTime = Time.time;
		}
		//else if (TimeSinceLastPurchase > ...) {
		// waited too long
		// PredictNextChoice ();
		//}
	}

	void PredictNextChoice () {
		nextChoice = Random.Range (0, unitProducer.BuyUnitConfigs.Length);
	}
}