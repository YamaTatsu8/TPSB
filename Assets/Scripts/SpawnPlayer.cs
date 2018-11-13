using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private Vector3 _pos;

	// Use this for initialization
	void Start () {

        Instantiate(_player, _pos,this.transform.rotation);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
