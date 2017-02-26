using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.AI;


[RequireComponent (typeof(NavMeshMover))]
[RequireComponent (typeof(NavMeshAgent))]
public class NavMeshPathFollower : MonoBehaviour
{
	public NavPath path;
	public NavPath.FollowDirection direction;
	public NavPath.RepeatMode mode;

	NavMeshMover mover;
	float maxDistanceToGoal;
	IEnumerator<Transform> pathIterator;

	#region Public

	public void SetPath (NavPath path, NavPath.FollowDirection pathDirection = NavPath.FollowDirection.Forward, NavPath.RepeatMode mode = NavPath.RepeatMode.Once)
	{
		Debug.Assert (path != null);

		this.path = path;
		this.direction = pathDirection;
		this.mode = mode;

		RestartPath ();
	}

	public void RestartPath ()
	{
		if (path != null) {
			pathIterator = path.GetPathEnumerator (direction);
			pathIterator.MoveNext ();
		}
	}

	#endregion


	void Start ()
	{
		mover = GetComponent<NavMeshMover> ();
		mover.StopMovingAtDestination = true;
		RestartPath ();
	}

	void Update ()
	{
		MoveAlongPath ();
	}

	void MoveAlongPath ()
	{
		if (pathIterator == null || pathIterator.Current == null)
			return;

		// move towards current target
		mover.CurrentDestination = pathIterator.Current.position;
	}

	void OnStartMove ()
	{
	}

	void OnStopMove ()
	{
		pathIterator.MoveNext ();
		//print ("Next Waypoint: " + pathIterator.Current);

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
}