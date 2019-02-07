using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public class NetworkWeaponManager : MonoBehaviour
=======
public class NetworkWeaponManager : Photon.MonoBehaviour
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
{
    // セレクタ
    private enum Selector
    {
        SEMI,
        AUTO,
        BURST,
    }

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

<<<<<<< HEAD
    // 撃つ弾の種類
    [SerializeField]
    private BulletType _type = BulletType.Normal;

=======
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
    // 弾を撃てるか
    private bool _isShot = true;

    // バースト射撃できるか
    private bool _isBurst = true;

    // 銃口
    private Transform _muzzle;

<<<<<<< HEAD
    // 弾のプレハブ
    [SerializeField]
    private GameObject _bulletPrefab;

    // SEの名前
    [SerializeField]
    private string _seName = "";
    
    private IEnumerator _routine;

    // コントローラー
    private GameController _con;

    // オーディオマネージャー
    private AudioManager _audioManager;

    // -PhotonView
    private PhotonView _photonView;
    // -UIを武器がActive時に表示するようにする
=======
    // コントローラー
    private GameController _con;

    // 弾のプレハブ
    [SerializeField]
    private GameObject _bulletPrefab;
    
    private IEnumerator _routine;

    // -ネットワーク
    private PhotonView _photonView;
    // -UI
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
    [SerializeField]
    private GameObject _weaponUI;

    public int Capacity
    {
        get { return _capacity; }
    }

    public int RemainingBullets
    {
        get { return _remainingBullets; }
    }

    public float ReloadTime
    {
        get { return _reloadTime; }
    }

    public int RoundsPerSecond
    {
        get { return _roundsPerSecond; }
    }

    public Transform Muzzle
    {
        get { return _muzzle; }
    }

<<<<<<< HEAD
    public BulletType Type
    {
        get { return _type; }
    }

=======
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
    public GameObject BulletPrefab
    {
        get { return _bulletPrefab; }
        set { _bulletPrefab = value; }
    }
    
    private void Awake()
    {
        _fireRate = 1.0f / _roundsPerSecond;

        _muzzle = this.transform.GetChild(0).transform;

        _remainingBullets = _capacity;

        _con = GameController.Instance;

<<<<<<< HEAD
        _audioManager = AudioManager.Instance;

=======
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
        _photonView = GetComponent<PhotonView>();
        if(_photonView.isMine)
        {
            _weaponUI.SetActive(true);
        }
    }
<<<<<<< HEAD

    private void Start()
    {
        _isBurst = true;
        _isShot = true;

        if (_routine != null)
        {
            StopCoroutine(_routine);
        }
    }

=======
    
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
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
                        object[] args1 = new object[] { 0.0f };
                        _photonView.RPC("Shot", PhotonTargets.All, args1);
                        //Shot(0.0f);
                        _isShot = false;
                        break;
                    case Selector.AUTO:
                        object[] args2 = new object[] { _fireRate };
                        _photonView.RPC("Shot", PhotonTargets.All, args2);
                        //Shot(_fireRate);
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
            this.transform.GetChild(1).GetComponent<NetworkDisplayData>().IsReloading = true;

            _isBurst = true;

            _routine = this.DelayMethodForSpecifiedTime(_reloadTime, () =>
            {
<<<<<<< HEAD
=======
                Debug.Log("Reload");
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
                _remainingBullets = _capacity;

                _routine = null;
            });
            StartCoroutine(_routine);
        }
    }

    [PunRPC]
    private void Shot(float fireRate)
    {
        if (this.GetComponent<NetworkRayCastShoot>().Shot(fireRate))
        {
<<<<<<< HEAD
            if (_seName != "")
            {
                _audioManager.PlaySE(_seName);
            }

=======
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
            _remainingBullets--;
        }
    }

    private void BurstShot()
    {
        if (_isBurst)
        {
            _isBurst = false;
<<<<<<< HEAD
            _routine = this.DelayMethod(0, () =>
=======
            _routine = this.DelayMethodOnce(0, () =>
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
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