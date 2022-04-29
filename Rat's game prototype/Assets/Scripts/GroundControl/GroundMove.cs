using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    public GameObject GroundManager;
    public GameObject Ground;
    private float _speed = 8;
    private float _lowerXValue = -1.5f;
    private float _upperXValue = 24.44f;
    private float _destroyXValue = -50;
    private GameObject _newpipe, _newpiperand;
    private bool _Instant = false;
    
    private void Start() {
        _newpipe = GroundManager.GetComponent<GroundManager>().GetPrefub();
    }
    private void Update() {
        transform.position = new Vector3(transform.position.x - _speed * Time.deltaTime, 0,0);
        if (transform.position.x <= _lowerXValue && !_Instant)
        {
            _newpiperand = Instantiate(_newpipe);
            _newpiperand.transform.position = new Vector3(_upperXValue, 0, 0);
            _newpiperand.GetComponent<GroundMove>().enabled = true;
            _Instant = true;
        }
        if (transform.position.x <= _destroyXValue)
        {
            Destroy(gameObject);
        }
    }
}
