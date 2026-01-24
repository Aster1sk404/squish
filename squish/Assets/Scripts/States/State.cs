using Unity.VisualScripting;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor.Experimental.GraphView;

public class State : MonoBehaviour
{
    protected PlayerMovement player;
    protected float defGravity = 1;
    protected float fallGravity = 2;
    protected float slideGravity = 0.3f;
    protected float jumpForce = 100;
    protected int defJumpsLeft = 2;
    protected int jumpsLeft;
    protected float speed = 5;
    protected float wallForce = 70;
    public bool isComplete = false;
    protected float startTime;
    public float time => Time.time - startTime;
    protected bool jumpClicked;
    protected float horMove;
    [HideInInspector] public Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = defGravity;
        player = GetComponent<PlayerMovement>();
    }
    public virtual void Enter()
    {
        
    }

    public virtual void Do()
    {
        Keyboard key = Keyboard.current;
        jumpClicked = key.spaceKey.wasPressedThisFrame;
        horMove = Convert.ToInt32(key.dKey.isPressed) - Convert.ToInt32(key.aKey.isPressed);
    }
    public virtual void FixedDo() { }
    public virtual void Exit() { }
    public void Move()
    {
        rb.linearVelocityX = speed * horMove;
    }
    public void Jump(float dir = 0)
    {
        rb.linearVelocityY = jumpForce;
        rb.linearVelocityX = dir * wallForce;
    }

    protected bool IsGrounded()
    {
        return player.IsGrounded();
    }
    protected bool IsRWalled()
    {
        return player.IsRWalled();
    }
    protected bool IsLWalled()
    {
        return player.IsLWalled();
    }

}

public class AirState : State
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void Do()
    {
        base.Do();
    }

    public override void FixedDo()
    {
        if (horMove != 0)
            Move();
        else
            rb.linearVelocityX = 0;

        if (rb.linearVelocityY < 0 || IsGrounded() || IsLWalled() || IsRWalled()) // END CHECK
            isComplete = true;
    }

    public override void Exit()
    {
        
    }
}

public class FallState : State
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void Do()
    {
        base.Do();
    }

    public override void FixedDo()
    {
        if (horMove != 0)
            Move();
        else
            rb.linearVelocityX = 0;

        if (IsGrounded() || IsLWalled() || IsRWalled()) // END CHECK
            isComplete = true;
    }

    public override void Exit()
    {
        
    }
}

public class GroundState : State
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void Do()
    {
        base.Do();
    }

    public override void FixedDo()
    {
        if (horMove != 0)
            Move();
        else
            rb.linearVelocityX = 0;
        if (jumpClicked)
            Jump();

        if (!IsGrounded()) // END CHECK
            isComplete = true;
    }

    public override void Exit()
    {
        
    }
}

public class LWallState : State
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void Do()
    {
        base.Do();
    }

    public override void FixedDo()
    {
        if (horMove != 0)
            Move();
        else
            rb.linearVelocityX = 0;
        if (jumpClicked)
        {
            Jump(1);
        }
        
        if (!IsLWalled()) // END CHECK
            isComplete = true;
    }

    public override void Exit()
    {
        
    }
}

public class RWallState : State
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void Do()
    {
        base.Do();
    }

    public override void FixedDo()
    {
        if (horMove != 0)
            Move();
        else
            rb.linearVelocityX = 0;
        if (jumpClicked)
        {
            Jump(-1);
        }
        
        if (!IsRWalled()) // END CHECK
            isComplete = true;
    }

    public override void Exit()
    {
        
    }
}
