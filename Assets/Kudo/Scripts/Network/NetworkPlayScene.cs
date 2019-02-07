using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayScene :  Util.SingletonMonoBehaviour<NetworkPlayScene>{

    //フェード
    GameObject _fadeOut;

    //Player
    private GameObject[] _player;

    ////敵
    //[SerializeField]
    //private GameObject _enemy;

    //終了フラグ
    private bool _flag = false;

    //
    private bool _fightFlag = false;


    private bool _playerFlag = false;

    [SerializeField]
    private Network _network;

    private bool _isCreate = false;

    // Use this for initialization
    void Start()
    {
        Fade fade = new Fade();

        _fadeOut = fade.CreateFade();

        _fadeOut.GetComponentInChildren<Fade>().FadeIn();

        ////敵を探す
        //_enemy = GameObject.Find("Enemy");

        //レンダリング設定
        RenderSettings.ambientSkyColor = Color.gray;

    }

    void Update()
    {
        if(_isCreate)
        {
            return;
        }
        NetworkPlayerReady manager = GameObject.FindObjectOfType<NetworkPlayerReady>();

        if (manager != null)
        {
            GameObject.Destroy(manager);
        }

        int _cnt = 0;
        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            _cnt++;
            if (_cnt == 2)
            {
                _network.PlayerInstantiate();
                _isCreate = true;
            }


        }
    }

        public void Search(bool falg)
    {
        if(!falg)
        {
            StartCoroutine(WinScene());
        }
        else
        {
            StartCoroutine(LoseScene());
        }
        //if (_flag == false)
        //{
        //    if (_player.GetComponent<Status>().getHP() <= 0)
        //    {
        //        StartCoroutine(LoseScene());
        //    }

        //    if (_enemy.GetComponent<Status>().getHP() <= 0)
        //    {
        //        StartCoroutine(WinScene());
        //    }
        //}


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
