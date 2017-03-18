using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAutoShooter : MonoBehaviour {
	public Rigidbody bulletPrefab;
	public Transform bulletStartPlace;
	public float cooldown = 1.0f;
	public float minSpeed = 10, maxSpeed = 15;

	void OnEnable () {
		InvokeRepeating ("Shoot", cooldown, cooldown);
	}

	void Shoot () {
		if (!gameObject.activeSelf) {
			// stop shooting when inactive
			CancelInvoke ("Shoot");
		} else {
			// create new bullet
			var body = Instantiate<Rigidbody> (bulletPrefab, bulletStartPlace.position, bulletStartPlace.rotation);

			// set faction
			FactionManager.SetFaction (body.gameObject, gameObject);

			// set its velocity
			var speed = Random.Range(minSpeed, maxSpeed);
			body.velocity = bulletStartPlace.transform.forward * speed;
		}
	}
}
