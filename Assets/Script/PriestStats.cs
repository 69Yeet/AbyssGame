using UnityEngine;

public class PriestStats : MonoBehaviour
{
    private int abyssLevel;
    [SerializeField] private int maxAbyssLevel = 9;
    [SerializeField] private int follyAbyssLevel = 3;

    public void IncreaseAbyss(int addedLevel)
    {
        abyssLevel += addedLevel;
    }
}
