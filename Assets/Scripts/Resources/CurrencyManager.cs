using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour {
	public static CurrencyManager Instance {
		get;
		private set;
	}

	public CurrencyManager() {
		Instance = this;
	}

	public Wallet GetWallet (FactionType factionType) {
		// TODO!
		return null;
	}
}
