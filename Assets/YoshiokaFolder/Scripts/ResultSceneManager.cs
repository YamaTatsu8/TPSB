using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSceneManager : MonoBehaviour
{
    private Material _stageSkybox;          //　スカイボックス保存用
    private GameController _controller;     //　ゲームコントローラー
    private GameObject _fadeObj;            //　フェード
    private GameObject _stage;              //　ステージ
    private GameObject _character;          //　キャラクター
    private string _characterName;           //　キャラクター名

    private bool _win = true;               //　True:勝ち、False:負け
    private bool _isStartFade = false;      //　True:フェード開始、False:フェード終了中
    private bool _isEndedAnimation = false; //  True:アニメーション終了、False:アニメーション中

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
        _isStartFade = false;
        _isEndedAnimation = false;
    }

    /// <summary>
    /// ヒエラルキー関係の初期化
    /// </summary>
    private void localInitialize()
    {//　フェードイン開始
        Fade fade = new Fade();
        _fadeObj = fade.CreateFade();
        _fadeObj.GetComponentInChildren<Fade>().FadeIn();

        //　戦っていたステージを読み込む
        GameObject obj = GameObject.Find("SceneManagerObject");
        StageSelectManager ssm = obj.GetComponent<SceneObserver>().GetStageSelectSceneData();
        _stage = (GameObject)Instantiate(Resources.Load("Prefabs/Stages/" + ssm.GetSelectStageName()));
        StageHeightAdjustment(ssm.GetSelectStageName());

        //　戦っていたキャラクターを読み込む
        GameObject ps = GameObject.Find("PlayerSystem");
        _characterName = ps.GetComponent<PlayerSystem>().getChar();
<<<<<<< HEAD
<<<<<<< HEAD
        _character = (GameObject)Instantiate(Resources.Load("Prefabs/ResultCharacter/" + _characterName));
        _character.transform.position = new Vector3(20f, 4.1f, -20f);
        if ((_characterName == "Noah") && (_win))
        {
            _character.transform.position = new Vector3(19.5f, 4.1f, -18f);
            _character.transform.Rotate(new Vector3(0, 30, 0));
        }
        _character.transform.Rotate(new Vector3(0, -30, 0));
        _character.name = _characterName;
        ps.GetComponent<PlayerSystem>().Init();
=======
        Debug.Log(_characterName);
        _characterName = "Ion";
=======
>>>>>>> Result画面の全キャラの勝利演出及び敗北演出完成
        _character = (GameObject)Instantiate(Resources.Load("Prefabs/ResultCharacter/" + _characterName));
        _character.transform.position = new Vector3(20f, 4.1f, -20f);
        if ((_characterName == "Noah") && (_win))
        {
            _character.transform.position = new Vector3(19.5f, 4.1f, -18f);
            _character.transform.Rotate(new Vector3(0, 30, 0));
        }
        _character.transform.Rotate(new Vector3(0, -30, 0));
        _character.name = _characterName;
<<<<<<< HEAD
>>>>>>> ResultSceneのIonとUnity-ChanとQueendivaの勝利敗北演出追加
=======
        ps.GetComponent<PlayerSystem>().Init();
>>>>>>> Result画面の全キャラの勝利演出及び敗北演出完成

        //　戦っていたskyboxを読み込む
        _stageSkybox = (Material)Instantiate(Resources.Load("Material/" + ssm.GetSelectStageName() + "BackGround"));
        RenderSettings.skybox = _stageSkybox;
        //　ステージのライティングを初期化
        RenderSettings.ambientSkyColor = Color.gray;
    }

    /// <summary>
    /// ステージ毎に高さを調整する
    /// </summary>
    private void StageHeightAdjustment(string Stagename)
    {
        Vector3 pos;
        switch (Stagename)
        {
            case "Stage1":
                pos = new Vector3(0, 0, 0);
                _stage.transform.localPosition = pos;
                break;
            case "Stage2":
                pos = new Vector3(0,4,0);
                _stage.transform.localPosition = pos;
                
                break;
            case "Stage3":
                pos = new Vector3(0, 4, 0);
                _stage.transform.localPosition = pos;
                break;
            case "Stage4":
                break;
            case "Stage5":
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // シーン更新
        SceneUpdate();
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    /// <returns>true:シーンが終了,false:シーンが終了していない</returns>
    public bool SceneUpdate()
    {
        ControllerUpdate();
        if (_win) { WinAnimationUpdate(); }
        else { LoseAnimationUpdate(); }

        //　シーンが遷移されていたら
        if (_fadeObj == null)
        {
            localInitialize();
        }
        //　フェードが終了したらアニメーションを開始する
        if (_fadeObj.GetComponentInChildren<Fade>().isCheckedFadeIn() && !_isEndedAnimation)
        {
            _isStartFade = false;
            GameObject camera = GameObject.Find("Camera");
            if (_win) { camera.GetComponent<Animator>().SetBool("startWinAnimation", true); }
            else { camera.GetComponent<Animator>().SetBool("startLoseAnimation", true); }
        }

        //　フェードが終了していたらシーンを変更する
        if (_fadeObj.GetComponentInChildren<Fade>().isCheckedFadeOut() && _isStartFade)
        {
            _isStartFade = false;
            return false;
        }
        return true;
    }

    /// <summary>
    /// 入力関係更新処理
    /// </summary>
    private void ControllerUpdate()
    {
        //　シーン遷移が開始されてた場合以下の処理を行わない
        if (_isStartFade)
        {
            return;
        }

        _controller.ControllerUpdate();

        //　Aボタンが押されたらフェード開始（連打無効）
        if (_controller.ButtonDown(Button.A) && _isEndedAnimation)
        {
            Fade fade = new Fade();
            _fadeObj = fade.CreateFade();
            _fadeObj.GetComponentInChildren<Fade>().FadeOut();
            _isStartFade = true;
        }
    }

    /// <summary>
    /// 勝利演出更新処理
    /// </summary>
    private void WinAnimationUpdate()
    {
        GameObject camera = GameObject.Find("Camera");
        AnimatorStateInfo cameraStateInfo = camera.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        if (cameraStateInfo.IsName("EndWinCamera"))
        {
            _isEndedAnimation = true;
        }
        if (_isEndedAnimation)
        {
            GameObject frame = GameObject.Find("WinFrame");
            frame.GetComponent<Animator>().SetBool("startAnimation", true);
            
            GameObject charactar = GameObject.Find(_characterName);
            charactar.GetComponent<Animator>().SetBool("startWinAnimation", true);

            AnimatorStateInfo winFrameStateInfo = frame.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            if (winFrameStateInfo.IsName("EndWinFrame"))
            {
                GameObject logo = GameObject.Find("WinLogo");
                logo.GetComponent<Animator>().SetBool("startWinAnimation", true);
            }
        }
    }

    /// <summary>
    /// 敗北演出更新処理
    /// </summary>
    private void LoseAnimationUpdate()
    {
        GameObject camera = GameObject.Find("Camera");
        AnimatorStateInfo cameraStateInfo = camera.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        if (cameraStateInfo.IsName("EndLoseCamera"))
        {
            _isEndedAnimation = true;
        }
        if (_isEndedAnimation)
        {
            GameObject frame = GameObject.Find("LoseFrame");
            frame.GetComponent<Animator>().SetBool("startAnimation", true);

            GameObject charactar = GameObject.Find(_characterName);
            charactar.GetComponent<Animator>().SetBool("startLoseAnimation", true);

            AnimatorStateInfo winFrameStateInfo = frame.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            if (winFrameStateInfo.IsName("EndWinFrame"))
            {
                GameObject logo = GameObject.Find("LoseLogo");
                logo.GetComponent<Animator>().SetBool("startLoseAnimation", true);
            }
        }
    }

    /// <summary>
    /// 勝敗セット
    /// </summary>
    /// <param name="win">true:勝ち false:負け</param>
    public void SetBattleResult(bool win)
    {
        _win = win;
    }
}
