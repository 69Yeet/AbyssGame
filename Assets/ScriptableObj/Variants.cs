using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Variants", menuName = "Scriptable Objects/Variants")]
public class Variants : ScriptableObject
{
    public int abyssLevel;
    public bool isPhysicalDamage;
    public bool hasRelatedResponse;
    public bool getsRelatedResponse;
    public bool wasAngered;
    public Texture2D texture;
    public GameObject model;
    public DialogueChoice scriptObj;
}
