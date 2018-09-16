using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


	public PlayerController thePlayer;

	private Vector3 lastPlayerPosition;
	private Vector3 lastCameraPosition;
	
	private float distanceToMove; 


	// Use this for initialization
	void Start () {
		thePlayer = FindObjectOfType<PlayerController>();
		lastPlayerPosition = thePlayer.transform.position; 
		lastCameraPosition.y = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {

		//Check how much should camera move
		distanceToMove = thePlayer.transform.position.x - lastPlayerPosition.x; 
	
		//Actual move of camera
		if((thePlayer.transform.position.y > transform.position.y * 5f) && !thePlayer.grounded )
		{
			transform.position = new Vector3(transform.position.x ,  transform.position.y * 3f, transform.position.z); 
			
		}
		if(thePlayer.grounded )
		{
			transform.position = new Vector3(transform.position.x ,lastCameraPosition.y, transform.position.z); 
			
		}
		transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y , transform.position.z); 
	
		 
		//Check where is Player now
		lastPlayerPosition = thePlayer.transform.position; 
		
	}
}
