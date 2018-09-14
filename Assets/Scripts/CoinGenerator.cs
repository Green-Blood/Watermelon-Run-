using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour {
	public ObjectPooler coinPool;
	public float distanceBetweenCoins;

    private GameObject[] coin;
    private int numberOfCoins;
    private int numberOfBlocks;
    private int firstStartingPoint;
    private bool isCollidedWithSpike;


	public void SpawnCoins(int platformSelector, float platformWidth, Vector3 startPosition)
	{
        switch (platformSelector) {
            case 0:
                numberOfBlocks = 3;
                break;
            case 1:
                numberOfBlocks = 5;
                break;
            case 2:
                numberOfBlocks = 7;
                break;
            case 3:
                numberOfBlocks = 9;
                break;
        }

        // Getting Random position on the platform
        firstStartingPoint = Random.Range(0, numberOfBlocks);
        coin = new GameObject[numberOfBlocks];
        // Getting Random number of coins
        numberOfCoins = Random.Range(1, numberOfBlocks + 1);
        if (numberOfCoins > numberOfBlocks - firstStartingPoint) {
            numberOfCoins = numberOfBlocks - firstStartingPoint;
        }
        for (int i = 0; i < numberOfCoins; i++) {
            if (isCollidedWithSpike) {
                isCollidedWithSpike = false;
                Debug.Log("this is collision function");
                break;
            }
            coin[i] = coinPool.GetPooledObject();
            coin[i].transform.position = new Vector3(startPosition.x - (platformWidth / 2) + (distanceBetweenCoins / 2) + firstStartingPoint + i, startPosition.y, startPosition.z);
            coin[i].SetActive(true);
        }
       // coin = coinPool.GetPooledObject();
       // coin.transform.position = new Vector3(startPosition.x - (platformWidth / 2) + (distanceBetweenCoins / 2), startPosition.y, startPosition.z);
        
        

		/*GameObject coin1 = coinPool.GetPooledObject();
		coin1.transform.position = startPosition;
		coin1.SetActive(true);
		GameObject coin2 = coinPool.GetPooledObject();
		coin2.transform.position = new Vector3(startPosition.x - distanceBetweenCoins, startPosition.y, startPosition.z);
		coin2.SetActive(true);
		GameObject coin3 = coinPool.GetPooledObject();
		coin3.transform.position = new Vector3(startPosition.x + distanceBetweenCoins, startPosition.y, startPosition.z);
		coin3.SetActive(true);
         * */
	}



    public void IsCollidedWithSpike() {
        isCollidedWithSpike = true;
    }
}
