using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
	public float radius = 10.0F;
	public float power = 100.0F;
	public float minDamage = 50, maxDamage = 100;
	public GameObject explosionPrefab;

	ParticleSystem.MinMaxCurve explSpeedSettings;

	void Start () {
		var particles = explosionPrefab.GetComponent<ParticleSystem> ();
		if (particles) {
			// modify lifetime, so that given the particle's intial speed, it can cover the entire radius
			var main = particles.main;
			explSpeedSettings = main.startSpeed;
			explSpeedSettings = radius / main.startLifetime.constantMax;
		}
	}

	void OnCollisionEnter (Collision other) {
		var isBomb = !!other.gameObject.GetComponent<Bomb> ();
		var isAllied = FactionManager.AreAllied (other.gameObject, gameObject);
		if (!isBomb && !isAllied) {
			Explode ();
			Destroy (gameObject);
		}
	}

	void Explode () {
		if (explosionPrefab) {
			var go = (GameObject)Instantiate (explosionPrefab);
			go.transform.position = transform.position;
			var particles = go.GetComponent<ParticleSystem> ();
			if (particles) {
				var main = particles.main;
				main.startSpeed = explSpeedSettings;
			}
			Destroy (go, 5);
		}

		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere (explosionPos, radius);
		foreach (Collider hit in colliders) {
			// add damage if object is Unit
			var unit = Unit.GetUnit(hit);
			if (unit && FactionManager.AreHostile(unit.gameObject, gameObject)) {
				// get random damage, then scale with distance
				var dmg = Random.Range (minDamage, maxDamage);
				var closestPoint = hit.ClosestPointOnBounds (explosionPos);
				var dist = Vector3.Distance(closestPoint, explosionPos);
				dmg = dmg * (1 - Mathf.Clamp01(dist / radius));

				// apply damage
				unit.Damage (dmg, FactionManager.GetFactionType (gameObject));
			}

			// make object fly!
			var rb = hit.GetComponent<Rigidbody> ();
			if (rb && !rb.GetComponent<Bomb> ()) {
				rb.AddExplosionForce (power, explosionPos, radius, 3.0F);
			}
		}
	}
}
