using UnityEngine;
using System.Collections;
using UnityEngine.AI;

/// <summary>
/// Just move around randomly
/// </summary>
[RequireComponent(typeof(NavMeshMover))]
[RequireComponent(typeof(NavMeshAgent))]
public class RandomWander : MonoBehaviour {
	public float randomSmoothness = 2;
	NavMeshMover mover;
	NavMeshAgent agent;

	void Start() {
		mover = GetComponent<NavMeshMover> ();
		agent = GetComponent<NavMeshAgent> ();
		mover.stopMovingAtDestination = false;

		MoveIntoRandomDirection ();
	}

	void Update() {
		if (mover.HasArrived) {
			MoveIntoRandomDirection ();
		}
	}

	#region Public
	public bool IsMoving {
		get { return mover.IsMoving; }
	}

	public void MoveIntoRandomDirection() {
		// determine new random point in space
		var dir = Random.insideUnitSphere;
		dir.y = 0;
		var ray = dir * randomSmoothness * agent.speed;
		var newPos = transform.position + ray;


		// project point onto NavMesh
		NavMeshHit hit;
		if (NavMesh.SamplePosition (newPos, out hit, 1000, NavMesh.AllAreas)) {
			// Go!
			mover.CurrentDestination = hit.position;
		}
	}

	public void StopMove() {
		mover.StopMove ();
	}
	#endregion
}