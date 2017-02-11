using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EcoCat : MonoBehaviour {

	private Rigidbody2D rigidBody2D;

	void Awake() {
		rigidBody2D = GetComponent<Rigidbody2D> ();
	}

	void Start() {
		InputManager.Instance.Jump.Subscribe (_ => {
			var originalVelocity = rigidBody2D.velocity;
			rigidBody2D.velocity = new Vector2(originalVelocity.x, 2.5f);
		}).AddTo(this);

		InputManager.Instance.HorizontalForce.Subscribe (force => {
			rigidBody2D.AddForce(Vector2.right * force * 3);
		}).AddTo (this);
	}
//	private Animator animator;
//
//	void Awake() {
//		animator = GetComponent<Animator> ();
//	}
//
//	void Start() {
//		animator.get
//	}

}
