/**
 * Activates and deactivates a GameObject when player enters object
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public class ToggleOnEnterExit : MonoBehaviour
{
	public GameObject toggledObject;
	ColorMixer colorMixer;
	bool startState;

	void Start ()
	{
		colorMixer = GetComponent<ColorMixer> ();
		startState = toggledObject.activeSelf;
	}

	void OnTriggerEnter (Collider other)
	{
		var triggerPlayer = other.GetComponent<Player> ();
		if (triggerPlayer != null && toggledObject != null) {
			// player entered
			toggledObject.SetActive (!toggledObject.activeSelf);

			UpdateColor (triggerPlayer);
		}
	}

	void UpdateColor (Player triggerPlayer)
	{
		if (colorMixer != null) {
			if (toggledObject.activeSelf != startState) {
				// player changed state
				colorMixer.MixColorWith (triggerPlayer.GetComponent<Renderer> ());
			} else {
				// reset
				colorMixer.ResetColor ();
			}
		}
	}
}