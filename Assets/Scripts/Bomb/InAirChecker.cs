using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InAirChecker : MonoBehaviour
{
   [SerializeField] private float _maxRayLength;
   [SerializeField] private Transform _carModelTransform;
   [SerializeField] private Vector3 _carModelOffset;
   private RaycastHit _hit;
   private int _groundLayerMask;
   private bool isInAir = false;

   public UnityEvent carInAir = new UnityEvent();
   public UnityEvent carGrounded = new UnityEvent();

   // Start is called before the first frame update
   void Start()
   {
      _groundLayerMask = LayerMask.GetMask("Ground");
      isInAir = false;
   }

   // Update is called once per frame
   void Update()
   {

   }

   private void FixedUpdate()
   {
      if (Physics.Raycast(this.transform.position, -Vector3.up, out _hit, _maxRayLength, _groundLayerMask))
      {
         Debug.DrawRay(this.transform.position, -Vector3.up * _hit.distance, Color.green);
         //_carModelTransform.position = _hit.point + _carModelOffset;

         if (isInAir)
         {
            isInAir = false;
            carGrounded.Invoke();
         }
      }
      else
      {
         if (!isInAir)
         {
            isInAir = true;
            carInAir.Invoke();
         }
         Debug.DrawRay(this.transform.position, -Vector3.up * _maxRayLength, Color.red);
      }
   }
}
