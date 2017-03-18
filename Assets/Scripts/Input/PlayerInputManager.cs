using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerInputManager : MonoBehaviour {
	public static PlayerInputManager Instance {
		get;
		private set;
	}

	public int nInputBlockers = 0;

	public PlayerInputManager () {
		Instance = this;
	}

	/// <summary>
	/// Player in-game actions through clicks should be ignored if:
	/// (1) player is currently in a menu or otherwise blocking default game input
	/// (2) player pressed a button somewhere on the interface
	/// </summary>
	public bool CanGameReceiveClick {
		get {
			var currentSelectedObj = EventSystem.current.currentSelectedGameObject;
			return IsDefaultGameInputEnabled &&
				(!currentSelectedObj);
		}
	}

	public bool IsDefaultGameInputEnabled {
		get {
			return nInputBlockers == 0;
		}
	}

	public void AddGameInputBlocker () {
		++nInputBlockers;
	}

	public void RemoveGameInputBlocker () {
		--nInputBlockers;
	}
}
