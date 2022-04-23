using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public List<GameObject> prefabs = new List<GameObject>();
    public GameObject GetPrefub() {
        return prefabs[Random.Range(0, prefabs.Count)];
    }
}
