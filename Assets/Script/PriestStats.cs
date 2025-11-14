using System.Collections.Generic;
using UnityEngine;

public class PriestStats : MonoBehaviour
{
    [SerializeField] private int abyssLevel;
    [SerializeField] private int maxAbyssLevel = 9;
    [SerializeField] private int follyAbyssLevel = 3;
    [SerializeField] private GameObject endGame;
    private List<string> relatedResponse = new List<string>();
    [SerializeField] private int sinners;

    public void IncreaseAbyss(int pos, priestEvent priestAffliction)
    {
        sinners++;
        abyssLevel += priestAffliction.abyssDamager;
        if (abyssLevel > maxAbyssLevel)
        {
            endGame.GetComponent<EndGame>().LossFunction();
        }
    }

    public int GetStack()
    {
        return sinners;
    }
}

public struct priestEvent
{
    public int abyssDamager;
}


