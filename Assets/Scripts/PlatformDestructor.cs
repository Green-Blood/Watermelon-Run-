using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestructor : MonoBehaviour {

	public GameObject platformDestructionPoint;

	// Use this for initialization
	void Start () {
		platformDestructionPoint = GameObject.Find("PlatformDestructionPoint");
	}
	
	// Update is called once per frame
	void Update () {
		//Check if x position is behind of the camera
		if(transform.position.x < platformDestructionPoint.transform.position.x)
		{
			//Destroys whenever object this script is attached to
			//Destroy(gameObject);
			gameObject.SetActive(false);
		}

	}
}
