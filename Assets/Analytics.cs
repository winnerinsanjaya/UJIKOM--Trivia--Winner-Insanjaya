using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trivia.Analytics
{
    public class Analytics : MonoBehaviour
    {
        public static Analytics analyticsInstance;
        private void Awake()
        {
            if (analyticsInstance == null)
            {
                analyticsInstance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }


        private void OnEnable()
        {
            EventManager.StartListening("FinishLevel", TrackFinishLevel);
            EventManager.StartListening("UnlockPack", TrackUnlockPack);
        }

        private void OnDisable()
        {
            EventManager.StopListening("FinishLevel", TrackFinishLevel);
            EventManager.StopListening("UnlockPack", TrackUnlockPack);
        }


        private void TrackFinishLevel(object levelid)
        {
            string id = (string)levelid;
            Debug.Log(id + "FINISHED FOR FIRST TIME");
        }
        
        private void TrackUnlockPack(object name)
        {
            string id = (string)name;
            Debug.Log(id + "+ UNLOCKED");
        }
    }
}
