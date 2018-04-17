using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamInfo : MonoBehaviour {

    public string teamName;
    public int teamIndex;

    public TeamInfo(string name, int index)
    {
        teamName = name;
        teamIndex = index;
    }
}
