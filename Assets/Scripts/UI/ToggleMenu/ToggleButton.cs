using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class ToggleButton : MonoBehaviour {
	public GameObject toggleObject;
	public Color activeColor = Color.green;
	public Color inactiveColor = Color.red;

	Button button;
	Image image;

	void Start() {
		Reset();

		button = GetComponent<Button> ();
		image = GetComponent<Image> ();
		UpdateColor ();
	}

	void Reset() {
		var button = GetComponent<Button> ();
		button.interactable = true;
		button.onClick.AddListener(DoToggle);
	}

	void DoToggle() {
		toggleObject.SetActive(!toggleObject.activeSelf);

		UpdateColor ();
	}

	void UpdateColor() {
		var colors = button.colors;
		colors.normalColor = toggleObject.activeSelf ? activeColor : inactiveColor;
		button.colors = colors;

		if (image) {
			image.color = colors.normalColor;
		}
	}
}
