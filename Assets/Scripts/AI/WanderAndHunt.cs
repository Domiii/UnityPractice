using UnityEngine;
using System.Collections;

namespace Strategies {
	/// <summary>
	/// Wander around randomly, then hunt any target in sight
	/// </summary>
	[RequireComponent(typeof(RandomWander))]
	[RequireComponent(typeof(HuntOnSight))]
	public class WanderAndHunt : MonoBehaviour {

	}
}