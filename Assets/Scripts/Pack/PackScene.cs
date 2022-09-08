using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Trivia.Pack
{
    public class PackScene : MonoBehaviour
    {
        [SerializeField]
        private Button _backButton;
        private void Start()
        {
            _backButton.onClick.AddListener(GoBack);
        }

        private void GoBack()
        {
            SceneManager.LoadScene("Home");
        }
    }
}
