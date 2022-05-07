using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    public GameObject GroundManager;
    private Vector2 direction = new Vector2(-0.2f, 0);
    private float _upperXValue = 26;
    private float _destroyXValue = -26;
    private float speed;
    private GameObject _newpipe, _newpiperand;
    private bool _Instant = false;
    
    private void Start() {
        _newpipe = GroundManager.GetComponent<GroundManager>().GetPrefub();
        speed = GroundManager.GetComponent<GroundManager>().speed;
    }
    private void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, 0);
    }
    private void Update() {
        speed = GroundManager.GetComponent<GroundManager>().speed;
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
