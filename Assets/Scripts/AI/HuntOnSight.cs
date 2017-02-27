using UnityEngine;
using System.Collections;

/// <summary>
/// Attack given target when close enough; else move and catch up
/// </summary>
[RequireComponent (typeof(UnitAttacker))]
[RequireComponent (typeof(NavMeshMover))]
public class HuntOnSight : MonoBehaviour
{
	UnitAttacker attacker;
	NavMeshMover mover;

	#region Public
	public Unit CurrentTarget {
		get {
			return attacker.CurrentTarget;
		}
	}
	#endregion

	void Awake ()
	{
		attacker = GetComponent<UnitAttacker> ();
		attacker.attackOnSight = true;

		mover = GetComponent<NavMeshMover> ();
		mover.stopMovingAtDestination = false;
	}

	void Update ()
	{
		if (attacker.IsCurrentValid) {
			if (attacker.IsCurrentInRange) {
				// keep attacking; also: make sure, we are not moving
				mover.StopMove ();
			} else {
				// target out of range -> move toward target
				mover.CurrentDestination = attacker.CurrentTarget.transform.position;
				attacker.StopAttack ();
			}
		} else {
			// no valid target
			StopHunting();
		}
	}

	/// <summary>
	/// Called when finished hunting.
	/// </summary>
	void StopHunting ()
	{
		attacker.StopAttack ();
		mover.StopMove ();
	}
}

