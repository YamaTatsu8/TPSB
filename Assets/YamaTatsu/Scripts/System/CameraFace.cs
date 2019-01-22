using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFace : MonoBehaviour {

    /// <summary>
    /// 顔の部分のボーン
    /// </summary>
    [SerializeField]
    private Transform coreBone = null;

    /// <summary>
    /// キャッシュ用カメラ Transform
    /// </summary>
    private Transform camra;

    /// <summary>
    /// 初期処理
    /// </summary>
    public void Start()
    {
        this.camra = Camera.main.transform;
    }

    /// <summary>
    /// 固定毎フレームの処理
    /// </summary>
    public void FixedUpdate()
    {
        var cameraPosition = this.camra.position;

        // 目の高さに合わせる
        cameraPosition.y -= 0.3f;

        // 顔だけカメラに向かせる（X軸の回転）
        this.transform.LookAt(cameraPosition);
        var angle = this.transform.rotation.eulerAngles.x;
        angle = angle > 180f ? angle - 360f : angle;
        this.coreBone.localRotation
            = Quaternion.Euler(90f + Mathf.Clamp(angle, -30f, 30f), 0f, 0f);

        // 身体全体をカメラに向ける（Y軸の回転）
        cameraPosition.y = this.transform.position.y;
        this.transform.LookAt(cameraPosition);
    }
}
