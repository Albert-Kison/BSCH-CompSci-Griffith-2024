using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveScript : MonoBehaviour
{
    public float maxSpeed;
    public float acceleration;
    public Rigidbody2D myRb;
    public bool isGrounded;
    public float jumpForce;
    public float secondaryJumpForce;
    public float secondaryJumpDelay;
    public bool secondaryJump;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        myRb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // flip Animator code
        if (Input.GetAxis("Horizontal") < 0)
        {
            anim.transform.localScale = new Vector3(-1, 1, 1); // if the player is pressing the left input, flips the character sprite to face left
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            anim.transform.localScale = new Vector3(1, 1, 1); // if the player is pressing the right input, flips the character sprite to face right
        }
        // END flip Animator code

        anim.SetFloat("speed", Mathf.Abs(myRb.velocity.x));

        if (Mathf.Abs(myRb.velocity.magnitude) < maxSpeed && Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            myRb.AddForce(new Vector2(acceleration * Input.GetAxis("Horizontal"), 0), ForceMode2D.Force);   // add force to the player when moving keys are pressed
        }

        if (Input.GetButtonDown("Jump") && isGrounded)  // allow jumping
        {
            myRb.AddForce(new Vector2(x: 0, y: jumpForce), ForceMode2D.Impulse);
            StartCoroutine(SecondaryJump());    // allow second jump first secondaryJumpDelay seconds
        }

        if (Input.GetButton("Jump") && secondaryJump == true && isGrounded == false)
        {
            myRb.AddForce(new Vector2(x: 0, y: secondaryJumpForce), ForceMode2D.Force); // add force to the second jump
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }

    IEnumerator SecondaryJump()
    {
        secondaryJump = true;
        yield return new WaitForSeconds(secondaryJumpDelay);
        secondaryJump = false;
        yield return null;
    }
}
