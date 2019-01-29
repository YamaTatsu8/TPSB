using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{

    //　非同期動作で使用するAsyncOperation
    private AsyncOperation _async;
    //　シーンロード中に表示するUI画面
    [SerializeField]
    private GameObject _UISlider;
    [SerializeField]
    private GameObject _UIText;
    //　読み込み率を表示するスライダー
    [SerializeField]
    private Slider _slider;
    //　Text
    [SerializeField]
    private Text _loadingText;

    private bool _isFinished;

    private static Loading _loading;

    //　Image
    //[SerializeField]
    //private Image _loadImage;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (_loading == null)
        {
            _loading = FindObjectOfType<Loading>() as Loading;
            DontDestroyOnLoad(_loading);
        }
        _isFinished = false;
    }

    /// <summary>
    /// 次のシーンに移行する
    /// </summary>
    public void NextScene()
    {
        //　ロード画面UIをアクティブにする
        _UISlider.SetActive(true);
        _UIText.SetActive(true);

        //　コルーチンを開始
        StartCoroutine("LoadData");
    }

    /// <summary>
    /// 読み込みゲージアニメーション
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadData()
    {
        // シーンの読み込みをする
        _async = SceneManager.LoadSceneAsync("CustomizeWindow");

        //　読み込みが終わるまで進捗状況をスライダーの値に反映させる
        while (!_async.isDone)
        {
            var progressVal = Mathf.Clamp01(_async.progress / 0.9f);
            _slider.value = progressVal;
            float num = Mathf.Floor(progressVal * 100);
            _loadingText.text = num.ToString() + "%";

            if (num == 100)
            {
                _isFinished = true;
            }
            yield return null;
        }
    }

    /// <summary>
    /// リセット
    /// </summary>
    public void FinalReset()
    {
        _isFinished = false;
        _UISlider.SetActive(false);
        _UIText.SetActive(false);
    }

    /// <summary>
    /// ロードが終了したかの情報
    /// </summary>
    /// <returns>true:終わった false:終わってない</returns>
    public bool IsFinished()
    {
        return _isFinished;
    }
}