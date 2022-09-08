
using System;

[Serializable]
public struct LevelFinishStruct
{
    public string _packName;
    public bool _first;
    public bool _second;
    public bool _third;
    public bool _four;
    public bool _five;

    public LevelFinishStruct(string _packName, bool _first, bool _second, bool _third, bool _four, bool _five)
    {
        this._packName = _packName;
        this._first = _first;
        this._second = _second;
        this._third = _third;
        this._four = _four;
        this._five = _five;
    }
}
