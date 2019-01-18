using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecker : Photon.MonoBehaviour {

    // プレイヤーのオンラインID
    [SerializeField]
    private int _id = 0;

    // networkManegerからIDを取得
    Network _network;

    // ネットワークビュー
    private PhotonView _photonView;

    GameObject[] _obj;
    PhotonPlayer[] _photonPlayer = new PhotonPlayer[2];

    // 変更が出来たらtrue
    private bool _isChange = false;

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

        _network = GameObject.FindObjectOfType<Network>();

        this.name = "Player" + _id;
	}
	
	void Update () {
        // -Photon上で自身ではなかったらreturn
        if (!_photonView.isMine)
        {
            return;
        }

        if(_isChange)
        {
            return;
        }

        _obj = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            if (_photonPlayer[i] == null)
            {
                _photonPlayer[i] = PhotonNetwork.playerList[i];
                _id = _photonPlayer[i].ID;
            }
        }

        for (int i = 0; i< _obj.Length; i++)
        {
            if (_obj[i].GetComponent<PlayerChecker>().ID != _id)
            {
                    _obj[i].tag = "Enemy";
                _isChange = true;
            }
        }
    }

}
