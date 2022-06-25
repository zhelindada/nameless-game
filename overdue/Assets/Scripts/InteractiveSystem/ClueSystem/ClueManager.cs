using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueManager : MonoSingleton<ClueManager>
{
    List<ClueData> allClueDatas;
    List<ClueData> foundClues;

    public void Awake()
    {
        Init();
        allClueDatas = new List<ClueData>();
        foundClues = new List<ClueData>();
        foreach (ClueData clue in Resources.LoadAll("Clue Datas", typeof(ClueData))) {
            Debug.Log("Clue loaded: " + clue);
        }
    }

    public bool AddNewFoundClue(int index) {
        if (!IsValidClue(index)) {
            Debug.Log("ClueManager: Invalid clue index: " + index);
            return false;
        }
        if (ContainsClue(index))
        {
            Debug.Log("ClueManager: Clue already found: " + index);
            return false;
        }
        var clueData = Resources.Load<ClueData>("Clue Datas/" + index);
        if (clueData == null)
        {
            Debug.LogError("ClueManager: Attempted to get ClueData resource failed: " + index + " - does not exist");
            return false;
        }
        Instance.foundClues.Add(clueData);

        return true;
    }

    public bool ContainsClue(int index) {
        bool contains = false;
        foreach (ClueData data in Instance.foundClues) {
            if (data.index == index)
                contains = true;
        }
        return contains;
    }

    public List<ClueData> GetFoundClues() {
        return Instance.foundClues;
    }

    private bool IsValidClue(int index) {
        bool isValid = false;
        foreach (ClueData data in Instance.allClueDatas)
        {
            if (data.index == index)
                isValid = true;
        }
        return isValid;
    }
}
