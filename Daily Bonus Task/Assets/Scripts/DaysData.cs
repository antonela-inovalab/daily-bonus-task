using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DaysData
{
    public int day;
    public int coins;
    public string status;
}

[System.Serializable]
public class Days
{
    public DaysData[] days;
}
