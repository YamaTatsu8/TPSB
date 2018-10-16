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

    private int _remainingBullets = 0;

    // 秒間発射数
    [SerializeField]
    private int _roundsPerSecond = 10;

    // リロードにかかる時間（秒）
    [SerializeField]
    private float _reloadTime = 2.0f;

    // 射撃モード
    [SerializeField]
    private Selector _mode = Selector.SEMI;

    // 弾を撃てるか
    private bool _isShot = true;

    // 銃口
    private Transform _muzzle;

    // 弾のプレハブ
    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private AudioClip _shotSound;

    private AudioSource _gunAudio;

    Coroutine coroutine;

    // プロパティ -----------------------------------

    //public int Capacity
    //{
    //    get { return _capacity; }
    //    set { _capacity = value; }
    //}

    public int RoundsPerSecond
    {
        get { return _roundsPerSecond; }
    }

    public float ReloadTime
    {
        get { return _reloadTime; }
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
        _muzzle = this.transform.GetChild(0).transform;

        _remainingBullets = _capacity;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Shot()
    {

        if (_remainingBullets == 0)
        {
            coroutine =  this.DelayOnce(_reloadTime, () =>
            {
                this.Reload();
            });
        }
        else
        {
            if (_isShot)
            {
                if (_mode != Selector.AUTO)
                {
                    _isShot = false;
                }
                if (this.GetComponent<RayCastShoot>().Shot())
                {
                    _remainingBullets--;
                    Debug.Log(_remainingBullets);
                }

            }
        }
    }

    public void Reload()
    {
        Debug.Log("Reload!");
        _remainingBullets = _capacity;

        // UIを表示する
    }
}