using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Trivia.DB;

namespace Trivia.Gameplay
{
    public class QuizController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _choiceButtonFabs;

        [SerializeField]
        private Transform _choiceButtonContainer;

        [SerializeField]
        private List<Button> _choiceButtonList;

        [SerializeField]
        private Image _questImage;

        [SerializeField]
        private Text _questionText;


        private void Start()
        {
            InitQuiz();
            CreateChoice();
        }

        private void InitQuiz()
        {
            Database db = FindObjectOfType<Database>();
            int questNumber = db._currentQuestion - 1;
            _questImage.sprite = db.QNA[questNumber].Image;
            _questImage.SetNativeSize();

            _questionText.text = db.QNA[questNumber].Question;

            SetAnswer(questNumber);
        }

        private void CreateChoice()
        {
            Database db = FindObjectOfType<Database>();
            int questNumber = db._currentQuestion - 1;


            for (int i = 0; i < 4; i++)
            {

                GameObject packBtn = Instantiate(_choiceButtonFabs, new Vector3(0, 0, 0), Quaternion.identity, _choiceButtonContainer);
                Text buttonTxt = packBtn.GetComponentInChildren<Text>();
                buttonTxt.text = db.QNA[questNumber].Choice[i];

                int choiceNumber = i + 1;


                Button createdPackButton = packBtn.GetComponent<Button>();

                _choiceButtonList.Add(createdPackButton);
                createdPackButton.onClick.AddListener(delegate
                {
                    SetChoice(choiceNumber);
                });
            }

        }

        private void SetChoice(int choice)
        {
            Debug.Log(choice);

            GameFlow gameflow = GetComponent<GameFlow>();
            gameflow.AnswerQuestion(choice);
        }

        private void SetAnswer(int questNumber)
        {
            GameFlow gameflow = GetComponent<GameFlow>();

            Database db = FindObjectOfType<Database>();

            gameflow.SetAnswer(db.QNA[questNumber].Answer);
        }
    }
}
