using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpriteStageSelect : MonoBehaviour
{
    //　定数
    private const int MAX_STAGE = 3;        //　最大ステージ数
    private const int MIN_STAGE = 1;        //　最小ステージ数

    private GameController _controller;     //　ゲームコントローラー

    private GameObject _stage;              //　ステージ管理者
    private string _stageName;              //　ステージ名
    private int _stageNumber;               //　ステージ番号
    private string _cursorName;

    [SerializeField]
    private Sprite _noSelect;
    [SerializeField]
    private Sprite _select;

	// Use this for initialization
	void Start ()
    {
        Initialize();
	}

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Initialize()
    {
        _controller = GameController.Instance;

        _stageName = "Stage";
        _stageNumber = 1;

        _cursorName = "StageImage";

        _stage = GameObject.Find("Stage");

        GameObject obj = GameObject.Find(_cursorName + _stageNumber.ToString());
        obj.GetComponent<Image>().sprite = _select;
        obj.gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    // Update is called once per frame
    void Update ()
    {
        //　コントローラー更新
        _controller.ControllerUpdate();

        //　上十字キー及び上左スティック
        if ((_controller.CheckDirectionOnce(Direction.Front, StickType.LEFTSTICK)) ||
            (_controller.CheckDirectionOnce(Direction.Front, StickType.CLOSS)))
        {
            //　カーソルが一番上を選択していたら一番下にする
            if (_stageNumber == MIN_STAGE)
            {
                ResetStageImage();
                _stageNumber = MAX_STAGE;
            }
            else
            {//　カーソルを一つ上にずらす
                ResetStageImage();
                _stageNumber--;
            }
            //　描画するステージを変更する
            _stage.GetComponent<StagePreview>().SetStage(_stageName + _stageNumber.ToString());

            //　カーソルを上にずらす
            GameObject obj = GameObject.Find(_cursorName + _stageNumber.ToString());
            obj.GetComponent<Image>().sprite = _select;
            obj.gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

            //　シーンオブサーバーに選択されている名前を投げる
            GameObject smo = GameObject.Find("SceneManagerObject");
            StageSelectManager ssm = smo.GetComponent<SceneObserver>().GetStageSelectSceneData();
            ssm.SetSelectStageName(_stageName + _stageNumber.ToString());
        }
        //　下十字キー及び下左スティック
        if ((_controller.CheckDirectionOnce(Direction.Back, StickType.LEFTSTICK)) ||
            (_controller.CheckDirectionOnce(Direction.Back, StickType.CLOSS)))
        {
            //　カーソルが一番下を選択していたら一番上にする
            if (_stageNumber == MAX_STAGE)
            {
                ResetStageImage();
                _stageNumber = MIN_STAGE;
            }
            else
            {//　カーソルを下にずらす
                ResetStageImage();
                _stageNumber++;
            }
            //　描画するステージを変更する
            _stage.GetComponent<StagePreview>().SetStage(_stageName + _stageNumber.ToString());

            //　カーソルを下にずらす
            GameObject obj = GameObject.Find(_cursorName + _stageNumber.ToString());
            obj.GetComponent<Image>().sprite = _select;
            obj.gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

            //　シーンオブサーバーに選択されている名前を投げる
            GameObject smo = GameObject.Find("SceneManagerObject");
            StageSelectManager ssm = smo.GetComponent<SceneObserver>().GetStageSelectSceneData();
            ssm.SetSelectStageName(_stageName + _stageNumber.ToString());
        }
    }

    /// <summary>
    /// 画像の初期化
    /// </summary>
    private void ResetStageImage()
    {
        GameObject beforeObj = GameObject.Find(_cursorName + _stageNumber.ToString());
        beforeObj.GetComponent<Image>().sprite = _noSelect;
        beforeObj.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
}
