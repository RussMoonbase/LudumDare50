using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
   [SerializeField] private int _checkpointNum;

   // Start is called before the first frame update
   void Start()
   {

   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.tag == "Player")
      {
         if (RaceManager.Instance.GetCurrentCheckpointNumber() == _checkpointNum - 1)
         {
            RaceManager.Instance.SetCurrentCheckpointNumber(_checkpointNum);
         }
      }
   }
}
