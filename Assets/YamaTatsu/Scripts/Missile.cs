using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{


    //敵
    private GameObject _player;

    //タイマー
    private float _timer;

    //発射フラグ
    private bool _flag = true;

    // Use this for initialization
    void Start()
    {

        _player = GameObject.Find("Enemy");

        _timer = 0;

    }

    // Update is called once per frame
    void Update()
    {

        if (_flag == true)
        {
            if (_timer > 0.4)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((_player.transform.position + new Vector3(0,-4,0)) - transform.position), Time.deltaTime * 12);

                Vector3 front = transform.TransformDirection(Vector3.forward);

                this.GetComponent<Rigidbody>().AddForce(front * 50.0f, ForceMode.Force);

            }
            else if (0.2 > _timer)
            {
                this.GetComponent<Rigidbody>().AddForce(this.transform.up * 1000 * Time.deltaTime, ForceMode.Force);
            }

            _timer += Time.deltaTime;

        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
        else
        {
            
        }
    }

    public void Shot()
    {
        _flag = true;
    }

}
