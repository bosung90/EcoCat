using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	public GameObject Can;
    public GameObject Bottle;
	public Transform CanSpawnArea;
	public CarbonLevel carbonLevel;

	public ReadOnlyReactiveProperty<bool> IsRaining {
		get {
			return isRaining.DistinctUntilChanged().ToReadOnlyReactiveProperty();
		}
	}
	// Time goes from 0 to 1, where 0 is midnight, 1 is also midnight
	public ReadOnlyReactiveProperty<float> TimeOfTheDay {
		get {
			return timeOfTheDay.ToReadOnlyReactiveProperty ();
		}
	}

	private BoxCollider2D canBoxCollider;
	private IObservable<Unit> CanSpawn;
    private IObservable<Unit> BottleSpawn;

	[SerializeField]
	private BoolReactiveProperty isRaining = new BoolReactiveProperty ();
	private ReactiveProperty<float> timeOfTheDay = new ReactiveProperty<float>(0f);

	private AudioSource endGameSound;

	void Awake() {
		if (Instance != null) {
			Destroy (this);
			return;
		}
		Instance = this;
		CanSpawn = Observable.Timer(TimeSpan.FromSeconds(4)).AsUnitObservable().Repeat();
        BottleSpawn = Observable.Timer(TimeSpan.FromSeconds(3.5)).AsUnitObservable().Repeat();
		canBoxCollider = CanSpawnArea.GetComponent<BoxCollider2D>();

		endGameSound = GetComponent<AudioSource> ();

		DontDestroyOnLoad (this);
	}

	void Start() {
		CanSpawn
			.Where(_ => CanSpawnArea != null)
			.Subscribe (_ => {
				var startX = CanSpawnArea.transform.position.x - canBoxCollider.bounds.size.x / 2f;
				var endX = CanSpawnArea.transform.position.x + canBoxCollider.bounds.size.x / 2f;
				var yPos = CanSpawnArea.transform.position.y;

				Instantiate(Can, new Vector3(UnityEngine.Random.Range(startX, endX), yPos, 0), Quaternion.identity);
		}).AddTo(this);

        BottleSpawn
            .Where(_ => CanSpawnArea != null)
            .Subscribe(_ => {
                var startX = CanSpawnArea.transform.position.x - canBoxCollider.bounds.size.x / 2f;
                var endX = CanSpawnArea.transform.position.x + canBoxCollider.bounds.size.x / 2f;
                var yPos = CanSpawnArea.transform.position.y;

                Instantiate(Bottle, new Vector3(UnityEngine.Random.Range(startX, endX), yPos, 0), Quaternion.identity);
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

	public void LoadScene(string sceneName) {

		if (sceneName == "gameOver") {
			endGameSound.Play ();
		}

		SceneManager.LoadScene (sceneName);
	}

	public void DecreaseCarbonPPM(float ppm) {
		carbonLevel.DecreaseCarbonPPM (ppm);
	}
}
