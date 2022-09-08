using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Trivia.DB;

namespace Trivia.Gameplay
{
    public class GameplayScene : MonoBehaviour
    {
        [SerializeField]
        private Text _labelText;

        [SerializeField]
        private Button _backButton;


        private void Start()
        {
            _backButton.onClick.AddListener(GoToLevelScene);
            SetTittle();
        }

        private void SetTittle()
        {
            Database db = FindObjectOfType<Database>();
            string packName = db._currentPack;
            int levelName = db._currentQuestion;

            string names = packName.Substring(packName.Length - 1);

            _labelText.text = "Level "+ names + "-" + levelName;
        }
        public void GoToLevelScene()
        {
            SceneManager.LoadScene("Level");
        }

        public void GoToPackScene()
        {
            SceneManager.LoadScene("Level");
        }
    }
}
