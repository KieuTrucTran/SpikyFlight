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
    private int bonbonScore = 0;
    public Text scoreText;
    public Text gameOverText;
    public GameObject playAgain;
    public Text title;
    public Text highScoreText;
    public Text bonbonScoreText;
    public GameObject bonbonImage;
    public Text tapToJump;

    private bool isDead = false;
    private bool isRotating = false;

    private bool isStarted = false;

    private bool isBonbonCollected = false;

    private bool firstBonbonSpawned = false;

    private LogicScript logicManager;
    public GameObject logicManagerObject;

    public GameObject bonbon1;
    public GameObject bonbon2;
    public GameObject bonbon3;

    public GameObject audioSource;
    AudioPlayer audioPlayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        title.gameObject.SetActive(true);
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highscore").ToString();
        bonbonScoreText.text = PlayerPrefs.GetInt("bonbonScore").ToString();

        tapToJump.gameObject.SetActive(true);
        logicManager = logicManagerObject.GetComponent<LogicScript>();

        audioPlayer = audioSource.GetComponent<AudioPlayer>();
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
            if(score > PlayerPrefs.GetInt("highscore"))
            {
                PlayerPrefs.SetInt("highscore", score);
                highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highscore").ToString();
                bonbonScoreText.text = PlayerPrefs.GetInt("bonbonScore").ToString();
            }
            return;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isStarted = true;
            rb.velocity = new Vector2(0, jumpForce);
            rb.constraints = RigidbodyConstraints2D.None;
        }

        if(isStarted) {
            title.gameObject.SetActive(false);
            highScoreText.gameObject.SetActive(false);
            bonbonScoreText.gameObject.SetActive(false);
            bonbonImage.gameObject.SetActive(false);
            tapToJump.gameObject.SetActive(false);
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
        if (collision.gameObject.tag.Contains("bonbon"))
        {
            audioPlayer.PlayBonbonSound();   
            isBonbonCollected = true;
            print("Bonbon collision");
            Destroy(collision.gameObject);
            int bonbonValue = collision.gameObject.tag[7] - '0';
            bonbonScore += bonbonValue;
            print("Bonbon Score: " + bonbonScore);
        }
        if(collision.gameObject.tag == "rightwall")
        {
            hitRightwall = true;

            //spawn first bonbon
            if(!firstBonbonSpawned)
            {
                spawnBonbon();
                firstBonbonSpawned = true;
            }

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

            if (isBonbonCollected)
            {
                spawnBonbon();
                isBonbonCollected = false;
            }
        }
        else if (collision.gameObject.tag == "leftwall"){
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

            if (isBonbonCollected)
            {
                spawnBonbon();
                isBonbonCollected = false;
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

            highScoreText.gameObject.SetActive(true);

            int oldBonbonScore = PlayerPrefs.GetInt("bonbonScore", bonbonScore);
            PlayerPrefs.SetInt("bonbonScore", bonbonScore + oldBonbonScore);
            bonbonScoreText.text = (oldBonbonScore + bonbonScore).ToString();
            bonbonScoreText.gameObject.SetActive(true);
            bonbonImage.gameObject.SetActive(true);

            isRotating = true;
            rb.gravityScale = 2;
            logicManager.gameOver();
        }
    }

    void OnCollisionStay2D(Collision2D collision) 
    {
        if(collision.gameObject.tag == "bottom" && !isDead) 
        {
            isDead = true;
            isRotating = false;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            logicManager.gameOver();
        }
    }

    void spawnBonbon()
    {
        GameObject currentBonbon = bonbon1;
        if(score >= 15) {
            currentBonbon = bonbon2;
        } if(score >= 30) {
            currentBonbon = bonbon3;
        }
        if(hitRightwall)
        {
            Instantiate(currentBonbon, new Vector3(-2, Random.Range(-3.0f, 3.0f), 0), Quaternion.identity);
        }
        else
        {
            Instantiate(currentBonbon, new Vector3(2, Random.Range(-3.0f, 3.0f), 0), Quaternion.identity);
        }
    }
}
