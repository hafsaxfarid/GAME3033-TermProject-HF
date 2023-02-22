using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Movement")]
    public float horizontalForce;
    public float verticalForce;

    public Transform groundOrigin;
    public float groundRadius;
    public LayerMask groundLayerMask;
    public bool isGrounded;

    [Range(0.1f, 0.9f)]
    public float airControlFactor;

    [Header("Animation")]
    public KnightAnimationStates kState;

    private Rigidbody2D playerRB;
   
    [SerializeField]
    private Animator playerAnimationController;
    
    private string animationState = "KnightAnimatorState";

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        CheckIsGrounded();
        KnightMove();
    }

    private void KnightMove()
    {
        float x = Input.GetAxisRaw("Horizontal");

        if (isGrounded)
        {
            // keyboard input    
            float y = Input.GetAxisRaw("Vertical");
            float jump = Input.GetAxisRaw("Jump");

            if (x != 0)
            {
                x = FlipAnimation(x);
                playerAnimationController.SetInteger(animationState, (int)KnightAnimationStates.KNIGHT_RUN); // run state
                kState = KnightAnimationStates.KNIGHT_RUN;
            }
            else
            {
                playerAnimationController.SetInteger(animationState, (int)KnightAnimationStates.KNIGHT_IDLE); // idle state
                kState = KnightAnimationStates.KNIGHT_IDLE;
            }

            float horizontalMoveForce = x * horizontalForce;
            float jumpMoveForce = y * verticalForce;

            float mass = playerRB.mass * playerRB.gravityScale;

            playerRB.AddForce(new Vector2(horizontalMoveForce, jumpMoveForce) * mass);
            playerRB.velocity *= 0.99f;
        }
        else
        {
            KnightJump();

            if (x != 0)
            {
                x = FlipAnimation(x);

                float horizontalMoveForce = x * horizontalForce * airControlFactor;
                float mass = playerRB.mass * playerRB.gravityScale;

                playerRB.AddForce(new Vector2(horizontalMoveForce, 0.0f) * mass);
            }
        }
    }

    private void KnightJump()
    {
        playerAnimationController.SetInteger(animationState, (int)KnightAnimationStates.KNIGHT_JUMP); // jump state
        playerAnimationController.SetTrigger("Knight-Jump");
        kState = KnightAnimationStates.KNIGHT_JUMP;
    }

    private void KnightAttack()
    {

    }

    private void KnightHurt()
    {

    }

    private void KnightDie()
    {

    }

    public void CheckIsGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(groundOrigin.position, groundRadius, Vector2.down, groundRadius, groundLayerMask);
        isGrounded = (hit) ? true : false;
    }


    private float FlipAnimation(float x)
    {
        x = (x > 0) ? 1 : -1;

        transform.localScale = new Vector2(x, 1.0f);
        return x;
    }

}
