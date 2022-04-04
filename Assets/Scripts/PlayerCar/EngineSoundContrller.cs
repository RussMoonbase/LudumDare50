using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSoundContrller : MonoBehaviour
{
   public AnimationCurve engineCurve;
   [SerializeField] private AudioClip[] _engineAudioClips;
   [SerializeField] private AudioSource _engineAudioSource1;
   [SerializeField] private AudioSource _engineAudioSource2;
   [SerializeField] private CarController _carController;

   private float _audioInverseLerp = 0.0f;
   private float _pitchLerp = 0.0f;
   // Start is called before the first frame update
   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {
      float curveValue = engineCurve.Evaluate(_carController.GetCurrentSpeed() / 524.0f);
      //Debug.Log("Engine Curve Value = " + curveValue);

      if (curveValue == 1.0f)
      {
         if (!_engineAudioSource2.isPlaying)
         {
            _engineAudioSource2.clip = _engineAudioClips[1];
            _engineAudioSource1.Stop();
            _engineAudioSource2.Play();
         }
         float testInverseLerp = Mathf.InverseLerp(engineCurve.keys[1].time, engineCurve.keys[2].time, _carController.GetCurrentSpeed() / 524.0f);
         float testLerp = Mathf.Lerp(0.6f, 1.0f, testInverseLerp);
         _engineAudioSource2.pitch = testLerp;
         //ChangeEnginePitch(1, 2, _engineAudioSource2);
      }
      else if (curveValue == 2.0f)
      {
         if (!_engineAudioSource1.isPlaying)
         {
            _engineAudioSource1.clip = _engineAudioClips[2];
            _engineAudioSource2.Stop();
            _engineAudioSource1.Play();
         }

         float testInverseLerp = Mathf.InverseLerp(engineCurve.keys[2].time, engineCurve.keys[3].time, _carController.GetCurrentSpeed() / 524.0f);
         float testLerp = Mathf.Lerp(0.6f, 1.0f, testInverseLerp);
         _engineAudioSource1.pitch = testLerp;
         //ChangeEnginePitch(2, 3, _engineAudioSource1);
      }
      else if (curveValue == 3.0f)
      {
         if (!_engineAudioSource2.isPlaying)
         {
            _engineAudioSource2.clip = _engineAudioClips[3];
            _engineAudioSource1.Stop();
            _engineAudioSource2.Play();
         }

         float testInverseLerp = Mathf.InverseLerp(engineCurve.keys[3].time, engineCurve.keys[4].time, _carController.GetCurrentSpeed() / 524.0f);
         float testLerp = Mathf.Lerp(0.6f, 1.0f, testInverseLerp);
         _engineAudioSource2.pitch = testLerp;
         //ChangeEnginePitch(3, 4, _engineAudioSource2);
      }

   }

   private void ChangeEnginePitch(int index1, int index2, AudioSource aSource)
   {
      _audioInverseLerp = Mathf.InverseLerp(engineCurve.keys[index1].time, engineCurve.keys[index2].time, _carController.GetCurrentSpeed() / 524.0f);
      _pitchLerp = Mathf.Lerp(0.6f, 1.0f, _audioInverseLerp);
      _engineAudioSource2.pitch = _pitchLerp;
   }
}
