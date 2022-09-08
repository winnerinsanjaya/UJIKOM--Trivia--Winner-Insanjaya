using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trivia.DB;


namespace Trivia.Save
{
    public class SaveData : MonoBehaviour
    {

        public static SaveData savedataInstance;
        private void Awake()
        {
            if (savedataInstance == null)
            {
                savedataInstance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }



            if (!PlayerPrefs.HasKey("CoinDB"))
            {
                PlayerPrefs.SetInt("CoinDB", 0);
            }
        }
        private void OnEnable()
        {
            EventManager.StartListening("FinishLevel", SaveLevel);
            EventManager.StartListening("SaveUnlock", SaveUnlockPacks);
            EventManager.StartListening("LevelADD", LevelFinish);
        }

        private void OnDisable()
        {
            EventManager.StopListening("FinishLevel", SaveLevel);
            EventManager.StopListening("SaveUnlock", SaveUnlockPacks);
            EventManager.StopListening("LevelADD", LevelFinish);
        }

        public void SaveLevelFinish(LevelFinishStruct level)
        {
            var json = JsonUtility.ToJson(level);
            PlayerPrefs.SetString(level._packName, json);

            Debug.Log(json);
        }

        public void SaveUnlockPack(int index)
        {
            Database db = FindObjectOfType<Database>();
            var json = JsonUtility.ToJson(new UnlockPackStruct(db._packsList[index].isUnlocked));
            PlayerPrefs.SetString("unlockPack"+index, json);

            Debug.Log(json);
        }

        public void SaveUnlockPacks(object index)
        {
            int i = (int)index;

            Database db = FindObjectOfType<Database>();
            var json = JsonUtility.ToJson(new UnlockPackStruct(db._packsList[i].isUnlocked));
            PlayerPrefs.SetString("unlockPack" + i, json);

            Debug.Log(json);
        }

        public void SaveCompletedPack(int index)
        {
            Database db = FindObjectOfType<Database>();
            var json = JsonUtility.ToJson(new UnlockPackStruct(db._packsList[index].isCompleted));
            PlayerPrefs.SetString("completedPack" + index, json);

            Debug.Log(json);
        }

        public void SaveCoin(int amt)
        {
            PlayerPrefs.SetInt("CoinDB", amt);
        }

        private void SaveLevel(object levelid)
        {
            Database db = FindObjectOfType<Database>();
            SaveLevelFinish(new LevelFinishStruct(db._currentPack, db.QNA[0].Finished, db.QNA[1].Finished, db.QNA[2].Finished, db.QNA[3].Finished, db.QNA[4].Finished));
        }

        private void LevelFinish(object packname)
        {
            string name = (string)packname;
            int current = PlayerPrefs.GetInt(name + "acumulate");
            int plus = current + 1;
            PlayerPrefs.SetInt(name+"acumulate", plus);

            if(plus == 5)
            {
                Database db = FindObjectOfType<Database>();
                int index = db._currentPackIndex;
                SaveCompletedPack(index);
            }
        }
    }
}
