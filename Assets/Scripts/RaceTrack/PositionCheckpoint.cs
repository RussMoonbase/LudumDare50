using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCheckpoint : MonoBehaviour
{
   //public int posCheckpointNum;

   //private int _playerPosCheckpointNum;
   //private int _aiCar1PosCheckpointNum;
   //private int _aiCar2PosCheckpointNum;
   [SerializeField] private float _playerTime;
   [SerializeField] private float _aiCar1Time;
   [SerializeField] private float _aiCar2Time;

   // Start is called before the first frame update
   void Start()
   {
      _playerTime = 1000000.0f;
      _aiCar1Time = 1000000.0f;
      _aiCar2Time = 1000000.0f;
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.tag == "Player")
      {
         _playerTime = Time.time;
      }

      if (other.tag == "AI_Car_1")
      {
         _aiCar1Time = Time.time;
      }

      if (other.tag == "AI_Car_2")
      {
         _aiCar2Time = Time.time;
      }

      if (_playerTime - _aiCar1Time < 0.0f && _playerTime - _aiCar2Time < 0.0f)
      {
         RaceUIManager.Instance.SetPositionText(1);
         RaceManager.Instance.playerPosition = 1;
      }
      else
      {
         if (_playerTime - _aiCar1Time > 0.0f && _playerTime - _aiCar2Time < 0.0f)
         {
            RaceUIManager.Instance.SetPositionText(2);
            RaceManager.Instance.playerPosition = 2;
         }
         else if (_playerTime - _aiCar1Time < 0.0f && _playerTime - _aiCar2Time > 0.0f)
         {
            RaceUIManager.Instance.SetPositionText(2);
            RaceManager.Instance.playerPosition = 2;
         }
         else if (_playerTime - _aiCar1Time > 0.0f && _playerTime - _aiCar2Time > 0.0f)
         {
            RaceUIManager.Instance.SetPositionText(3);
            RaceManager.Instance.playerPosition = 3;
         }

      }



      //_playerPosCheckpointNum = RaceManager.Instance.playerPositionCheckpoint;
      //_aiCar1PosCheckpointNum = RaceManager.Instance.aiCar1PositionCheckpoint;
      //_aiCar2PosCheckpointNum = RaceManager.Instance.aiCar2PositionCheckpoint;

      //if (_playerPosCheckpointNum > _aiCar1PosCheckpointNum && _playerPosCheckpointNum > _aiCar2PosCheckpointNum)
      //{
      //   RaceUIManager.Instance.SetPositionText(1);
      //}
      //else if ((_playerPosCheckpointNum > _aiCar1PosCheckpointNum && _playerPosCheckpointNum < _aiCar2PosCheckpointNum) ||
      //   (_playerPosCheckpointNum < _aiCar1PosCheckpointNum && _playerPosCheckpointNum > _aiCar2PosCheckpointNum))
      //{
      //   RaceUIManager.Instance.SetPositionText(2);
      //}
      //else if (_playerPosCheckpointNum < _aiCar1PosCheckpointNum && _playerPosCheckpointNum < _aiCar2PosCheckpointNum)
      //{
      //   RaceUIManager.Instance.SetPositionText(3);
      //}
   }
}
