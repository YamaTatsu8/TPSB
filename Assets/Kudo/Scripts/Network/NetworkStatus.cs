using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class NetworkStatus : Photon.MonoBehaviour {

    //　HP
    [SerializeField]
    private float _HP = 100;

    //アニメーター
    private Animator _animator;

    [SerializeField]
    private GameObject _obj;

    private PhotonView _photonView;

    private GameObject _hitObj;
    private bool _isEnemy;
    private int _hitNum;
    private string _hitStr;
    private float _time;

    bool _deadFlag = false;

    [SerializeField]
    private GameObject _canvas;

    public float HP
    {
        get
        {
            return _HP;
        }
        set
        {
            _HP = value;
        }
    }

    // Use this for initialization
    void Start () {

        _animator = _obj.GetComponent<Animator>();

        if (this.gameObject.name == "TrainingEnemy")
        {
            _isEnemy = true;
            _hitNum = 0;
            _time = 3.0f;
            _hitStr = " H I T";
            _hitObj = GameObject.Find("HitCnt");
            _hitObj.SetActive(false);
        }
        else { _isEnemy = false; }

        _photonView = GetComponent<PhotonView>();

        if(_photonView.isMine)
        {
            _canvas.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
	    
        // HPが0以下の場合破壊
        if(!_deadFlag && _HP < 0)
        {
            _deadFlag = true;
            NetworkPlayScene.Instance.Search(_photonView.isMine);

            Destroy(this.gameObject);
        }

        if (_HP >= 100)
        {
            _HP = 100;
        }

        if (_hitObj == null) { return; }
        if (!_hitObj.activeSelf) { return; }
        _time -= Time.deltaTime;
        if (_time <= 0.0f)
        {
            _hitNum = 0;
            _time = 3.0f;
            _HP = 100;
            _hitObj.SetActive(false);
        }

        _animator.SetTrigger("Idle");
    }

    public bool getDestroy()
    {
        return true;
    }

    //ダメージを与える処理
    public void hitDamage(int damage)
    {
        //　ターゲットが敵の場合
        if (_isEnemy)
        {
            if (!_hitObj.activeSelf) { _hitObj.SetActive(true); }
            _hitNum++;
            _hitObj.GetComponent<Text>().text = _hitNum.ToString() + _hitStr;
            _time = 1.0f;

            float hp = _HP;
            hp -= damage;
            if (hp >= 1)
            {
                _HP -= damage;
                SetShereHP(_HP);
            }
        }
        else
        {
            _HP -= damage;
            SetShereHP(_HP);
        }
        //_animator.SetTrigger("Damage");
    }

    public void RecoveryHP(int heal)
    {
        Debug.Log("回復");
        _HP += heal;
        SetShereHP(_HP);
    }

    public float getHP()
    {
        return (float)_HP;
    }

    public void SetShereHP(float num)
    {
        if (!_photonView.isMine)
        {
            return;
        }

        var properties = new ExitGames.Client.Photon.Hashtable();
        properties.Add("Number", num);

        PhotonNetwork.player.SetCustomProperties(properties);

    }

    public void OnPhotonPlayerPropertiesChanged(object[] i_playerAndUpdatedProps)
    {

        var player = i_playerAndUpdatedProps[0] as PhotonPlayer;
        var properties = i_playerAndUpdatedProps[1] as ExitGames.Client.Photon.Hashtable;

        object flagvalue = null;
        if (properties.TryGetValue("Number", out flagvalue))
        {
            float receiveNum = (float)flagvalue;
            var playerObjects = GameObject.FindGameObjectsWithTag("Player");
            var playerObject = playerObjects.FirstOrDefault(obj => obj.GetComponent<PhotonView>().ownerId == player.ID);

            playerObject.GetComponent<NetworkStatus>().HP = receiveNum;

            return;
        }
    }

}
