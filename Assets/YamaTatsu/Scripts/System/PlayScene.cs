using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScene : MonoBehaviour {

    //フェード
    GameObject _fadeOut;

    //Player
    private GameObject _player;

    //敵
    private GameObject _enemy;

    //終了フラグ
    private bool _flag = false;

	// Use this for initialization
	void Start () {

        Fade fade = new Fade();

        _fadeOut = fade.CreateFade();

        _fadeOut.GetComponentInChildren<Fade>().FadeIn();

        //プレイヤーを探す
        _player = GameObject.Find("Player");

        //敵を探す
        _enemy = GameObject.Find("Enemy");
		
	}
	
	// Update is called once per frame
	void Update () {

        if (_flag == false)
        {
            if (_player.GetComponent<Status>().getHP() <= 0 || _enemy.GetComponent<Status>().getHP() <= 0)
            {
                StartCoroutine(NextScene());
            }
        }

	}

    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds(1.0f);
        _flag = true;

    }

    public bool getFlag()
    {
        return _flag;
    }

}
