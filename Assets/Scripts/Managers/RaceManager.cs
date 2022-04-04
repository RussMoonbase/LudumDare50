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
   public ParticleSystem[] playerCarExpolsionEffects;
   public int playerPosition = 0;

   private bool _hasRaceFinished = false;
   public GameObject playerCarModel;
   public GameObject playerCarNonMergedModel;
   public GameObject[] bombModels;
   public GameObject[] raycastObjects;
   public Rigidbody[] nonMergedCarModelRigidbodies;
   private bool _explodeCarRoutineCalled = false;

   private void Awake()
   {
      _instance = this;
   }

   // Start is called before the first frame update
   void Start()
   {
      _explodeCarRoutineCalled = false;
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
      RaceUIManager.Instance.SetCountDownText("2");
      SoundManager.Instance.PlaySoundEffect();
      yield return new WaitForSeconds(1.0f);
      RaceUIManager.Instance.SetCountDownText("1");
      SoundManager.Instance.PlaySoundEffect();
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
            RaceUIManager.Instance.SetFinalPositionText(playerPosition);
            SoundManager.Instance.PlayWinSoundEffect();
         }
         else
         {
            if (_attachedBombsOnPlayer > 0)
            {
               RaceUIManager.Instance.SetLoseExplanationText("You still have bombs attached!");

               if (!_explodeCarRoutineCalled)
               {
                  _explodeCarRoutineCalled = true;
                  StartCoroutine(ExplodeCarRoutine());
               }
            }
            else
            {
               RaceUIManager.Instance.SetLoseExplanationText("You missed a checkpoint...");
            }


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

   private IEnumerator ExplodeCarRoutine()
   {
      yield return new WaitForSeconds(2.0f);
      ExplodeCar();
   }

   private void ExplodeCar()
   {
      for (int i = 0; i < raycastObjects.Length; i++)
      {
         raycastObjects[i].SetActive(false);
      }

      for (int i = 0; i < playerCarExpolsionEffects.Length; i++)
      {
         playerCarExpolsionEffects[i].Play();
      }
      SoundManager.Instance.PlayExplosionSoundEffect();

      for (int i = 0; i < bombModels.Length; i++)
      {
         bombModels[i].SetActive(false);
      }

      playerCarModel.SetActive(false);
      playerCarNonMergedModel.SetActive(true);

      for (int i = 0; i < nonMergedCarModelRigidbodies.Length; i++)
      {
         nonMergedCarModelRigidbodies[i].AddExplosionForce(2000.0f, nonMergedCarModelRigidbodies[i].gameObject.transform.position, 5.0f);
      }

   }
}
