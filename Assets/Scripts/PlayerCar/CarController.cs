using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
   public AnimationCurve accelCurve;
   private float _accelSpeed;
   [SerializeField] private float _maxSpeed;
   [SerializeField] private float _turnSpeed;
   [SerializeField] private float _driftForceAmount;

   [SerializeField] private Rigidbody _rigidbody;
   [SerializeField] private Transform[] _wheelTurnPoints;
   [SerializeField] private Transform[] _driftPoints;

   private float _forwardInput;
   private float _rightInput;

   private float _accelTimer = 0;
   private float _driftInput = 0;

   // Start is called before the first frame update
   void Start()
   {
      _accelTimer = 0.0f;
   }

   // Update is called once per frame
   void Update()
   {
      _forwardInput = Input.GetAxis("Vertical");
      _rightInput = Input.GetAxis("Horizontal");

      if (Input.GetKey(KeyCode.L))
      {
         _driftInput = 1.0f;
      }
      else
      {
         _driftInput = 0.0f;
      }
      Debug.Log("Drift Input = " + _driftInput);
      
      if (_forwardInput > 0)
      {
         if (_accelTimer <= 7.0f)
         {
            _accelTimer += Time.deltaTime;
            
         }
         

      }
      else if (_forwardInput == 0)
      {
         //if (_accelTimer >= 0)
         //{
         //   _accelTimer -= Time.deltaTime;
         //}
         _accelTimer = 0.0f;
      }
      //Debug.Log("AccelTimer = " + _accelTimer);
      _accelSpeed = accelCurve.Evaluate(_accelTimer);
   }

   private void FixedUpdate()
   {
      if (_forwardInput != 0)
      {
         _rigidbody.AddForce(this.transform.forward * _forwardInput * _accelSpeed);
      }
      
      if (_rightInput != 0)
      {
         //_rigidbody.AddTorque(this.transform.up * _rightInput * _turnSpeed);
         _rigidbody.AddForceAtPosition(this.transform.right * -_rightInput * _turnSpeed, transform.TransformPoint(_wheelTurnPoints[0].localPosition));
         _rigidbody.AddForceAtPosition(this.transform.right * -_rightInput * _turnSpeed, transform.TransformPoint(_wheelTurnPoints[1].localPosition));
      }

      if (_driftInput > 0.0f)
      {
         //_rigidbody.AddForceAtPosition(this.transform.right * -_driftInput * _driftForceAmount, transform.TransformPoint(_driftPoints[0].localPosition));
         //_rigidbody.AddForceAtPosition(this.transform.right * -_driftInput * _driftForceAmount, transform.TransformPoint(_driftPoints[1].localPosition));
         _rigidbody.AddForceAtPosition(this.transform.right * _driftForceAmount * -30.0f, transform.TransformPoint(_driftPoints[2].localPosition));
      }
   }
}
