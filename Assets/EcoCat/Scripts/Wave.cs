using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

	void Start () {
		LeanTween.moveLocalX (this.gameObject, transform.position.x + 1, 2.3f)
			.setEase(LeanTweenType.easeInOutCubic)
			.setLoopPingPong (0);

		LeanTween.moveLocalY (this.gameObject, transform.position.y + 0.1f, 3.7f)
			.setEase(LeanTweenType.easeInOutCubic)
			.setLoopPingPong (0);
	}
}
