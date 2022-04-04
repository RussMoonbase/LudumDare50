using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionChecker : MonoBehaviour
{
   [SerializeField] private float _timer = 0.0f;
   // Start is called before the first frame update
   void Start()
   {
      _timer = 0.0f;
   }

   private void Update()
   {
      _timer += Time.deltaTime;
   }

   public float GetCurrentTimerAmount()
   {
      return _timer;
   }


}
