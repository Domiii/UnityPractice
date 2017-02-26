using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	// die when falling below this y
	public float deathDepth = -10;

	void FixedUpdate ()
	{
		if (GetComponent<Rigidbody>().IsSleeping())
			GetComponent<Rigidbody>().WakeUp();
		
		if (transform.position.y < deathDepth) {
			// player dies when falling too far
			OnDeath ();
		}
	}

	// this function is primarly called when Unit dies: SendMessage ("OnDeath"...)
	void OnDeath ()
	{
		// reset scene!
		Scene scene = SceneManager.GetActiveScene (); 
		SceneManager.LoadScene (scene.name);
	}
}