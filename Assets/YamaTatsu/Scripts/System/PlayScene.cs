using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScene : MonoBehaviour {

    //Player
    private GameObject _player;

    //敵
    private GameObject _enemy;

	// Use this for initialization
	void Start () {

        //プレイヤーを探す
        _player = GameObject.Find("Player");

        //敵を探す
        _enemy = GameObject.Find("Enemy");
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(_player.GetComponent<Status>().getHP() <= 0 || _enemy.GetComponent<Status>().getHP() <= 0)
        {
            Debug.Log("リザルト画面へ移動");
            //シーン遷移を書く
        }

	}
}
