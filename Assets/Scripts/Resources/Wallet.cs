using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Wallets contain money.
/// </summary>
/// [ExecuteInEditMode]
public class Wallet : MonoBehaviour {
	[SerializeField]
	private int _credits = 100;

	public int Credits {
		get { return _credits; }
		set {
			_credits = value;
			UpdateText ();
		}
	}

	// Use this for initialization
	void Awake () {
		UpdateText ();
	}

	public void GainCredits (int credits) {
		Credits += credits;
	}

	public void DeductCredits (int credits) {
		Credits -= credits;
	}

	#region UI
	public Text creditText;

	public void UpdateText () {
		if (creditText != null) {
			creditText.text = Credits.ToString ();
		}
	}
	#endregion
}
