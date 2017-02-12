using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class WavePivot : MonoBehaviour {

	void Start () {
		CarbonLevel.Instance.carbonLevelFull.DistinctUntilChanged().Subscribe (isFull => {
			if(isFull) {
				var pos = this.transform.position;
				pos.y = 0.35f;
				this.transform.position = pos;
			} else {
				var pos = this.transform.position;
				pos.y = 0f;
				this.transform.position = pos;
			}
		}).AddTo (this);
	}

}
