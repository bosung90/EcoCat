using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class InputManager : MonoBehaviour {

	public static InputManager Instance;
	public IObservable<Unit> Jump;

//	private 

	void Awake() {
		Instance = this;
		Jump = Observable
			.EveryUpdate()
			.Where(_ => Input.GetKeyDown (KeyCode.Space))
			.AsUnitObservable();
	}


}
