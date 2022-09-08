using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Trivia.DB;
using UnityEngine.SceneManagement;

namespace Trivia.Level
{
    public class LevelData : MonoBehaviour
    {
        [SerializeField]
        private GameObject _levelButtonFabs;

        [SerializeField]
        private Transform _levelButtonContainer;

        [SerializeField]
        private List<Button> _levelButtonList;

        private void Start()
        {
            CreateLevel();
        }

        private void CreateLevel()
        {

            for (int i = 0; i < 5; i++)
            {
                GameObject packBtn = Instantiate(_levelButtonFabs, new Vector3(0, 0, 0), Quaternion.identity, _levelButtonContainer);
                Text buttonTxt = packBtn.GetComponentInChildren<Text>();

                Database db = FindObjectOfType<Database>();


                if (db.QNA[i].Finished)
                {
                    Transform childBtn =  packBtn.transform.GetChild(1);
                    childBtn.gameObject.SetActive(true);
                }
                int questionnumber = i + 1;

                string name = db._currentPack;
                string names = name.Substring(name.Length - 1);
                buttonTxt.text = names +" - "+ (questionnumber);

                Button createdPackButton = packBtn.GetComponent<Button>();

                _levelButtonList.Add(createdPackButton);
                createdPackButton.onClick.AddListener(delegate
                {
                    OnClickedPack(questionnumber);
                });
            }

        }
        private void OnClickedPack(int levels)
        {
            Database db = FindObjectOfType<Database>();
            db._currentQuestion = levels;


            GameplayScene();
        }

        private void GameplayScene()
        {
            SceneManager.LoadScene("Gameplay");
        }
    }
}
