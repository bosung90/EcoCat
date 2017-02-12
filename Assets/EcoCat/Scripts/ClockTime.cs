using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ClockTime : MonoBehaviour {

	public Text hour1, hour2;
	public Text minute1, minute2;
	public Text amOrPm;

	void Start () {
		GameManager.Instance.TimeOfTheDay.Subscribe (timeOfDay => {
			int hour = (int)(timeOfDay*24f%12);
			int minute = (int)((timeOfDay%(1/24f))*24f*60f);
			string amOrPm = timeOfDay<0.5? "AM" : "PM";
			this.hour1.text = (hour/10).ToString();
			this.hour2.text = (hour%10).ToString();
			this.minute1.text = (minute/10).ToString();
			this.minute2.text = (minute%10).ToString();
			this.amOrPm.text = amOrPm;
		}).AddTo (this);
	}
}
