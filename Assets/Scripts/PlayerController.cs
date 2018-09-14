using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {

	//Velocity of the player 
	public float moveSpeed;
	private float moveSpeedStore;
	//Increasing speed of the player
	public float speedMultiplier;
	//point when speed should increase the speed
	public float speedIncreaseMilestone;
	public float speedIncreaseMilestoneStore;
	private float speedMilestoneCount;
	private float speedMilestoneCountStore;
	//How far can player jump
	public float jumpForce;
	//How long can player jump
	public float jumpTime;
	private float jumpTimeCounter;

	private bool stoppedJumping;
	private bool canDoubleJump;

	public bool grounded; 
	public LayerMask whatIsGround;
	public Transform groundCheck;
	public float groundCheckRadius;
	//private Collider2D myCollider; 
	private Rigidbody2D myRigidbody;
	public GameManager theGameManager;
	private Animator myAnimator;
	
	public AudioSource jumpSound;
	public AudioSource deathSound;
	
	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D>();

		//myCollider = GetComponent<Collider2D>();

		myAnimator = GetComponent<Animator>();

		jumpTimeCounter = jumpTime;

		speedMilestoneCount = speedIncreaseMilestone;
		moveSpeedStore = moveSpeed;
		speedMilestoneCountStore = speedMilestoneCount;
		speedIncreaseMilestoneStore = speedIncreaseMilestone;
		stoppedJumping = true;
	}
	
	// Update is called once per frame
	void Update () {

		//grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
		if (transform.position.x > speedMilestoneCount)
		{
			speedMilestoneCount += speedIncreaseMilestone;

			
			speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;
			moveSpeed = moveSpeed * speedMultiplier;
		}


		//Movement of the player 
		myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
		//Check if the player press the space,mouse or upArrow, then jump!
		if(((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.UpArrow))) && !EventSystem.current.IsPointerOverGameObject())
		{
			if(grounded)
			{
				myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
				stoppedJumping = false;
				jumpSound.Play();
			}
			if(!grounded && canDoubleJump)
			{
				myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
				jumpTimeCounter = jumpTime;
				stoppedJumping = false;
				canDoubleJump = false;
				jumpSound.Play();
			}
			
		}
		//Check again,but this time, check also time.
		if((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0) || Input.GetKey(KeyCode.UpArrow)) && !stoppedJumping)
		{
			if(jumpTimeCounter > 0)
			{
				myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
				jumpTimeCounter -= Time.deltaTime;

			}
		}
		
		if(Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.UpArrow) )
		{
			jumpTimeCounter = 0;
			stoppedJumping = true;
		}
		if(grounded)
		{
			jumpTimeCounter = jumpTime;
			canDoubleJump = true;
		} 

		//Animation of Player
		myAnimator.SetFloat ("Speed", myRigidbody.velocity.x);
		myAnimator.SetBool("Grounded", grounded);
	}
	//Collisions
	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "killbox")
		{
			
			theGameManager.RestartGame();
			moveSpeed = moveSpeedStore;
			speedMilestoneCount = speedMilestoneCountStore;
			speedIncreaseMilestone = speedMilestoneCountStore;
			deathSound.Play();
		}
	}

    
	

}
