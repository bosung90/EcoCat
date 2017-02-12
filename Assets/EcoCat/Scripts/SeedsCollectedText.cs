using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class SeedsCollectedText : MonoBehaviour
{

    public EcoCat ecoCat;
    private Text text;

    void Awake()
    {
        text = GetComponent<Text>();

        ecoCat.NumSeedsCollected.Subscribe(numSeeds => {
            string seedsCollectedText = string.Format("Seeds Collected: {0}", numSeeds);
            text.text = seedsCollectedText;
        }).AddTo(this);
    }
}
