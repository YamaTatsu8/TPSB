using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSpawn : MonoBehaviour {


    //ステージオブジェクト
    private GameObject _stage;

    //マテリアル
    private Material _skybox;

	// Use this for initialization
	void Start () {

        //オブサーバーを探す
        GameObject obj = GameObject.Find("SceneManagerObject");
        //オブサーバーをゲットコンポーネント
        StageSelectManager test = obj.GetComponent<SceneObserver>().GetStageSelectSceneData();

        _stage = (GameObject)Instantiate(Resources.Load("Prefabs/Stages/" + test.GetSelectStageName()));

        _skybox = (Material)Resources.Load("Material/" + test.GetSelectStageName() + "BackGround");

        RenderSettings.skybox = _skybox;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
