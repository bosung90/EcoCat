using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class GameStartButton : MonoBehaviour {

	private Button button;

	void Awake() {
		button = GetComponent<Button> ();
	}

	void Start () {
		button.OnClickAsObservable ().Subscribe (_ => {
			GameManager.Instance.LoadScene("main");
		}).AddTo (this);
	}
}
