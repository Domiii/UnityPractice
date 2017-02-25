using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveAndTurnWithKeyboard : MonoBehaviour {
  public float moveSpeed = 2;
  public float rotationSpeed = 180;
  Rigidbody body;

  void Start() {
    body = GetComponent<Rigidbody> ();
  }

  void FixedUpdate () {
    // check where we are moving
    var rotation = Input.GetAxisRaw("Horizontal");
    var forward = Input.GetAxisRaw("Vertical");

    // rotate
    transform.Rotate(Vector3.up, rotationSpeed * rotation * Time.fixedDeltaTime);

    // compute forward velocity
    var move = forward * transform.forward;
    move.y = 0;
    move.Normalize ();
    move *= moveSpeed;

    // keep vertical speed
    move.y = body.velocity.y;

    body.velocity = move;
  }
}
