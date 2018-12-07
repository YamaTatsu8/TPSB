using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScene : MonoBehaviour {

    //フェード
    GameObject _fadeOut;

    //Player
    private GameObject _player;

    //敵
    [SerializeField]
    private GameObject _enemy;

    //終了フラグ
    private bool _flag = false;

    //
    private bool _fightFlag = false;


    private bool _playerFlag = false;


	// Use this for initialization
	void Start () {

        Fade fade = new Fade();

        _fadeOut = fade.CreateFade();

        _fadeOut.GetComponentInChildren<Fade>().FadeIn();

        //プレイヤーを探す
        _player = GameObject.FindGameObjectWithTag("Player");

        //敵を探す
        _enemy = GameObject.Find("Enemy");

        //レンダリング設定
        RenderSettings.ambientSkyColor = Color.gray;
		
	}
	
	// Update is called once per frame
	void Update () {

        if(_playerFlag == false)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
       
            _playerFlag = true;
        }

        if (_flag == false)
        {
            if (_player.GetComponent<Status>().getHP() <= 0)
            {
                StartCoroutine(LoseScene());
            }

            if(_enemy.GetComponent<Status>().getHP() <= 0)
            {
                StartCoroutine(WinScene());
            }
        }
	}

    private IEnumerator LoseScene()
    {
        yield return new WaitForSeconds(1.0f);
        _fightFlag = false;
        _flag = true;

    }

    private IEnumerator WinScene()
    {
        yield return new WaitForSeconds(1.0f);
        _fightFlag = true;
        _flag = true;
    }

    public bool getFlag()
    {
        return _flag;
    }

    public bool getFightFlag()
    {
        return _fightFlag;
    }

}
