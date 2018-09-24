using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPoints : MonoBehaviour {
	public int scoreToGive;
	private ScoreManager theScoreManager;
    private CoinGenerator theCoinGenerator;
	private AudioSource coinSound;
	// Use this for initialization
	void Start () {
		theScoreManager = FindObjectOfType<ScoreManager>();
		coinSound = GameObject.Find("CoinSound").GetComponent<AudioSource>();
        theCoinGenerator = FindObjectOfType<CoinGenerator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.name == "Player")
		{
			theScoreManager.AddScore(scoreToGive);
			gameObject.SetActive(false);
			if(coinSound.isPlaying)
			{
				coinSound.Stop();
				coinSound.Play();

			}
			else
			{
				coinSound.Play();
			}			
		}
        if (other.gameObject.tag == "killbox") {
            theCoinGenerator.IsCollidedWithSpike();
        }
        
        
	}
}
