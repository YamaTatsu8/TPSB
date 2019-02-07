using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecker : Photon.MonoBehaviour {

    // プレイヤーのオンラインID
    [SerializeField]
    private int _id = 0;

<<<<<<< HEAD
=======
    // networkManegerからIDを取得
    Network _network;

>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
    // ネットワークビュー
    private PhotonView _photonView;

    GameObject[] _obj;
    PhotonPlayer[] _photonPlayer = new PhotonPlayer[2];

<<<<<<< HEAD
    // 変更が出来たらtrue
    private bool _isChange = false;

=======
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
    public int ID
    {
        get
        {
            return _id;
        }
    }

	void Start () {
        // NetworkViewのコンポーネント
        _photonView = GetComponent<PhotonView>();

<<<<<<< HEAD
        _id = _photonView.ownerId;

        this.name = "Player" + _photonView.ownerId;

        //カメラに自分をセットさせる
        Camera.main.GetComponent<NetworkLookCamera>().SetPlayer(this.gameObject);

    }

    void Update()
    {
        // -Photon上で自身ではなかったらreturn
        if (!_photonView.isMine)
        {
            return;
        }

        if (_isChange)
        {
            return;
        }

=======
        _network = GameObject.FindObjectOfType<Network>();

	}
	
	void Update () {
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
        _obj = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            if (_photonPlayer[i] == null)
            {
                _photonPlayer[i] = PhotonNetwork.playerList[i];
                _id = _photonPlayer[i].ID;
            }
        }

<<<<<<< HEAD
        for (int i = 0; i < _obj.Length; i++)
        {
            if (_obj[i].GetComponent<PlayerChecker>().ID != _id)
            {
                //_obj[i].tag = "Enemy";
                _obj[i].layer = LayerMask.NameToLayer("Enemy");
                _isChange = true;
=======
        for (int i = 0; i< _obj.Length; i++)
        {
            if (_obj[i].GetComponent<PlayerChecker>().ID != _id)
            {
                if (_photonView.isMine)
                {
                    _obj[i].tag = "Enemy";
                }
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
            }
        }
    }

}
