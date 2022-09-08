using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trivia.DB;
using Trivia.Save;


namespace Trivia.currency
{
    public class Currency : MonoBehaviour
    {
        public static Currency currencyInstance;
        private void Awake()
        {
            if (currencyInstance == null)
            {
                currencyInstance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }


        private void OnEnable()
        {
            EventManager.StartListening("FinishLevel", AddCurrency);
        }

        private void OnDisable()
        {
            EventManager.StopListening("FinishLevel", AddCurrency);
        }
        public void AddCurrency(object levelid)
        {
            int curCoin = PlayerPrefs.GetInt("CoinDB");
            curCoin += 20;
            SaveData savedata = FindObjectOfType<SaveData>();
            savedata.SaveCoin(curCoin);
            Debug.Log("COIN" + curCoin);
        }

    }
}
