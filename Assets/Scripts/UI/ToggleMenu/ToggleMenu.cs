using UnityEngine;
using UnityEngine.UI;
using System.Collections;


/// <summary>
/// The ToggleMenu automatically generates buttons for each child object in the given container. 
/// When pressed, the buttons enable and disable those objects.
/// </summary>
public class ToggleMenu : MenuBuilder<ToggleButton> {
	public GameObject toggleObjectsContainer;

	void Start() {
		BuildMenu ();
	}

	public void BuildMenu() {
		var t = toggleObjectsContainer.transform;
		var toggleObjects = new GameObject[t.childCount];
		for (var i = 0; i < toggleObjects.Length; ++i) {
			var child = t.GetChild (i);
			toggleObjects[i] = child.gameObject;
		}

		BuildMenu (toggleObjects, DecorateButton);
	}

	void DecorateButton(ToggleButton btn, GameObject obj) {
		btn.toggleObject = obj;

		var text = btn.GetComponentInChildren<Text> ();
		text.text = obj.name;
	}
}
