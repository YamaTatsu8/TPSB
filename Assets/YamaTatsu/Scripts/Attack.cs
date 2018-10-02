using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    //コントローラのスクリプト
    GameController controller;

    //弾
    //[SerializeField]
    //private GameObject _bullet;

    //弾を飛ばす力
    [SerializeField]
    private float _bulletPower = 300.0f;

    //
    private int _timeCount = 1;

    //インターバル
    const int TIME_INTERVAL = 5;

    //武器の切り替え
    private bool _changeWeapon = true;

    [SerializeField]
    private GameObject _Bullet1;
    [SerializeField]
    private GameObject _Bullet2;


	// Use this for initialization
	void Start () {

        controller = GameController.Instance;
        this.gameObject.GetComponent<Shot>()._bulletPrefab = _Bullet1;
    }
	
	// Update is called once per frame
	void Update () {

        controller.ControllerUpdate();

        Debug.Log(_changeWeapon);

        if(controller.ButtonDown(Button.X))
        {
            _changeWeapon = !_changeWeapon;

            if (_changeWeapon == true)
            {
                this.gameObject.GetComponent<Shot>()._bulletPrefab = _Bullet1;
            }
            else
            {
                this.gameObject.GetComponent<Shot>()._bulletPrefab = _Bullet2;
            }
        }

        if (controller.TriggerDown(Trigger.Left))
        {
            _timeCount += 1;

            if (_changeWeapon == true)
            {
                //武器１
                //if (_timeCount > TIME_INTERVAL)
                {
                    
                    this.gameObject.GetComponent<Shot>().Shot1();

                    //弾にダメージをセット
                    Debug.Log("武器１");

                }
            }
            else
            {
                //武器２
                Debug.Log("武器２");
               
                this.gameObject.GetComponent<Shot>().Shot1();
            }
        }
        else
        {
            _timeCount = TIME_INTERVAL;
        }

    }
}
