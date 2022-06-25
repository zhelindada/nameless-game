using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Clue Data")]
public class ClueData : ScriptableObject
{
    public string clueName;
    public int index;
    public string desc;
    public Sprite img;
}
