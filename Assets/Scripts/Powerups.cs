using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
  public bool doublePoints;
  public bool safeMode;
  public bool shield;
  public bool rocketSpeed;

  public float powerupLenght;
  private PowerUpManager thePowerUpManager;
  public Sprite[] powerUpSprites;

  // Use this for initialization
  void Start()
  {
    thePowerUpManager = FindObjectOfType<PowerUpManager>();
  }

  void Awake()
  {
    int powerSelector = Random.Range(0, 3);

    switch (powerSelector)
    {
      case 0:
        doublePoints = true;
        break;
      case 1:
        safeMode = true;
        break;
      case 2:
        shield = true;
        break;
      case 3:
        rocketSpeed = true;
        break;
    }
    GetComponent<SpriteRenderer>().sprite = powerUpSprites[powerSelector];


  }
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.name == "Player")
    {
      thePowerUpManager.ActivatePowerup(doublePoints, safeMode, shield, rocketSpeed, powerupLenght);
    }
    gameObject.SetActive(false);

  }
}
