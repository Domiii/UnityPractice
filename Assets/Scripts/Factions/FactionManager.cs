using UnityEngine;
using System.Collections;
using System.Linq;

public static class FactionManager {
	#region Static Methods
	public static bool AreHostile(GameObject obj1, GameObject obj2) {
		return GetFactionType(obj1) != GetFactionType(obj2) || GetFactionType(obj1) == FactionType.None;
	}

	public static bool AreAllied(GameObject obj1, GameObject obj2) {
		return GetFactionType(obj1) == GetFactionType(obj2);
	}

	public static FactionType GetFactionType(GameObject obj) {
		var factionMember = obj.GetComponent<FactionMember> ();
		if (factionMember == null) {
			factionMember = obj.GetComponentInParent<FactionMember> ();
		}
		return factionMember != null ? factionMember.FactionType : default(FactionType);
	}

	public static void SetFaction(GameObject dest, GameObject src) {
		SetFaction(dest, GetFactionType (src));
	}

	public static void SetFaction(GameObject dest, FactionType type) {
		// make sure, projectile has faction
		if (dest.GetComponent<FactionMember> () == null) {
			dest.AddComponent (typeof(FactionMember));
		}
		dest.GetComponent<FactionMember>().FactionType = type;
	}
	#endregion
}