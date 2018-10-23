using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // セレクタ
    private enum Selector
    {
        SEMI,
        AUTO,
        BURST,
    }

    // 変数 ----------------------------------------

    // 装弾数
    [SerializeField]
    private int _capacity = 30;

    // 残弾数
    private int _remainingBullets = 0;

    // 秒間発射数
    [SerializeField]
    private int _roundsPerSecond = 100;

    // 発射間隔
    private float _fireRate;

    // リロードにかかる時間（秒）
    [SerializeField]
    private float _reloadTime = 2.0f;

    // 射撃モード
    [SerializeField]
    private Selector _mode = Selector.AUTO;

    // 弾を撃てるか
    private bool _isShot = true;

    // バースト射撃できるか
    private bool _isBurst = true;

    // 銃口
    private Transform _muzzle;

    private GameController _con;

    // 弾のプレハブ
    [SerializeField]
    private GameObject _bulletPrefab;

    //[SerializeField]
    //private AudioClip _shotSound;

    //private AudioSource _gunAudio;

    // プロパティ -----------------------------------

    public int RoundsPerSecond
    {
        get { return _roundsPerSecond; }
    }

    public Transform Muzzle
    {
        get { return _muzzle; }
    }

    public GameObject BulletPrefab
    {
        get { return _bulletPrefab; }
        set { _bulletPrefab = value; }
    }

    // 関数 ----------------------------------------

    private void Awake()
    {
        _fireRate = 1.0f / _roundsPerSecond;

        _muzzle = this.transform.GetChild(0).transform;

        _remainingBullets = _capacity;

        _con = GameController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        _con.ControllerUpdate();

        if (!_isShot)
        {
            if (!_con.CheckUseTrigger())
            {
                _isShot = true;
            }
        }
    }

    public void Attack()
    {
        if (_remainingBullets == 0)
        {
            this.Reload();
        }
        else
        {
            if (_isShot)
            {
                switch (_mode)
                {
                    case Selector.SEMI:
                        Shot(0.0f);
                        _isShot = false;
                        break;
                    case Selector.AUTO:
                        Shot(_fireRate);
                        break;
                    case Selector.BURST:
                        BurstShot();
                        _isShot = false;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void Reload()
    {
        if (_remainingBullets < 30)
        {
            this.DelayOnce(_reloadTime, () =>
            {
                _remainingBullets = _capacity;

                // UIを表示する

            });
        }
    }

    private void Shot(float fireRate)
    {
        if (this.GetComponent<RayCastShoot>().Shot(fireRate))
        {
            _remainingBullets--;
        }
    }

    private void BurstShot()
    {
        if (_isBurst)
        {
            _isBurst = false;
            this.Delay(0, () =>
            {
                Shot(_fireRate);
                this.Delay(1.0f / _roundsPerSecond, () =>
                {
                    Shot(_fireRate);
                    this.Delay(1.0f / _roundsPerSecond, () =>
                    {
                        Shot(0.0f);
                        this.Delay(1.0f / _roundsPerSecond, () =>
                        {
                            _isBurst = true;
                        });
                    });
                });
            });
        }
    }
}