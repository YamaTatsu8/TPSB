using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
using UnityEngine.UI;

public class NetworkMissileShot : MonoBehaviour {
=======

public class NetworkMissileShot : Photon.MonoBehaviour {
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f

    //ミサイル
    [SerializeField]
    private GameObject[] _missiles;

    //ミサイルプレハブ
    [SerializeField]
    private GameObject _missile;

    //コントローラのスクリプト
    GameController controller;

    //弾数数え
    private int _magazin;

    //リロードフラグ
    private bool _reload = false;

    //reload時間
    private float _time = 0.0f;

    //
    private bool _flag = true;

<<<<<<< HEAD
    //
    [SerializeField]
    private Slider _slider;

    // -PhotonView
    PhotonView _photonView;
=======
    // -ネットワーク
    private PhotonView _photonView;
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f

    // Use this for initialization
    void Start () {

        controller = GameController.Instance;

        _magazin = 0;

        //_missile = (GameObject)Instantiate(Resources.Load("Prefabs/Missile"));

        // -PhotonViewのコンポーネント
        _photonView = GetComponent<PhotonView>();
    }
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
        // 自身でなかったらreturn
        if(!_photonView.isMine)
        {
            return;
        }

        controller.ControllerUpdate();

        if (controller.TriggerDown(Trigger.RIGHT) && _flag == true) 
        {
            _flag = false;
    
            if (_magazin < 6)
            {
                //_missiles[_magazin] = (GameObject)Instantiate(Resources.Load("Prefabs/Missile"));
                _missiles[_magazin] = PhotonNetwork.Instantiate("NetworkMissile", transform.position, Quaternion.identity, 0);
                _missiles[_magazin].transform.position = this.transform.position;
                //_missiles[_magazin].GetComponent<Missile>().Shot();
                _magazin++;
            }
            else
            {
                _reload = true;
            }
        }
        
        if(controller.TriggerDown(Trigger.RIGHT) == false)
        {
            _flag = true;
        }

        if(_reload == true)
        {
            _time += Time.deltaTime;

            if(_time > 5.0f)
            {
                _reload = false;
                _magazin = 0;
                _time = 0;
            }

            _slider.value = _time / 5;

=======

        controller.ControllerUpdate();

        // -誰がボタンを押したか確認
        if(_photonView.isMine)
        {
            if (controller.TriggerDown(Trigger.RIGHT) && _flag == true)
            {
                _flag = false;

                if (_magazin < 6)
                {
                    //_missiles[_magazin] = (GameObject)Instantiate(Resources.Load("Prefabs/Missile"));
                    _missiles[_magazin] = PhotonNetwork.Instantiate("NetworkMissile", transform.position, Quaternion.identity, 0);
                    _missiles[_magazin].transform.position = this.transform.position;
                    //_missiles[_magazin].GetComponent<Missile>().Shot();
                    _magazin++;
                }
                else
                {
                    _reload = true;
                }
            }

            if (controller.TriggerDown(Trigger.RIGHT) == false)
            {
                _flag = true;
            }

            if (_reload == true)
            {
                _time += Time.deltaTime;

                if (_time > 5.0f)
                {
                    _reload = false;
                    _magazin = 0;
                    _time = 0;
                }
            }

>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
        }

    }

<<<<<<< HEAD
    public int getCount()
    {
        return 6 - _magazin;
    }

    public float getTime()
    {
        return _time;
    }

=======
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
}
