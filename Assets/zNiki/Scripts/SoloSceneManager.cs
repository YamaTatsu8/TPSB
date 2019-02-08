using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoloSceneManager : MonoBehaviour {

    public bool _isFinish;

    public bool _isWin = false;

    private GameObject _player;

    private GameObject _boss;

	// Use this for initialization
	void Start ()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        _boss = GameObject.FindGameObjectWithTag("Enemy");
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (_boss.GetComponent<EnemyController>()._enemyHealth == 0)
        {
            _isWin = true;

            _isFinish = true;
        }
        else if (_player.GetComponent<Status>().getHP() == 0)
        {
            _isFinish = true;
        }

    }
}
