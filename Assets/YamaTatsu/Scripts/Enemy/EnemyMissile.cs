using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour {

    //プレイヤー
    [SerializeField]
    private GameObject _player;

    //エフェクト
    [SerializeField]
    private GameObject _effect;

    //タイマー
    private float _timer;

    //発射フラグ
    private bool _flag = true;

    //ロックフラグ
    private bool _lockFlag = false;

    // Use this for initialization
    void Start () {

        _player = GameObject.Find("Player");

        _timer = 0.0f;

	}
	
	// Update is called once per frame
	void Update () {


        //if(_lockFlag == false)
        //{
        //    _lockFlag = true;
        //    _player = GameObject.Find("Player");
        //}

        if (_flag == true)
        {
            if (_timer > 0.4)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((_player.transform.position + new Vector3(0, -2, 0)) - transform.position), Time.deltaTime * 100);

                Vector3 front = transform.TransformDirection(Vector3.forward);

                this.GetComponent<Rigidbody>().AddForce(front * 50.0f, ForceMode.Force);

            }
            else if (0.2 > _timer)
            {
                this.GetComponent<Rigidbody>().AddForce(this.transform.up * 1000 * Time.deltaTime, ForceMode.Force);
            }

            _timer += Time.deltaTime;

            if (4.0 < _timer)
            {
                Instantiate(_effect, this.transform.position, this.transform.rotation);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(_effect, this.transform.position, this.transform.rotation);

            collision.gameObject.GetComponent<Status>().hitDamage(10);

            Destroy(this.gameObject);
        }
        else
        {
            Instantiate(_effect, this.transform.position, this.transform.rotation);

            Destroy(this.gameObject);
        }
    }

    public void Shot()
    {
        _flag = true;
    }


}
