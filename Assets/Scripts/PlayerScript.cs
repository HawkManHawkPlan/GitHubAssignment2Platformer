using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed, jumpForce;
    private float hozMovement;
    public Text score;
    public TMP_Text lives, loss, winner;
    private int scoreValue = 0, livesValue = 3;
    public bool grounded, hasNotLost, hasWon, hasFinishedL1;
    public GameObject theCamera;

    private Animator animator;

    private bool facingRightField = true;
    public bool facingRightProp
    {
        get { return facingRightField; }
        set
        {
            if (facingRightField == true && value == false)
            {
                //Face left
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            if (facingRightField == false && value == true)
            {
                //Face right
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            facingRightField = value;
        }
    }


    private void Awake()
	{
        animator = GetComponent<Animator>();
        loss.enabled = false;
        winner.enabled = false;
        hasNotLost = true;
        hasWon = false;
        hasFinishedL1 = false;
	}


	void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        score.text = "Coins: "+ scoreValue.ToString();
        lives.text = "Lives: " + livesValue.ToString();
    }
	private void Update()
	{
		if (hasNotLost && hasWon == false)
		{
			if (!grounded)
			{
                animator.Play("Jump");
			}
            hozMovement = Input.GetAxis("Horizontal");
            if (!Input.anyKeyDown && hozMovement == 0 && grounded)
            {
                animator.Play("Idle");
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
			    if (grounded)
			    {
                    rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
			    }
            }
            if (hozMovement != 0)
            {
				if (grounded)
				{
                    animator.Play("Walk");
				}
                if (hozMovement > 0)
                {
                    facingRightProp = true;
                }
                if (hozMovement < 0)
                {
                    facingRightProp = false;
                }
            }
		}
        if(hasNotLost == false)
		{
            loss.enabled = true;
		}
        if(hasWon)
		{
            winner.enabled = true;
        }
    }
    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(hozMovement * speed, rb2d.velocity.y);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.GetComponent<BoxCollider2D>().tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Coins: " + scoreValue.ToString();
            if(scoreValue == 4)
			{
                gameObject.transform.position = new Vector3(-230, 1, 0);
                livesValue = 3;
                lives.text = "Lives: " + livesValue.ToString();
            }
            if(scoreValue == 10)
			{
                hasWon = true;
                gameObject.GetComponent<AudioSource>().Play();

            }
        }
        if (collision.GetComponent<BoxCollider2D>().tag == "Enemy")
		{
            livesValue -= 1;
            lives.text = "Lives: " + livesValue.ToString();
            if(livesValue == 0 && hasWon ==false)
			{
                hasNotLost = false;
                hozMovement = 0;
			}
        }
        Destroy(collision.gameObject);

    }

}
