using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	//public GameObject owner;
	public float speed = 10;
	public float pushPower = 100;
	public bool destroyOnCollision = false;
	public float damageMin;
	public float damageMax;

	bool isDestroyed = false;

	void Start () {
		Destroy (gameObject, 10);		// destroy after at most 10 seconds
	}

	void OnTriggerEnter (Collider col) {
		if (isDestroyed) {
			// do nothing
			return;
		}

		var target = Unit.GetUnit(col.gameObject);
		if (target != null) {
			// when colliding with Unit -> Check if we can attack the Unit
			if (target.CanBeAttacked && FactionManager.AreHostile (gameObject, target.gameObject)) {
				DamageTarget (target);
			}
		}
		//else if (col.gameObject != owner && col.GetComponent<Bullet>() == null && col.GetComponentInParent<Bullet>() == null && destroyOnCollision) {
		else if (!FactionManager.AreAllied (gameObject, col.gameObject)) {
			// hit something that is not an enemy unit -> Destroy anyway
//			print (string.Format("{0} vs. {1} ({2}, {3})", 
//				FactionManager.GetFactionType(gameObject), FactionManager.GetFactionType(col.gameObject),
//				gameObject.name, col.gameObject.name));

			if (pushPower != 0) {
				// push the other object!
				var otherBody = col.gameObject.GetComponent<Rigidbody> ();
				if (otherBody) {
					var targetPos = col.ClosestPointOnBounds (transform.position);
					otherBody.AddForceAtPosition (transform.forward * pushPower, targetPos, ForceMode.Impulse);
				}
			}

			if (destroyOnCollision) {
				// destroy on impact!
				DestroyThis ();
			}
		}
	}

	void DamageTarget (Unit target) {
		// damage the unit!
		//var damageInfo = ObjectManager.Instance.Obtain<DamageInfo> ();
		var damage = Random.Range (damageMin, damageMax);
		target.Damage (damage, FactionManager.GetFactionType(gameObject));
		DestroyThis ();
	}

	void DestroyThis () {
		Destroy (gameObject);
		isDestroyed = true;
	}
}