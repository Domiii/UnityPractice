using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainMaker : MonoBehaviour
{
	public Rigidbody rainDropPrefab;
	public float rainDropLifeTime = 2;
	new Collider collider;

	void Start ()
	{
		collider = GetComponent<Collider> ();
		collider.isTrigger = true;  // isTrigger = false makes no sense here
	}

	void Update ()
	{
		DropOne ();
	}

	void DropOne ()
	{
		var min = collider.bounds.min;
		var max = collider.bounds.max;
		var pos = new Vector3 (Random.Range (min.x, max.x), Random.Range (min.y, max.y), Random.Range (min.z, max.z));
		var drop = Instantiate (rainDropPrefab, pos, Quaternion.identity);
		Destroy (drop.gameObject, rainDropLifeTime);
	}
}
