using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    //コントローラー
    GameController controller;

    //プレイヤー
    [SerializeField]
    private GameObject player;

    //LockOn
    private GameObject lockOnTarget;

    //視界角度制限
    private const float ANGLE_LIMIT_UP = 60f;
    private const float ANGLE_LIMIT_DOWN = -60f;

    //ロックオンしてるかのフラグ
    private bool _LookFlag = false;

    [SerializeField]
    private Vector3 _offset;

    // Use this for initialization
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        controller = GameController.Instance;

    }

    // Update is called once per frame
    void Update()
    {

        controller.ControllerUpdate();

        //
        transform.position = player.transform.position;

        //R1を押したとき、相手をロックオンする
        if (controller.ButtonDown(Button.R1))
        {
            GameObject target = GameObject.FindGameObjectWithTag("Enemy");

            _LookFlag = !_LookFlag;

            if (target != null && _LookFlag == true)
            {
                lockOnTarget = target;
            }
            else
            {
                lockOnTarget = null;

                iTween.RotateTo(gameObject, iTween.Hash("rotation", player.transform.eulerAngles, "time", 0.5f));

            }

        }

        if (lockOnTarget)
        {
            lockOnTargetObject(lockOnTarget);
        }
        else
        {
            rotateCameraAngle();
        }

        float angle_x = 180f <= transform.eulerAngles.x ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;
        transform.eulerAngles = new Vector3(
            Mathf.Clamp(angle_x, ANGLE_LIMIT_DOWN, ANGLE_LIMIT_UP),
            transform.eulerAngles.y,
            transform.eulerAngles.z);

    }


    //カメラアングル
    private void rotateCameraAngle()
    {
        //右スティックで視点の変更
        Vector3 angle = new Vector3(
            Input.GetAxis("R-StickHorizontal") * 2,
            Input.GetAxis("R-StickVertical") * 2,
            0);

        transform.eulerAngles += new Vector3(angle.y, angle.x);
    }

    //相手を見る
    private void lockOnTargetObject(GameObject target)
    {
        transform.LookAt(target.transform, Vector3.up);
    }
}
