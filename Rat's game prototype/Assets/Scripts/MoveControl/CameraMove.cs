using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + 4, transform.position.y, transform.position.z);
    }
}
