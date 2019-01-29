using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePreview : MonoBehaviour
{
    [SerializeField]
    float _speed = 0.5f;                    //　回転速度

    private string _stageName;              //　ステージ名
    private GameObject _stage;              //　ステージ保管用の空Obj
    private Vector3 _rot = Vector3.zero;    //　ステージのローテーション

    // Use this for initialization
    void Start()
    {
        Initialize();
	}

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Initialize()
    {
        _stageName = "Stage1";
        SetStage(_stageName);
    }
	
	// Update is called once per frame
	void Update ()
    {
        StageUpdate();
    }

    /// <summary>
    /// ステージオブジェクト更新処理
    /// </summary>
    public void StageUpdate()
    {
        //　ステージを回転させる
        _rot.y = _speed;
        _stage.transform.Rotate(new Vector3(0, _rot.y, 0));
    }

    /// <summary>
    /// ステージを変更する
    /// </summary>
    /// <param name="stageName">変更したいステージ名</param>
    public void SetStage(string stageName)
    {
        //　ステージが変更される場合、前回描画していたステージを削除する
        if (_stage != null)
        {
            GameObject stage = GameObject.Find(_stageName);
            Destroy(stage);
        }

        //　その後新たにステージを作り直す
        GameObject obj = (GameObject)Instantiate(Resources.Load("Prefabs/MiniStages/" + stageName));
        obj.name = stageName;
        _stage = obj;
        _stageName = obj.name;
    }

    //---------------------------------------------------------------------------------
    //　Setter
    //---------------------------------------------------------------------------------
    /// <summary>
    /// 座標セット
    /// </summary>
    /// <param name="pos">座標</param>
    public void SetPosition(Vector3 pos)
    {
        _stage.transform.localPosition = pos;
    }

    /// <summary>
    /// スケールセット
    /// </summary>
    /// <param name="scale">スケール</param>
    public void SetScale(Vector3 scale)
    {
        _stage.transform.localScale = scale;
    }
}
