using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Trivia.Gameplay
{
    public class Countdown : MonoBehaviour
    {
        [SerializeField]
        private float _countdownTimer;
        private float _defTimer;

        [SerializeField]
        private Image _countdownFill;


        private void Start()
        {
            _defTimer = _countdownTimer;
        }

        private void Update()
        {
            CheckCountdown();
        }


        private void SetFill()
        {
            _countdownFill.fillAmount = _countdownTimer / _defTimer;
        }

        private void CheckCountdown()
        {
            _countdownTimer -= Time.deltaTime;
            SetFill();
            if (_countdownTimer <= 0)
            {
                GameFlow gameflow = FindObjectOfType<GameFlow>();
                gameflow.SetLose();
            }
        }
    }
}
