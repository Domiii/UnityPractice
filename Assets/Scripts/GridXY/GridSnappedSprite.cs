using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// Draggable buildings are always snapped to grid, and can be dragged by mouse.
/// </summary>
public class GridSnappedSprite : MonoBehaviour {
	public SpriteRenderer sprite;
	public bool canDrag = true;

	private bool mouseDown = false;
	private Vector3 dragOffset;
	private bool restrictX;
	private bool restrictY;
	private float fakeX;
	private float fakeY;
	private float myWidth;
	private float myHeight;

	void Start()
	{
		if (sprite == null) {
			sprite = GetComponent<SpriteRenderer> ();
		}
		if (sprite == null) {
			Debug.LogError ("sprite not assigned in DraggableSprite", this);
			return;
		}
		SnapToGrid ();
	}


	void OnMouseDown() 
	{
		if (canDrag) {
			mouseDown = true;
			var mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			// make sure that the cell of the sprite that the mouse touched is always in the same cell as the mouse
			dragOffset = (transform.position - sprite.bounds.min) - SnappingGrid.Instance.SnapToGridFloorXY (mousePos - sprite.bounds.min + Vector3.one * 0.01f);
		}
	}

	void OnMouseUp() 
	{
		mouseDown = false;
	}

	void SnapToGrid() {
		SnappingGrid.Instance.SnapToGridXY (sprite);
	}


	void Update () 
	{
		if (mouseDown) {
			var mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			transform.position = mousePos + dragOffset;
			SnapToGrid ();
		}
	}
}