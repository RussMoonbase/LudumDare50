using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineChecker : MonoBehaviour
{
   // Start is called before the first frame update
   void Start()
   {

   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.tag == "Player")
      {
         RaceManager.Instance.FinishLineHitByPlayer();
         
      }
      else if (other.tag == "AI_Car_1" || other.tag == "AI_Car_2")
      {
         if (other.GetComponent<CarController>())
         {
            other.GetComponent<CarController>().SetCanCarMove(false);
         }
      }
   }

}
