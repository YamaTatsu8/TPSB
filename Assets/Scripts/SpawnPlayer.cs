using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private Vector3 _pos;

    //PlayerSystem
    private PlayerSystem _playerSystem;

	// Use this for initialization
	void Start () {

        _playerSystem = FindObjectOfType<PlayerSystem>();

        _player = (GameObject)Resources.Load("Prefabs/PlayerModel/" + _playerSystem.getChar());

        Debug.Log(_player);

        Instantiate(_player, _pos,this.transform.rotation);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
