using UnityEngine.UI;
using UnityEngine;
[System.Serializable]
public class QuizStructure
{
    public Sprite Image;
    public string Question;
    public string[] Choice;
    public string Hint;
    public int Answer;
    public bool Finished;
}
