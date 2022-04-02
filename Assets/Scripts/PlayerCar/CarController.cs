using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
   [SerializeField] private float _speed;
   [SerializeField] private float _maxSpeed;
   [SerializeField] private float _turnSpeed;

   [SerializeField] private Rigidbody _rigidbody;
   [SerializeField] private Transform[] _wheelTurnPoints;

   private float _forwardInput;
   private float _rightInput;

   // Start is called before the first frame update
   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {
      _forwardInput = Input.GetAxis("Vertical");
      _rightInput = Input.GetAxis("Horizontal");
   }

   private void FixedUpdate()
   {
      if (_forwardInput != 0)
      {
         _rigidbody.AddForce(this.transform.forward * _forwardInput * _speed);
      }
      
      if (_rightInput != 0)
      {
         _rigidbody.AddForceAtPosition(this.transform.right * _rightInput * _turnSpeed, transform.TransformPoint(_wheelTurnPoints[0].localPosition));
         _rigidbody.AddForceAtPosition(this.transform.right * _rightInput * _turnSpeed, transform.TransformPoint(_wheelTurnPoints[1].localPosition));
      }
   }
}
