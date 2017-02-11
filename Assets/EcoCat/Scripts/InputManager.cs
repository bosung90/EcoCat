using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class InputManager : MonoBehaviour {

	public static InputManager Instance;

	private Subject<Unit> jump = new Subject<Unit>();
	public IObservable<Unit> Jump;

	void Awake() {
		Instance = this;
		Jump = Observable.EveryUpdate ().Where (_ => Input.GetKeyDown (KeyCode.Space)).AsUnitObservable ();
	}


}
