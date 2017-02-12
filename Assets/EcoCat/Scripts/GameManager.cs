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

	[SerializeField]
	private BoolReactiveProperty isRaining = new BoolReactiveProperty ();
	public ReadOnlyReactiveProperty<bool> IsRaining {
		get {
			return isRaining.DistinctUntilChanged().ToReadOnlyReactiveProperty();
		}
	}

	// Time goes from 0 to 1, where 0 is midnight, 1 is also midnight
	private ReactiveProperty<float> timeOfTheDay = new ReactiveProperty<float>(0f);
	public ReadOnlyReactiveProperty<float> TimeOfTheDay {
		get {
			return timeOfTheDay.ToReadOnlyReactiveProperty ();
		}
	}

	void Awake() {
		Instance = this;
		CanSpawn = Observable.Timer(TimeSpan.FromSeconds(4)).AsUnitObservable().Repeat();
		canBoxCollider = CanSpawnArea.GetComponent<BoxCollider2D>();


	}

	void Start() {
		CanSpawn.Subscribe (_ => {
			var startX = CanSpawnArea.transform.position.x - canBoxCollider.bounds.size.x / 2f;
			var endX = CanSpawnArea.transform.position.x + canBoxCollider.bounds.size.x / 2f;
			var yPos = CanSpawnArea.transform.position.y;

			Instantiate(Can, new Vector3(UnityEngine.Random.Range(startX, endX), yPos, 0), Quaternion.identity);
		}).AddTo(this);

		Observable.EveryUpdate ().Subscribe (_ => {
			timeOfTheDay.Value += Time.deltaTime / 48f;
			if(timeOfTheDay.Value > 1) timeOfTheDay.Value = 0;
		}).AddTo (this);

		Observable.Timer (TimeSpan.FromSeconds(23))
			.RepeatUntilDestroy(this)
			.Subscribe(_ => {
				isRaining.Value = true;
				Observable.Timer(TimeSpan.FromSeconds(7)).Subscribe(__ => {
					isRaining.Value = false;
				});
		}).AddTo(this);
	}
}
