using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAutoShooter : MonoBehaviour
{
	public Rigidbody bulletPrefab;
	public Transform bulletStartPlace;
	public float cooldown = 1.0f;
	public float speed = 10;

	void OnEnable ()
	{
		InvokeRepeating ("Shoot", cooldown, cooldown);
	}

	void Shoot ()
	{
		if (!gameObject.activeSelf) {
			// stop shooting when inactive
			CancelInvoke ("Shoot");
		} else {
			// create new bullet
			var body = Instantiate<Rigidbody>(bulletPrefab, bulletStartPlace.position, bulletStartPlace.rotation);

			// set its velocity
			body.velocity = bulletStartPlace.transform.forward * speed;
		}
	}
}
