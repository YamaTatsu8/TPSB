using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour {

    public float sensitivity = 1.0f;
    public bool reverseX = true;
    public float clampAngle = 60;
    private GameObject followTarget;

    void Awake()
    {
        followTarget = GameObject.FindGameObjectWithTag("Player").transform.Find("FollowTarget").gameObject; ;
    }

   
    // Update is called once per frame
    void Update()
    {
        var viewY = Input.GetAxis("R-StickVertical") * sensitivity;
        viewY *= reverseX ? -1 : 1;
        var nowRot = this.transform.eulerAngles;
        var newX = nowRot.x + viewY;
        newX -= newX > 180 ? 360 : 0;
        newX = Mathf.Abs(newX) > clampAngle ? clampAngle * Mathf.Sign(newX) : newX;
        this.transform.eulerAngles = new Vector3(newX, followTarget.transform.eulerAngles.y, 0);
    }
}
