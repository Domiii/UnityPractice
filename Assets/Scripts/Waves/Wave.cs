using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// A single wave of NPCs. Spawns a new NPC
/// </summary>
[Serializable]
public class Wave {
	public WaveGenerator waveGenerator;
	public WaveTemplate waveTemplate;

	/// <summary>
	/// The set of enemies attacking this round.
	/// </summary>
	public List<NavMeshPathFollower> npcs = new List<NavMeshPathFollower>();

	float lastUpdate;

	public Wave(WaveGenerator waveGenerator) {
		this.waveGenerator = waveGenerator;
	}

	public bool HaveAllNPCsSpawned {
		get {
			// we have spawned the total amount of enemies planned for this wave
			return npcs.Count >= waveTemplate.amount;
		}
	}
	
	
	// Use this for initialization
	public void Start () {
		waveGenerator.SpawnNextNPC(this);
		ResetTimer ();
	}

	public void Update() {
		if (!HaveAllNPCsSpawned) {
			var now = Time.time;
			var timeSinceLastUpdate = now - lastUpdate;
			
			if (timeSinceLastUpdate >= waveTemplate.delayBetweenNPCs) {
				waveGenerator.SpawnNextNPC(this);
				ResetTimer ();
			}
		}
	}
	
	public void ResetTimer() {
		// reset timer
		lastUpdate = Time.time;
	}
}
