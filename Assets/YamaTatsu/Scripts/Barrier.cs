using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour {

    //コントローラー
    GameController controller;

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

    //Guardのオブジェクト
    [SerializeField]
    private GameObject _guard;

    // Use this for initialization
    void Start () {

        Debug.Log(_guard);

        controller = GameController.Instance;

        _animator = GetComponent<Animator>();

        _guard = GameObject.Find("Guard");

        _guard.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {


        GameObject target = GameObject.FindGameObjectWithTag("Player");

        controller.ControllerUpdate();

        //左ショルダーボタンが押された時
        if (Input.GetButton("L1"))
        {
            _guard.SetActive(true);
            _animator.SetBool("Guard", true);
            transform.LookAt(target.transform, Vector3.up);
        }
        else
        {
            _guard.SetActive(false);
            _animator.SetBool("Guard", false);
        }

        // 0になったらバリアを消す       
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
