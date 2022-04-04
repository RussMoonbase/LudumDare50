using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitController : MonoBehaviour
{
   public Transform[] waypoints;
   [SerializeField] private float _xOffset;
   [SerializeField] private Transform _aiCarTransform;
   [SerializeField] private float _maxDistanceFromCar;
   [SerializeField] private float _moveSpeed;
   private int _targetWaypointIndex = 0;
   private float _distanceToCar = 0.0f;

   private bool _canMove = true;
   // Start is called before the first frame update
   void Start()
   {
      _canMove = true;
      _targetWaypointIndex = 0;
   }

   // Update is called once per frame
   void Update()
   {
      if (!_canMove)
         return;

      _distanceToCar = (_aiCarTransform.position - this.transform.position).sqrMagnitude;
      if (_distanceToCar < _maxDistanceFromCar)
      {
         this.transform.LookAt(waypoints[_targetWaypointIndex]);
         this.transform.Translate(_xOffset, 0.0f, _moveSpeed);

         if (Vector3.Distance(this.gameObject.transform.position, waypoints[_targetWaypointIndex].transform.position) < 1.0f)
         {
            ++_targetWaypointIndex;

            if (_targetWaypointIndex >= waypoints.Length)
            {
               _targetWaypointIndex = 0;
            }
         }
      }

   }

   public void RaceFinished()
   {
      _canMove = false;
   }
}
