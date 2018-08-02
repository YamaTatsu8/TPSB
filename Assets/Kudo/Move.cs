using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    private float _speed = 0.01f;
    private int _count = 0;

    void Start () {
		
	}
	
	void Update () {

        //int num = 0;
        //_count++;
        //if (_count == 120)
        //{
        //    num = Random.Range(0, 4);
        //    Debug.Log(num);
        //    _count = 0;
        //}


        //switch (num)
        //{
        //    case 0:
        //        transform.position += transform.forward * _speed;
        //        Debug.Log("now" + num);
        //        break;
        //    case 1:
        //        transform.position -= transform.forward * _speed;
        //        Debug.Log("now" + num);
        //        break;
        //    case 2:
        //        transform.position += transform.right * _speed;
        //        Debug.Log("now" + num);
        //        break;
        //    case 3:
        //        transform.position -= transform.right * _speed;
        //        Debug.Log("now" + num);
        //        break;
        //    default:
        //        break;
        //}

        if (transform.position.y < -10)
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }
}
