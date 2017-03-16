using UnityEngine;
using UnityEditor;


/// <summary>
/// Allows editing a scene property through inspector.
/// Works in collaboration with SceneField.
/// see: http://answers.unity3d.com/questions/242794/inspector-field-for-scene-asset.html
/// </summary>
[CustomPropertyDrawer (typeof(SceneField))]
public class SceneFieldPropertyDrawer : PropertyDrawer {
	public override void OnGUI (Rect _position, SerializedProperty _property, GUIContent _label) {
		EditorGUI.BeginProperty (_position, GUIContent.none, _property);
		SerializedProperty sceneAsset = _property.FindPropertyRelative ("m_SceneAsset");
		SerializedProperty sceneName = _property.FindPropertyRelative ("m_SceneName");
		_position = EditorGUI.PrefixLabel (_position, GUIUtility.GetControlID (FocusType.Passive), _label);
		if (sceneAsset != null) {
			EditorGUI.BeginChangeCheck ();

			Object value = EditorGUI.ObjectField (_position, sceneAsset.objectReferenceValue, typeof(SceneAsset), false);
			if (EditorGUI.EndChangeCheck ()) {
				sceneAsset.objectReferenceValue = value;
				if (sceneAsset.objectReferenceValue != null) {
					sceneName.stringValue = (sceneAsset.objectReferenceValue as SceneAsset).name;
				}
			}

		}
		EditorGUI.EndProperty ();
	}
}