using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Trivia.DB;
using Trivia.Save;
using System;

namespace Trivia.Pack
{
    public class PackData : MonoBehaviour
    {
        [SerializeField]
        private GameObject _packButtonFabs;

        [SerializeField]
        private Transform _packButtonContainer;

        [SerializeField]
        private List<Button> _packButtonList;

        private List<PackListStructure> packlist;
        private void Start()
        {
            LoadPack();
            CreatePackButton();
        }

        private void CreatePackButton()
        {



            int packCount = PlayerPrefs.GetInt("packCount");

            for (int i =0; i < packCount; i++)
            {
                GameObject packBtn = Instantiate(_packButtonFabs, new Vector3(0, 0, 0), Quaternion.identity, _packButtonContainer);
                Text buttonTxt = packBtn.GetComponentInChildren<Text>();

                Database db = FindObjectOfType<Database>();
                int price = db._packsList[i]._packPrice;
                string name = db._packsList[i].packName;


                Transform childLock = packBtn.transform.GetChild(1);
                

                if (db._packsList[i].isUnlocked)
                {
                    //Transform childBtn = packBtn.transform.GetChild(1);
                    childLock.gameObject.SetActive(false);
                }

                if (db._packsList[i].isCompleted)
                {
                    Transform childBtn = packBtn.transform.GetChild(2);
                    childBtn.gameObject.SetActive(true);
                }

                buttonTxt.text = name;
                Transform child1 = childLock.transform.GetChild(1);
                Transform child2 = child1.transform.GetChild(1);
                Text childtext = child2.GetComponent<Text>();
                childtext.text = price + " Gold";

                int indexNumber = i;

                Button createdPackButton = packBtn.GetComponent<Button>();
                _packButtonList.Add(createdPackButton);
                createdPackButton.onClick.AddListener(delegate
                {
                    OnClickedPack(name, price, indexNumber);
                });
            }
        }

        private void OnClickedPack(string name, int price, int index)
        {

            int curCoin = PlayerPrefs.GetInt("CoinDB");
            if (price <= curCoin)
            {
                Database db = FindObjectOfType<Database>();
                db.SetPack(name);

                Debug.Log(price.ToString());

                db.LoadQNA(name);

                db._packsList[index].isUnlocked = true;

                db._packsList[index].isCompleted = true;

                List<PackListStructure> packlist = db._packsList;
                // SaveData savedata = FindObjectOfType<SaveData>();

                // savedata.SaveUnlockPack(index);
                //savedata.SaveCompletedPack(index);
                db._currentPackIndex = index;

                EventManager.TriggerEvent("UnlockPack", name);
                EventManager.TriggerEvent("SaveUnlock", index);
                LevelScene();
            }

            
        }

        private void LevelScene()
        {
            SceneManager.LoadScene("Level");
        }

        private void LoadPack()
        {
            Database db = FindObjectOfType<Database>();
            for(int i = 0; i < db._packsList.Count; i++)
            {
                var json = PlayerPrefs.GetString("unlockPack" + i, "{}");
                UnlockPackStruct unlock = JsonUtility.FromJson<UnlockPackStruct>(json);
                db._packsList[i].isUnlocked = unlock.isUnlock;

                var jsonS = PlayerPrefs.GetString("completedPack" + i, "{}");
                UnlockPackStruct completed = JsonUtility.FromJson<UnlockPackStruct>(jsonS);
                db._packsList[i].isCompleted = completed.isUnlock;
            }
        }
    }
}

[Serializable]
public struct UnlockPackStruct
{
    public bool isUnlock;

    public UnlockPackStruct(bool isUnlock)
    {
        this.isUnlock = isUnlock;
    }
}

