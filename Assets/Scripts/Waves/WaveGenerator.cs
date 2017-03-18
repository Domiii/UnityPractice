using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WaveGenerator : MonoBehaviour {
	/// <summary>
	/// Time between waves in seconds.
	/// </summary>
	public float delayBetweenWaves = 30;
	public NavPath path;
	public NavPath.FollowDirection pathDirection;
	public NavPath.RepeatMode pathMode = NavPath.RepeatMode.Once;
	public WaveTemplate[] waveTemplates;
	public Text infoText;

	List<Wave> waves;

	float lastUpdate;

	public Wave CurrentWave {
		get {
			return waves.LastOrDefault();
		}
	}

	public int CurrentWaveNumber {
		get {
			return waves.Count+1;
		}
	}

	/// <summary>
	/// The enemy template of the next wave (following the current wave)
	/// </summary>
	public WaveTemplate NextWaveTemplate {
		get {
			if (waveTemplates.Length <= waves.Count) {
				// already spawned all waves
				return null;
			}
			return waveTemplates[waves.Count];
		}
	}

	public bool HasMoreWaves {
		get {
			return NextWaveTemplate != null;
		}
	}

	void ShowText(string text) {
		if (infoText != null) {
			infoText.text = text;
		}
	}

	// Use this for initialization
	void Start () {
		if (waveTemplates == null || waveTemplates.Length == 0) {
			Debug.LogError("WaveTemplates are empty");
		}
		if (waveTemplates == null || waveTemplates.Length == 0) {
			Debug.LogError("WavePath is not set");
		}

		waves = new List<Wave> ();

		ResetTimer ();
	}
	
	// Update is called once per frame
	void Update () {
		// update all currently running waves
		foreach (var wave in waves) {
			wave.Update();
		}

		// check if we need to spawn another wave
		if (HasMoreWaves) {
			UpdateWaveProgress ();
		} else {
			ShowText("Last Wave!");
		}
	}

	void UpdateWaveProgress() {
		var now = Time.time;
		var timeSinceLastUpdate = now - lastUpdate;

		ShowText("Next wave: " + CurrentWaveNumber + " (" + (delayBetweenWaves - timeSinceLastUpdate).ToString ("0") + "s)");
		
		if (timeSinceLastUpdate >= delayBetweenWaves) {
			StartNextWave ();
		}
	}
	
	public void ResetTimer() {
		// reset timer
		lastUpdate = Time.time;
	}
	
	// start next wave
	public void StartNextWave() {
		if (NextWaveTemplate == null) {
			// all waves done!
			return;
		}

		// create new wave
		var wave = new Wave (this);
		wave.waveTemplate = NextWaveTemplate;
		waves.Add (wave);

		// spawn first enemy in new wave
		wave.Start ();

		ResetTimer ();
	}
	
	// start next enemy of given wave
	public void SpawnNextNPC(Wave wave) {
		var npcGO = (GameObject)Instantiate (wave.waveTemplate.npcPrefab, transform.position, transform.rotation);
		var npc = npcGO.GetComponent<NavMeshPathFollower> ();
		npc.path = path;
		npc.direction = pathDirection;
		npc.mode = pathMode;

		wave.npcs.Add (npc);

		// add faction of WaveGenerator to new NPC
		FactionManager.SetFaction (npcGO, gameObject);

//		// set color
//		var renderer = GetComponent<SpriteRenderer> ();
//		var followerRenderer = follower.GetComponent<SpriteRenderer> ();
//		if (renderer != null) {
//			followerRenderer.color = renderer.color;
//		}
	}
}
