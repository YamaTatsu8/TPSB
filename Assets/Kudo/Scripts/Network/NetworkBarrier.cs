using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkBarrier : MonoBehaviour {

    //コントローラー
    GameController controller;

    private GameObject _object;

    //バるあフラグ
    private bool _flag = false;

    //生成してるかどうか
    private bool _reFlag = false;

    //リミット
    [SerializeField]
    private float _limitTime = 2.0f;

    //HP
    [SerializeField]
    private int _HP = 10;

    //アニメーター
    private Animator _animator;

<<<<<<< HEAD
    [SerializeField]
    private GameObject _obj;

=======
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
    //Guardのオブジェクト
    [SerializeField]
    private GameObject _guard;

    //展開
    private bool _barrier = false;

<<<<<<< HEAD
    // -PhotonView
=======
    // -ネットワーク
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
    private PhotonView _photonView;

    // Use this for initialization
    void Start () {

<<<<<<< HEAD
        controller = GameController.Instance;

        _animator = _obj.GetComponent<Animator>();
=======
        Debug.Log(_guard);

        controller = GameController.Instance;

        _animator = GetComponent<Animator>();
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f

        _guard = GameObject.Find("Guard");

        _guard.SetActive(false);

<<<<<<< HEAD
        // -PhotonViewのコンポーネント
=======
        // -ネットワーク
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
        _photonView = GetComponent<PhotonView>();
    }
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
        // -自身でなかったらreturn
        if(!_photonView.isMine)
        {
            return;
        }
=======

>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f

        GameObject target = GameObject.FindGameObjectWithTag("Player");

        controller.ControllerUpdate();

<<<<<<< HEAD
        //左ショルダーボタンが押された時
        if (Input.GetButton("L1"))
        {
            _guard.SetActive(true);
            _animator.SetBool("Guard", true);
            transform.LookAt(target.transform, Vector3.up);
            _guard.GetComponent<BoxCollider>().enabled = true;
            _barrier = true;
        }
        else
        {
            _guard.SetActive(false);
            _animator.SetBool("Guard", false);
            _guard.GetComponent<BoxCollider>().enabled = false;
            _barrier = false;
        }

        // 0になったらバリアを消す       
        if(_HP <= 0)
=======
        // -誰がボタンを押したかをチェック
        if(_photonView.isMine)
        {
            //左ショルダーボタンが押された時
            if (Input.GetButton("L1"))
            {
                _guard.SetActive(true);
                _animator.SetBool("Guard", true);
                transform.LookAt(target.transform, Vector3.up);
                _guard.GetComponent<BoxCollider>().enabled = true;
                _barrier = true;
            }
            else
            {
                _guard.SetActive(false);
                _animator.SetBool("Guard", false);
                _guard.GetComponent<BoxCollider>().enabled = false;
                _barrier = false;
            }

        }

        // 0になったらバリアを消す       
        if (_HP <= 0)
>>>>>>> 302a37d95035faead75c65fed7201c2371f53c1f
        {
            Destroy(_object);
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "EnemyBullet" && _barrier == true)
        {
            Debug.Log("Barrier");
            _HP -= 1;
        }
    }

}
