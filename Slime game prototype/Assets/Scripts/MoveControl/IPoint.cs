using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IPoint : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject player;
    bool _hold;
    public void OnPointerDown(PointerEventData eventData)
    {
        _hold = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _hold = false;
    }
    private void Update()
    {
        if (_hold)
        {
            player.GetComponent<PlayerControl>().Jump();
        }
    }
}
