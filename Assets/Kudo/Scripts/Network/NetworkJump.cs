using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkJump : MonoBehaviour
{

    GameController controller;

    private Canvas gage;

    //ジャンプ量
    private Vector3 _jump = new Vector3(0, 0, 0);
 
    Rigidbody rb;

    private float moveX = 0;
    private float moveZ = 0;

    private float BmoveX = 0;
    private float BmoveZ = 0;

    //地面に当たっているかの判定
    private bool _groundFlag = true;

    private float _speed = 0.5f;

    public Canvas canvas;

    //アニメーター
    private Animator _animator;

<<<<<<< HEAD
    // -PhotonView
    private PhotonView _photonView;

=======
    // -ネットワーク
    private PhotonView _photonView;


>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
    // Use this for initialization
    void Start()
    {

        controller = GameController.Instance;

        //rigidbodyのコンポーネントを取得
        rb = GetComponent<Rigidbody>();

        gage = canvas;

<<<<<<< HEAD
=======
        //_gage = gage.GetComponent<EP>();

        //アニメーターのコンポーネント
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
        _animator = GetComponent<Animator>();

        // -PhotonViewのコンポーネント
        _photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        // -自身でなかったらreturn
        if(!_photonView.isMine)
        {
            return;
        }

        controller.ControllerUpdate();

        Fly();
=======

        controller.ControllerUpdate();

        if(_photonView.isMine)
        {
            Fly();
        }

        //Boost();
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f

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
            _animator.SetBool("Jump", true);

            gage.GetComponent<EP>().UseEp(5);
            //gage.GetComponent<EP>().get();

<<<<<<< HEAD
        }
        else if(Input.GetButton("A") && gage.GetComponent<EP>().getBoostFlag() == true)
=======


        }
        else if(Input.GetButton("A"))
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
        {
            rb.velocity = new Vector3(rb.velocity.x, 10, rb.velocity.z);
            _groundFlag = false;

            //飛ぶモーションに変更
<<<<<<< HEAD
            //_animator.SetBool("Jump", true);
=======
            _animator.SetBool("Jump", true);
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f

            gage.GetComponent<EP>().UseEp(50);
        }
        else
        {
<<<<<<< HEAD
            //_animator.SetBool("Jump", false);
            gage.GetComponent<EP>().RecoveryEP(3);
        }

 
=======
            _animator.SetBool("Jump", false);
            gage.GetComponent<EP>().RecoveryEP(3);
        }
        
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
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

<<<<<<< HEAD
=======
    private void OnCollisionEnter(Collision collision)
    {
       
    }

>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
}
