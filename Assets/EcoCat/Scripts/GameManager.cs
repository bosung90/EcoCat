using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	public GameObject Can;
	public Transform CanSpawnArea;
	private BoxCollider2D canBoxCollider;
	private IObservable<Unit> CanSpawn;

	void Awake() {
		Instance = this;
		CanSpawn = Observable.Timer(TimeSpan.FromSeconds(4d)).AsUnitObservable().Repeat();
		canBoxCollider = CanSpawnArea.GetComponent<BoxCollider2D>();
	}

	void Start() {
		CanSpawn.Subscribe (_ => {
			var startX = CanSpawnArea.transform.position.x - canBoxCollider.bounds.size.x/2f;
			var endX = CanSpawnArea.transform.position.x + canBoxCollider.bounds.size.x/2f;
			var yPos = CanSpawnArea.transform.position.y;

			Instantiate(Can, new Vector3(UnityEngine.Random.Range(startX, endX), yPos, 0), Quaternion.identity);
		}).AddTo(this);
	}
}
