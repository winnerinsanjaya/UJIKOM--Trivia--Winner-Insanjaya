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

        [SerializeField]
        private Text _goldText;

        private void Start()
        {
            _backButton.onClick.AddListener(GoBack);
        }

        private void GoBack()
        {
            SceneManager.LoadScene("Home");
        }

        private void Update()
        {
            int curCoin = PlayerPrefs.GetInt("CoinDB");
            _goldText.text = curCoin.ToString();
        }
    }
}
