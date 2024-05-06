using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D playerRb;
    public float jumpSpeed;
    public LayerMask groundLayer;
    public float groundCheckRadius;
    public Vector3 groundCheckPositionDelta;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        //{
        //    transform.position = new Vector3(transform.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime, transform.position.y, transform.position.z);
        //}
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            playerRb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, playerRb.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            playerRb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }
    }

    private bool IsGrounded()
    {
        var hit = Physics2D.CircleCast((Vector2) (transform.position + groundCheckPositionDelta), groundCheckRadius, Vector2.down, 0, groundLayer);
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = IsGrounded() ? Color.green : Color.red;
        Gizmos.DrawSphere(transform.position + groundCheckPositionDelta, groundCheckRadius);
    }
}
