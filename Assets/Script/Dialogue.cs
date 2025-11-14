using UnityEngine;
using TMPro;
using System.Collections;
using System;
using System.Collections.Generic;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDialogue;
    [SerializeField] private float textSpeed;
    [SerializeField] private GameObject[] buttons = new GameObject[3];
    private int index;
    private string line;
    private DialogueChoice dialogue;

    void Start()
    {
        textDialogue.text = string.Empty;    
    }

    void OnEnable()
    {
        textDialogue.text = string.Empty;
        StartDialogue();
    }

    private void StartDialogue()
    {
        index = 0;
        line = dialogue.dialogueChoices[index].line;

        StartCoroutine(TypeLine());
    }

    public void SetDialogue(DialogueChoice scriptObj)
    {
        dialogue = scriptObj;
    }

    public void SetLineDialogue(string diaLine)
    {
        line = diaLine;
    }

    private IEnumerator TypeLine()
    {
        foreach (char c in line.ToCharArray())
        {
            textDialogue.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void NextLine()
    {
        textDialogue.text = string.Empty;
    }

    public void ButtonChoose1()
    {
        index = dialogue.dialogueChoices[index].dictAdd[0];
        DeActivateButton();
    }

    public void ButtonChoose2()
    {
        index = dialogue.dialogueChoices[index].dictAdd[1];
        DeActivateButton();
    }

    public void ButtonChoose3()
    {
        index = dialogue.dialogueChoices[index].dictAdd[2];
        DeActivateButton();
    }

    private void DeActivateButton()
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(false);
        }
    }

    public void StartConfessing(Vector3 rot, Vector3 pos, DialogueChoice scriptObj)
    {
        dialogue = scriptObj;
        gameObject.SetActive(true);

    }

    public void EndConfessing(int pos, priestEvent priest)
    {
        gameObject.SetActive(false);
    }

    private void ContinueDialogue()
    {
        int addressCount = dialogue.dialogueChoices[index].dictAdd.Count;
        if (addressCount == 1)
        {
            index = dialogue.dialogueChoices[index].dictAdd[0];
        }
        else if (addressCount > 1)
        {
            for (int i = 0; i < addressCount; i++)
            {
                buttons[i].SetActive(true);
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = dialogue.dialogueChoices[dialogue.dialogueChoices[index].dictAdd[i]].line;
            }
        }
    }
}

[System.Serializable]
public struct DialogueChoices
{
    public string line;
    public List<int> dictAdd;
    public int abyssLvl;
}
