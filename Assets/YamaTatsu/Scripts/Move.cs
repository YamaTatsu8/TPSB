using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    //GameControllerのスクリプト
    GameController controller;

    //speed
    private float moveX = 0;
    private float moveZ = 0;

    //Playerの座標
    private Vector3 Player_pos;

    //Speed
    [SerializeField]
    private float _speed = 8.0f;

    //移動方向
    private Vector3 moveDirection = Vector3.zero;

    //
    private Rigidbody rb;

    //ブーストフラグ
    private bool _boostFlag = false;

    public Canvas canvas;

    private Canvas gage;

    //歩きのパーティクル
    [SerializeField]
    private GameObject _walkFoot;

    //カメラ
    [SerializeField]
    private Camera _mainCamera;

    //アニメーター
    private Animator _animator;

    // Use this for initialization
    void Start () {

        controller = GameController.Instance;

        Player_pos = GetComponent<Transform>().position;
        //
        rb = GetComponent<Rigidbody>();

        _animator = GetComponent<Animator>();

        gage = canvas;

        _walkFoot.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        controller.ControllerUpdate();

        if(Input.GetAxis("L-StickHorizontal") > 0 || Input.GetAxis("L-StickVertical") > 0)
        {
            _walkFoot.SetActive(true);
        }
        else
        {
            _walkFoot.SetActive(false);
        }

        //移動
        if (_boostFlag == false)
        {
            moveX = Input.GetAxis("L-StickHorizontal") * _speed;
            moveZ = Input.GetAxis("L-StickVertical") * _speed;
        }
        else
        {
            moveX = Input.GetAxis("L-StickHorizontal") * _speed * 2;
            moveZ = Input.GetAxis("L-StickVertical") * _speed * 2;
        }
       

        Boost();

    }

    private void FixedUpdate()
    {
        //moveDirection = Vector3.zero;

        moveDirection = new Vector3(moveX, 0, moveZ);

        if (_boostFlag == false)
        {

            if (moveDirection.magnitude > 0.1f)
            {
                // カメラの方向から、X-Z平面の単位ベクトルを取得
                Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

                // 方向キーの入力値とカメラの向きから、移動方向を決定
                Vector3 moveForward = cameraForward * moveZ + Camera.main.transform.right * moveX;

                // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
                rb.velocity = moveForward * _speed + new Vector3(0, rb.velocity.y, 0);
                _animator.SetFloat("Speed", moveDirection.magnitude);

                //transform.LookAt(transform.position + moveForward);

                transform.rotation = Quaternion.LookRotation(moveForward);

                Debug.Log(Quaternion.LookRotation(moveForward));

            }
            else
            {
                _animator.SetFloat("Speed", 0f);
            }

        }
        else
        {
            // カメラの方向から、X-Z平面の単位ベクトルを取得
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

            // 方向キーの入力値とカメラの向きから、移動方向を決定
            Vector3 moveForward = cameraForward * moveZ + Camera.main.transform.right * moveX;

            // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
            rb.velocity = moveForward * _speed + new Vector3(0, rb.velocity.y, 0);

            //transform.LookAt(transform.position + moveForward);

             transform.rotation = Quaternion.LookRotation(moveForward);

        }

        //rb.velocity = new Vector3(moveX, rb.velocity.y, moveZ);

    }

    private void Boost()
    {
        if(Input.GetButton("B") && gage.GetComponent<EP>().getBoostFlag() == true)
        {
            _boostFlag = true;
            _animator.SetBool("Run", true);
            gage.GetComponent<EP>().UseEp(5);
        }
        else
        {
            _boostFlag = false;
            _animator.SetBool("Run", false);
            gage.GetComponent<EP>().RecoveryEP(3);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //
        if (collision.gameObject.tag == "Ground")
        {
            //Debug.Log("地面");
        }
    }

}
