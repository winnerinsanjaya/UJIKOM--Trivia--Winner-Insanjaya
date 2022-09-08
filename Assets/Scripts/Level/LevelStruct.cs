
using System;

[Serializable]
public struct LevelStruct
{
    public int LevelID;
    public string PackID;
    public string question;
    public string A;
    public string B;
    public string C;
    public string D;
    public string hint;
    public int correct;

    public LevelStruct(int LevelID, string PackID, string question, string A, string B, string C, string D, string hint, int correct)
    {
        this.LevelID = LevelID;
        this.PackID = PackID; 
        this.question = question;
        this.A = A;
        this.B = B;
        this.C = C;
        this.D = D;
        this.hint = hint;
        this.correct = correct;
    }
}
