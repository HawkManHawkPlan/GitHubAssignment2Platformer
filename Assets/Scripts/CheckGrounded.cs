using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGrounded : MonoBehaviour
{
    [SerializeField] private bool groundedField = false;
    public bool groundedProperty
    {
        get { return groundedField; }
        set
        {
            if (groundedField == false && value == true)
            {
                //Debug.Log("Grounded changed to true");
                gameObject.GetComponent<PlayerScript>().grounded = value;
            }
            if (groundedField == true && value == false)
            {
                gameObject.GetComponent<PlayerScript>().grounded = value;
            }
            groundedField = value;
        }
    }

    public LayerMask groundOnly;
    public Transform leftUp, rightDown;
    [SerializeField] private BoxCollider2D collBox;
	private void Awake()
	{
        collBox = gameObject.GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        groundedProperty = Physics2D.OverlapArea(leftUp.position, rightDown.position, groundOnly);
        //Debug.Log(groundedField);
    }


}
