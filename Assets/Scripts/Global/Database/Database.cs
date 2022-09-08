using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Trivia.DB
{
    public class Database : MonoBehaviour
    {
        public static Database databaseInstance;
        public List<QuizStructure> QNA;

        [SerializeField]
        private int _linesQuest;
        
        public string _currentPack;

        public int _currentQuestion;

        public List<PackListStructure> _packsList;

        private void Awake()
        {
            if (databaseInstance == null)
            {
                databaseInstance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            SetPackCount();
        }

        public void LoadQNA(string pack)
        {
            var textFile = Resources.Load<TextAsset>(pack + "/" + pack);
            List<string> words = new List<string>(textFile.text.Split('\n'));

            int lineQNA = 0;

            
            for (int i = 0; i < words.Count / _linesQuest; i++)
            {
                int number = int.Parse(words[lineQNA]);
                Sprite image = Resources.Load<Sprite>(pack + "/" + number);
                string question = words[lineQNA + 1];
                string A = words[lineQNA + 2];
                string B = words[lineQNA + 3];
                string C = words[lineQNA + 4];
                string D = words[lineQNA + 5];
                
                int correct = int.Parse(words[lineQNA + 6]);

                string hint = words[lineQNA + 2 + (correct-1)];

                lineQNA += 7;
                AddQNA(number - 1, pack, image, question, A, B, C, D, hint, correct);
            }
        }
        private void AddQNA(int number, string packID, Sprite image, string question, string A, string B, string C, string D, string hint, int correct)
        {
            QNA[number].Image = Resources.Load<Sprite>(packID + "/" + (number+1).ToString());
            QNA[number].Question = question;
            QNA[number].Choice[0] = A;
            QNA[number].Choice[1] = B;
            QNA[number].Choice[2] = C;
            QNA[number].Choice[3] = D;
            QNA[number].Hint = hint;
            QNA[number].Answer = correct;
        }

        private void SetPackCount()
        {
            PlayerPrefs.SetInt("packCount", _packsList.Count);
        }

        public void SetPack(string packname)
        {
            _currentPack = packname;
        }

    }
}
