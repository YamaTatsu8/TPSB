using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSystem : Util.SingletonMonoBehaviour<StageSystem> {

    private string _stageName = "";

    public string StageName
    {
        get
        {
            return _stageName;
        }
        set
        {
            _stageName = value;
        }
    }

    public void SetStageName()
    {
        GameObject obj = GameObject.Find("SceneManagerObject");
        StageSelectManager ssm = obj.GetComponent<NetworkSceneObserver>().GetStageSelectSceneData();

        _stageName = ssm.GetSelectStageName();

    }

}
