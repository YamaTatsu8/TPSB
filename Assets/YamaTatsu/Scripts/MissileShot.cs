using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileShot : MonoBehaviour {

    //ミサイル
    [SerializeField]
    private GameObject[] _missiles;

    //ミサイルプレハブ
    [SerializeField]
    private GameObject _missile;

    //コントローラのスクリプト
    GameController controller;

    //弾数数え
    private int _magazin;

    //リロードフラグ
    private bool _reload = false;

    //reload時間
    private float _time = 0.0f;

    //
    private bool _flag = true;

    // Use this for initialization
    void Start () {

        controller = GameController.Instance;

        _magazin = 0;
    
        //_missile = (GameObject)Instantiate(Resources.Load("Prefabs/Missile"));
      
    }
	
	// Update is called once per frame
	void Update () {

        controller.ControllerUpdate();

        if (controller.TriggerDown(Trigger.RIGHT) && _flag == true) 
        {
            _flag = false;
    
            if (_magazin < 6)
            {
                _missiles[_magazin] = (GameObject)Instantiate(Resources.Load("Prefabs/Missile"));
                _missiles[_magazin].transform.position = this.transform.position;
                //_missiles[_magazin].GetComponent<Missile>().Shot();
                _magazin++;
            }
            else
            {
                _reload = true;
            }
        }
        
        if(controller.TriggerDown(Trigger.RIGHT) == false)
        {
            _flag = true;
        }

        if(_reload == true)
        {
            _time += Time.deltaTime;

            if(_time > 5.0f)
            {
                _reload = false;
                _magazin = 0;
                _time = 0;
            }
        }

    }

}
