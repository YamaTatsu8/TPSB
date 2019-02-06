using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine.SceneManagement;


public class Pausable : MonoBehaviour
{
    [SerializeField]
    //　ポーズした時に表示するUIのプレハブ
    private GameObject pauseUIPrefab;
    //　ポーズUIのインスタンス
    private GameObject pauseUIInstance;

    //プレイヤー
    private GameObject _player;

    private GameController _controller;

    private bool _flag;

    //playerを探すフラグ
    private bool _playerFlag;

    private void Start()
    {
        _controller = GameController.Instance;

        _flag = false;

        _playerFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        _controller.ControllerUpdate();

        //playerを見つけたらtrueにする
        if(_playerFlag == false)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _playerFlag = true;
        }

        if (_controller.ButtonDown(Button.START))
        {
            _flag = false;

            if (pauseUIInstance == null)
            {
                pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
                _player.GetComponent<Move>().SetPauseFlag(true);
                _player.GetComponent<Jump>().SetPauseFlag(true);
                Time.timeScale = 0f;
            }
            else
            {
                Destroy(pauseUIInstance);
                _player.GetComponent<Move>().SetPauseFlag(false);
                _player.GetComponent<Jump>().SetPauseFlag(false);
                Time.timeScale = 1f;
            }
        }

    }

    public void SetPause()
    {
        Destroy(pauseUIInstance);
        _player.GetComponent<Move>().SetPauseFlag(false);
        _player.GetComponent<Jump>().SetPauseFlag(false);
        Time.timeScale = 1f;
    }

}