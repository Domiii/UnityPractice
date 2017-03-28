using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Platform))]
public class PlatformEditor : Editor {
	public override void OnInspectorGUI() {
		base.OnInspectorGUI ();

		var platform = (Platform)target;


		if (GUILayout.Button ("Restart")) {
			platform.RestartPath ();
		}
	}
}