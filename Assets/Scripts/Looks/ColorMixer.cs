using UnityEngine;

[RequireComponent (typeof(Renderer))]
public class ColorMixer : MonoBehaviour {
	public float mixRatio = 0.5f;

	Renderer ownRenderer;
	Material originalMaterial;

	void Start () {
		ownRenderer = GetComponent<Renderer> ();
		originalMaterial = new Material (ownRenderer.material);
	}

	public void MixColorWith (Renderer mixRenderer) {
		if (mixRenderer != null) {
			var mat1 = mixRenderer.material;
			var mat2 = ownRenderer.material;
			ownRenderer.material.Lerp (mat1, mat2, mixRatio);
		}
	}

	public void ResetColor () {
		// reset color
		ownRenderer.material = originalMaterial;
	}
}
