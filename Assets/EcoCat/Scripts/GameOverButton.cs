using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class GameOverButton : MonoBehaviour {

	private Button button;

	void Awake() {
		button = GetComponent<Button> ();
	}

	void Start () {
		button.OnClickAsObservable ().Subscribe (_ => {
			GameManager.Instance.PlayBGSound();
			GameManager.Instance.LoadScene("startScene");
		}).AddTo (this);
	}
}
