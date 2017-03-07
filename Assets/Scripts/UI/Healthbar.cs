using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Step #1: Create an image GameObject
/// Step #2: Give the image a WHITE sprite (e.g. a white rectangle)
/// Step #3: Attach Healthbar component
/// Step #4: Assign Unit to Healthbar
/// 
/// Done!
/// 
/// Also consider the video "Online step-by-step Healthbar tutorial":
/// @see https://www.youtube.com/watch?v=1lrkgdENfqM&list=PLX-uZVK_0K_402gTvjaP5mIE8p5PFI1HD
/// </summary>
public class Healthbar : MonoBehaviour {
	public Unit unit;
	public Color goodColor;
	public Color badColor;

	Image image;

	void Reset() {
		goodColor = Color.green;
		badColor = Color.red;
	}

	void Start() {
		image = GetComponent<Image> ();
		if (image.type != Image.Type.Filled) {
			// make sure, image type is filled
			image.type = Image.Type.Filled;
			image.fillMethod = Image.FillMethod.Horizontal;
		}
	}

	void Update() {
		var ratio = unit.health / unit.maxHealth;

		// set color
		var color = Color.Lerp(badColor, goodColor, ratio);

		image.color = color;

		// set size healthbar size
		image.fillAmount = ratio;
	}
}
