using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EcoCat : MonoBehaviour {

	private Animator animator;

	private Rigidbody2D rigidBody2D;
	private ReactiveProperty<int> numCansCollected = new ReactiveProperty<int> (0);
	public ReadOnlyReactiveProperty<int> NumCanCollected {
		get {
			return numCansCollected.ToReadOnlyReactiveProperty();
		}
	}

	private ReactiveProperty<float> hungerLevel = new ReactiveProperty<float> (1);
	public ReadOnlyReactiveProperty<float> HungerLevel {
		get {
			return hungerLevel.DistinctUntilChanged().ToReadOnlyReactiveProperty();
		}
	}

	public ReadOnlyReactiveProperty<bool> IsCatWalking;

	void Awake() {
		rigidBody2D = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();

		IsCatWalking = Observable
			.EveryUpdate ()
			.Select (_ => Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
			.ToReadOnlyReactiveProperty ();
	}

	void Start() {
		InputManager.Instance.Jump.Subscribe (_ => {
			var originalVelocity = rigidBody2D.velocity;
			rigidBody2D.velocity = new Vector2(originalVelocity.x, 2.5f);
		}).AddTo(this);

		InputManager.Instance.HorizontalForce.Subscribe (force => {
			rigidBody2D.AddForce(Vector2.right * force * 8);
		}).AddTo (this);

		Observable.EveryUpdate ().Subscribe (_ => {
			var decreaseAmount = Time.deltaTime / 100f;
			if(hungerLevel.Value > decreaseAmount) {
				hungerLevel.Value -= decreaseAmount;
			} else {
				hungerLevel.Value = 0;
			}
		}).AddTo (this);

		IsCatWalking.Subscribe (isWalking => {
			animator.SetBool("Walking", isWalking);
		}).AddTo (this);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Can") {
			Destroy (coll.gameObject);
			numCansCollected.Value++;
		}
	}
}
