using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;
    public Text score;
    private int scoreValue = 0;
    public bool grounded;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
    }
	private void Update()
	{
    if (Input.GetKeyDown(KeyCode.W))
        {
			if (grounded)
			{
                rb2d.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
			}
        }
    }
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rb2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }

    }

}
