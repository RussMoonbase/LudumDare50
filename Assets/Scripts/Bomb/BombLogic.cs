using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLogic : MonoBehaviour
{
  // private bool _fxPlayed = false;
   [SerializeField] private ParticleSystem _particleSystem;
   // Start is called before the first frame update
   void Start()
   {

   }

   //// Update is called once per frame
   //void Update()
   //{

   //}

   private void OnTriggerEnter(Collider other)
   {
      Debug.Log("Bomb Hit Something");
      this.gameObject.transform.parent = other.transform;
      RaceManager.Instance.SubtractBombAmount();

      SoundManager.Instance.PlayExplosionSoundEffect();

      if (_particleSystem)
      {
         if(!_particleSystem.isPlaying)
         {
            Debug.Log("Explosion Played");
            _particleSystem.Play();
         }
         
      }
      if (other.GetComponent<Rigidbody>())
      {
         Rigidbody rBody = other.GetComponent<Rigidbody>();
         rBody.isKinematic = false;
         rBody.AddExplosionForce(2000.0f, other.transform.position, 5.0f);
      }

   }
}
