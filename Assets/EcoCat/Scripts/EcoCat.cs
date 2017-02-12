using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EcoCat : MonoBehaviour {

	private Rigidbody2D rigidBody2D;
    private bool facingRight;
    //private float maxSpeed = 1.0f;

	void Awake() {
		rigidBody2D = GetComponent<Rigidbody2D> ();
	}

	void Start() {
		InputManager.Instance.Jump.Subscribe (_ => {
			var originalVelocity = rigidBody2D.velocity;
			rigidBody2D.velocity = new Vector2(originalVelocity.x, 2.5f);
		}).AddTo(this);

		InputManager.Instance.HorizontalForce.Subscribe (force => {
			rigidBody2D.AddForce(Vector2.right * force * 8);
		}).AddTo (this);

        facingRight = true;

	}

    void Update()
    {
        // Check the velocity of the cat, and if the direction faced =/= velocity
        // then call Flip()
        Vector2 vel = rigidBody2D.velocity;
        if (vel.x < 0 && facingRight)
        {
            Flip();
        } else if (vel.x > 0 && !facingRight)
        {
            Flip();
        }
        
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        facingRight = !facingRight;
    }

    //	private Animator animator;
    //
    //	void Awake() {
    //		animator = GetComponent<Animator> ();
    //	}
    //
    //	void Start() {
    //		animator.get

//		animator.get
//	}

}
