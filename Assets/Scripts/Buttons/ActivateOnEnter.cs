/**
 * Activates a GameObject when player enters and deactivates it when player leaves this object
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public class ActivateOnEnter : MonoBehaviour
{
	public GameObject toggledObject;
	Player player;
	ColorMixer colorMixer;

	void Start ()
	{
		colorMixer = GetComponent<ColorMixer> ();
	}

	void OnTriggerEnter (Collider other)
	{
		if (toggledObject != null) {
			return;
		}
		var triggerPlayer = other.GetComponent<Player> ();
		if (triggerPlayer != null) {
			// player entered
			player = triggerPlayer;
			toggledObject.SetActive (true);

			if (colorMixer != null) {
				// update color!
				colorMixer.MixColorWith (player.GetComponent<Renderer> ());
			}
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (toggledObject != null) {
			return;
		}
		var triggerPlayer = other.GetComponent<Player> ();
		if (triggerPlayer != null && triggerPlayer == player) {
			// player left
			toggledObject.SetActive (false);
			player = null;

			if (colorMixer != null) {
				// update color!
				colorMixer.ResetColor ();
			}
		}
	}
}