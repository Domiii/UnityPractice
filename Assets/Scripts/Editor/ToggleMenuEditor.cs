using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(ToggleMenu))]
public class ToggleMenuEditor : Editor {
	public override void OnInspectorGUI() {
		base.OnInspectorGUI ();

		var menu = (ToggleMenu)target;

		//var levelManager = (LevelManager)target;

		if (GUILayout.Button ("Build Menu")) {
			menu.BuildMenu ();
		}

		if (GUILayout.Button ("Clear Menu")) {
			menu.ClearMenu ();
		}
	}
}
