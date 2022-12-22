using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : player
{
    private PlayerMovement movement;
    
    void Awake()
    {
        TryGetComponent(out rig2D);
        TryGetComponent(out animator);
        TryGetComponent(out movement);
    }

    private void FixedUpdate()
    {
        Animate();
    }

    private void Animate()
    {
        // Animação do personagem
        if (rig2D.velocity == Vector2.zero)
        {
            animator.Play("player_idle");
        } 
        else if (rig2D.velocity.y < .1f)
        {
            animator.Play("player_walk");
        } 
        else if (rig2D.velocity.y > 0 && movement.isGrounded() && movement.GetIfIsDoubleJumping())
        {
            animator.Play("player_jump");
        }
        else if (!movement.GetIfIsDoubleJumping())
        {
            animator.Play("player_doubleJump");
        }
        
        // Rotação do personagem
        if (movement.GetDirection() > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (movement.GetDirection() < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
