using UnityEngine;

public class RayViewer : MonoBehaviour
{
    [SerializeField]
    private float _lineLength = 10f;

    [SerializeField]
    private Transform _view = null;

    // カメラ                             
    private Camera _camera = null;

    //
    private Vector3 _rayOrigin;

    void Start()
    {
        // カメラを取得
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (_camera != null)
        {
            // ビューポートの中心にベクターを作成
            _rayOrigin = _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            // ラインの描画
            Debug.DrawRay(_rayOrigin, _camera.transform.forward * _lineLength, Color.green);
        }
        else if (_view != null)
        {
            Debug.DrawRay(_view.transform.position, _view.transform.forward * _lineLength, Color.green);
        }
    }
}