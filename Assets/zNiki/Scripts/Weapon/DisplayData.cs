using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayData : MonoBehaviour
{
    private bool _isReloading = false;

    // 経過した時間
    private float _elapsedTime;

    // リロードにかかる時間
    private float _reloadTime;

    // 装弾数表示用テキスト
    [SerializeField]
    private Text _capacityText;

    // 残弾数表示用テキスト
    [SerializeField]
    private Text _remainingBulletsText;

    // リロード状況表示用スライダー
    [SerializeField]
    private Slider _reloadTimeSlider;

    // 武器マネージャー保持変数
    private WeaponManager _parentWeaponManager;

    public bool IsReloading
    {
        set { _isReloading = value; }
    }

    // Use this for initialization
    void Start ()
    {
        _parentWeaponManager = GetComponentInParent<WeaponManager>();

        _reloadTime = _parentWeaponManager.ReloadTime;

        _capacityText.text = _parentWeaponManager.Capacity.ToString();

        _remainingBulletsText.text = _parentWeaponManager.RemainingBullets.ToString();
    }
	
	// Update is called once per frame
	void Update ()
    {
        _remainingBulletsText.text = _parentWeaponManager.RemainingBullets.ToString();

        if (_isReloading)
        {
            Reload();
        }
    }

    private void Reload()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _reloadTime)
        {
            _elapsedTime = 0.0f;

            _isReloading = false;
        }

        _reloadTimeSlider.value = _elapsedTime / _reloadTime;
    }
}
