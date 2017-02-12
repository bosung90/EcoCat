using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class HungerLevelFill : MonoBehaviour {

	public EcoCat ecoCat;
	private Image image;

	void Awake() {
		image = GetComponent<Image> ();

		ecoCat.HungerLevel.Subscribe (hungerLevel => {
			image.fillAmount = hungerLevel;
		}).AddTo (this);
	}

}
