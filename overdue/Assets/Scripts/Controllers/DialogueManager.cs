using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoSingleton<DialogueManager>
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject choicesPanel;
    [SerializeField] private GameObject choicesPrefab;
    [SerializeField] private GameObject goNextButton;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string[] choices;

    Story story;
    private void Awake()
    {
        Init();
        ClearEverything();
    }

    public void StartStory(TextAsset inkJson)
    {
        Debug.Log("DialogueManager: Starting story");
        ClearEverything();
        story = new Story(inkJson.text);
        dialoguePanel.SetActive(true);
        GoNextLine();
    }

    public void GoNextLine()
    {
        if (story.canContinue)
        {
            text.text = story.Continue();
            text.text.Trim();
        }
        else
        {
            ClearEverything();
        }

        List<Choice> choices = story.currentChoices;
        List<string> tags = story.currentTags;
        if (choices.Count == 0)
            return;
        foreach (Choice c in choices)
        {

            GameObject choiceButton = Instantiate(choicesPrefab, choicesPanel.transform, worldPositionStays: false);
            Button button = choiceButton.GetComponent<Button>();
            TextMeshProUGUI text = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            text.text = c.text;
            button.onClick.AddListener(() => { Instance.ChooseChoice(c.index); });
        }
        choicesPanel.SetActive(true);
        goNextButton.SetActive(false);
    }
    public void ChooseChoice(int choice)
    {
        story.ChooseChoiceIndex(choice);
        GoNextLine();
        goNextButton.SetActive(true);
        choicesPanel.SetActive(false);
    }

    private void ClearEverything()
    {
        dialoguePanel.SetActive(false);
        choicesPanel.SetActive(false);
        ClearChoices();
    }

    private void ClearChoices()
    {
        while (choicesPanel.transform.childCount != 0)
        {
            Destroy(choicesPanel.transform.GetChild(0));
        }
    }

    protected new void Init() {
        base.Init();
        Instance = this;
    }
}
