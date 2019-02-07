using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomWeapon : MonoBehaviour {

    //武器一覧
    [SerializeField]
    private GameObject[] _weapon;

    //アニメーター
    private Animator _animator;

    // Use this for initialization
    void Start () {

        _animator = this.gameObject.GetComponent<Animator>();

        _weapon[0] = GameObject.Find("SMG");

        _weapon[1] = GameObject.Find("Laser");

        _weapon[2] = GameObject.Find("Rifle");

        _weapon[3] = GameObject.Find("Rifle2");

        for(int i = 0; i < 4; i++)
        {
            _weapon[i].SetActive(false);
        }

    }
	
	// Update is called once per frame
	void Update () {
		

       
	}

    //ポーズのアニメーションに設定
    public void SetPose(string name)
    {
        _animator.SetBool("Pose", true);
        for (int i = 0; i < 4; i++)
        {
            Debug.Log(_weapon[i]);

            if (_weapon[i].name == name)
            {
                _weapon[i].SetActive(true);
            }
            else
            {
                _weapon[i].SetActive(false);
            }
        }
    }

}
