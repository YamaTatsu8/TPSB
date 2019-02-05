using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetLocalPlayer : NetworkBehaviour{

	// Update is called once per frame
	void Update () {

        if (!isLocalPlayer)
        {
            return;
        }

    }

    public override void OnStartLocalPlayer()
    {
        //カメラに自分をセットさせる
        Camera.main.GetComponent<LookCamera>().SetPlayer(this.gameObject);
    }

}
