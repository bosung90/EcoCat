using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class HungerLevelFill : MonoBehaviour {

	public EcoCat ecoCat;
	private Image image;

	public Color full, mid, danger;

	void Awake() {
		image = GetComponent<Image> ();

		ecoCat.HungerLevel.Subscribe (hungerLevel => {
			image.fillAmount = hungerLevel;
			if(hungerLevel>0.5) {
				image.color = Color.Lerp(mid, full, (hungerLevel-0.5f) * 2);
			} else {
				image.color = Color.Lerp(danger, mid, (hungerLevel * 2f));
			}
		}).AddTo (this);
	}

}
