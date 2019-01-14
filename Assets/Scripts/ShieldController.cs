
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShieldController : MonoBehaviour 
{
#region Variables
    private ShieldController theShieldController;
    public PowerUpManager thePowerUpManager;
#endregion

void Start () 
{
    theShieldController = FindObjectOfType<ShieldController>();
    theShieldController.gameObject.SetActive(false);
    thePowerUpManager.spikeTrigger(false);
}

void Update () 
{

}

// public void setActive(bool cond) {
//     theShieldController.gameObject.SetActive(cond);
// }
}
