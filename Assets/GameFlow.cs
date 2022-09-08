using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Trivia.DB;

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
            Debug.Log("WIN");
        }

        public void SetLose()
        {
            Debug.Log("LOSE");
        }
    }

}