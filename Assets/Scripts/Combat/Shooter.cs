﻿using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Shooter is used by other components to shoot stuff.
/// Make sure to setup it's weapon first (especially the bulletPrefab)!
/// </summary>
public class Shooter : MonoBehaviour {
	public WeaponConfig weapon = new WeaponConfig ();
	public float turnSpeed = 1000.0f;
	public Transform shootTransform;

	Vector3 currentTarget;
	float lastShotTime = -10000;
	bool isAttacking;

	public bool IsAttacking {
		get {
			return isAttacking;
		}
	}

	public void StartShootingAt (Vector3 target) {
		currentTarget = target;
		isAttacking = true;
	}

	public void StopShooting () {
		isAttacking = false;
	}


	public void ShootAt (Transform t) {
		ShootAt (t.position);
	}

	public void ShootAt (Vector3 target) {
		if (weapon == null || weapon.bulletPrefab == null) {
			return;
		}

		if (weapon.bulletCount > 1) {
			// shoot N bullets in a cone from -coneAngle/2 to +coneAngle/2
			var dir = target - transform.position;
			dir.Normalize ();
			dir = Quaternion.Euler (0, -weapon.ConeAngle / 2, 0) * dir;

			var deltaAngle = weapon.ConeAngle / (weapon.bulletCount - 1);

			for (var i = 0; i < weapon.bulletCount; ++i) {
				//dir.Normalize ();
				ShootBullet (dir);
				dir = Quaternion.Euler (0, deltaAngle, 0) * dir;

			}
		} else {
			// just one bullet straight forward
			ShootBullet (shootTransform.forward);
		}

		// reset shoot time
		lastShotTime = Time.time;
	}

	void Awake () {
		if (weapon == null) {
			print ("Shooter is missing Weapon");
			return;
		}
		if (weapon.bulletPrefab == null) {
			print ("Shooter's Weapon is missing Bullet Prefab");
			return;
		}

		if (shootTransform == null) {
			shootTransform = transform;
		}
		isAttacking = false;
	}

	void Update () {
		if (isAttacking) {
			// some debug stuff
			var dir = currentTarget - transform.position;
			Debug.DrawRay (transform.position, dir);

			// rotate toward target
			RotateTowardTarget ();

			// keep shooting
			var delay = Time.time - lastShotTime;
			if (delay < weapon.attackDelay) {
				// still on cooldown
				return;
			}
			ShootAt (currentTarget);
		}
	}

	Quaternion GetRotationToward (Vector3 target) {
		Vector3 dir = target - shootTransform.position;
		return GetRotationFromDirection (dir);
	}

	Quaternion GetRotationFromDirection (Vector3 dir) {
		var angle = Mathf.Atan2 (dir.x, dir.z) * Mathf.Rad2Deg;
		return Quaternion.AngleAxis (angle, Vector3.up);
	}

	void RotateTowardTarget () {
		var rigidbody = GetComponent<Rigidbody> ();
		if (rigidbody != null && (rigidbody.constraints & RigidbodyConstraints.FreezePositionY) != 0) {
			// don't rotate if rotation has been constrained
			return;
		}

		var agent = GetComponent<NavMeshAgent> ();
		if (agent != null) {
			turnSpeed = agent.angularSpeed;
		}
		var targetRotation = GetRotationToward (currentTarget);
		transform.rotation = Quaternion.RotateTowards (transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
	}

	void ShootBullet (Vector3 dir) {
		// create a new bullet
		var bullet = (Bullet)Instantiate (weapon.bulletPrefab, shootTransform.position, GetRotationFromDirection (dir));

		// set bullet faction
		FactionManager.SetFaction (bullet.gameObject, gameObject);

		// make sure, we always shoot horizontally
		dir.y = 0;

		// set velocity
		var rigidbody = bullet.GetComponent<Rigidbody> ();
		rigidbody.velocity = dir * bullet.speed;
		bullet.damageMin = weapon.damageMin;
		bullet.damageMax = weapon.damageMax;
	}

	void OnDeath (DamageInfo damageInfo) {
		enabled = false;
	}
}