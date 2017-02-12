using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class InputManager : MonoBehaviour {

	public static InputManager Instance;
	public IObservable<Unit> Jump;
	public IObservable<float> HorizontalForce;
	public EcoCat ecoCat;

	void Awake() {
		Instance = this;
		Jump = Observable
			.EveryUpdate()
			.Where(_ => Input.GetKeyDown (KeyCode.Space) && ecoCat.IsOnGround.Value)
			.AsUnitObservable();

		HorizontalForce = Observable.EveryFixedUpdate ()
			.Select (_ => Input.GetAxis ("Horizontal"))
			.Where (force => force != 0)
			.AsObservable ();
	}
}
