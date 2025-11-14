using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDialogue;
    [SerializeField] private float textSpeed;
    [SerializeField] private GameObject[] buttons = new GameObject[3];
    private int index;
    private string line;
    private DialogueChoice dialogue;
    private bool canContinue;
    private GameObject toDestroy;
    private bool isNull;

    void Start()
    {
        textDialogue.text = string.Empty;    
    }

    void OnEnable()
    {
        isNull = false;
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
        textDialogue.text = string.Empty;
        foreach (char c in line.ToCharArray())
        {          
            yield return new WaitForSeconds(textSpeed);
            textDialogue.text += c;
        }
    }

    private void NextLine()
    {
        textDialogue.text = string.Empty;
    }

    public void ButtonDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (textDialogue.text == line)
            {
                ContinueDialogue();

            }
            else
            {
                StopAllCoroutines();
                textDialogue.text = string.Empty;
                textDialogue.text = line;
            }
        }
    }

    public void ButtonChoose1()
    {
        index = dialogue.dialogueChoices[index].dictAdd[0];
        index = dialogue.dialogueChoices[index].dictAdd[0];
        //Debug.Log(index);
        ContinueDialogue();
        DeActivateButton();
    }

    public void ButtonChoose2()
    {
        index = dialogue.dialogueChoices[index].dictAdd[1];
        index = dialogue.dialogueChoices[index].dictAdd[0];

        ContinueDialogue();
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
        //Debug.Log(addressCount);
        int isNullFun = dialogue.dialogueChoices[index].dictAdd[0];
        //Debug.Log(isNullFun);
        if (isNull)
        {
            Destroy(toDestroy);
        }
        if (isNullFun == 0)
        {
            isNull = true;
        }
        if (addressCount == 1)
        {
            line = dialogue.dialogueChoices[index].line;
            index = dialogue.dialogueChoices[index].dictAdd[0];
            //Debug.Log(index);
            StartCoroutine(TypeLine());
        }
        else if (addressCount > 1)
        {
            for (int i = 0; i < addressCount; i++)
            {
                buttons[i].SetActive(true);
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = dialogue.dialogueChoices[dialogue.dialogueChoices[index].dictAdd[i]].line;
            }
        }
        //Debug.Log(index);
        /*int addressCount = dialogue.dialogueChoices[index].dictAdd.Count;
        Debug.Log(addressCount);
        int isNull = dialogue.dialogueChoices[index].dictAdd[0];
        if (isNull == 0)
        {
            Destroy(toDestroy);
        }
        if (addressCount == 1)
        {
            //Debug.Log(index);
            index = dialogue.dialogueChoices[index].dictAdd[0];
            StartCoroutine(TypeLine());
        }
        else if (addressCount > 1)
        {
            for (int i = 0; i < addressCount; i++)
            {
                buttons[i].SetActive(true);
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = dialogue.dialogueChoices[dialogue.dialogueChoices[index].dictAdd[i]].line;
            }
        }

        line = dialogue.dialogueChoices[index].line;*/
    }

    public void SetDestroy(GameObject go)
    {
        toDestroy = go;
    }
}

[System.Serializable]
public struct DialogueChoices
{
    public string line;
    public List<int> dictAdd;
    public int abyssLvl;
}
