using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSceneManager : MonoBehaviour
{
    // SkyboxMaterial
    [SerializeField]
    Material _stage1Skybox;                 
    [SerializeField]
    Material _stage2Skybox;

    private GameController _controller;     //　ゲームコントローラー
    private GameObject _fadeObj;            //　フェード

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
    {//　とりあえずステージ１のskyBox
        RenderSettings.skybox = _stage1Skybox;

        _controller = GameController.Instance;
        _isStartFade = false;
        _isEndedAnimation = false;
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
        AnimationUpdate();

        //　ステージが遷移されていたら
        if (_fadeObj == null)
        {
            Fade fade = new Fade();
            _fadeObj = fade.CreateFade();
            _fadeObj.GetComponentInChildren<Fade>().FadeIn();
        }
        //　フェードが終了したらアニメーションを開始する
        if (_fadeObj.GetComponentInChildren<Fade>().isCheckedFadeIn() && !_isEndedAnimation)
        {
            _isStartFade = false;
            GameObject camera = GameObject.Find("Camera");
            camera.GetComponent<Animator>().SetBool("startAnimation", true);
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
        _controller.ControllerUpdate();

        //　Aボタンが押されたらフェード開始（連打無効）
        if (_controller.ButtonDown(Button.A) && !_isStartFade && _isEndedAnimation)
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
    private void AnimationUpdate()
    {
        GameObject camera = GameObject.Find("Camera");
        AnimatorStateInfo cameraStateInfo = camera.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        if (cameraStateInfo.IsName("EndCamera"))
        {
            _isEndedAnimation = true;
        }
        if (_isEndedAnimation)
        {
            GameObject winFrame = GameObject.Find("WinFrame");
            winFrame.GetComponent<Animator>().SetBool("startAnimation", true);

            GameObject unityChan = GameObject.Find("unitychan");
            unityChan.GetComponent<Animator>().SetBool("startAnimation", true);

            AnimatorStateInfo winFrameStateInfo = winFrame.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            if (winFrameStateInfo.IsName("EndWinFrame"))
            {
                GameObject winLogo = GameObject.Find("WinLogo");
                winLogo.GetComponent<Animator>().SetBool("startAnimation", true);
            }
        }
    }
}
