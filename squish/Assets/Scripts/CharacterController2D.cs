using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine.Rendering;

public class CharacterController2D : MonoBehaviour
{
    public bool dKey, aKey, spaceKey;
    [SerializeField] private InputActionAsset inputAsset;
	[SerializeField] private LayerMask groundLayer;
    private InputAction jumpAction;
    private InputAction moveAction;
	protected PlayerMovement player;
    public float defGravity = 1, fallGravity = 2, slideGravity = 0.3f;
    public float jumpForce = 100, wallForce = 70;
    public float minVelAnim = 0.2f;
    public float minScale = 0.8f, maxScale = 1.25f;
    public int defJumpsLeft = 2, jumpsLeft;
    public float speed = 2, maxSpeed = 10;
	public bool jumpClicked;
    public float horMove;
	public float startTime;
    public float prevVelX, prevVelY;
	public Rigidbody2D rb;
    public SpriteRenderer sr;
    public ParticleSystem ps;
    public float transition = 0.2f;
    public bool isComplete = false;
	public float jumpBuffer = 0.2f;
    public float time => Time.time - startTime;
    [SerializeField] private Transform DLCheck, DRCheck, TLCheck, TRCheck;
	[SerializeField] private float checkRadius = 0.05f;
	public StateSystem pl;
    Keyboard key;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = defGravity;
        sr = GetComponent<SpriteRenderer>();
        ps = GetComponent<ParticleSystem>();
		/*DLCheck = transform.Find("DLCheck");
		DRCheck = transform.Find("DRCheck");
		TLCheck = transform.Find("TLCheck");
		TRCheck = transform.Find("TRCheck");*/
	}

    void Update()
    {
        prevVelX = rb.linearVelocityX;
        prevVelY = rb.linearVelocityY;
		key = Keyboard.current;
		if (key.spaceKey.wasPressedThisFrame || spaceKey)
        {
            StartCoroutine(JumpBuffer());
            jumpsLeft--;
        }
			
        horMove = Convert.ToInt32(key.dKey.isPressed || dKey) - Convert.ToInt32(key.aKey.isPressed || aKey);
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
        {
            rb.linearVelocityX = Mathf.Clamp(rb.linearVelocityX, -maxSpeed, maxSpeed);
            rb.linearVelocityX += speed * horMove;
        }
		Debug.Log("Moving! Direction: " + horMove);
        
    }
    public void Jump(float dir = 0)
    {
		Debug.Log("Jump Called! Direction: " + dir);
        if (jumpsLeft > 0)
        {
            rb.linearVelocityY = jumpForce;
            if (dir != 0)
                WallJump(dir);
        }
    }

    void WallJump(float dir)
    {
        rb.linearVelocityX = dir * wallForce;
    }
    
    public float Map(float old_value, float old_min, float old_max, float new_min, float new_max)
    {
        return (old_value - old_min) * (new_max - new_min) / (old_max - old_min) + new_min;
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