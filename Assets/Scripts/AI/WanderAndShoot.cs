using UnityEngine;
using System.Collections;

[RequireComponent (typeof(RandomWander))]
[RequireComponent (typeof(UnitAttacker))]
public class WanderAndShoot : MonoBehaviour
{
	UnitAttacker attacker;
	RandomWander wander;

	void Awake ()
	{
		attacker = GetComponent<UnitAttacker> ();
		attacker.attackOnSight = true;

		wander = GetComponent<RandomWander> ();
	}

	void Update ()
	{
		if (attacker.IsCurrentValid && attacker.IsCurrentInRange) {
			// Don't move while attacker is attacking
			wander.StopMove ();
		} else if (!wander.IsMoving) {
			// target out of range -> keep wandering
			wander.MoveIntoRandomDirection();
		}
	}
}
