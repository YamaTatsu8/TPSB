using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponManager : MonoBehaviour
{
    // セレクタ
    public enum Selector
    {
        SEMI,
        AUTO,
        BURST,
    }

    // 装弾数
    public int _capacity = 30;

    // 秒間発射数
    public int _roundsPerSecond = 100;

    // 弾速
    public float _bulletSpeed = 50.0f;

    // リロードにかかる時間（秒）
    public float _reloadTime = 2.0f;

    // 射撃モード
    public Selector _mode = Selector.SEMI;

    // 撃つ弾の種類
    public BulletType _type = BulletType.Normal;

    // 弾のプレハブ
    public GameObject _bulletPrefab;

    // SEの名前
    public string _seName = "";

    // ガトリングの片割れ
    public GameObject _gatling = null;

    // ビット用管理リスト
    public List<GameObject> _bits;

    // 残弾数
    private int _remainingBullets = 0;

    // 発射間隔
    private float _fireRate;

    // 弾を撃てるか
    private bool _isShot = true;

    // バースト射撃できるか
    private bool _isBurst = true;

    // 銃口
    private Transform _muzzle;

    private IEnumerator _routine;

    // コントローラー
    private GameController _con;

    // オーディオマネージャー
    private AudioManager _audioManager;

    public int RemainingBullets
    {
        get { return _remainingBullets; }
    }

    public float FireRate
    {
        get { return _fireRate; }
    }

    public Transform Muzzle
    {
        get { return _muzzle; }
    }
   
    private void Awake()
    {
        _fireRate = 1.0f / _roundsPerSecond;

        //_muzzle = this.transform.GetChild(0).transform;
        _muzzle = this.transform.Find("Muzzle").transform;

        _remainingBullets = _capacity;

        _con = GameController.Instance;

        _audioManager = AudioManager.Instance;
    }

    private void Start()
    {
        _isBurst = true;
        _isShot = true;

        if (_routine != null)
        {
            StopCoroutine(_routine);
        }
    }

    private void Update()
    {
        _con.ControllerUpdate();

        if (!_isShot)
        {
            if (!_con.CheckUseTrigger())
            {
                _isShot = true;
            }
        }

        if (_type == BulletType.Bit)
        {
            for (int i = _bits.Count - 1; i >= 0; i--)
            {
                if (_bits[i] == null)
                {
                    _bits.Remove(_bits[i]);
                }
            }

            if (_remainingBullets == 0 && _bits.Count == 0)
            {
                Reload();
            }
        }
    }

    private void OnEnable()
    {
        if (_routine != null)
        {
            StartCoroutine(_routine);
        }

        _isBurst = true;
    }

    private void OnDisable()
    {
        if (_routine != null)
        {
            StopCoroutine(_routine);
        }
    }

    public void Attack()
    {
        if(_type == BulletType.Bit)
        {
            if (_isShot && _remainingBullets != 0)
            {
                Shot(0.0f);
                _isShot = false;
            }
        }
        else if (_remainingBullets == 0)
        {
            Reload();
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
                        if (_gatling != null)
                        {
                            _gatling.GetComponent<WeaponManager>().Attack();
                        }
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
        if (_remainingBullets < _capacity && _routine == null)
        {
            this.transform.Find("WeaponUI").GetComponent<DisplayData>().IsReloading = true;

            _isBurst = true;

            _routine = this.DelayMethodForSpecifiedTime(_reloadTime, () =>
            {
                _remainingBullets = _capacity;

                _routine = null;
            });
            StartCoroutine(_routine);
        }
    }

    private void Shot(float fireRate)
    {
        if (this.GetComponent<RayCastShoot>().Shot(fireRate))
        {
            if (_seName != "")
            {
                _audioManager.PlaySE(_seName);
            }

            _remainingBullets--;
        }
    }

    private void BurstShot()
    {
        if (_isBurst)
        {
            _isBurst = false;
            _routine = this.DelayMethod(0, () =>
            {
                Shot(_fireRate);
                this.Delay(1.0f / _roundsPerSecond, () =>
                {
                    if (_remainingBullets > 0)
                    {
                        Shot(_fireRate);
                        this.Delay(1.0f / _roundsPerSecond, () =>
                        {
                            if (_remainingBullets > 0)
                            {
                                Shot(0.0f);
                                this.Delay(1.0f / _roundsPerSecond, () =>
                                {
                                    _isBurst = true;
                                });
                            }
                        });
                    }                    

                });
                _routine = null;
            });
            StartCoroutine(_routine);
        }
    }
}