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
    private float _speed = 4.0f;

    //
    private Rigidbody rb;

    //ブーストフラグ
    private bool _boostFlag = false;

    public Canvas canvas;

    private Canvas gage;

    // Use this for initialization
    void Start () {

        controller = GameController.Instance;

        Player_pos = GetComponent<Transform>().position;
        //
        rb = GetComponent<Rigidbody>();

        gage = canvas;

    }

    // Update is called once per frame
    void Update()
    {

        controller.ControllerUpdate();

        //移動
        moveX = Input.GetAxis("L-StickHorizontal") * _speed;
        moveZ = Input.GetAxis("L-StickVertical") * _speed;


        //左スティックと十字キーの入力を取る
        //if (controller.Move(Direction.Front))
        //{
        //    Debug.Log("UP");
        //    moveZ = _speed;
        //    rb.velocity = Vector3.forward * _speed;
        //}
        //if (controller.Move(Direction.Back))
        //{
        //    Debug.Log("DOWN");
        //    rb.velocity = Vector3.back * _speed;
        //}
        //if (controller.Move(Direction.Left))
        //{
        //    Debug.Log("LEFT");
        //    rb.velocity = Vector3.left * _speed;
        //}
        //if (controller.Move(Direction.Right))
        //{
        //    Debug.Log("RIGHT");
        //    rb.velocity = Vector3.right * _speed;
        //}

        Boost();

        Debug.Log(_boostFlag);
    }

    private void FixedUpdate()
    {
        if (_boostFlag == false)
        {
            //カメラの方向から、ベクトルを取得
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

            Vector3 moveForward = cameraForward * moveZ + Camera.main.transform.right * moveX;

            rb.velocity = moveForward * _speed + new Vector3(0, rb.velocity.y, 0);

            if(moveForward != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveForward);
            }
        }
        else
        {
            //カメラの方向から、ベクトルを取得
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

            Vector3 moveForward = cameraForward * moveZ + Camera.main.transform.right * moveX;

            rb.velocity = moveForward * _speed * 10.0f + new Vector3(0, rb.velocity.y, 0);

            if (moveForward != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveForward);
            }
        }
    }

    private void Boost()
    {
        if(Input.GetButton("B") && gage.GetComponent<EP>().getBoostFlag() == true)
        {
            _boostFlag = true;
            gage.GetComponent<EP>().UseEp(5);
        }
        else
        {
            _boostFlag = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("地面");
            gage.GetComponent<EP>().RecoveryEP(10);

        }
    }

}
