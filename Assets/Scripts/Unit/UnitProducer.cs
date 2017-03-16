using UnityEngine;
using System.Collections;

public class UnitProducer : MonoBehaviour {
	[SerializeField]
	private BuyUnitConfig[] buyUnitConfigs;

	public Transform spawnPoint;
	public NavPath path;
	public NavPath.FollowDirection pathDirection;


	public Wallet Wallet {
		get {
			var factionType = FactionManager.GetFactionType (gameObject);
			return CurrencyManager.Instance.GetWallet (factionType);
		}
	}

	public BuyUnitConfig[] BuyUnitConfigs {
		get { return buyUnitConfigs; }
		private set {
			buyUnitConfigs = value;
		}
	}
	
	public BuyUnitStatus[] BuyUnitStatuses {
		get; 
		private set;
	}

	public BuyUnitStatus[] ResetUnitStatuses() {
		if (buyUnitConfigs == null) {
			buyUnitConfigs = new BuyUnitConfig[0];
		}
		
		BuyUnitStatuses = new BuyUnitStatus[buyUnitConfigs.Length];

		for (int i = 0; i < buyUnitConfigs.Length; ++i) {
			var cfg = buyUnitConfigs[i];

			BuyUnitStatuses[i] = new BuyUnitStatus(this, cfg);
		}

		return BuyUnitStatuses;
	}
	
	
	// Use this for initialization
	void Awake () {
		if (spawnPoint == null) {
			spawnPoint = transform;
		}
		if (path == null || BuyUnitConfigs == null) {
			Debug.LogErrorFormat(this, "Invalid UnitManager is missing SpawnPoint, Path or BuyUnitConfigs.");
			return;
		}

		ResetUnitStatuses ();
	}


	public BuyUnitStatus GetStatus(int index) {
		return BuyUnitStatuses[index];
	}
	
	
	#region Unit Purchasing
	public bool TryBuyUnit(int unitIndex) {
		if (unitIndex < 0 || unitIndex > BuyUnitStatuses.Length) {
			// invalid index
			return false;
		}
		
		// select prefab
		var status = BuyUnitStatuses [unitIndex];
		if (status.CanBuy) {
			status.BuyUnit();
			return true;
		}
		return false;
	}


	internal void ProduceUnit(BuyUnitConfig cfg) {
		var unitPrefab = cfg.UnitPrefab;
		
		// create new unit at current position
		var go = (GameObject)Instantiate(unitPrefab, spawnPoint.position, Quaternion.identity);
		
		// set path
		var pathFollower = go.GetComponent<NavMeshPathFollower>();
		pathFollower.path = path;
		pathFollower.direction = pathDirection;
		
		// add faction
		FactionManager.SetFaction (go, gameObject);

		// add loot via script for now
		var lootSettings = go.AddComponent<HasLoot> ();
		lootSettings.minLootCredits = Mathf.RoundToInt(cfg.CreditCost * 0.9f);
		lootSettings.maxLootCredits = Mathf.RoundToInt(cfg.CreditCost * 1.6f);
	}
	#endregion
}
