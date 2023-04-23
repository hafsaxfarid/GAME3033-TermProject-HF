using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Player Detection")]


    [Header("Movement")]
    public float runForce;
    public bool isGrounded;
    public float groundRadius;
    public LayerMask groundLayerMask;
    public Transform groundOrigin;

    [Header("Animation")]
    public Animator enemyAnimationController;
    public EnemyAnimationStates eState;
    private string animationState = "EnemyAnimatorState";

    private Rigidbody2D enemyRB;

    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        isGrounded = false;
    }

    void FixedUpdate()
    {
        EnemyMove();
        CheckIsGrounded();
    }

    private void EnemyMove()
    {
        if(isGrounded)
        {
            enemyAnimationController.SetInteger(animationState, (int)EnemyAnimationStates.ENEMY_IDLE); // idle state
            eState = EnemyAnimationStates.ENEMY_IDLE;
        }
    }


    private void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
    }

    public void CheckIsGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(groundOrigin.position, groundRadius, Vector2.down, groundRadius, groundLayerMask);
        isGrounded = (hit) ? true : false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundOrigin.position, groundRadius);
    }
}
