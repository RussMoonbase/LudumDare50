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
   [SerializeField] private int _currentCheckpointNumber = 0;

   private void Awake()
   {
      _instance = this;
   }

   // Start is called before the first frame update
   void Start()
   {
      _currentCheckpointNumber = 0;
      StartCoroutine(CountdownToStartRoutine());
   }

   // Update is called once per frame
   void Update()
   {

   }

   private IEnumerator CountdownToStartRoutine()
   {
      RaceUIManager.Instance.SetCountDownText("3");
      yield return new WaitForSeconds(1.0f);
      RaceUIManager.Instance.SetCountDownText("2");
      yield return new WaitForSeconds(1.0f);
      RaceUIManager.Instance.SetCountDownText("1");
      yield return new WaitForSeconds(1.0f);
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
}
