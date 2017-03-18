using UnityEngine;
using System.Collections;

namespace Strategies {
	/// <summary>
	/// Wander around randomly, then hunt any target in sight.
	/// TODO: Keep wandering after hunting.
	/// TODO: Replace this whole thing with UtilityAI.
	/// </summary>
	[RequireComponent(typeof(RandomWander))]
	[RequireComponent(typeof(HuntOnSight))]
	public class WanderAndHunt : MonoBehaviour {

	}
}