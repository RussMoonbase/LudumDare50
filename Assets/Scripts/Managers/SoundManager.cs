using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
   public enum SoundFxNames{
      Countdown,
      Go,
      Finish,
      Hit,
      Explosion,
      Win,
      Lose
   };

   private static SoundManager _instance;
   [SerializeField] private AudioSource _soundFxAudioSource;
   [SerializeField] private AudioClip _countdownAudioClip;
   [SerializeField] private AudioClip _goAudioClip;
   [SerializeField] private AudioClip _winAudioClip;
   [SerializeField] private AudioClip _loseAudioClip;
   public static SoundManager Instance
   {
      get
      {
         return _instance;
      }
   }

   private void Awake()
   {
      _instance = this;
   }
   // Start is called before the first frame update
   void Start()
   {

   }

   public void SetSoundFxAudioClip(SoundFxNames sFxName)
   {
      switch (sFxName)
      {
         case SoundFxNames.Countdown:
            _soundFxAudioSource.clip = _countdownAudioClip;
            break;
         case SoundFxNames.Go:
            _soundFxAudioSource.clip = _goAudioClip;
            break;
         case SoundFxNames.Win:
            _soundFxAudioSource.clip = _winAudioClip;
            break;
         case SoundFxNames.Lose:
            _soundFxAudioSource.clip = _loseAudioClip;
            break;
      }
   }

   public void PlaySoundEffect()
   {
      _soundFxAudioSource.Play();
   }

   public void PlayWinSoundEffect()
   {
      SetSoundFxAudioClip(SoundFxNames.Win);
      PlaySoundEffect();
   }

   public void PlayLoseSoundEffect()
   {
      SetSoundFxAudioClip(SoundFxNames.Lose);
      PlaySoundEffect();
   }

}
