using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// The LevelMenu automatically generates buttons for all levels registered in the LevelManager.
/// </summary>
public class LevelMenu : MenuBuilder<LevelButton> {
	public void BuildMenu() {
		BuildMenu (LevelManager.Instance.levels, DecorateButton);
	}

	void DecorateButton (LevelButton btn, string levelName) {
		var text = btn.GetComponentInChildren<Text> ();
		text.text = levelName;
		btn.level = levelName;
	}
}
