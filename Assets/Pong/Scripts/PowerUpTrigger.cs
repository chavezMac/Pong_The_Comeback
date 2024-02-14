using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTrigger : MonoBehaviour
{
      public GameManager gameManager;
   
      private void OnTriggerEnter(Collider other)
      {
         gameManager.OnPowerUpTrigger(this);
      }
}
