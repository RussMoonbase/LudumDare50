using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLauncher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.tag == "Player")
      {
         CarController carController = other.gameObject.GetComponent<CarController>();

         if (carController)
         {
            carController.LaunchCar();
         }
      }
   }
}
