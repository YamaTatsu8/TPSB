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

    //武器の名前
    [SerializeField]
    private string _weponName1;
    [SerializeField]
    private string _weponName2;
    [SerializeField]
    private string _subWeponName;

    [SerializeField]
    private GameObject _wepon1;

    [SerializeField]
    private GameObject _wepon2;

    [SerializeField]
    private GameObject _subWepon;
   

	// Use this for initialization
	void Start () {

        controller = GameController.Instance;
        //this.gameObject.GetComponent<Shot>()._bulletPrefab = _Bullet1;

        //
        PlayerSystem playerSystem = FindObjectOfType<PlayerSystem>();

        _weponName1 = playerSystem.getMain1();

        _weponName2 = playerSystem.getMain2();

        _subWeponName = playerSystem.getSub();

        //Resorcesから武器を探して装備する



    }
	
	// Update is called once per frame
	void Update () {

        controller.ControllerUpdate();

        Debug.Log(_changeWeapon);

        //if(controller.ButtonDown(Button.X))
        //{
        //    _changeWeapon = !_changeWeapon;

        //    if (_changeWeapon == true)
        //    {
        //        this.gameObject.GetComponent<Shot>()._bulletPrefab = _Bullet1;
        //    }
        //    else
        //    {
        //        this.gameObject.GetComponent<Shot>()._bulletPrefab = _Bullet2;
        //    }
        //}

        //if (controller.TriggerDown(Trigger.Left))
        //{
        //    _timeCount += 1;

        //    if (_changeWeapon == true)
        //    {
        //        //武器１
        //        //if (_timeCount > TIME_INTERVAL)
        //        {
                    
        //            this.gameObject.GetComponent<Shot>().Shot1();

        //            //弾にダメージをセット
        //            Debug.Log("武器１");

        //        }
        //    }
        //    else
        //    {
        //        //武器２
        //        Debug.Log("武器２");
               
        //        this.gameObject.GetComponent<Shot>().Shot1();
        //    }
        //}
        //else
        //{
        //    _timeCount = TIME_INTERVAL;
        //}

        
    }
}
