using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkStatus : MonoBehaviour {

    //　HP
    [SerializeField]
    private float _HP = 100;

    //アニメーター
    private Animator _animator;

    [SerializeField]
    private GameObject _obj;

    private PhotonView _photonView;

    bool _deadFlag = false;

    // Use this for initialization
    void Start () {

        _animator = _obj.GetComponent<Animator>();

        _photonView = GetComponent<PhotonView>();
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

        _animator.SetTrigger("Idle");
    }

    public bool getDestroy()
    {
        return true;
    }

    //ダメージを与える処理
    public void hitDamage(int damage)
    {
        _HP -= damage;
        _animator.SetTrigger("Damage");
    }

    public float getHP()
    {
        return (float)_HP;
    }
  
}
