using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterController : MonoBehaviour
{
    public float speed;

    private Animator animator;
    private Rigidbody2D rigidBody2D;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            dir.x = -1;
            // Since we're moving to the left, flip the sprite if it isn't already flipped
            if (this.gameObject.transform.localScale.x > 0.0f)
            {
                Vector3 newLocalScale = this.gameObject.transform.localScale;
                newLocalScale.x *= -1.0f;
                this.gameObject.transform.localScale = newLocalScale;
            }
            
            //animator.SetInteger("Direction", 3);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir.x = 1;
            // Since we're moving to the right, flip the sprite if it is already flipped
            if (this.gameObject.transform.localScale.x < 0.0f)
            {
                Vector3 newLocalScale = this.gameObject.transform.localScale;
                newLocalScale.x *= -1.0f;
                this.gameObject.transform.localScale = newLocalScale;
            }

            //animator.SetInteger("Direction", 2);
        }

        if (Input.GetKey(KeyCode.W))
        {
            dir.y = 1;
            //animator.SetInteger("Direction", 1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dir.y = -1;
            //animator.SetInteger("Direction", 0);
        }

        dir.Normalize();
        animator.SetBool("IsMoving", dir.magnitude > 0);

        rigidBody2D.velocity = speed * dir;


    }
}
