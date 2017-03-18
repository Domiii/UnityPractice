using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public class DeathTrap : MonoBehaviour {
	void OnTriggerEnter (Collider other) {
		var triggerer = Unit.GetUnit(other.gameObject);
		if (triggerer != null) {
			// unit entered! (-> Death)
			triggerer.Kill (FactionManager.GetFactionType (gameObject));
		}
	}
}