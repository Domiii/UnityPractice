using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

/// <summary>
/// This is a singleton class: Only one object of it ever exists.
/// You can add it to an empty GameObject called "LevelManager".
/// The levels array contains the names of all level scenes.
/// Make sure to also add them to the build settings before building.
/// 
/// Usually the LevelManager is used by the LevelMenu to switch between levels.
/// The LevelManager also uses PlayerPrefs to store level progress to file.
/// Furthermore, it offers methods to display a canvas when a level has been won or lost.
/// 
/// Once created, the LevelManagerEditor will provide an option to automatically add all
/// scenes that have a name starting with levelPrefix to the levels array.
/// </summary>
public class LevelManager : MonoBehaviour {
	public string mainMenuScene = "MainMenu";
	public string[] levels;
	public Canvas wonDisplay;
	public Canvas lostDisplay;
	public string levelPrefix = "level";

	public static LevelManager Instance {
		get;
		private set;
	}

	LevelManager () {
		Instance = this;
	}

	public string CurrentSceneName {
		get { return SceneManager.GetActiveScene ().name; }
	}

	void Start () {
		OnLevelStart ();
	}

	string GetLevelCompletedKey (string level) {
		return "level__" + level;
	}

	bool HasAlreadyCompletedLevel (string name) {
		return PlayerPrefs.GetInt (GetLevelCompletedKey (name), 0) > 0;
	}

	void SetLevelCompleted (string name, bool completed) {
		PlayerPrefs.SetInt (GetLevelCompletedKey (name), completed ? 1 : 0);
		PlayerPrefs.Save ();
	}

	void OnLevelStart () {
		if (wonDisplay != null) {
			wonDisplay.gameObject.SetActive (false);
			lostDisplay.gameObject.SetActive (false);
		}
		//GameManager.Instance.IsPaused = false;
	}

	public int GetLevelIndex (string name) {
		return System.Array.IndexOf (levels, name);
	}

	public bool IsLevelUnlocked (string name) {
		// level is unlocked if it's the first level, or the previous level has already been completed
		var previousLevel = GetPreviousLevel (name);
		return previousLevel == null || HasAlreadyCompletedLevel (previousLevel);
	}

	public void ResetPlayerData () {
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.Save ();

		RestartCurrentScene ();
	}

	public void NotifyLevelWon () {
		//GameManager.Instance.IsPaused = true;
		SetLevelCompleted (CurrentSceneName, true);
		wonDisplay.gameObject.SetActive (true);
	}

	public void NotifyLevelLost () {
		//GameManager.Instance.IsPaused = true;
		lostDisplay.gameObject.SetActive (true);
	}

	public void RestartCurrentScene () {
		GotoLevel (CurrentSceneName);
	}

	public void GotoNextLevel () {
		var level = GetNextLevel ();
		if (level != null) {
			GotoLevel (level);
		}
	}

	public void GotoMainMenu () {
		GotoLevel (mainMenuScene);
	}

	public string GetPreviousLevel (string name) {
		var index = GetLevelIndex (name) - 1;
		if (index >= 0 && index < levels.Length) {
			// go to next level!
			return levels [index];
		} else {
			// there are no more levels!
			return null;
		}
	}

	public string GetNextLevel () {
		var index = GetLevelIndex (CurrentSceneName) + 1;
		if (index > 0 && index < levels.Length) {
			// go to next level!
			return levels [index];
		} else {
			// there are no more levels!
			return null;
		}
	}

	public void GotoLevel (string level) {
		//OnLevelStart ();
		SceneManager.LoadScene (level);
	}
}
