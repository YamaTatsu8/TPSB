using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRotate : MonoBehaviour {

    //メインエイムオブジェ
    private GameObject _mainAimObj;

    //サブエイムオブジェ
    public GameObject _subAimObj;

    //エイムスピード
    [SerializeField]
    private float _subAimSpeed = 1.0f;

    [SerializeField]
    private float _sensitivity = 1.0f;

    private bool _reverseX = false;

    private bool _reverseY = false;

    public float clampAngle = 60;

    private void Awake()
    {
        //サブがある時だけメイン情報を取得
        _mainAimObj = _subAimObj != null ? this.transform.Find("MainAim").gameObject : null; 
    }

	// Update is called once per frame
	void Update () {

        //コントローラのRスティック
        var viewX = Input.GetAxis("R-StickHorizontal") * _sensitivity;

        //x回転方向逆転
        viewX *= _reverseX ? -1 : 1;

        var viewY = Input.GetAxis("R-StickVertical") * _sensitivity;

        //y回転方向逆転
        viewY *= _reverseY ? -1 : 1;

        //メインエイム回転
        var nowRot = this.transform.localEulerAngles;
        var newX = this.transform.localEulerAngles.x + viewY;

        newX -= newX > 180 ? 360 : 0;

        newX = Mathf.Abs(newX) > clampAngle ? clampAngle * Mathf.Sign(newX) : newX;

        this.transform.localEulerAngles = new Vector3(newX, nowRot.y + viewX, 0);
        //サブ照準移動
        if (_subAimObj != null)
        {
            var thisPos = _subAimObj.transform.position;
            var targetPos = _mainAimObj.transform.position;
            _subAimObj.transform.position = Vector3.Lerp(thisPos, targetPos, _subAimSpeed * Time.deltaTime);
        }

    }
}
