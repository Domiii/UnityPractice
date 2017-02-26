using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Shooter))]
public class ShootInDirection : MonoBehaviour
{
	public Vector3 destination;
	Shooter shooter;

	void Awake ()
	{
		shooter = GetComponent<Shooter> ();
	}

	void Update ()
	{
		shooter.StartShootingAt (destination);
	}
}