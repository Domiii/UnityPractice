using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows editing a scene property through inspector.
/// Works in collaboration with SceneFieldPropertyDrawer.
/// see: http://answers.unity3d.com/questions/242794/inspector-field-for-scene-asset.html
/// </summary>
[System.Serializable]
public class SceneField {
	[SerializeField]
	private Object m_SceneAsset;
	[SerializeField]
	private string m_SceneName = "";

	public SceneField() {}

	public SceneField(Object sceneAsset, string sceneName) {
		m_SceneAsset = sceneAsset;
		m_SceneName = sceneName;
	}

	public string SceneName {
		get { return m_SceneName; }
	}

	// makes it work with the existing Unity methods (LoadLevel/LoadScene)
	public static implicit operator string (SceneField sceneField) {
		return sceneField.SceneName;
	}

	public override string ToString () {
		return SceneName;
	}
}