using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWheel : MonoBehaviour
{
   [SerializeField] private Rigidbody _carRigidbody;
   [SerializeField] private float _springConstant;
   [SerializeField] private float _dampingConstant;
   [SerializeField] private float _springRestLength;
   [SerializeField] private float _springMaxLength;
   private int _groundLayerMask; 

   private float _springForce = 0;
   private float _dampingForce = 0;
   private float _finalForce = 0;

   private RaycastHit _hit;

   // Start is called before the first frame update
   void Start()
   {
      _groundLayerMask = LayerMask.GetMask("Ground");
   }

   // Update is called once per frame
   //void Update()
   //{

   //}

   private void FixedUpdate()
   {
      if (Physics.Raycast(this.transform.position, -Vector3.up, out _hit, _springMaxLength, _groundLayerMask))
      {
         Debug.DrawRay(this.transform.position, -Vector3.up * _hit.distance, Color.yellow);
         _springForce = -_springConstant * (_hit.distance - _springRestLength);
         _dampingForce = -_dampingConstant * _carRigidbody.GetPointVelocity(transform.TransformPoint(this.transform.localPosition)).y;
         _finalForce = _springForce + _dampingForce;

         _carRigidbody.AddForceAtPosition(_finalForce * _hit.normal, transform.TransformPoint(this.transform.localPosition));
      }
      else
      {
         Debug.DrawRay(this.transform.position, -Vector3.up * _springMaxLength, Color.gray);
      }
   }
}
