using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.SceneManagement;

public class GameStartButton : MonoBehaviour {

	private Button button;

    public string sceneToLoad = "main";

	void Awake() {
		button = GetComponent<Button> ();
	}

	void Start () {
		button.OnClickAsObservable ().Subscribe (_ => {
            SceneManager.LoadScene(sceneToLoad);
		}).AddTo (this);
	}
}
