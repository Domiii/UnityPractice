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

	void Die (float damagePoints) {
		health = 0;
		isInvulnerable = false;

		// notify other components on this object about its death
		SendMessage ("OnDeath", SendMessageOptions.DontRequireReceiver);

		// destroy on death
		Destroy (gameObject);
	}
	#endregion

	#region Damage
	public void Kill () {
		// no health left after this!
		Damage (health);
	}

	public void Damage (float damagePoints) {
		if (!CanBeAttacked) {
			// cannot be attacked right now
			return;
		}
    
		health -= damagePoints;
    
		if (!IsAlive) {
			// died from damage
			Die (damagePoints);
		}
	}
	#endregion
}


