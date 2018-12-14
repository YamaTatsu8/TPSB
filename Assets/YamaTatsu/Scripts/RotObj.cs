using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotObj : MonoBehaviour {

    //コントローラ
    GameController _controller;

    //
    private float _rotZ;

    private float _rotY;

	// Use this for initialization
	void Start () {

        _controller = GameController.Instance;

        _rotZ = 0;
        _rotY = 0;

	}
	
	// Update is called once per frame
	void Update () {

        //
        //_rotZ = _controller.CheckDirection(Direction.Left, StickType.RIGHTSTICK) * 2;
        //_rotZ = _controller.CheckDirection(Direction.Right, StickType.RIGHTSTICK) * 2;
        //_rotY = _controller.CheckDirection(Direction.Front, StickType.RIGHTSTICK) * 2;
        //_rotY = _controller.CheckDirection(Direction.Back, StickType.RIGHTSTICK) * 2;

        //transform.Rotate(0, _rotY, _rotZ);

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
