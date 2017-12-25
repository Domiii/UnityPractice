using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(LevelMenu))]
public class LevelMenuEditor : Editor {
	public override void OnInspectorGUI() {
		base.OnInspectorGUI ();

		var menu = (LevelMenu)target;


		if (GUILayout.Button ("Build Menu")) {
			LevelManagerEditor.ResetLevelList ();
			menu.BuildMenu ();
		}

		if (GUILayout.Button ("Clear Menu")) {
			menu.ClearMenu ();
		}
	}
}