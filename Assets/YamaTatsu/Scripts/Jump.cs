using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jump : MonoBehaviour
{

    GameController controller;

    private Canvas gage;

    //ジャンプ量
    private Vector3 _jump = new Vector3(0, 0, 0);
    //ジャンプ力
    [SerializeField]
    private float _jumpPower = 5.0f;

    Rigidbody rb;

    private float moveX = 0;
    private float moveZ = 0;

    private float BmoveX = 0;
    private float BmoveZ = 0;

    //地面に当たっているかの判定
    private bool _groundFlag = true;

    private float _speed = 0.5f;

    //ブースト力
    [SerializeField]
    private float _boost = 5;

    public Canvas canvas;

    //アニメーター
    private Animator _animator;




    // Use this for initialization
    void Start()
    {

        controller = GameController.Instance;

        //rigidbodyのコンポーネントを取得
        rb = GetComponent<Rigidbody>();

        gage = canvas;

        //_gage = gage.GetComponent<EP>();

        //アニメーターのコンポーネント
        _animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        controller.ControllerUpdate();

        Fly();

        //Boost();

    }

    private void FixedUpdate()
    {
        
    }

    //飛ぶ関数
    private void Fly()
    {
        if (Input.GetButton("A") && gage.GetComponent<EP>().getBoostFlag() == true)
        {
            moveX = Input.GetAxis("L-StickHorizontal") * _speed;

            moveZ = Input.GetAxis("L-StickVertical") * _speed;

            rb.velocity = new Vector3(moveX, _jumpPower, moveZ);
            _groundFlag = false;

            //飛ぶモーションに変更
            _animator.SetBool("Jump", true);

            gage.GetComponent<EP>().UseEp(5);
            //gage.GetComponent<EP>().get();

        }
        else
        {
            _animator.SetBool("Jump", false);
        }
        
    }

    //ブーストする関数
    private void Boost()
    {
        if (Input.GetButton("B"))
        {

            rb.AddForce(Vector3.forward * 0.1f, ForceMode.Impulse);

            Debug.Log(rb);

            rb.velocity += (Vector3.forward * 0.1f) / rb.mass;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _groundFlag = true;
        }
    }

}
