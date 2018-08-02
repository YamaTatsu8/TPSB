using UnityEngine;

public class RayViewer : MonoBehaviour
{
    // カメラ                             
    private Camera _camera;            

    void Start()
    {
        // カメラを取得
        _camera = GetComponentInParent<Camera>();
    }

    void Update()
    {
        // ビューポートの中心にベクターを作成
        Vector3 rayOrigin = _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        // ラインの描画
        Debug.DrawRay(rayOrigin, _camera.transform.forward * 30f, Color.green);
    }
}