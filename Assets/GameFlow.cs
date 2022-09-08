using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Trivia.DB;
using Trivia.Save;
using Trivia.currency;
using UnityEngine.SceneManagement;

namespace Trivia.Gameplay
{
    public class GameFlow : MonoBehaviour
    {
        [SerializeField]
        private int _answer;

        public void AnswerQuestion(int choice)
        {
            if (choice == _answer)
            {
                Debug.Log("Benar");
                SetWin();
            }

            if (choice != _answer)
            {
                Debug.Log("Salah");
                SetLose();
            }
        }

        public void SetAnswer(int answer)
        {
            _answer = answer;
        }

        public void SetWin()
        {
            Database db = FindObjectOfType<Database>();

            int current = PlayerPrefs.GetInt(db._currentPack + "acumulate")+1;

            if (!db.QNA[db._currentQuestion - 1].Finished)
            {

                db.SetFinished();
                Debug.Log("WIN");
                string packName = db._currentPack;
                int levelName = db._currentQuestion;
                string names = packName.Substring(packName.Length - 1);
                string LevelID = names + "-" + levelName;
                EventManager.TriggerEvent("FinishLevel", LevelID);
                EventManager.TriggerEvent("LevelADD", packName);
                if (current < 5)
                {
                    NextLevel(db._currentQuestion);
                }
                if(current >= 5)
                {
                    SceneManager.LoadScene("Pack");
                }


            }

            else
            {

                Debug.Log("WIN BUT HAVE FINISHED BEFORE");
                //if doesnt
            }

        }

        public void SetLose()
        {
            SceneManager.LoadScene("Level");
            Debug.Log("LOSE");
        }

        private void NextLevel(int next)
        {
            Database db = FindObjectOfType<Database>();
            db._currentQuestion = next + 1;
            SceneManager.LoadScene("Gameplay");
        }
    }

}