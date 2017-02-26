using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Renderer))]
public class ColorMixer : MonoBehaviour
{
	public float mixRatio = 0.5f;

	Renderer ownRenderer;
	Color originalColor;

	void Start ()
	{
		ownRenderer = GetComponent<Renderer> ();
		originalColor = ownRenderer.material.color;
	}

	public void MixColorWith (Renderer mixRenderer)
	{
		if (mixRenderer != null) {
			var color1 = mixRenderer.material.color;
			var color2 = ownRenderer.material.color;
			ownRenderer.material.color = Color.Lerp (ownRenderer.material.color, ownRenderer.material.color, mixRatio);
		}
	}

	public void ResetColor ()
	{
		// reset color
		ownRenderer.material.color = originalColor;
	}
}
