using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

	public void DestroyAfterXSeconds(float seconds) {
		Destroy (this.gameObject, seconds);
	}
}
