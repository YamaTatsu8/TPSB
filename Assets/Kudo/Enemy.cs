using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


    public enum State
    {
        ShortAttack,    // 近距離時
        DistantAttack   // 遠距離時
    }

    public GameObject _player;
    public float _speed;

    public GameObject _gun;
    public GameObject _sword;
    public GameObject _bullet;

    private State _state;
    private int _changeCount = 0;
    private int _bulletCount = 0;
    private float _bulletPower = 500f;


    void Start () {
        _state = State.DistantAttack;
        Debug.Log(_state);
    }

    void Update () {

        // 3秒ごとに遠距離か近距離か決める
        _changeCount++;
        if(_changeCount == 180)
        {
            SetState();
            _changeCount = 0;
            Debug.Log(_state);
        }


        switch (_state)
        {
            case State.ShortAttack:
                _sword.SetActive(true);
                _gun.SetActive(false);

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_player.transform.position - transform.position), 0.3f);
                transform.position += transform.forward * _speed;
                break;
            case State.DistantAttack:
                _sword.SetActive(false);
                _gun.SetActive(true);

                Ray ray = new Ray(_gun.transform.position, _player.transform.position);
                RaycastHit hit = new RaycastHit();

                if (Physics.Raycast(ray.origin, ray.direction, out hit, 5))
                {
                    // playerと当たったら当たらない位置まで後退する
                    if (hit.collider.gameObject.CompareTag("Player"))
                    {
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_player.transform.position - transform.position), 0.3f);
                        transform.position -= transform.forward * _speed;
                    }

                    // 壁と当たったときRayが当たらないところまで移動
                    //if (hit.collider.gameObject.CompareTag("Wall"))
                    //{
                    //}

                }

                Debug.DrawRay(ray.origin, ray.direction, Color.yellow);

                // この辺に弾の発射について記入ーーーーーーーーーーーーーー
                //_bulletCount++;
                //if (_bulletCount > 30)
                //{
                //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_target.transform.position - transform.position), 0.3f);
                //    var bulletInstance = Instantiate(_bullet, _gun.transform.position, _gun.transform.rotation) as GameObject;
                //    bulletInstance.GetComponent<Rigidbody>().AddForce(bulletInstance.transform.forward * _bulletPower);
                //    Destroy(bulletInstance, 5f);
                //    _bulletCount = 0;
                //}
                break;
            default:
                break;

        }

        if (transform.position.y < -10)
        {
            transform.position = new Vector3(0, 0, 0);
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (_state)
            {
                case State.ShortAttack:
                    // ダメージ判定（プレイヤーへ
                    break;
                case State.DistantAttack:
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_player.transform.position - transform.position), 0.3f);
                    transform.position -= transform.forward * _speed;
                    break;
                default:
                    break;
            }

        }
    }

    // ステートの切り替え（ランダムで切替
    void SetState()
    {
        int num = Random.Range(0, 2);

        switch (num)
        {
            case 0:
                _state = State.ShortAttack;
                break;
            case 1:
                _state = State.DistantAttack;
                break;
        }

    }
}
