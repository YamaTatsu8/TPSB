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

    //エネルギーの消費量
    [SerializeField]
    private float _ep = 5;

    //jump消費量
    [SerializeField]
    private float _jumpEP = 50;

    [SerializeField]
    private float _recoveryEP = 1;

    Rigidbody rb;

    private float moveX = 0;
    private float moveZ = 0;

    private float BmoveX = 0;
    private float BmoveZ = 0;

    //地面に当たっているかの判定
    private bool _groundFlag = true;

    public Canvas canvas;

    //アニメーター
    private Animator _animator;

    //pauseフラグ
    private bool _pauseFlag;

    // Use this for initialization
    void Start()
    {

        controller = GameController.Instance;

        //rigidbodyのコンポーネントを取得
        rb = GetComponent<Rigidbody>();

        gage = canvas;

        _pauseFlag = false;

    }

    // Update is called once per frame
    void Update()
    {

        controller.ControllerUpdate();

        //ポーズフラグが立ったら通らない
        if (_pauseFlag == false)
        {
            Fly();
        }

    }

    private void FixedUpdate()
    {
        
    }

    //飛ぶ関数
    private void Fly()
    {
        if (controller.ButtonDown(Button.A) && gage.GetComponent<EP>().getBoostFlag() == true)
        {
    
            rb.velocity = new Vector3(0, 10, 0);

            _groundFlag = false;

            //飛ぶモーションに変更
            //_animator.SetBool("Jump", true);

            gage.GetComponent<EP>().UseEp(_ep);
            //gage.GetComponent<EP>().get();

        }
        else if(Input.GetButton("A") && gage.GetComponent<EP>().getBoostFlag() == true)
        {
            rb.velocity = new Vector3(rb.velocity.x, 10, rb.velocity.z);
            _groundFlag = false;

            //飛ぶモーションに変更
            //_animator.SetBool("Jump", true);

            gage.GetComponent<EP>().UseEp(_jumpEP);
        }
        else
        {
            //_animator.SetBool("Jump", false);
            gage.GetComponent<EP>().RecoveryEP(_recoveryEP);
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

    public void SetPauseFlag(bool flag)
    {
        _pauseFlag = flag;
    }

}
