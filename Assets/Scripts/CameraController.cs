using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


  private PlayerController thePlayer;
  private PlatformGenerator thePlatformGenerator;
  private Camera cam;
  private Vector3 lastPlayerPosition;
  private Vector3 lastCameraPosition;

  private float distanceToMove;
  public float offset;
  public float zoomOutSpeed;
  public float zoomInSpeed;


  // Use this for initialization
  void Start()
  {
    cam = GetComponent<Camera>();
    thePlatformGenerator = FindObjectOfType<PlatformGenerator>();
    thePlayer = FindObjectOfType<PlayerController>();
    lastPlayerPosition = thePlayer.transform.position;
    lastCameraPosition.y = transform.position.y;
  }

  // Update is called once per frame
  void Update()
  {

    //Check how much should camera move
    distanceToMove = thePlayer.transform.position.x - lastPlayerPosition.x;

    //Actual move of camera
    if ((thePlayer.transform.position.y > transform.position.y * 5f) && !thePlayer.grounded)
    {
      transform.position = new Vector3(transform.position.x, transform.position.y * 3f, transform.position.z);

    }
    if (thePlayer.grounded)
    {
      transform.position = new Vector3(transform.position.x, lastCameraPosition.y, transform.position.z);

    }
    transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);


    //Check where is Player now
    lastPlayerPosition = thePlayer.transform.position;

    if (thePlayer.transform.position.y - offset > thePlatformGenerator.getMaxHeight())
    {
      //Debug.Log("thePlayer.transform.position.y = " + thePlayer.transform.position.y);
      //Debug.Log("thePlatformGenerator.getMaxHeight() = " + thePlatformGenerator.getMaxHeight());
      cam.orthographicSize += zoomOutSpeed * Time.deltaTime;
      cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 5, 10);
      Debug.Log("cam.orthographicSize " + cam.orthographicSize);


    }
    if (thePlayer.transform.position.y - offset < thePlatformGenerator.getMaxHeight())
    {

      cam.orthographicSize -= zoomInSpeed * Time.deltaTime;
      cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 5, 10);
    }

  }
}
