using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Shooter))]
public class UnitAttacker : MonoBehaviour {
	public float attackRadius = 10.0f;
	public bool attackOnSight = false;

	Unit currentTarget;
	Shooter shooter;
	Collider[] collidersInRange;

	void Awake () {
		shooter = GetComponent<Shooter> ();
	}

	void Update () {
		if (attackOnSight) {
			EnsureTarget ();
		}
		KeepAttackingCurrentTarget ();
	}

	void OnDeath () {
		enabled = false;
	}


	#region Public
	public Unit CurrentTarget {
		get {
			return currentTarget;
		}
	}

	public bool CanAttackCurrentTarget {
		get { return IsCurrentValid && IsCurrentInRange; }
	}

	public bool IsCurrentValid {
		get {
			return currentTarget != null && IsValidTarget (currentTarget);
		}
	}

	public bool IsCurrentInRange {
		get {
			return currentTarget != null && IsInRange (currentTarget);
		}
	}

	public bool IsInRange (Unit target) {
		var dist = (target.transform.position - transform.position).sqrMagnitude;
		return dist <= attackRadius * attackRadius;
	}

	public bool IsValidTarget (Unit target) {
		return target.CanBeAttacked && FactionManager.AreHostile (gameObject, target.gameObject);
	}

	public bool CanAttack (Unit target) {
		return IsInRange (target) && IsValidTarget (target);
	}

	bool KeepAttackingCurrentTarget () {
		if (CanAttackCurrentTarget) {
			shooter.StartShootingAt (currentTarget.transform.position);
			return true;
		}
		StopAttack ();
		return false;
	}

	public bool StartAttack (Unit target) {
		if (CanAttackCurrentTarget) {
			StopAttack ();
		}

		currentTarget = target;
		if (CanAttackCurrentTarget) {
			shooter.StartShootingAt (target.transform.position);
			return true;
		}
		return false;
	}

	public void StopAttack () {
		shooter.StopShooting ();
	}
	#endregion


	#region Finding Targets
	public bool EnsureTarget () {
		// #1 keep attacking previous target.
		// #2 if currently has no target: look for new target to attack
		if (!CanAttackCurrentTarget && !FindNewTarget ()) {
			// could not find a valid target -> Stop
			StopAttack ();
			return false;
		}
		return true;
	}

	public bool FindNewTarget () {
		// find new target
		var target = FindTarget ();

		if (target != null) {
			return StartAttack (target);
		}
		return false;
	}

	Unit FindTarget () {
		if (collidersInRange == null) {
			collidersInRange = new Collider[128];
		}
		var nResults = Physics.OverlapSphereNonAlloc (transform.position, attackRadius, collidersInRange);
		for (var i = 0; i < nResults; ++i) {
			var collider = collidersInRange [i];
			var unit = collider.GetComponent<Unit> ();
			if (unit != null && IsValidTarget (unit)) {
				return unit;
			}
		}

		// no valid target found
		return null;
	}
	#endregion
}