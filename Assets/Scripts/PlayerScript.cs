using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;
    private float hozMovement;
    public Text score;
    private int scoreValue = 0;
    public bool grounded;

    private Animator animator;
    public int animState = 0;

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
	}


	void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
    }
	private void Update()
	{
        hozMovement = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.W))
        {
			if (grounded)
			{
                rb2d.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
			}
        }
        if (hozMovement != 0)
        {
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
    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(hozMovement * speed, rb2d.velocity.y);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.GetComponent<BoxCollider2D>().tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.gameObject);
        }
    }

}
