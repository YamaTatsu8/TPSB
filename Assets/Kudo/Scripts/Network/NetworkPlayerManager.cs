using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayerManager : MonoBehaviour {

    private bool _isRedey;

    private PhotonView _photonView;

    private bool _isEachReady = false;
    private bool[] _playerFlag = new bool[2];

	void Start () {

        _photonView = GetComponent<PhotonView>();

        this.name += _photonView.ownerId; 
	}
	
	void Update () {
		
        if(!_photonView.isMine)
        {
            return;
        }

        int flagCnt = 0;
        for(int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            NetworkPlayerManager manager = null;
            switch(i)
            {
                case 0:
                    manager = GameObject.Find("NetworkPlayerManager1").GetComponent<NetworkPlayerManager>();
                    if(manager != null)
                    {
                        //_playerFlag[i] = 
                    }
                    break;
                case 1:
                    break;
                default:
                    break;
            }

            if(_playerFlag[i])
            {
                flagCnt++;
            }

            if(flagCnt >= 2)
            {
                _isEachReady = true;
            }
        }
	}
}
