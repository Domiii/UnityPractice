using UnityEngine;
using System.Collections;
using UnityEngine.AI;


[RequireComponent (typeof(NavMeshAgent))]
[RequireComponent (typeof(HasSpeed))]
public class NavMeshMover : MonoBehaviour
{
	NavMeshAgent agent;
	HasSpeed hasSpeed;
	bool isMoving;

	// used for internal mini hack
	bool startedMovingFlag;

	#region Public

	public bool StopMovingAtDestination {
		get;
		set;
	}

	public bool HasArrived {
		get;
		private set;
	}

	public bool IsMoving {
		get { return !isMoving; }
	}

	public Vector3 CurrentDestination {
		get { return agent.destination; }
		set {
			if (!isMoving) {
				// start moving!
				StartMove ();
			}

			// update position
			agent.SetDestination (value);

			//print (string.Join(", ", new []{ agent.pathStatus.ToString (), agent.remainingDistance.ToString () }));
		}
	}

	public void StopMove ()
	{ 
		if (isMoving) {
			HasArrived = true;
			isMoving = false;
			agent.Stop ();
			NotifyOnStopMove ();
		}
	}

	#endregion

	void StartMove ()
	{
		if (!isMoving) {
			HasArrived = false;
			startedMovingFlag = false;
			isMoving = true;
			agent.Resume ();
			NotifyOnStartMove ();
		}
	}

	void Reset ()
	{
		StopMovingAtDestination = true;
	}

	void Start ()
	{
		agent = GetComponent<NavMeshAgent> ();
		hasSpeed = GetComponent<HasSpeed> ();
	}

	protected void NotifyOnStartMove ()
	{
		SendMessage ("OnStartMove", SendMessageOptions.DontRequireReceiver);
	}

	protected void NotifyOnStopMove ()
	{
		SendMessage ("OnStopMove", SendMessageOptions.DontRequireReceiver);
	}

	void FixedUpdate ()
	{
		// always set agent speed to HasSpeed's speed
		// (so we can control all speed through HasSpeed)
		agent.speed = hasSpeed.speed;
	}

	void LateUpdate ()
	{
		if (isMoving) {
			if (!startedMovingFlag) {
				// hackfix: during the first update cycle after assigning a target, remainingDistance is still 0!
				startedMovingFlag = true;
			} else if (StopMovingAtDestination) {
				if (agent.remainingDistance <= agent.stoppingDistance) {
					// done!
					StopMove ();
				}
			}
		}
	}
}