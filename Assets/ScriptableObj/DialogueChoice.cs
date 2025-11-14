using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueChoice", menuName = "Scriptable Objects/DialogueChoice")]
[System.Serializable]
public class DialogueChoice : ScriptableObject
{
    [SerializeField] public List<DialogueChoices> dialogueChoices;
}
