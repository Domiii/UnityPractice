using UnityEngine;
using System.Collections;
using UnityEngine.AI;


[RequireComponent (typeof(NavMeshAgent))]
[RequireComponent (typeof(HasSpeed))]
public class NavMeshMover : MonoBehaviour
{
	public bool stopMovingAtDestination = false;

	NavMeshAgent agent;
	HasSpeed hasSpeed;
	bool isMoving;

	// used for internal mini hack
	bool startedMovingFlag;

	#region Public

	public bool HasArrived {
		get;
		private set;
	}

	public bool IsMoving {
		get { return isMoving; }
	}

	public Vector3 CurrentDestination {
		get { return agent.destination; }
		set {
			// start moving!
			StartMove ();

			// update position
			agent.SetDestination (value);

			//print (string.Join(", ", new []{ agent.pathStatus.ToString (), agent.remainingDistance.ToString () }));
		}
	}

	public void StopMove ()
	{ 
		if (isMoving) {
			isMoving = false;
			agent.Stop ();
			NotifyOnStopMove ();
		}
	}

	#endregion

	void StartMove ()
	{
		HasArrived = false;	
		if (!isMoving) {
			startedMovingFlag = false;
			isMoving = true;
			agent.Resume ();
			NotifyOnStartMove ();
		}
	}

	void Awake ()
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
			} else if (agent.remainingDistance <= agent.stoppingDistance) {
				// done!
				HasArrived = true;
				if (stopMovingAtDestination) {
					StopMove ();
				}
			}
		}
	}
}