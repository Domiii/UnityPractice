using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDMovement : MonoBehaviour
{
	public float speed;

	void FixedUpdate ()
	{
		var move = Vector3.zero;

		move.x = Input.GetAxis ("Horizontal");
		move.z = Input.GetAxis ("Vertical");
	}
}
