using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HasLoot : MonoBehaviour {
	public int minLootCredits, maxLootCredits;
	private Text lootText;

	void OnDeath (DamageInfo damageInfo) {
		var faction = CurrencyManager.Instance.GetWallet (damageInfo.sourceFactionType);
		if (faction != null) {
			// give credits to killer
			var lootCredits = Random.Range (minLootCredits, maxLootCredits);
			faction.GainCredits (lootCredits);
		}
	}
}
