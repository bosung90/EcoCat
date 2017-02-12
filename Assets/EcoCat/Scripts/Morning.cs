using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class Morning : MonoBehaviour {

	private Image image;

	void Awake() {
		image = GetComponent<Image> ();
	}

	void Start () {
		GameManager.Instance.TimeOfTheDay.Subscribe (timeOfTheDay => {
			var opacity = Mathf.Sin(timeOfTheDay * 2 * Mathf.PI) / 2f + 0.5f;
			image.color = new Color(255, 255, 255, opacity);
		}).AddTo (this);
	}
}
