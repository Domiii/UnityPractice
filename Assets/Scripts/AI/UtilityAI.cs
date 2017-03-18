using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIAction {
	/// <summary>
	/// The component that is active to perform this action.
	/// </summary>
	public MonoBehaviour component;

	/// <summary>
	/// The evaluator we use to evaluate this action's current utility.
	/// </summary>
	public UtilityAI.Evaluator evaluator;

	public override string ToString () {
		return component.GetType().Name;
	}
}

/// <summary>
/// Utility AIs are simple yet versatile, rule-based AIs that picks the best action
/// from a set of actions by continuously evaluating each action's utility. 
/// It picks the action with the highest utility at any point in time.
/// When an action is picked, it is enabled, and all other actions should be disabled.
/// 
/// TODO: Allow editing in inspector
/// TODO: Allow multiple, non-conflicting actions (e.g. "attack + run to cover") to be enabled at the same time?
/// </summary>
public class UtilityAI : MonoBehaviour {
	public delegate float Evaluator(AIAction action);
	
	public List<AIAction> actions = new List<AIAction>();

	AIAction currentAction;

	public AIAction CurrentAction {
		get {
			return currentAction;
		}
		private set {
			if (value != currentAction) {
				if (currentAction != null) {
					OnActionStop (currentAction);
				}
				var oldAction = currentAction;
				currentAction = value;
				if (currentAction != null) {
					OnActionStart (currentAction, oldAction);
				}
			}
		}
	}

	void Start () {
		actions.AddRange(new []{
			new AIAction {
				component = GetComponent<UnitAttacker>(),
				evaluator = action => ((UnitAttacker)action.component).EnsureTarget() ? 100 : 0
			},
			new AIAction {
				component = GetComponent<NavMeshPathFollower>(),
				evaluator = action => 1
			}
		});

		actions.ForEach (act => {
			act.component.enabled = false;
		});
		PickAction ();
	}

	void Update () {
		PickAction ();
	}

	AIAction PickAction () {
		return CurrentAction = (from action in actions 
				orderby action.evaluator(action) descending
			select action).First();
	}

	void OnActionStop (AIAction oldAction) {
		oldAction.component.enabled = false;
	}

	void OnActionStart (AIAction newAction, AIAction oldAction) {
		newAction.component.enabled = true;
	}
}
