using UnityEngine;
using System.Collections;

public class SnappingGrid : MonoBehaviour {
	public static SnappingGrid Instance {
		get;
		private set;
	}

	public float cellSize = 1f;
	public IntVector2 min = new IntVector2(-10, -10);
	public IntVector2 max = new IntVector2(10, 10);

	public Vector3 MinPos {
		get;
		private set;
	}

	public Vector3 MaxPos {
		get;
		private set;
	}

	public SnappingGrid() {
		Instance = this;
	}

	public void Awake() {
		MinPos = new Vector3 (min.x * cellSize, min.y * cellSize, 0);
		MaxPos = new Vector3 (max.x * cellSize, max.y * cellSize, 0);
	}

	public void SnapToGridXY(SpriteRenderer sprite) {
		SnapToGridXY (sprite.transform, sprite.bounds);
	}

//	public void SnapToGridXZ(Collider collider) {
//		var pos3 = collider.transform.position;
//		var pos = new Vector2(pos3.x, pos3.z);
//		SnapToGrid (pos, collider.bounds);
//	}

	public void SnapToGridXY(Transform transform, Bounds bounds) {
		var spriteMin = bounds.min;
		var snappedMin = SnapToGridFloorXY (spriteMin);
		var spriteMax = bounds.max;
		var snappedMax = SnapToGridCeilXY (spriteMax);
		var w = 2 * bounds.extents.x;
		var h = 2 * bounds.extents.y;
		var snappedW = SnapToGridCeilXY(w);
		var snappedH = SnapToGridCeilXY(h);

		Vector3 snappedPos;
		if (IsTouchingLeftEdge (snappedMin)) {
			// don't go smaller than min
			snappedMin.x = MinPos.x;
			snappedMax.x = snappedMin.x + snappedW;
		} if (IsTouchingBottomEdge (snappedMin)) {
			// don't go smaller than min
			snappedMin.y = MinPos.y;
			snappedMax.y = snappedMin.y + snappedH;
		} else if (IsTouchingRightEdge (snappedMax)) {
			// don't go bigger than max	
			snappedMax.x = MaxPos.x;
			snappedMin.x = snappedMax.x - snappedW;
		} else if (IsTouchingTopEdge (snappedMax)) {
			// don't go bigger than max	
			snappedMax.y = MaxPos.y;
			snappedMin.y = snappedMax.y - snappedH;
		}

		// anchor position based on snapped min vertex
		snappedPos = snappedMin + (transform.position - bounds.min);

		// if sprite does is not exactly a multiple of cellSize, make sure, it's centered inside the cells
		var offsetX = 0.5f * (cellSize - Mathf.Repeat(w-0.01f, cellSize));
		var offsetY = 0.5f * (cellSize - Mathf.Repeat(h-0.01f, cellSize));
		snappedPos.x += offsetX;
		snappedPos.y += offsetY;

		transform.position = snappedPos;
	}

	public bool IsTouchingLeftEdge(Vector3 pos) {
		return pos.x <= MinPos.x;
	}

	public bool IsTouchingBottomEdge(Vector3 pos) {
		return pos.y <= MinPos.y;
	}

	public bool IsTouchingRightEdge(Vector3 pos) {
		return pos.x >= MaxPos.x;
	}

	public bool IsTouchingTopEdge(Vector3 pos) {
		return pos.y >= MaxPos.y;
	}

	public float SnapToGridCeilXY(float t) {
		return Mathf.Ceil(t / cellSize) * cellSize;
	}

	public Vector3 SnapToGridXY(Vector3 pos) {
		return SnapToGridFloorXY (pos);
	}

	public Vector3 SnapToGridFloorXY(Vector3 pos) {
		var res = new Vector3(Mathf.Floor (pos.x / cellSize), Mathf.Floor (pos.y / cellSize), 0) * cellSize;
		res.x = (int)Mathf.Clamp (res.x, min.x, max.x);
		res.y = (int)Mathf.Clamp (res.y, min.y, max.y);
		return res;
	}

	public Vector3 SnapToGridCeilXY(Vector3 pos) {
		var res = new Vector3(Mathf.Ceil (pos.x / cellSize), Mathf.Ceil (pos.y / cellSize), 0) * cellSize;
		res.x = (int)Mathf.Clamp (res.x, min.x, max.x);
		res.y = (int)Mathf.Clamp (res.y, min.y, max.y);
		return res;
	}

	void OnDrawGizmosSelected() {
		Vector3 from = new Vector3(), to = new Vector3();
		from.x = min.x * cellSize;
		to.x = max.x * cellSize;

		for (var j = min.y; j <= max.y; ++j) {
			from.y = to.y = j * cellSize;
			Gizmos.DrawLine (from, to);
		}

		from.y = min.y * cellSize;
		to.y = max.y * cellSize;
		for (var i = min.x; i <= max.x; ++i) {
			from.x = to.x = i * cellSize;
			Gizmos.DrawLine (from, to);
		}
	}
}
