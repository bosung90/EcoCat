using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class CansCollectedText : MonoBehaviour {

	public EcoCat ecoCat;
	private Text text;

	void Awake() {
		text = GetComponent<Text> ();

		ecoCat.NumCanCollected.Subscribe (numCan => {
			string cansCollectedText = string.Format("Cans Collected: {0}", numCan);
			text.text = cansCollectedText;
		}).AddTo (this);
	}
}
