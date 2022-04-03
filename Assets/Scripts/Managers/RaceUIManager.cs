using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaceUIManager : MonoBehaviour
{
   private static RaceUIManager _instance;
   public static RaceUIManager Instance
   {
      get
      {
         return _instance;
      }
   }

   [SerializeField] private TextMeshProUGUI _countdownText;
   [SerializeField] private CanvasGroup _countdownCanvasGroup;

   private void Awake()
   {
      _instance = this;
   }
   // Start is called before the first frame update
   void Start()
   {

   }

   public void SetCountDownText(string theStr)
   {
      _countdownText.text = theStr;
   }

   public void SetCountdownCanvasGroupAlpha(float alphaAmount)
   {
      if (alphaAmount >= 0.0f && alphaAmount <= 1.0f)
      {
         _countdownCanvasGroup.alpha = alphaAmount;
      }
   }
}
