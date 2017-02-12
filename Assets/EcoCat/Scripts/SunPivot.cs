using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SunPivot : MonoBehaviour {

	private RectTransform rectTransform;
//	private float sunZ = 10f;

	void Awake() {
		rectTransform = GetComponent<RectTransform> ();
	}

	// Use this for initialization
	void Start () {
		GameManager.Instance.TimeOfTheDay.Subscribe (timeOfTheDay => {
			var degree = timeOfTheDay * 360f;
			rectTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, -degree));
		}).AddTo (this);
	}
	
	// Update is called once per frame
	void Update () {
//		sunZ = sunZ - Time.deltaTime;
//		rectTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, sunZ));
	}
}
