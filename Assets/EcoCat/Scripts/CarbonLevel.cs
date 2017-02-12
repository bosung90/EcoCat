using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class CarbonLevel : MonoBehaviour {

	private Image image;

	private ReactiveProperty<float> carbonLevelFill = new ReactiveProperty<float>(0f);
	public ReadOnlyReactiveProperty<bool> carbonLevelFull;

	void Awake() {
		image = GetComponent<Image> ();

		carbonLevelFull = carbonLevelFill
			.Select (fillLevel => fillLevel >= 1)
			.DistinctUntilChanged()
			.ToReadOnlyReactiveProperty ();
	}

	void Start () {
		Observable.EveryUpdate ().Subscribe (_ => {
			carbonLevelFill.Value = Mathf.Min(1f, carbonLevelFill.Value + Time.deltaTime/23f);
		}).AddTo (this);

		carbonLevelFill
			.Subscribe (fillLevel => {
			image.fillAmount = fillLevel;
			image.color = Color.Lerp(Color.white, Color.red, fillLevel);
		}).AddTo (this);


	}
}
