using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    //ミサイル
    [SerializeField]
    private GameObject[] _missiles;

    //ミサイルプレハブ
    [SerializeField]
    private GameObject _missile;

    //弾数数え
    private int _magazin;

    //リロードフラグ
    private bool _reload = false;

    //reload時間
    private float _time = 0.0f;

    //発射タイミング
    private float _shootTimer = 0.0f;

    //
    private float _TIME_MAX = 5.0f;


    //
    private bool _flag = true;

    // Use this for initialization
    void Start () {

        _magazin = 0;

        _TIME_MAX = Random.Range(5.0f, 10.0f);

    }
	
	// Update is called once per frame
	void Update () {

       
        _flag = false;

        _shootTimer += Time.deltaTime;

        if(_shootTimer >= _TIME_MAX)
        {
            _shootTimer = 0.0f;
            _TIME_MAX = Random.Range(6.0f, 10.0f);
            Shot();
        }

     
        if (_reload == true)
        {
            _time += Time.deltaTime;

            if (_time > 5.0f)
            {
                _reload = false;
                _magazin = 0;
                _time = 0;
            }
        }

    }

    private void Shot()
    {
        if (_magazin < 6)
        {
            _missiles[_magazin] = (GameObject)Instantiate(Resources.Load("Prefabs/EnemyMissile"));
            _missiles[_magazin].transform.position = this.transform.position;
            //_missiles[_magazin].GetComponent<Missile>().Shot();
            _magazin++;
        }
        else
        {
            _reload = true;
        }
    }

}
