﻿using UnityEngine;
using System;
using System.Collections.Generic;

public static class Extensions {
	public static C GetFirstAscendantComponent<C>(this Transform obj) 
		where C : MonoBehaviour
	{
		var component = obj.GetComponent<C> ();
		if (component) {
			return component;
		}
		if (obj.parent != null) {
			return obj.parent.GetFirstAscendantComponent<C>();
		}
		return default(C);
	}

	public static Transform GetRootTransform(this Transform obj) {
		if (obj.parent != null) {
			return GetRootTransform(obj.parent);
		}
		return obj;
	}

	public static Transform FindChildByTag (this Transform target, String tag) {
		if (target.CompareTag (tag))
			return target;
		
		// recurse
		for (int i = 0; i < target.childCount; ++i) {
			var result = FindChildByTag (target.GetChild (i), tag);
			if (result != null)
				return result;
		}
		
		return null;
	}

	public static C FindFirstDescendantWithComponent<C> (this Transform target) {
		var result = target.GetComponent<C> ();
		if (result != null) {
			return result;
		}

		// recurse
		for (int i = 0; i < target.childCount; ++i) {
			var child = target.GetChild (i);
			result = FindFirstDescendantWithComponent<C> (child);
			if (result != null)
				return result;
		}
		
		return default(C);
	}

	public static List<C> FindDescendantsByName<C> (this Transform target, String name) {
		var list = new List<C> ();
		FindDescendantsByName<C> (target, name, list);
		return list;

	}

	public static void FindDescendantsByName<C> (this Transform target, String name, List<C> list) {
		// recurse
		for (int i = 0; i < target.childCount; ++i) {
			var child = target.GetChild (i);

			if (child.name == name) {
				var component = child.GetComponent<C> ();
				if (component != null) {
					list.Add (component);
				}
			}
			
			FindDescendantsByName<C> (child, name, list);
		}
	}

	/// <summary>
	/// Recursively looks for the first descendant object of given name.
	/// </summary>
	/// <returns>The first descendant by name.</returns>
	public static C FindFirstDescendantByName<C> (this Transform target, String name) {
		var child = target.Find (name);
		if (child != null) {
			var component = child.GetComponent<C> ();
			if (component != null) {
				return component;
			}
		}
		
		// recurse
		for (int i = 0; i < target.childCount; ++i) {
			C result = FindFirstDescendantByName<C> (target.GetChild (i), name);
			if (result != null)
				return result;
		}
		
		return default(C);
	}

	/// <summary>
	/// Recursively looks for the first descendant object of given name.
	/// </summary>
	/// <returns>The first descendant by name.</returns>
	public static Transform FindFirstDescendantByName (this Transform target, String name) {
		var child = target.Find (name);
		if (child != null) {
			return child;
		}
		
		// recurse
		for (int i = 0; i < target.childCount; ++i) {
			var result = FindFirstDescendantByName (target.GetChild (i), name);
			if (result != null)
				return result;
		}
		
		return null;
	}
}

public static class EnumerableExtensions {
	public static void ForEach<T> (this IEnumerable<T> source, Action<T> action) {
		if (source == null)
			throw new ArgumentNullException ("source");
		if (action == null)
			throw new ArgumentNullException ("action");

		foreach (T item in source) {
			action (item);
		}
	}
}