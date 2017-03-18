using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour {
	public Transform origin;
	public Transform target;

	float originDistance;

	void Start() {
		if (origin) {
			originDistance = Vector3.Distance(origin.position, transform.position);
		}

		if (!target) {
			target = Camera.main.transform;
		}
	}

	void FixedUpdate () {
		transform.LookAt (
			transform.position + target.forward,
			target.up);
		
		var dir = (target.position - origin.position).normalized;
		transform.position = origin.position + dir * originDistance;
	}
}