using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	public float radius = 10.0F;
	public float power = 100.0F;

	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.GetComponent<Bomb> () == null) {
			Explode ();
			Destroy (gameObject);
		}
	}

	void Explode ()
	{
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere (explosionPos, radius);
		foreach (Collider hit in colliders) {
			Rigidbody rb = hit.GetComponent<Rigidbody> ();

			if (rb != null && rb.GetComponent<Bomb> () == null) {
				// TODO: add damage here...
				rb.AddExplosionForce (power, explosionPos, radius, 3.0F);
			}

		}
	}
}
