using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomWeapon : MonoBehaviour {

    //武器一覧
    [SerializeField]
    private GameObject[] _weapon;

    // Use this for initialization
    void Start () {


        
	}
	
	// Update is called once per frame
	void Update () {
		

       
	}

    public void setWeapon(string name)
    {
        for(int i = 0; i < 5; i++)
        {
            if(_weapon[i].name == name)
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
