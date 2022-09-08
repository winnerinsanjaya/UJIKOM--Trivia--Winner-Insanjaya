using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Trivia.DB;

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


        private void Start()
        {
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

                string name = db._packsList[i].packName;

                buttonTxt.text = name;

                int price = db._packsList[i]._packPrice;

                Button createdPackButton = packBtn.GetComponent<Button>();
                _packButtonList.Add(createdPackButton);
                createdPackButton.onClick.AddListener(delegate
                {
                    OnClickedPack(name, price);
                });
            }
        }

        private void OnClickedPack(string name, int price)
        {
            Database db = FindObjectOfType<Database>();
            db.SetPack(name);

            Debug.Log(price.ToString());

            db.LoadQNA(name);

            LevelScene();
        }

        private void LevelScene()
        {
            SceneManager.LoadScene("Level");
        }
    }
}
