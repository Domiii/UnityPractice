using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	public float maxHealth = 100;
	public float health = 100;
	public bool canBeAttacked = true;

	#region Life, Health + Death

	public bool IsAlive {
		get { return health > 0; }
	}

	public void Kill ()
	{
		// no health left after this!
		Damage (health);
	}

	void Die (float damagePoints)
	{
		health = 0;
		canBeAttacked = false;

		// notify other components on this object about its death
		SendMessage ("OnDeath", SendMessageOptions.DontRequireReceiver);

		// destroy on death
		Destroy (gameObject);
	}

	#endregion


	#region Damage

	public void Damage (float damagePoints)
	{
		if (!IsAlive || !canBeAttacked) {
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