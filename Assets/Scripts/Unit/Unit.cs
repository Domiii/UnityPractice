using UnityEngine;

/// <summary>
/// Unit gives objects life (and death!).
/// In our test scene, the Shooter component is doing a lot of damage.
/// We can also pair Unit with a Healthbar object to visualize it’s current health.
/// </summary>
public class Unit : MonoBehaviour {
	public float maxHealth = 100;
	public float health = 100;
	public bool isInvulnerable = false;

	#region Life, Health + Death
	public bool IsAlive {
		get { return health > 0; }
	}

	public bool CanBeAttacked {
		get {
			return IsAlive && !isInvulnerable;
		}
	}

	void Die (DamageInfo damageInfo) {
		health = 0;
		isInvulnerable = false;

		// notify other components on this object about its death
		SendMessage ("OnDeath", damageInfo, SendMessageOptions.DontRequireReceiver);

		// destroy on death
		Destroy (gameObject);
	}
	#endregion

	#region Damage
	public void Kill (FactionType sourceFactionType) {
		if (CanBeAttacked) {
			// no health left after this!
			Die (new DamageInfo { damage = health, sourceFactionType = sourceFactionType });
		}
	}

	public void Damage (float damagePoints, FactionType sourceFactionType) {
		Damage (new DamageInfo { damage = damagePoints, sourceFactionType = sourceFactionType });
	}

	public void Damage (DamageInfo damageInfo) {
		if (!CanBeAttacked) {
			// cannot be attacked right now
			return;
		}
    
		health -= damageInfo.damage;
    
		if (!IsAlive) {
			// died from damage
			Die (damageInfo);
		}
	}
	#endregion


	public static Unit GetUnit<C>(C component) 
		where C : Component
	{
		return GetUnit (component.gameObject);
	}
		
	/// <summary>
	/// Helper method to get the Unit component of the given GO or its ancestors.
	/// </summary>
	public static Unit GetUnit(GameObject go) {
		// check if the given object is a Unit
		var unit = go.GetComponent<Unit>();
		if (unit == null && go.transform.parent != null) {
			// check faction type of parent (and recurse through all ancestors)
			unit = GetUnit(go.transform.parent.gameObject);
		}
		return unit;
	}
}


