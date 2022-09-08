using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Trivia.DB;

namespace Trivia.Level
{
    public class LevelScene : MonoBehaviour
    {
        [SerializeField]
        private Button _backButton;

        [SerializeField]
        private Text _labelText;

        private void Start()
        {
            _backButton.onClick.AddListener(GoBack);
            SetLabelText();
        }


        private void GoBack()
        {
            SceneManager.LoadScene("Pack");
        }

        private void SetLabelText()
        {
            Database db = FindObjectOfType<Database>();
            string levelname = db._currentPack;
            _labelText.text = "Level " + levelname;
        }
    }
}
