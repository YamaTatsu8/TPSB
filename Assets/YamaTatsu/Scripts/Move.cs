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
    private float _speed = 2.0f;

    //
    private Rigidbody rb;

	// Use this for initialization
	void Start () {

        controller = GameController.Instance;

        Player_pos = GetComponent<Transform>().position;

        //
        rb = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {

        controller.ControllerUpdate();

        //移動
        //moveX = Input.GetAxis("L-StickHorizontal") * _speed;
        //moveZ = Input.GetAxis("L-StickVertical") * _speed;

        // 左スティックと十字キーの入力を取る
        if (controller.Move(Direction.Front))
        {
            Debug.Log("UP");
            moveZ = _speed;
        }
        if (controller.Move(Direction.Back))
        {
            Debug.Log("DOWN");
            moveZ = -_speed;
        }
        if (controller.Move(Direction.Left))
        {
            Debug.Log("LEFT");
            moveX = _speed;
        }
        if (controller.Move(Direction.Right))
        {
            Debug.Log("RIGHT");
            moveX = -_speed;
        }


    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(moveX, rb.velocity.y, moveZ);
    }

}
