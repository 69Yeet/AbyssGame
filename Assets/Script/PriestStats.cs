using System.Collections.Generic;
using UnityEngine;

public class PriestStats : MonoBehaviour
{
    private int abyssLevel;
    [SerializeField] private int maxAbyssLevel = 9;
    [SerializeField] private int follyAbyssLevel = 3;
    private List<string> relatedResponse = new List<string>();

    public void IncreaseAbyss(int pos, priestEvent priestAffliction)
    {
        abyssLevel += priestAffliction.abyssDamager;
    }
}

public struct priestEvent
{
    public int abyssDamager;
}
