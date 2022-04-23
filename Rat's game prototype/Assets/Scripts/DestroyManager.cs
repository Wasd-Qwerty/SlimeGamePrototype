using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyManager : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.x <= -50)
        {
            Destroy(gameObject);
        }
    }
}
