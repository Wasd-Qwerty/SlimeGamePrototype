using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour, IPointerDownHandler
{
    public GameManager gameManager;
    public GameObject Coins, Tutorial, GameOverUI;
    private Rigidbody2D _rb;
    public float jumpForce = 40f;
    private int _coins;
    public bool onGround, resume;
    public Transform groundCheck;
    public float checkRadius = 0.5f;
    public LayerMask Ground;
    public Animator anim;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _coins = PlayerPrefs.GetInt("coins", _coins);
        Coins.GetComponent<Text>().text = _coins.ToString();
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
            _rb.velocity = new Vector2(0,jumpForce);
            
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (onGround)
        {
            Tutorial.SetActive(false);
            _rb.velocity = new Vector2(0, jumpForce);

        }
    }
    void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, Ground);
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
        if (collision.gameObject.tag == "ObjectResp")
        {
            gameManager.GameOver();
            Destroy(collision.gameObject);
        }
    }
    public void ResumePlay()
    {
        resume = true;
        transform.position = new Vector2(-7, 7);
        Time.timeScale = 1;
        GameOverUI.SetActive(false);
    }
}
