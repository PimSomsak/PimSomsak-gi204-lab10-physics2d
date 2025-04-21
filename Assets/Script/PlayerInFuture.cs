using UnityEngine;
using UnityEngine.UI;

public class PlayerInFuture : MonoBehaviour
{
    public int orb = 0;

    public Animator animator;
    public Rigidbody2D rb;
    public float jumpHeight = 5f;
    private bool isOnGround = true;

    private float movement;
    public float moveSpeed = 7f;
    private bool facingRight = true;

    void Update()
    {

        movement = Input.GetAxis("Horizontal");

        if (movement < 0 && facingRight) //flip player
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
            facingRight = false;
        }
        if (movement > 0 && !facingRight)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            facingRight = true;
        }
        if (Input.GetKey(KeyCode.Space) && isOnGround) //Jump
        {
            Jump();
            isOnGround = false;
            animator.SetBool("isJump", true);
        }

        if (movement != 0) //or (Mathf.Abs(movement) > 0) for facingleft movement < 0
        {
            animator.SetFloat("Run", 1f);
        }
        else if (movement == 0)
        {
            animator.SetFloat("Run", 0f);
        }


    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(movement, 0f, 0f) * Time.fixedDeltaTime * moveSpeed;
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isOnGround = true;
            animator.SetBool("isJump", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Orb")
        {
            orb++;
            other.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Collected");
            Destroy(other.gameObject, 2f);
        }
    }
}
