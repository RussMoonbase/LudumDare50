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
   [SerializeField] private CanvasGroup _winCanvasGroup;
   [SerializeField] private CanvasGroup _loseCanvasGroup;
   [SerializeField] private TextMeshProUGUI _positionText;

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

   public void SetWinOrLoseText(bool playerWon)
   {
      if (playerWon)
      {
         _winCanvasGroup.alpha = 1.0f;
      }
      else
      {
         _loseCanvasGroup.alpha = 1.0f;
      }
   }

   public void SetPositionText(int posNum)
   {
      if (posNum == 1)
      {
         _positionText.text = "1/3";
      }
      else if (posNum == 2)
      {
         _positionText.text = "2/3";
      }
      else if (posNum == 3)
      {
         _positionText.text = "3/3";
      }
   }
}
