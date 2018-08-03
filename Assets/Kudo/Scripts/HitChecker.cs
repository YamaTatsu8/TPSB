using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChecker : MonoBehaviour {

    private bool _isPlayerHit = false;
    private bool _isWallHit = false;

    private int _playerHitCount = 0;
    private int _wallHitCount = 0;

    public bool PlayerHIt
    {
        get
        {
            return _isPlayerHit;
        }
        set
        {
            _isPlayerHit = value;
        }
    }

    public bool WallHit
    {
        get
        {
            return _isWallHit;
        }
        set
        {
            _isWallHit = value;
        }
    }

    void Start () {
		
	}
	
	void Update () {
        if(_isPlayerHit)
        {
            _playerHitCount++;
            if(_playerHitCount == 180)
            {
                _isPlayerHit = false;
                _playerHitCount = 0;
            }
        }
        if (_isWallHit)
        {
            _wallHitCount++;
            if (_wallHitCount == 300)
            {
                _isWallHit = false;
                _wallHitCount = 0;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Wall")
        {
            _isWallHit = true;
        }

        if (other.gameObject.tag == "Player")
        {
            _isPlayerHit = true;
        }
    }
}
