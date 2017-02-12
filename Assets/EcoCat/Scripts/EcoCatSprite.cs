using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EcoCatSprite : MonoBehaviour {
	private Animator animator;

	public EcoCat ecoCat;

	void Awake() {
		animator = GetComponent<Animator> ();
	}

	void Start() {
		ecoCat.IsCatWalking.Subscribe (isWalking => {
			animator.SetBool("Walking", isWalking);
		}).AddTo (this);

		ecoCat.FacingRight.Subscribe (isFacingRight => {
			LookDirection(isFacingRight);
		}).AddTo (this);
	}

	void LookDirection(bool isRight) {
        Vector3 scale = transform.localScale;
		scale.x = isRight ? 1 : -1;
        transform.localScale = scale;
	}
}
