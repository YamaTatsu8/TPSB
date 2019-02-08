using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NetworkRayCastShoot : MonoBehaviour
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
    private float _bulletSpeed;

    // 射程
    private float _range = 100.0f;

    // 対象の位置
    private Vector3 _targetPos = Vector3.zero;

    // 弾の種類
    private BulletType _type;

    // 
    private List<GameObject> _bits;

    // Use this for initialization
    void Start ()
    {
        _bulletSpeed = this.GetComponent<NetworkWeaponManager>()._bulletSpeed;

        _fireRate = 1.0f / this.GetComponent<NetworkWeaponManager>()._roundsPerSecond;

        _fpsCam = Camera.FindObjectOfType<Camera>();

        _muzzle = this.GetComponent<NetworkWeaponManager>().Muzzle;

        _bulletPrefab = this.GetComponent<NetworkWeaponManager>()._bulletPrefab;

        _type = this.GetComponent<NetworkWeaponManager>()._type;

        _bits = this.GetComponent<NetworkWeaponManager>()._bits;
    }

    public bool Shot(float fireRate)
    {
        _fireRate = fireRate;

        if (transform.root.GetComponent<NetworkAttack>())
        {
            _targetPos = transform.root.GetComponent<NetworkAttack>().getPosition();
        }

        if (Time.time > _nextTime)
        {
            // 次弾発射までの時間更新
            _nextTime = Time.time + _fireRate;

            // ビューポートの中心にベクターを作成
            Vector3 rayOrigin = _fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            Ray ray = new Ray(rayOrigin, _fpsCam.transform.forward);

            // 弾丸の生成
            GameObject bulletClone = Instantiate<GameObject>(_bulletPrefab);

            // 弾丸の位置を調整
            bulletClone.transform.position = _muzzle.position;

            switch (_type)
            {
                case BulletType.Normal:
                case BulletType.Gatling:
                    if (_targetPos != Vector3.zero)
                    {
                        bulletClone.GetComponent<Rigidbody>().velocity = (_targetPos - bulletClone.transform.position).normalized * _bulletSpeed;
                    }
                    break;

                case BulletType.Missile:
                    Debug.Log(this._targetPos);
                    StartCoroutine(Missile(bulletClone, this.GetComponent<NetworkRayCastShoot>(), _muzzle.position));
                    break;

                case BulletType.Laser:

                    RaycastHit hit;

                    bulletClone.GetComponent<LineRenderer>().SetPosition(0, transform.position);

                    if (Physics.Raycast(ray, out hit, _range))
                    {
                        bulletClone.GetComponent<LineRenderer>().SetPosition(1, hit.point + ray.direction);
                        if (hit.transform.tag == "Enemy")
                        {
                            hit.transform.GetComponent<NetworkStatus>().hitDamage(bulletClone.GetComponent<NetworkBulletController>().BulletDamage);
                            //object[] args1 = new object[] { bulletClone.GetComponent<NetworkBulletController>().BulletDamage };

                            //hit.transform.GetComponent<NetworkStatus>().GetComponent<PhotonView>().RPC("hitDamage", PhotonTargets.All, args1);
                        }
                    }
                    else
                    {
                        bulletClone.GetComponent<LineRenderer>().SetPosition(1, ray.origin + ray.direction * _range);
                    }
                    break;

                case BulletType.Heal:
                    // 弾をキャラの子に
                    bulletClone.transform.parent = this.transform.parent;
                    // 弾の位置を再調整
                    bulletClone.transform.position = this.transform.parent.position;
                    // 回復できるようにする
                    bulletClone.GetComponent<NetworkBulletController>().IsAttack = false;
                    break;

                case BulletType.Bit:
                    // ビットを配列に追加
                    _bits.Add(bulletClone);

                    // ビットをキャラの子に
                    bulletClone.transform.parent = this.transform.parent;

                    // ビットの位置を再調整
                    switch (_bits.Count)
                    {
                        case 1:
                            bulletClone.transform.position = this.transform.parent.position + new Vector3(-0.3f, 2.0f, 0);
                            break;
                        case 2:
                            bulletClone.transform.position = this.transform.parent.position + new Vector3(0.3f, 2.0f, 0);
                            break;
                        case 3:
                            bulletClone.transform.position = this.transform.parent.position + new Vector3(-0.5f, 1.5f, 0);
                            break;
                        case 4:
                            bulletClone.transform.position = this.transform.parent.position + new Vector3(0.5f, 1.5f, 0);
                            break;
                        default:
                            break;
                    }
                    return true;

                default:
                    break;
            }

            bulletClone.GetComponent<NetworkBulletController>().DeleteBullet(bulletClone);

            return true;
        }
        return false;
    }

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
}