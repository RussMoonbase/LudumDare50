using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
   public AnimationCurve accelCurve;
   private float _accelSpeed;
   [SerializeField] private bool _isPlayerCar;
   [SerializeField] private float _maxSpeed;
   [SerializeField] private float _turnSpeed;
   [SerializeField] private float _driftForceConstant;
   [SerializeField] private float _driftPower;
   [SerializeField] private float _downPower;

   [SerializeField] private Rigidbody _rigidbody;
   [SerializeField] private Transform[] _wheelTurnPoints;
   [SerializeField] private Transform[] _driftPoints;
   [SerializeField] private Vector3 _carCenterOfMass;
   [SerializeField] private Transform _rabbitTransform;

   private float _forwardInput;
   private float _rightInput;

   private float _accelTimer = 0;
   private float _driftInput = 0;

   private bool _isGrounded = true;
   private bool _canBeLaunched = false;

   private bool _canCarMove = false;

   // Start is called before the first frame update
   void Start()
   {
      _accelTimer = 0.0f;
      _rigidbody.centerOfMass = _carCenterOfMass;
      _isGrounded = true;
      _canBeLaunched = false;
      _canCarMove = false;
   }

   // Update is called once per frame
   void Update()
   {
      if (!_canCarMove)
         return;

      if (_isPlayerCar)
      {
         if (_isGrounded)
         {
            _forwardInput = Input.GetAxis("Vertical");
         }
         else
         {
            if (!_canBeLaunched)
            {
               _forwardInput = Input.GetAxis("Vertical") * 0.2f;
            }

         }
         _rightInput = Input.GetAxis("Horizontal");

         if (Input.GetKey(KeyCode.L))
         {
            _driftInput = 1.0f;
            _rigidbody.drag = 1.5f;
         }
         else
         {
            _driftInput = 0.0f;

            if (_rigidbody.drag != 1.0f)
            {
               _rigidbody.drag = 1.0f;
            }
         }

         if (Input.GetKey(KeyCode.J))
         {
            _driftInput = -1.0f;
            _rigidbody.drag = 1.5f;
         }
      }
      else
      {
         Debug.Log("IS Ai CAr");
         _forwardInput = 0.75f;

         if (_rabbitTransform)
         {
            UpdateAiCarSteering();
         }
      }



      //Debug.Log("Drift Input = " + _driftInput);
      
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
      if (!_canCarMove)
         return;

      if (_forwardInput != 0)
      {
         _rigidbody.AddForce(this.transform.forward * _forwardInput * _accelSpeed);
         if (_rigidbody.velocity.magnitude > 524.0f)
         {
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, 524.0f);
         }

      }

      
      if (_rightInput != 0)
      {
         _rigidbody.AddForceAtPosition(this.transform.right * _rightInput * _turnSpeed, transform.TransformPoint(_wheelTurnPoints[0].localPosition));
         _rigidbody.AddForceAtPosition(this.transform.right * _rightInput * _turnSpeed, transform.TransformPoint(_wheelTurnPoints[1].localPosition));
      }

      if (_driftInput > 0.0f)
      {
         _rigidbody.AddForceAtPosition(this.transform.right * _driftForceConstant * -_driftPower, transform.TransformPoint(_driftPoints[0].localPosition));
         _rigidbody.AddForceAtPosition(this.transform.right * _driftForceConstant * -_driftPower, transform.TransformPoint(_driftPoints[1].localPosition));

      }

      if (_driftInput < 0.0f)
      {
         _rigidbody.AddForceAtPosition(this.transform.right * _driftForceConstant * _driftPower, transform.TransformPoint(_driftPoints[0].localPosition));
         _rigidbody.AddForceAtPosition(this.transform.right * _driftForceConstant * _driftPower, transform.TransformPoint(_driftPoints[1].localPosition));
      }

      if (!_isGrounded)
      {
         _rigidbody.AddForce(-Vector3.up * _downPower);
         //Debug.Log("IsGround = false");
      }

      //Debug.Log("Car Speed = " + _rigidbody.velocity.magnitude);
   }

   public float GetCurrentSpeed()
   {
      return _rigidbody.velocity.magnitude;
   }

   public void SetIsGrounded(bool booleanValue)
   {
      _isGrounded = booleanValue;
   }

   public void LaunchCar()
   {
      Debug.Log("Launch Car called");
      _rigidbody.AddForce(this.transform.forward * 200000.0f, ForceMode.Impulse);
      _rigidbody.AddForce(this.transform.up * 200000.0f, ForceMode.Impulse);
      _driftPower *= 3.0f;
   }

   public void StartRace()
   {
      _canCarMove = true;
   }

   private void UpdateAiCarSteering()
   {
      Vector3 relative = transform.InverseTransformPoint(_rabbitTransform.position);
      float steerAngle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
      float steerAmount = Mathf.Clamp(steerAngle * 0.5f, -1.0f, 1.0f);
      if (Mathf.Abs(steerAmount) > 0.75f)
      {
         _forwardInput = 0.35f;
      }
      else if (Mathf.Abs(steerAmount) < 0.25f)
      {
         _forwardInput = 0.85f;
      }
      _rightInput = steerAmount;
   }

   //private void OnDrawGizmos()
   //{
   //   Gizmos.color = Color.red;
   //   Gizmos.DrawSphere(this.transform.position + transform.rotation * _carCenterOfMass, 5.0f);
   //}
}
