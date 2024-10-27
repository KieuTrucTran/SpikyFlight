using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpForce = 8f;

    private bool hitRightwall = false;

    public int score = 0;
    public Text scoreText;
    public Text gameOverText;

    private bool isDead = false;
    private bool isRotating = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isRotating) 
        {
            transform.Rotate(0, 0, 270 * Time.deltaTime);
            return;
        }
        if(isDead) 
        {
            return;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(0, jumpForce);
        }

        if(hitRightwall)
        {
            transform.Translate(Vector2.left * (Time.deltaTime * 2));
        }
        else
        {
            transform.Translate(Vector2.right * (Time.deltaTime * 2));
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "rightwall")
        {
            hitRightwall = true;
            transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);
            score++;
            if(score > 9) 
            {
                scoreText.text = "" + score;
            }
            else 
            {
                scoreText.text = "0" + score;
            }
        }
        else {
            hitRightwall = false;
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            score++;
            if(score > 9) 
            {
                scoreText.text = "" + score;
            }
            else 
            {
                scoreText.text = "0" + score;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.tag == "spike" && isRotating)
        {
            isRotating = false;
            isDead = true;
        }
        if(collision.gameObject.tag == "spike" && !isDead) 
        {
            gameOverText.gameObject.SetActive(true);
            isRotating = true;
            rb.gravityScale = 2;
        }
    }
}
