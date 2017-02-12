using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class RainSpawn : MonoBehaviour {

	public GameObject rain;
	private BoxCollider2D boxCollider2D;

	void Awake() {
		boxCollider2D = GetComponent<BoxCollider2D> ();
	}

	void Start () {
		var startX = transform.position.x - boxCollider2D.size.x / 2f;
		var endX = transform.position.x + boxCollider2D.size.x / 2f;

		var startY = transform.position.y - boxCollider2D.size.y / 2f;
		var endY = transform.position.y + boxCollider2D.size.y / 2f;

		Observable
			.EveryUpdate()
			.Where(_ => GameManager.Instance.IsRaining.Value)
			.Subscribe (_ => {
				// Spawn rain at random position
				var randomX = Random.Range(startX, endX);
				var randomY = Random.Range(startY, endY);
				var newRain = Instantiate(rain, new Vector3(randomX, randomY, 1f), Quaternion.identity);
				Destroy(newRain, 1f);
				// Better way is to spawn 100 rains, and when rain hits bottom reset its y position to top
		}).AddTo (this);
	}
}
