using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;
    public float circleArea;
    private bool isFacingRight = true;
    public Animator PlayerAnimator;
    public AudioSource JumpSound;
    public AudioSource StepsSounds;

    public bool ground;
    public bool wall;



    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private void Update()
    {
        ControlPlayerMovement();
       

        //WallSlide();
        //WallJump();

  
         Flip();

    }

    private void FixedUpdate()
    {

        PlayerMovementHorizontal();
    }




    private bool IsGrounded()
    {
        ground = Physics2D.OverlapCircle(groundCheck.position, circleArea, groundLayer);
        print(ground);
      
        return ground;
    }
    private void PlayerMovementHorizontal()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (horizontal != 0 && rb.velocity.x != 0)
        {
            PlayerAnimator.SetBool("Walking", true);
            if(IsGrounded())
            {
                StepsSounds.UnPause();
                PlayerAnimator.SetBool("Jump", false);
            }
            else
            {
                StepsSounds.Pause();
                PlayerAnimator.SetBool("Jump", true);
            }
        }
        else 
        {
            PlayerAnimator.SetBool("Walking", false);
            StepsSounds.Pause();

        }
        if ( rb.velocity.y == 0 )
        {
            PlayerAnimator.SetBool("Jump", false);
         
        }
    }

    private void ControlPlayerMovement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            PlayerAnimator.SetBool("Jump", true);
            JumpSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
           // rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
      

    }


    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.position, circleArea);
    }

}