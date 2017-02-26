/**
 * This "PickUp" requires a "Player" with a "HasSpeed" component to enter and
 * confirm with key press before being picked up.
 * 
 * NOTE: This component only works for players (AI or other units cannot confirm keys!)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickupWConfirmation : MonoBehaviour
{
	// player speed multiplier when picked up
	public float speedFactor = 2;
	// confirmNotice 確認通知
	public GameObject confirmNotice;

	Player player;

	void Update ()
	{
		if (player != null && Input.GetKeyDown (KeyCode.E)) {
			player.GetComponent<HasSpeed> ().speed *= speedFactor;
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter (Collider other)
	{
		var triggerer = other.GetComponent<Player> ();
		if (triggerer != null) {
			// player entered the PickUp
			player = triggerer;
			confirmNotice.SetActive (true);
		}
	}

	void OnTriggerExit (Collider other)
	{
		var triggerer = other.GetComponent<Player> ();
		if (triggerer != null && triggerer == player) {
			confirmNotice.SetActive (false);
			player = null;
		}
	}
}
