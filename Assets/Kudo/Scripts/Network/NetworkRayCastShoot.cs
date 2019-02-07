using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

<<<<<<< HEAD
public class NetworkRayCastShoot : MonoBehaviour
=======
public class NetworkRayCastShoot : Photon.MonoBehaviour
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
{
    // 発射間隔
    private float _fireRate;

    // 次弾発射までの時間
    private float _nextTime = 0;

    // カメラ
    private Camera _fpsCam;

    // 銃口
    private Transform _muzzle;

    // 弾のプレハブ
    private GameObject _bulletPrefab;

    // 弾速
    private float _bulletSpeed = 50.0f;

<<<<<<< HEAD
    // 射程
    private float _range = 30.0f;

    // 対象の位置
    private Vector3 _targetPos = Vector3.zero;

    // 弾の種類
    private BulletType _type;

=======
    // 射程(DrawLineの距離)
    private float _range = 30.0f;

    private Vector3 _targetPos = Vector3.zero;

>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
    // Use this for initialization
    void Start ()
    {
        _fireRate = 1.0f / this.GetComponent<NetworkWeaponManager>().RoundsPerSecond;
        
        _fpsCam = Camera.FindObjectOfType<Camera>();

        _muzzle = this.GetComponent<NetworkWeaponManager>().Muzzle;

        _bulletPrefab = this.GetComponent<NetworkWeaponManager>().BulletPrefab;
<<<<<<< HEAD

        _type = this.GetComponent<NetworkWeaponManager>().Type;
=======
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
    }

    public bool Shot(float fireRate)
    {
        _fireRate = fireRate;

        _targetPos = transform.root.GetComponent<NetworkAttack>().getPosition();

        if (Time.time > _nextTime)
        {
            // 次弾発射までの時間更新
            _nextTime = Time.time + _fireRate;

            // ビューポートの中心にベクターを作成
            Vector3 rayOrigin = _fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            Ray ray = new Ray(rayOrigin, _fpsCam.transform.forward);

            // 弾丸の生成
            GameObject bulletClone = Instantiate<GameObject>(_bulletPrefab);
<<<<<<< HEAD

            // 弾丸の位置を調整
            bulletClone.transform.position = _muzzle.position;

            switch (_type)
            {
                case BulletType.Normal:
                    if (_targetPos != Vector3.zero)
                    {
                        bulletClone.GetComponent<Rigidbody>().velocity = (_targetPos - bulletClone.transform.position).normalized * _bulletSpeed;
                    }
                    break;
                case BulletType.Missile:
                    StartCoroutine(Missile(bulletClone, this.GetComponent<NetworkRayCastShoot>(), _muzzle.position));
                    break;
                case BulletType.Laser:
                    break;
                default:
                    break;
            }
            
            //RaycastHit hit;
=======
            // bulletCloneの位置を調整
            bulletClone.transform.position = _muzzle.position;

            RaycastHit hit;

            if (_targetPos != Vector3.zero)
            {
                bulletClone.GetComponent<Rigidbody>().velocity = (_targetPos - bulletClone.transform.position).normalized * _bulletSpeed;
            }
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f

            //if (Physics.Raycast(ray, out hit, _range))
            //{
            //    // レイのヒットした地点に飛ばす
            //    bulletClone.GetComponent<Rigidbody>().velocity = (hit.point - bulletClone.transform.position).normalized * _bulletSpeed;
            //}
            //else
            //{
            //    // 射程距離分進んだ地点に飛ばす
            //    bulletClone.GetComponent<Rigidbody>().velocity = (ray.GetPoint(_range) - bulletClone.transform.position).normalized * _bulletSpeed;
            //}

<<<<<<< HEAD
            bulletClone.GetComponent<NetworkBulletController>().DeleteBullet(bulletClone);
=======
            bulletClone.GetComponent<BulletController>().DeleteBullet(bulletClone);
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f

            return true;
        }
        return false;
    }
<<<<<<< HEAD

    public static IEnumerator Missile(GameObject bulletClone, NetworkRayCastShoot r, Vector3 muzzlePos)
    {
        float timer = 0.0f;

        Vector3 targetPos = Vector3.zero; 

        while (timer < 4.0)
        {
            targetPos = r.transform.root.GetComponent<NetworkAttack>().getPosition();

            double dir = Math.Sqrt(Math.Abs(muzzlePos.x - targetPos.x) * 2 + Math.Abs(muzzlePos.z - targetPos.z) * 2);

            targetPos = targetPos + new Vector3(0, (float)dir / 4.5f, 0);

            if (timer > 0.4)
            {
                bulletClone.transform.rotation = Quaternion.Lerp(
                    bulletClone.transform.rotation,
                    Quaternion.LookRotation((targetPos + new Vector3(0, -2, 0)) - bulletClone.transform.position),
                    Time.deltaTime * 100);

                Vector3 front = bulletClone.transform.TransformDirection(Vector3.forward);

                bulletClone.GetComponent<Rigidbody>().AddForce(front * 150.0f, ForceMode.Force);
            }
            else if (0.2 > timer)
            {
                bulletClone.GetComponent<Rigidbody>().AddForce(bulletClone.transform.up * 5000 * Time.deltaTime, ForceMode.Force);
            }

            timer += Time.deltaTime;

            yield return null;
        }
    }
=======
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
}