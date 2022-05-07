using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public float speed;
    private void Start()
    {
        speed = 10;
    }
    private void Update()
    {
        Debug.Log(speed);
        if (speed < 14 && Time.timeScale == 1)
        {
            speed += 0.001f;
        }
    }
    public List<GameObject> prefabs = new List<GameObject>();
    public GameObject GetPrefub() {
        return prefabs[Random.Range(0, prefabs.Count)];
    }
}
