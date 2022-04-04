using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RaceManager : MonoBehaviour
{
   private static RaceManager _instance;
   public static RaceManager Instance
   {
      get
      {
         return _instance;
      }
   }

   public UnityEvent RaceStarted = new UnityEvent();
   public UnityEvent RaceFinished = new UnityEvent();
   [SerializeField] private int _currentCheckpointNumber = 0;
   [SerializeField] private int _maxCheckpoints;
   [SerializeField] private int _attachedBombsOnPlayer = 8;
   private bool _hasRaceFinished = false;

   private void Awake()
   {
      _instance = this;
   }

   // Start is called before the first frame update
   void Start()
   {
      _hasRaceFinished = false;
      _currentCheckpointNumber = 0;
      StartCoroutine(CountdownToStartRoutine());
   }

   // Update is called once per frame
   void Update()
   {

   }

   private IEnumerator CountdownToStartRoutine()
   {
      SoundManager.Instance.SetSoundFxAudioClip(SoundManager.SoundFxNames.Countdown);
      SoundManager.Instance.PlaySoundEffect();
      RaceUIManager.Instance.SetCountDownText("3");
      yield return new WaitForSeconds(1.0f);
      SoundManager.Instance.PlaySoundEffect();
      RaceUIManager.Instance.SetCountDownText("2");
      yield return new WaitForSeconds(1.0f);
      SoundManager.Instance.PlaySoundEffect();
      RaceUIManager.Instance.SetCountDownText("1");
      yield return new WaitForSeconds(1.0f);
      SoundManager.Instance.SetSoundFxAudioClip(SoundManager.SoundFxNames.Go);
      SoundManager.Instance.PlaySoundEffect();
      RaceUIManager.Instance.SetCountDownText("GO!");
      RaceStarted.Invoke();
      yield return new WaitForSeconds(1.0f);
      RaceUIManager.Instance.SetCountdownCanvasGroupAlpha(0.0f);
   }

   public int GetCurrentCheckpointNumber()
   {
      return _currentCheckpointNumber;
   }

   public void SetCurrentCheckpointNumber(int newNum)
   {
      if (newNum > _currentCheckpointNumber)
      {
         _currentCheckpointNumber = newNum;
      }
   }

   public void FinishLineHitByPlayer()
   {
      if (!_hasRaceFinished)
      {
         _hasRaceFinished = true;

         if (_attachedBombsOnPlayer == 0 && _currentCheckpointNumber == _maxCheckpoints)
         {
            RaceUIManager.Instance.SetWinOrLoseText(true);
            SoundManager.Instance.PlayWinSoundEffect();
         }
         else
         {
            RaceUIManager.Instance.SetWinOrLoseText(false);
            SoundManager.Instance.PlayLoseSoundEffect();
         }
         
         RaceFinished.Invoke();
      }
      
   }

   public void SubtractBombAmount()
   {
      --_attachedBombsOnPlayer;
   }
}
