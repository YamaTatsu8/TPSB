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

    private GameController _controller;

    private bool _flag;

    private void Start()
    {
        _controller = GameController.Instance;

        _flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        _controller.ControllerUpdate();

        if (_controller.ButtonDown(Button.START))
        {
            _flag = false;

            if (pauseUIInstance == null)
            {
                pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
                Time.timeScale = 0f;
            }
            else
            {
                Destroy(pauseUIInstance);
                Time.timeScale = 1f;
            }
        }

        if(_flag == true)
        {
            Time.timeScale = 1f;
        }

    }

    public void SetPause(bool flag)
    {
        _flag = flag;
    }

}