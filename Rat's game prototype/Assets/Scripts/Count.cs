using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Count : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject CountObj;
    private float _countText = 0;
    
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1)
        {
            _countText += Time.deltaTime;
            CountObj.GetComponent<Text>().text = Math.Round(_countText, 2).ToString();
        }
    }
}
