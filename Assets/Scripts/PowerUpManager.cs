using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {

    
	private bool doublePoints;
	private bool safeMode;
    private bool shield;
    private bool rocketSpeed;

	private bool powerupActive;
	private float powerupLengthCounter;
	private ScoreManager theScoreManager;
	private GameManager theGameManager;
	private PlatformGenerator thePlatformGenerator;
	private float normalPointsPerSecond;
	private float spikeRate;
	private PlatformDestructor[] spikeList;
    public SpikeScript[] spikeCollider;
    public ShieldController theShieldController;

    public bool GetShield() {
        return shield;
    }
	// Use this for initialization
    void Start() {
        theScoreManager = FindObjectOfType<ScoreManager>();
        thePlatformGenerator = FindObjectOfType<PlatformGenerator>();
        theGameManager = FindObjectOfType<GameManager>();
        //theShieldController = FindObjectOfType<ShieldController>();
        
    }
	// Update is called once per frame
	void Update () {
        
		if(powerupActive)
		{
			powerupLengthCounter -= Time.deltaTime;
			
			if(doublePoints)
			{
				theScoreManager.pointsPerSecond = normalPointsPerSecond * 2;
				theScoreManager.shouldDouble = true;
			}
			if (safeMode)
			{
				thePlatformGenerator.randomSpikeThreshold = 0f;
			}
            if (shield)
			 {
                spikeTrigger(true);
            }
			//reset powerups if the player is killed
			if(theGameManager.powerUpReset)
			{
				powerupLengthCounter = 0;
				theGameManager.powerUpReset = false;
			}
			if(powerupLengthCounter <= 0 )
			{
				theScoreManager.pointsPerSecond = normalPointsPerSecond;
				theScoreManager.shouldDouble = false;
                spikeTrigger(false);
                //spikeCollider.isTrigger = false;
               // Debug.Log("Spike Collider = " + spikeCollider.isTrigger);
                thePlatformGenerator.randomSpikeThreshold = spikeRate;
				powerupActive = false;
			}
		}
	}
	public void ActivatePowerup(bool points, bool safe, bool shieldPower, bool rocket, float time)
	{
		doublePoints = points;
		safeMode = safe;
        shield = shieldPower;
        rocketSpeed = rocket;
		powerupLengthCounter = time;
        if (!powerupActive) {
            normalPointsPerSecond = theScoreManager.pointsPerSecond;
            spikeRate = thePlatformGenerator.randomSpikeThreshold;
        }
		if(safeMode)
		{
			spikeList = FindObjectsOfType<PlatformDestructor>();
			for(int i = 0; i < spikeList.Length; i++)
			{
				if(spikeList[i].gameObject.name.Contains("spikes"))
				{
					spikeList[i].gameObject.SetActive(false);
				}
			}
		}
		if(!shield)
		{
			spikeTrigger(false);
		}
		powerupActive = true;
	}



    public void spikeTrigger(bool cond) {
        spikeCollider = FindObjectsOfType<SpikeScript>();
        //theShieldController.setActive(cond);
		theShieldController.gameObject.SetActive(cond);
        for (int i = 0; i < spikeCollider.Length; i++) {
            spikeCollider[i].GetComponent<EdgeCollider2D>().isTrigger = cond;
        }
		
    }

}
