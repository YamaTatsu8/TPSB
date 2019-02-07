using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewStage : MonoBehaviour {

    [SerializeField]
    private GameObject _stage;

	void Start () {
        GameObject obj = GameObject.Find("SceneManagerObject");
        StageSelectManager ssm = obj.GetComponent<NetworkSceneObserver>().GetStageSelectSceneData();
        _stage = (GameObject)Instantiate(Resources.Load("Prefabs/Stages/" + ssm.GetSelectStageName()));
    }

    //void Update () {
    //}
}
