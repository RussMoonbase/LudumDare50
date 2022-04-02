using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWheel : MonoBehaviour
{
   [SerializeField] private Rigidbody _carRigidbody;
   [SerializeField] private float _springConstant;
   [SerializeField] private float _dampingAmount;
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
   void Update()
   {

   }

   private void FixedUpdate()
   {
      if (Physics.Raycast(this.transform.position, -Vector3.up, out _hit, 10.0f, _groundLayerMask))
      {
         Debug.DrawRay(this.transform.position, -Vector3.up * _hit.distance, Color.yellow);
         Debug.Log(this.gameObject.name + " ray cast hit");
      }
      else
      {
         //Debug.DrawRay(this.transform.localPosition, -Vector3.up * 500.0f, Color.gray);
         Debug.Log(this.gameObject.name + " ray cast DID NOT hit");
      }
   }
}
