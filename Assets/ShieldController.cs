/*
* Copyright (c) Anvar Abdulsatarov
* anvar-abd_97@mail.ru
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShieldController : MonoBehaviour 
{
#region Variables
    private ShieldController theShieldController;
#endregion

void Start () 
{
    theShieldController = FindObjectOfType<ShieldController>();
    theShieldController.setActive(false);
}

void Update () 
{

}

public void setActive(bool cond) {
    theShieldController.setActive(cond);
}
}
