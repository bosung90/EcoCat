using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class MoneyCollectedText : MonoBehaviour {

	private Text text;

	void Awake() {
		text = GetComponent<Text> ();
	}

	void Start() {
		GameManager.Instance.Money.Subscribe (money => {
			string cansCollectedText = string.Format("Money Collected: {0}", money);
			text.text = cansCollectedText;
		}).AddTo (this);
	}
}
