using UnityEngine;
using System.Collections;


public class BuyUnitStatus {
	float lastBuyTime;
	UnitProducer unitProducer;

	public BuyUnitConfig Config {
		get;
		private set;
	}

	public BuyUnitStatus (UnitProducer unitManager, BuyUnitConfig cfg) {
		lastBuyTime = Time.time;
		this.unitProducer = unitManager;
		Config = cfg;
	}

	public float SecondsSinceLastBuy {
		get { return Time.time - lastBuyTime; }
	}

	public bool IsReady {
		get { return SecondsSinceLastBuy >= Config.CooldownSeconds; }
	}

	public bool HasSufficientFunds {
		get { 
			if (unitProducer.Wallet != null) {
				return unitProducer.Wallet.Credits >= Config.CreditCost;
			}
			return true;
		}
	}

	public bool CanBuy {
		get { return IsReady && HasSufficientFunds; }
	}

	public bool TryBuyUnit () {
		if (CanBuy) {
			BuyUnit ();
			return true;
		}
		return false;
	}

	public void BuyUnit () {
		lastBuyTime = Time.time;
		if (unitProducer.Wallet != null) {
			unitProducer.Wallet.DeductCredits (Config.CreditCost);
		}
		unitProducer.ProduceUnit (Config);
	}
}
