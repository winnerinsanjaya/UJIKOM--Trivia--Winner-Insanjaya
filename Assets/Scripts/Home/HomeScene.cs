using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace Trivia.Home
{
    public class HomeScene : MonoBehaviour
    {
        [SerializeField]
        private Button _playButton;

        private void Awake()
        {
            _playButton.onClick.AddListener(StartPlay);
        }
        private void StartPlay()
        {
            SceneManager.LoadScene("Pack");
        } 
    }
}
