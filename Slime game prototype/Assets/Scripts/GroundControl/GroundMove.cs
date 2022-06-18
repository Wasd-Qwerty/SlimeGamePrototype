using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    public GameObject GroundManager;
    private float _upperXValue = 26;
    private float _destroyXValue = -26;
    public float _speed;
    private GameObject _newpipe;
    private bool _Instant = false;
    
    private void Awake() {
        _newpipe = GroundManager.GetComponent<GroundManager>().GetPrefub();
        _speed = GroundManager.GetComponent<GroundManager>().speed;
    }
    private void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x - _speed * Time.deltaTime, 0);
    }
    private void Update() {
        _speed = GroundManager.GetComponent<GroundManager>().speed;
        if (transform.position.x <= 0 && !_Instant)
        {
            _newpipe = Instantiate(_newpipe);
            _newpipe.transform.position = new Vector3(transform.position.x + _upperXValue, 0, 0);
            _newpipe.GetComponent<GroundMove>().enabled = true;
            _Instant = true;
        }
        if (transform.position.x <= _destroyXValue)
        {
            Destroy(gameObject);
        }
    }
}
