using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class BottlesCollectedText : MonoBehaviour
{

    public EcoCat ecoCat;
    private Text text;

    void Awake()
    {
        text = GetComponent<Text>();

        ecoCat.NumBottlesCollected.Subscribe(numBottles => {
            string bottlesCollectedText = string.Format("Bottles Collected: {0}", numBottles);
            text.text = bottlesCollectedText;
        }).AddTo(this);
    }
}
