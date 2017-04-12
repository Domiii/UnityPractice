using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A platform transports any root object that has feet.
/// </summary>
public class Platform : MonoBehaviour {
	public float speed = 10;
	public float waitAtNodeTime = 3;
	public NavPath path;
	public NavPath.FollowDirection direction;
	public NavPath.RepeatMode mode = NavPath.RepeatMode.Repeat;

	IEnumerator<Transform> pathIterator;
	bool waiting = false;


	#region Public
	public void SetPath (NavPath path, NavPath.FollowDirection pathDirection = NavPath.FollowDirection.Forward, NavPath.RepeatMode mode = NavPath.RepeatMode.Once) {
		Debug.Assert (path != null);

		this.path = path;
		this.direction = pathDirection;
		this.mode = mode;

		RestartPath ();
	}

	public void RestartPath () {
		if (path != null) {
			pathIterator = path.GetPathEnumerator (direction);
			pathIterator.MoveNext ();

			if (HasArrived) {
				NextWaypoint ();
			}
		}
	}

	public bool IsValid {
		get {
			return pathIterator != null && pathIterator.Current != null;
		}
	}

	public Vector3 DistanceToTarget {
		get {
			return pathIterator.Current.position - transform.position;
		}
	}

	public float DeltaPerTick {
		get {
			return speed * Time.fixedDeltaTime;
		}
	}

	public bool HasArrived {
		get {
			if (!IsValid) {
				return false;
			}
			return DistanceToTarget.sqrMagnitude < DeltaPerTick * DeltaPerTick;
		}
	}
	#endregion


	void Start () {
		RestartPath ();
	}

	void FixedUpdate () {
		if (!waiting) {
			MoveAlongPath ();
		}
	}

	void MoveAlongPath () {
		if (!IsValid)
			return;

		// get direction toward target
		var direction = DistanceToTarget;
		direction.Normalize ();
		 
		// move toward target
		transform.position += direction * DeltaPerTick;

		// check if we arrived at current target
		if (HasArrived) {
			Invoke ("NextWaypoint", waitAtNodeTime);
			waiting = true;
		}
	}

	void NextWaypoint () {
		waiting = false;

		// start moving toward next waypoint!
		pathIterator.MoveNext ();


		// check if we arrived at final waypoint
		if (pathIterator.Current == null) {
			OnPathFinished ();
		}
	}

	void OnPathFinished() {
		// finished path once
		switch (mode) {
			case NavPath.RepeatMode.Once:	
				// done!
				break;
			case NavPath.RepeatMode.Mirror:
				// reverse direction and walk back
				direction = direction == NavPath.FollowDirection.Forward ? 
					NavPath.FollowDirection.Backward :
					NavPath.FollowDirection.Forward;
				RestartPath ();
				MoveAlongPath ();
				break;
			case NavPath.RepeatMode.Repeat:
				// start from the beginning
				RestartPath ();
				MoveAlongPath ();
				break;
		}
	}


	void OnCollisionEnter (Collision other) {
		var feet = other.gameObject.GetComponent<Feet> ();
		if (feet) {
			// add the object that owns the feet as child to platform
			feet.owner.parent = transform;
		}
	}

	void OnCollisionExit (Collision other) {
		var last = other.transform;
		var feet = other.gameObject.GetComponent<Feet> ();
		if (feet) {
			// remove object from platform
			feet.owner.parent = null;
		}
	}
}
