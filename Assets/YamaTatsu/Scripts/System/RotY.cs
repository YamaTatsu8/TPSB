using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotY : MonoBehaviour {

    //毎フレームの回転角度
    [SerializeField]
    private float _rot = 0.1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(new Vector3(0, _rot, 0));

	}
}
