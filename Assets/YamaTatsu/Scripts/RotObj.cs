using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotObj : MonoBehaviour {

    //コントローラ
    GameController _controller;

    //カメラ
    GameObject _camera;

    //
    private float _rotZ;

    private float _rotY;

	// Use this for initialization
	void Start () {

        _controller = GameController.Instance;

        _camera = GameObject.Find("Main Camera");

        _rotZ = 0;
        _rotY = 0;

	}
	
	// Update is called once per frame
	void Update () {

        rotateCmaeraAngle();

    }

    private void rotateCmaeraAngle()
    {
        Vector3 angle = new Vector3(
            Input.GetAxis("R-StickHorizontal") * 4,
            Input.GetAxis("R-StickVertical") * 4,
            0
        );

        transform.eulerAngles += new Vector3(-angle.y, angle.x);
    }

}
