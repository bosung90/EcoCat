using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

	[Range(0f, 2f)]
	public float moveXDelta = 1f;

	[Range(0f, 10f)]
	public float moveXPeriod = 2.3f;

	[Range(0f, 2f)]
	public float moveYDelta = 0.1f;

	[Range(0f, 10f)]
	public float moveYPeriod = 3.7f;

	void Start () {
		LeanTween.moveLocalX (this.gameObject, transform.position.x + moveXDelta, moveXPeriod)
			.setEase(LeanTweenType.easeInOutCubic)
			.setLoopPingPong (0);

		LeanTween.moveLocalY (this.gameObject, transform.position.y + moveYDelta, moveYPeriod)
			.setEase(LeanTweenType.easeInOutCubic)
			.setLoopPingPong (0);
	}
}
