﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public GameManager gameManager;
    private Rigidbody2D _rb;
    public GameObject Coins;
    private int _coins;
    public bool onGrounded;
    public Animator anim;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _coins = PlayerPrefs.GetInt("coins", _coins);
        Coins.GetComponent<Text>().text = _coins.ToString();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGrounded || Input.touchCount > 0 && onGrounded)
        {
            _rb.velocity = Vector2.up * 40f;
            anim.SetBool("isGround", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            _coins += 1;
            Coins.GetComponent<Text>().text = _coins.ToString();
            PlayerPrefs.SetInt("coins", _coins);
            PlayerPrefs.Save();
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ObjectResp")
        {
            gameManager.GameOver();
        }
        if (collision.gameObject.tag == "Ground")
        {
            anim.SetBool("isGround", true);
            onGrounded = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            
            onGrounded = false;
        }
    }
}
