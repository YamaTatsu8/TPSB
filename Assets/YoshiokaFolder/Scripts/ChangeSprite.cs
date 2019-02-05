using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour
{
    //　定数
    private int MAX_BAR = 3;        //　最大BAR数
    private int MIN_BAR = 1;        //　最小BAR数

    private GameController _controller;   //　ゲームコントローラー
    private GameObject _fadeObj;            //　fadeobj
    private int _cursorNum;               //　Bar番号
    private string _cursorName;

    [SerializeField]
    private Sprite _noSelect;
    [SerializeField]
    private Sprite _select;
    [SerializeField]
    private int _returnNum = 3;

    private bool _isReturn = false;
    private bool _isStartFade;              //　フェードが開始されているかチェックするフラグ

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
        _controller = GameController.Instance;

        _cursorNum = 1;

        _cursorName = "StageImage";

        GameObject obj = GameObject.Find(_cursorName + _cursorNum.ToString());
        obj.GetComponent<Image>().sprite = _select;
        obj.gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

        MAX_BAR = this.gameObject.transform.childCount;
        _isReturn = false;
        _isStartFade = false;
    }

    // Update is called once per frame
    void Update()
    {
        //　ステージが遷移されていたら
        if (_fadeObj == null)
        {
            Fade fade = new Fade();
            _fadeObj = fade.CreateFade();
            _fadeObj.GetComponentInChildren<Fade>().FadeIn();
        }
        //　フェードが終了していたらシーンを変更する
        if (_fadeObj.GetComponentInChildren<Fade>().isCheckedFadeOut() && _isStartFade)
        {
            _isStartFade = false;
            _isReturn = true;
        }
        //　コントローラー更新
        _controller.ControllerUpdate();

        //　上十字キー及び上左スティック
        if ((_controller.CheckDirectionOnce(Direction.Front, StickType.LEFTSTICK)) ||
            (_controller.CheckDirectionOnce(Direction.Front, StickType.CLOSS)))
        {
            //　カーソルが一番上を選択していたら一番下にする
            if (_cursorNum == MIN_BAR)
            {
                ResetStageImage();
                _cursorNum = MAX_BAR;
            }
            else
            {//　カーソルを一つ上にずらす
                ResetStageImage();
                _cursorNum--;
            }

            //　カーソルを上にずらす
            GameObject obj = GameObject.Find(_cursorName + _cursorNum.ToString());
            obj.GetComponent<Image>().sprite = _select;
            obj.gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
        //　下十字キー及び下左スティック
        if ((_controller.CheckDirectionOnce(Direction.Back, StickType.LEFTSTICK)) ||
            (_controller.CheckDirectionOnce(Direction.Back, StickType.CLOSS)))
        {
            //　カーソルが一番下を選択していたら一番上にする
            if (_cursorNum == MAX_BAR)
            {
                ResetStageImage();
                _cursorNum = MIN_BAR;
            }
            else
            {//　カーソルを下にずらす
                ResetStageImage();
                _cursorNum++;
            }

            //　カーソルを下にずらす
            GameObject obj = GameObject.Find(_cursorName + _cursorNum.ToString());
            obj.GetComponent<Image>().sprite = _select;
            obj.gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
        if (_cursorNum == _returnNum)
        {
            if (_controller.ButtonDown(Button.A)) 
            {
                //　フェードアウトを開始する
                if (_fadeObj == null)
                {
                    Fade fade = new Fade();
                    _fadeObj = fade.CreateFade();
                }
                _fadeObj.GetComponentInChildren<Fade>().FadeOut();
                _isStartFade = true;
            }
        }
    }

    /// <summary>
    /// 画像の初期化
    /// </summary>
    private void ResetStageImage()
    {
        GameObject beforeObj = GameObject.Find(_cursorName + _cursorNum.ToString());
        beforeObj.GetComponent<Image>().sprite = _noSelect;
        beforeObj.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    public bool ReturnToMenu()
    {
        return _isReturn;
    }
}
