using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpForce = 8f;

    public bool hitRightwall = false;

    public int score = 0;
    public Text scoreText;
    public Text gameOverText;
    public GameObject playAgain;
    public Text title;

    private bool isDead = false;
    private bool isRotating = false;

    private bool isStarted = false;

    private LogicScript logicManager;
    public GameObject logicManagerObject;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        logicManager = logicManagerObject.GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isStarted == false) 
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
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
            isStarted = true;
            rb.velocity = new Vector2(0, jumpForce);
            rb.constraints = RigidbodyConstraints2D.None;
        }

        if(isStarted) {
            if(hitRightwall)
            {
                transform.Translate(Vector2.left * (Time.deltaTime * 2.2f));
            }
            else
            {
                transform.Translate(Vector2.right * (Time.deltaTime * 2.2f));
            }
        }
    }

    public int getScore() 
    {
        return score;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "rightwall")
        {
            hitRightwall = true;
            transform.localScale = new Vector3(-0.6f, 0.6f, 0.6f);
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
            transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
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
            playAgain.SetActive(true);
            title.gameObject.SetActive(true);
            isRotating = true;
            rb.gravityScale = 2;
            logicManager.gameOver();
        }
    }

    void OnCollisionStay2D(Collision2D collision) {
        if(collision.gameObject.tag == "bottom" && !isDead) 
        {
            isDead = true;
            isRotating = false;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            logicManager.gameOver();
        }
    }
}
