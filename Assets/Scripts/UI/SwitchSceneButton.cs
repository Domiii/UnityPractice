using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class SwitchSceneButton : MonoBehaviour {
	public SceneField scene;

	void Start () {
		var btn = GetComponent<Button> ();
		btn.onClick.AddListener(LoadScene);
	}

	void LoadScene() {
		SceneManager.LoadScene (scene.SceneName);
	}
}
