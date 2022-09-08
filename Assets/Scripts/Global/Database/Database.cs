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

        private LevelFinishStruct levelfinish;

        public int _currentPackIndex;

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

        private void Update()
        {


            int curCoin = PlayerPrefs.GetInt("CoinDB");
            Debug.Log("COIN" + curCoin);
        }
        public void LoadQNA(string pack)
        {
            var textFile = Resources.Load<TextAsset>(pack + "/" + pack);
            List<string> words = new List<string>(textFile.text.Split('\n'));

            int lineQNA = 0;

            var json = PlayerPrefs.GetString(pack, "{}");
            levelfinish = JsonUtility.FromJson<LevelFinishStruct>(json);

            for (int i = 0; i < words.Count / _linesQuest; i++)
            {
                int number = int.Parse(words[lineQNA]);
                string question = words[lineQNA + 1];
                string A = words[lineQNA + 2];
                string B = words[lineQNA + 3];
                string C = words[lineQNA + 4];
                string D = words[lineQNA + 5];
                
                int correct = int.Parse(words[lineQNA + 6]);

                string hint = words[lineQNA + 2 + (correct-1)];

                bool isFinished = false;
                if (number == 1)
                {
                    isFinished = levelfinish._first;
                } 
                if (number == 2)
                {
                    isFinished = levelfinish._second;
                } 
                if (number == 3)
                {
                    isFinished = levelfinish._third;
                } 
                if (number == 4)
                {
                    isFinished = levelfinish._four;
                } 
                if (number == 5)
                {
                    isFinished = levelfinish._five;
                }



                

                lineQNA += 7;
                AddQNA(new LevelStruct(number - 1, pack, question, A, B, C, D, hint, correct), isFinished);
            }
        }
        private void AddQNA(LevelStruct level, bool finish)
        {
            QNA[level.LevelID].Image = Resources.Load<Sprite>(level.PackID + "/" + (level.LevelID+1).ToString());
            QNA[level.LevelID].Question = level.question;
            QNA[level.LevelID].Choice[0] = level.A;
            QNA[level.LevelID].Choice[1] = level.B;
            QNA[level.LevelID].Choice[2] = level.C;
            QNA[level.LevelID].Choice[3] = level.D;
            QNA[level.LevelID].Hint = level.hint;
            QNA[level.LevelID].Answer = level.correct;
            QNA[level.LevelID].Finished = finish;
        }

        private void SetPackCount()
        {
            PlayerPrefs.SetInt("packCount", _packsList.Count);
        }

        public void SetPack(string packname)
        {
            _currentPack = packname;
        }

        public void SetFinished()
        {
            QNA[_currentQuestion-1].Finished = true;
        }

        public void LoadPack(List<PackListStructure> packlist)
        {
            for(int i = 0; i < 1; i++)
            {
                _packsList[i].isUnlocked = packlist[i].isUnlocked;
                _packsList[i].isCompleted = packlist[i].isCompleted;
            }
        }
    }
}
