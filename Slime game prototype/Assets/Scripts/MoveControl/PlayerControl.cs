﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour, IPointerDownHandler
{
    public GameManager gameManager;
    public GameObject Coins, Tutorial, GameOverUI;
    private Rigidbody2D _rb;
    private float _jumpForce = 40f, _checkRadius = 0.1f;
    private int _coins;
    public bool onGround, resume;
    public Transform groundCheck;
    public LayerMask Ground;
    public Animator anim;

    private AudioSource _audiosource;
    public AudioClip onGrounded;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _coins = PlayerPrefs.GetInt("coins", _coins);
        Coins.GetComponent<Text>().text = _coins.ToString();
        _audiosource = GetComponent<AudioSource>();
    }
    void Update()
    {
        CheckingGround();
        if (onGround)
        {
            anim.SetBool("onGround", true);
        }
        else
        {
            anim.SetBool("onGround", false);
        }
    }
    public void Jump()
    {
        if (onGround && Time.timeScale == 1)
        {
            Tutorial.SetActive(false);
            _rb.velocity = new Vector2(0,_jumpForce);
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (onGround)
        {
            Tutorial.SetActive(false);
            _rb.velocity = new Vector2(0, _jumpForce);

        }
    }
    public void onGroundedPlay()
    {
        _audiosource.PlayOneShot(onGrounded);
    }
    void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(groundCheck.position, _checkRadius, Ground);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.tag == "Coin")
        {
            _coins += 1;
            Coins.GetComponent<Text>().text = _coins.ToString();
            PlayerPrefs.SetInt("coins", _coins);
            PlayerPrefs.Save();
            Destroy(collision.gameObject);
        }*/
        if (collision.gameObject.tag == "ObjectResp")
        {
            gameManager.GameOver();
            Destroy(collision.gameObject);
        }
    }
}
