using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour {

    //コントローラー
    GameController controller;

    //バリア
    [SerializeField]
    private GameObject _barrier;

    private GameObject _object;

    //バるあフラグ
    private bool _flag = false;

    //生成してるかどうか
    private bool _reFlag = false;

    //リミット
    [SerializeField]
    private float _limitTime = 2.0f;

    //HP
    [SerializeField]
    private int _HP = 10;

    //アニメーター
    private Animator _animator;

    // Use this for initialization
    void Start () {

        controller = GameController.Instance;

        _animator = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {

        controller.ControllerUpdate();

        //左ショルダーボタンが押された時
        if (controller.ButtonDown(Button.L1))
        {
            _flag = !_flag;
        }

        if (_flag == true && _reFlag == false)
        {
            _object = GameObject.Instantiate(_barrier);
            _animator.SetBool("Guard", true);
            _reFlag = true;
        }
        else if(_flag == false)
        {
            Destroy(_object);
            _reFlag = false;
        }

        if(_reFlag == true)
        {
            _object.transform.position = transform.position;
        }

        if(_HP <= 0)
        {
            Destroy(_object);
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Hit");
            _HP -= 1;
        }
    }

}
