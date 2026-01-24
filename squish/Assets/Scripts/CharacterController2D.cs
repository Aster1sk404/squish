using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Unity.VisualScripting;
using System.Collections;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputAsset;
	[SerializeField] private LayerMask groundLayer;
    private InputAction jumpAction;
    private InputAction moveAction;
	protected PlayerMovement player;
    public float defGravity = 1;
    public float fallGravity = 2;
    public float slideGravity = 0.3f;
    public float jumpForce = 100;
    public int defJumpsLeft = 2;
    public int jumpsLeft;
    public float speed = 2;
    public float maxSpeed = 10;
    public float wallForce = 70;
	public bool jumpClicked;
	public float horMove;
	public float startTime;
	public Rigidbody2D rb;
    public bool isComplete = false;
	public float jumpBuffer = 0.2f;
    public float time => Time.time - startTime;
    [SerializeField] private Transform DLCheck, DRCheck, TLCheck, TRCheck;
	[SerializeField] private float checkRadius = 0.05f;
	public StateSystem pl;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = defGravity;
		/*DLCheck = transform.Find("DLCheck");
		DRCheck = transform.Find("DRCheck");
		TLCheck = transform.Find("TLCheck");
		TRCheck = transform.Find("TRCheck");*/
	}

    void Update()
    {
		Keyboard key = Keyboard.current;
		if (key.spaceKey.wasPressedThisFrame)
        {
            StartCoroutine(JumpBuffer());
        }
			
        horMove = Convert.ToInt32(key.dKey.isPressed) - Convert.ToInt32(key.aKey.isPressed);
        pl.DoState();
    }

    void FixedUpdate()
    {
        pl.FixedDoState();
    }

	IEnumerator JumpBuffer()
	{
        jumpClicked = true;
		yield return new WaitForSeconds(jumpBuffer);
		jumpClicked = false;
	}

	public void Move()
    {
        if (horMove != 0)
            if (rb.linearVelocityX <= maxSpeed && rb.linearVelocityX >= -maxSpeed)
                rb.linearVelocityX += speed * horMove;
            else if (rb.linearVelocityX > maxSpeed)
                rb.linearVelocityX = maxSpeed;
            if (rb.linearVelocityX < -maxSpeed)
                rb.linearVelocityX = -maxSpeed;
		Debug.Log("Moving! Direction: " + horMove);
        
    }
    public void Jump(float dir = 0)
    {
		Debug.Log("Jump Called! Direction: " + dir);
        rb.linearVelocityY = jumpForce;
        if (dir != 0)
            WallJump(dir);
    }

    void WallJump(float dir)
    {
        rb.linearVelocityX = dir * wallForce;
    }
    
	public bool IsGrounded()
    {
        bool dr = Physics2D.OverlapCircle(DRCheck.position , checkRadius, groundLayer) != null;
        bool tl = Physics2D.OverlapCircle(TLCheck.position , checkRadius, groundLayer) != null;
        bool dl = Physics2D.OverlapCircle(DLCheck.position , checkRadius, groundLayer) != null;
        bool tr = Physics2D.OverlapCircle(TRCheck.position , checkRadius, groundLayer) != null;

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
        bool dl = Physics2D.OverlapCircle(DLCheck.position , checkRadius, groundLayer) != null;
        bool dr = Physics2D.OverlapCircle(DRCheck.position , checkRadius, groundLayer) != null;
        bool tl = Physics2D.OverlapCircle(TLCheck.position , checkRadius, groundLayer) != null;
        bool tr = Physics2D.OverlapCircle(TRCheck.position , checkRadius, groundLayer) != null;

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
        bool dl = Physics2D.OverlapCircle(DLCheck.position , checkRadius, groundLayer) != null;
        bool dr = Physics2D.OverlapCircle(DRCheck.position , checkRadius, groundLayer) != null;
        bool tl = Physics2D.OverlapCircle(TLCheck.position , checkRadius, groundLayer) != null;
        bool tr = Physics2D.OverlapCircle(TRCheck.position , checkRadius, groundLayer) != null;

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