using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotObj : MonoBehaviour {

   // Use this for initialization
	void Start () {

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
