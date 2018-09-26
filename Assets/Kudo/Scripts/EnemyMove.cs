using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    public float _speed;

    void Start () {
		
	}
	
	void Update () {

        if (Input.GetKey("up"))
        {
            transform.position += transform.forward * _speed;
        }
        if (Input.GetKey("down"))
        {
            transform.position -= transform.forward * _speed;
        }
        if (Input.GetKey("right"))
        {
            transform.position += transform.right * _speed;
        }
        if (Input.GetKey("left"))
        {
            transform.position -= transform.right * _speed;
        }

        if (transform.position.y < -10)
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }
}
