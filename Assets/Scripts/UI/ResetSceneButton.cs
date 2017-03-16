using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class ResetSceneButton : MonoBehaviour {
	void Start () {
		var btn = GetComponent<Button> ();

		btn.onClick.AddListener(ResetScene);
	}

	void ResetScene() {
		var scene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (scene.name);
	}
}
