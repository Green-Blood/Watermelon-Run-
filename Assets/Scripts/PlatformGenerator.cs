using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {

	public GameObject thePlatform;
	public Transform generationPoint;
	//Distance between platforms
	public float distanceBetween;
	private float platformWidth;

	public float distanceBetweenMax;
	public float distanceBetweenMin;
	public ObjectPooler[] theObjectPools;

	//public GameObject[] thePlatforms;
	private int platformSelector;
	private float[] platformWidths;
	//Height of platforms
	private float minHeight;
	public Transform maxHeightPoint;
	private float maxHeight;
	public float maxHeightChange;
	private float heightChange;
	private CoinGenerator theCoinGenerator;

	public float randomCoinThreshold;
	
	public float randomSpikeThreshold;
	public ObjectPooler spikePool;

	public float powerUpHeight;
	public ObjectPooler powerUpPool;
	public float powerUpThresHold;
    private bool spikeOnTheLeft;
    private float subtractPlatformDistance;
    public float subtractPlatformDistanceOld;


	// Use this for initialization
	void Start () {
		//platformWidth = thePlatform.GetComponent<BoxCollider2D>().size.x;
		platformWidths = new float[theObjectPools.Length];
		for (int i = 0; i < theObjectPools.Length; i++)
		{
			platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
		}
		//Starting point of Height
		minHeight = transform.position.y;
		maxHeight = maxHeightPoint.position.y;
		theCoinGenerator = FindObjectOfType<CoinGenerator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x < generationPoint.position.x)
		{

            platformGenerate();

            coinGenerate();
			
			//spikeGenerate();
			
			transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);
            


		}
	}

    // Generate the Platforms and the Powerups
    void platformGenerate() {
        //Randomize distance between platforms

        // If spike is spawned on the left of the platform, then we make distanceBetweenMax smaller
        if (spikeOnTheLeft) {
            subtractPlatformDistance = subtractPlatformDistanceOld;
            // Reset spikeOnTheLeft value
            spikeOnTheLeft = false;
        }
        distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax - subtractPlatformDistance);
        // Reset Platform distance
        subtractPlatformDistance = 0.0f;
        //Randomly pick a of platforms
        platformSelector = Random.Range(0, theObjectPools.Length);

        //Height Change
        heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);

        if (heightChange > maxHeight) {

            heightChange = maxHeight;
        }
        else if (heightChange < minHeight) {
            heightChange = minHeight;
        }

        // Generate Powerups
        powerupGenerate();

        //Move platform
        transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, heightChange, transform.position.z);

        spikeGenerate();
        //Getting object pool
        GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();

        newPlatform.transform.position = transform.position;
        newPlatform.transform.rotation = transform.rotation;
        newPlatform.SetActive(true); 
    }



    // Generate powerups
    void powerupGenerate() {
        //Creating random powerups
        if (Random.Range(0f, 100f) < powerUpThresHold) {
            GameObject newPowerUp = powerUpPool.GetPooledObject();
            newPowerUp.transform.position = transform.position + new Vector3(distanceBetween / 2f, Random.Range(powerUpHeight / 2f, powerUpHeight), 0f);
            newPowerUp.SetActive(true);

        }
    }

    // Generate Coins
    void coinGenerate() {
        if (Random.Range(0f, 100f) < randomCoinThreshold) {
            //Generating random Coins
            theCoinGenerator.SpawnCoins(platformSelector, platformWidths[platformSelector], new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z));
          
        }
    }


    // Generate Spikes
    void spikeGenerate() {
        if (Random.Range(0f, 100f) < randomSpikeThreshold) {
            //Generating random spikes
            GameObject newSpike = spikePool.GetPooledObject();
            float SpikeXPosition = Random.Range(-platformWidths[platformSelector] / 2f + 1f, platformWidths[platformSelector] / 2f - 1f);
            if (SpikeXPosition == -platformWidths[platformSelector] / 2f + 1f) {
                 spikeOnTheLeft = true;
            }
            Vector3 spikePosition = new Vector3(SpikeXPosition, 0.4f, 0f);
            newSpike.transform.position = transform.position + spikePosition;
            newSpike.transform.rotation = transform.rotation;
            newSpike.SetActive(true);
           

        }
    }
}
