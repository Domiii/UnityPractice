using UnityEngine;
using UnityEngine.UI;

public abstract class MenuBuilder<ButtonComponentType> : MonoBehaviour
	where ButtonComponentType : Component
{
	public float padding = 4;
	public float buttonWidth = 60;
	public float buttonHeight = 24;
	public ButtonComponentType buttonPrefab;
	public Transform buttonParent;

	/// <summary>
	/// By default, all buttons are added as children to the ToggleMenu object.
	/// However, a custom buttonParent (e.g. a Panel within the menu) can be assigned.
	/// If buttonPrefab is not assigned, this property returns the menu's transform.
	/// </summary>
	public Transform RealButtonParent {
		get {
			return buttonParent != null ? buttonParent : transform;
		}
	}

	public void ClearMenu () {
		var children = RealButtonParent.GetComponentsInChildren<ButtonComponentType> ();
		foreach (var child in children) {
			DestroyImmediate (child.gameObject);
		}
	}


	public void BuildMenu<ButtonDataType> (ButtonDataType[] allData, System.Action<ButtonComponentType, ButtonDataType> buttonDecorator) {
		ClearMenu ();

		var parent = RealButtonParent;
		var parentRect = parent.GetComponent<RectTransform> ().rect;
		//var buttonRT = levelButtonPrefab.GetComponent<RectTransform> ();
		//var buttonRect = buttonRT.rect;

		var parentW = parentRect.width;
		var parentH = parentRect.height;
		var nCols = (int)((parentW - padding) / (buttonWidth + padding));
		var nRows = (int)Mathf.Ceil (allData.Length / (float)nCols);

		var fullW = nCols * (buttonWidth + padding) + padding;
		var margin = (parentW - fullW) / 2;

		var iButton = 0;
		var y = parentH - padding;
		for (var j = 0; j < nRows; ++j) {
			var x = margin;

			for (var i = 0; i < nCols && iButton < allData.Length; ++i) {
				var buttonData = allData [iButton++];
				var btn = (ButtonComponentType)Instantiate (buttonPrefab, parent, true);
				var newRectTransform = btn.GetComponent<RectTransform> ();
				newRectTransform.localScale = Vector3.one;
				newRectTransform.sizeDelta = Vector2.one;
				newRectTransform.offsetMax = newRectTransform.offsetMin = Vector2.zero;
				newRectTransform.anchorMin = new Vector2 (x / parentW, (y - buttonHeight) / parentH);
				newRectTransform.anchorMax = new Vector2 ((x + buttonWidth) / parentW, (y) / parentH);

				buttonDecorator (btn, buttonData);

				x += buttonWidth + padding;
			}
			y -= buttonHeight + padding;
		}
	}
}