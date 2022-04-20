using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameManager gameManager;
    private Rigidbody2D rb;

    public bool isGrounded;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = Vector2.up * 40f;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Коллизия с " + collision.gameObject.tag);
        if (collision.gameObject.tag == "ObjectResp")
            gameManager.GameOver();
        if (collision.gameObject.tag == "Ground")
            isGrounded = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = false;
    }
}
