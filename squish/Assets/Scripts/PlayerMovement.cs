using UnityEditor;
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using Unity.Android.Gradle.Manifest;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private InputActionAsset inputAsset;
    private InputAction jumpAction;
    private InputAction moveAction;
    
    [SerializeField] private Transform DLCheck;
    [SerializeField] private Transform DRCheck;
    [SerializeField] private Transform TLCheck;
    [SerializeField] private Transform TRCheck;

    //STATES
    private State GroundState;
    private State AirState;
    private State FallState;
    private State RWallState;
    private State LWallState;
    private State state;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GroundState = GetComponent<GroundState>();
        AirState = GetComponent<AirState>();
        FallState = GetComponent<FallState>();
        RWallState = GetComponent<RWallState>();
        LWallState = GetComponent<LWallState>();

        SwitchState(GroundState);
    }

    void Update()
    {
        state.Do();

        if (state.isComplete)
        {
            SelectState();
        }
    }

    void FixedUpdate()
    {
        state.FixedDo();
    }

    void SwitchState(State newState)
    {
        if (state != null)
            state.Exit();

        state = newState;
        state.isComplete = false;
        state.Enter();
        state.isComplete = false;
    }
    void SelectState()
    {
        if (IsGrounded())
            SwitchState(GroundState);
        else if (IsRWalled())
            SwitchState(RWallState);
        else if (IsLWalled())
            SwitchState(LWallState);
        else if (rb.linearVelocityY >= 0)
            SwitchState(AirState);
        else
            SwitchState(FallState);
    }
    public bool IsGrounded()
    {
        bool dl = Physics2D.OverlapCircle(DLCheck.position , 0.3f);
        bool dr = Physics2D.OverlapCircle(DRCheck.position , 0.3f);
        bool tl = Physics2D.OverlapCircle(TLCheck.position , 0.3f);
        bool tr = Physics2D.OverlapCircle(TRCheck.position , 0.3f);

        if ((dr || dl) && !(tl || tr))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool IsRWalled()
    {
        bool dl = Physics2D.OverlapCircle(DLCheck.position , 0.3f);
        bool dr = Physics2D.OverlapCircle(DRCheck.position , 0.3f);
        bool tl = Physics2D.OverlapCircle(TLCheck.position , 0.3f);
        bool tr = Physics2D.OverlapCircle(TRCheck.position , 0.3f);

        if ((dr || tr) && !(dl || tl))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool IsLWalled()
    {
        bool dl = Physics2D.OverlapCircle(DLCheck.position , 0.3f);
        bool dr = Physics2D.OverlapCircle(DRCheck.position , 0.3f);
        bool tl = Physics2D.OverlapCircle(TLCheck.position , 0.3f);
        bool tr = Physics2D.OverlapCircle(TRCheck.position , 0.3f);

        if ((dl || tl) && !(dr || tr))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}