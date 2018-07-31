using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    //コントローラのスクリプト
    GameController controller;

    //弾
    [SerializeField]
    private GameObject _bullet;

    //弾を飛ばす力
    [SerializeField]
    private float _bulletPower = 300.0f;

    //
    private int _timeCount = 1;

    //インターバル
    const int TIME_INTERVAL = 5;

    //武器の切り替え
    private bool _changeWeapon = true;



	// Use this for initialization
	void Start () {

        controller = GameController.Instance;

	}
	
	// Update is called once per frame
	void Update () {

        controller.ControllerUpdate();


        if(controller.ButtonDown(Button.X))
        {
            _changeWeapon = !_changeWeapon;
        }

        if (controller.TriggerDown(Trigger.Left))
        {
            _timeCount += 1;

            if (_changeWeapon == true)
            {
                //武器１
                if (_timeCount > TIME_INTERVAL)
                {
                    _timeCount = 0;

                    //画面の中央座標を取得
                    Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2, 0);
                   
                    GameObject bullets = GameObject.Instantiate(_bullet,transform.position,Quaternion.identity) as GameObject;

                    Vector3 force;

                    force = this.gameObject.transform.forward * 1000;

                    //弾にダメージをセット
                    Debug.Log("武器１");

                }
            }
            else
            {
                //武器２
                Debug.Log("武器２");
            }
        }
        else
        {
            _timeCount = TIME_INTERVAL;
        }

    }
}
