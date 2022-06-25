using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    public TMP_Text text;
    public TMP_Text stackSize;

    public void SetName(string i) {
        text.text = i;
    }
    public void SetStackSize(int i)
    {
        stackSize.text = i.ToString();
    }
    public void SetStackSize(string i)
    {
        stackSize.text = i;
    }

    public void SetAsEmpty() {
        SetName("");
        stackSize.text = "";
    }
}
