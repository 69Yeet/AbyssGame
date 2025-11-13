using UnityEngine;

[CreateAssetMenu(fileName = "Variants", menuName = "Scriptable Objects/Variants")]
public class Variants : ScriptableObject
{
    [SerializeField] private int abyssLevel;
    [SerializeField] private bool isPhysicalDamage;
    [SerializeField] private bool hasRelatedResponse;
    [SerializeField] private bool getsRelatedResponse;
    [SerializeField] private bool wasAngered;
    [SerializeField] private GameObject model;
}
