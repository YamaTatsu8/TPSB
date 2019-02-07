using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkStageSpawn : MonoBehaviour {


    //ステージオブジェクト
    private GameObject _stage;

    //マテリアル
    private Material _skybox;

	// Use this for initialization
	void Start () {

        //オブサーバーを探す
        //GameObject obj = GameObject.Find("SceneManagerObject");
        GameObject obj = GameObject.Find("GameManager");
        //オブサーバーをゲットコンポーネント
        //StageSelectManager test = obj.GetComponent<NetworkSceneObserver>().GetStageSelectSceneData();
        string test = obj.GetComponent<StageSystem>().StageName;

        _stage = (GameObject)Instantiate(Resources.Load("Prefabs/Stages/" + test));

        _skybox = (Material)Resources.Load("Material/" + test + "BackGround");

        RenderSettings.skybox = _skybox;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
