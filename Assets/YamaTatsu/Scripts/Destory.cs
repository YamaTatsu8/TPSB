using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destory : MonoBehaviour {


    //タイマー
    private float _timer = 0;

    //MAXタイマー
    [SerializeField]
    private float _MAXTIME;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        _timer += Time.deltaTime;

        if(_MAXTIME < _timer)
        {
            Destroy(this.gameObject);
        }

	}
}
