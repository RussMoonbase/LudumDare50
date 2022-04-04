using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
   bool _spaceBarPressed = false;
   // Start is called before the first frame update
   void Start()
   {
      _spaceBarPressed = false;
   }

   // Update is called once per frame
   void Update()
   {
      if (Input.GetKeyDown(KeyCode.Space))
      {
         if (!_spaceBarPressed)
         {
            _spaceBarPressed = true;
            SceneManager.LoadScene(1);
         }
      }
   }
}
