using UnityEditor;
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using Unity.Android.Gradle.Manifest;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float defGravity = 1;
    [SerializeField] private float fallGravity = 2;
    [SerializeField] private float slideGravity = 0.3f;
    [SerializeField] private float jumpForce = 100;
    [SerializeField] private float speed = 5;
    [SerializeField] private float wallForce = 70;
    [SerializeField] private Transform DLCheck;
    [SerializeField] private Transform DRCheck;
    [SerializeField] private Transform TLCheck;
    [SerializeField] private Transform TRCheck;
    private enum PlayerState {
        jumping,
        falling,
        walled,
        grounded
        };
    private enum CheckState
    {
        lWalled,
        rWalled,
        grounded,
        aired
    }
    [SerializeField] private PlayerState ps = PlayerState.falling;
    [SerializeField] private CheckState cs;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        switch (ps)
        {
            case PlayerState.jumping:
                JumpState();
                break;
            case PlayerState.falling:
                FallState();
                break;
            case PlayerState.walled:
                WallState();
                break;
            case PlayerState.grounded:
                GroundState();
                break;
        }
    }

    IEnumerator JumpState()
    {
        //ACTION
        rb.linearVelocityY -= defGravity;

        //PASSING
        if (rb.linearVelocityY < 0)
            ps = PlayerState.falling;   //TO FALLSTATE
        else if (GroundCheck() == CheckState.lWalled || GroundCheck() == CheckState.rWalled)
            ps = PlayerState.walled;    //TO WALLSTATE
        yield return 0;
    }
    IEnumerator FallState()
    {
        //ACTION
        rb.linearVelocityY -= fallGravity;

        //PASSING
        if (GroundCheck() == CheckState.grounded)
            ps = PlayerState.grounded;  //TO GROUNDSTATE
        else if (GroundCheck() == CheckState.lWalled || GroundCheck() == CheckState.rWalled)
            ps = PlayerState.walled;    //TO WALLSTATE
        yield return 0;
    }
    IEnumerator WallState()
    {
        //ACTION
        rb.linearVelocityY -= slideGravity;
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (GroundCheck() == CheckState.lWalled)
                rb.linearVelocityX = wallForce;
            else if (GroundCheck() == CheckState.rWalled)
                rb.linearVelocityX = -wallForce;
            Jump(); //TO JUMPSTATE
        }

        //PASSING
        yield return 0;
    }
    IEnumerator GroundState()
    {
        //ACTION
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            Jump(); //TO JUMPSTATE
        
        //PASSING
        if (GroundCheck() == CheckState.aired)
            ps = PlayerState.falling;   //TO FALLSTATE
        yield return 0;
    }

    CheckState GroundCheck()
    {
        bool tl = Physics2D.OverlapCircle(TLCheck.position, 0.3f);
        bool tr = Physics2D.OverlapCircle(TRCheck.position, 0.3f);
        bool dl = Physics2D.OverlapCircle(DLCheck.position, 0.3f);
        bool dr = Physics2D.OverlapCircle(DRCheck.position, 0.3f);

        if (dl || dr)
            return CheckState.grounded;
        else if (tl && dl)
            return CheckState.lWalled;
        else if (tr && dr)
            return CheckState.rWalled;
        else
            return CheckState.aired;
    }


    IEnumerator Jump()
    {
        rb.linearVelocityY = jumpForce;
        ps = PlayerState.jumping;
        yield return 0;
    }
}
