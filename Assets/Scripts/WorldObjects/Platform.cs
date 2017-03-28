using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
	public float speed = 5;
	public NavPath path;
	public NavPath.FollowDirection direction;
	public NavPath.RepeatMode mode = NavPath.RepeatMode.Repeat;

	IEnumerator<Transform> pathIterator;

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
		}
	}

	#endregion


	void Start () {
		RestartPath ();
	}

	void FixedUpdate () {
		MoveAlongPath ();
	}

	void MoveAlongPath () {
		if (pathIterator == null || pathIterator.Current == null)
			return;

		// get direction toward target
		var target = pathIterator.Current.position;
		var direction = target - transform.position;
		direction.Normalize ();
		 
		// move toward target
		var moveDelta = speed * Time.fixedDeltaTime;
		transform.position += direction * moveDelta;

		// check if we arrived at current target
		if ((transform.position - target).sqrMagnitude < moveDelta*moveDelta) {
			NextWaypoint ();
		}
	}

	void NextWaypoint() {
		// start moving toward next waypoint!
		pathIterator.MoveNext ();

		// check if we arrived at final waypoint
		if (pathIterator.Current == null) {
			// finished path once
			switch (mode) {
				case NavPath.RepeatMode.Once:	
					// done!
					break;
				case NavPath.RepeatMode.Mirror:
					// reverse direction and walk back
					direction = direction == NavPath.FollowDirection.Forward ? NavPath.FollowDirection.Backward : NavPath.FollowDirection.Forward;
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
	}


	void OnCollisionEnter(Collision other) {
		var feet = other.gameObject.GetComponent<Feet> ();
		if (feet) {
			// get root object, and add as child to platform
			var root = feet.transform.GetRootTransform ();
			root.parent = transform;
		}
	}

	void OnCollisionExit(Collision other) {
		// check if object is child of platform
		var last = other.transform;
		var current = other.transform.parent;

		while (current != null) {
			var platform = current.GetComponent<Platform> ();
			if (platform == this) {
				// break lose from platform
				last.parent = null;
				break;
			}
			last = current;
			current = current.parent;
		}
	}
}
