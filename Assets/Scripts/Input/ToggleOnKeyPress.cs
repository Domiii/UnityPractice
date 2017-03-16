using UnityEngine;


public class ToggleOnKeyPress : MonoBehaviour {
	public KeyCode key;
	public GameObject targetObject;

	void Update() {
		if (Input.GetKeyDown (key)) {
			targetObject.SetActive(!targetObject.activeSelf);
		}
	}
}