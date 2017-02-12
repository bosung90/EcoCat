using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclableGenerator : MonoBehaviour {

    public int count = 0;
    private int maxCount = 5;
    public GameObject prefab;

	// Use this for initialization
	void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update () {
		if(count < maxCount)
        {
            SpawnRecyclable();
        }
	}

    // 
    void SpawnRecyclable()
    {

        Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
