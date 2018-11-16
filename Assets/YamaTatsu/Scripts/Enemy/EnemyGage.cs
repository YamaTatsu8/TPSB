using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGage : MonoBehaviour {

    //　HP表示用スライダー
    [SerializeField]
    private Slider hpSlider;

    [SerializeField]
    private int _MAX_HP;

    //
    [SerializeField]
    private GameObject _obj;

    void Start()
    {
        //　自身のルートに取り付けている敵のステータス取得
        //　HP用Sliderを子要素から取得
        hpSlider = GetComponent<Slider>();
        //　スライダーの値0～1の間になるように比率を計算
        hpSlider.value = _MAX_HP / _MAX_HP;
    }

    // Update is called once per frame
    void Update()
    {
        //　カメラと同じ向きに設定
        transform.rotation = Camera.main.transform.rotation;

        hpSlider.value = (float)_obj.GetComponent<Status>().getHP() / _MAX_HP;
    }
}
