using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PLAYERSCRIPT : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text winText;
    public Text loseText;
    public Text score;
    private int scoreValue = 0;
    public Text lives;
    private int livesValue = 3;
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public Animator anim;
    private bool facingRight = true;


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
        score.text = scoreValue.ToString();
        lives.text = livesValue.ToString();
        livesValue = 3;
        winText.text = "";
        loseText.text = "";
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
          anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {            
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }
    }
    
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue == 5)
            {
                transform.position = new Vector2(75.13f, 0f);
                livesValue = 3;
                lives.text = livesValue.ToString();
            }
            if (scoreValue >= 9)
            {
                winText.text = "You win! Game created by Samantha Barrizonte!";
                musicSource.clip = musicClipOne;
                musicSource.Stop();
                musicSource.clip = musicClipTwo;
                musicSource.Play();
                musicSource.loop = false;
            }
        }
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
            if (livesValue <= 0)
            {
                loseText.text = "You lose. Try again!";
                Destroy(GameObject.FindWithTag("Player"));
            }
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }          
        }

        if (Input.GetKey("escape"))
            Application.Quit();
    }
}