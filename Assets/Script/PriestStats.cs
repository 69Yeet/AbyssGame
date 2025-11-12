using UnityEngine;

public class PriestStats : MonoBehaviour
{
    private float abyssLevel;
    private ConfessionVariants stats;
    private void OnCollisionEnter(Collision collision)
    {
        stats = collision.gameObject.GetComponent<ConfessionVariants>();
        stats.OnConfession += ReceiveAbyss;
    }

    private void ReceiveAbyss(int npc_abyssLevel)
    {
        abyssLevel += npc_abyssLevel;
        ConfessionVariants stats = GetComponent<ConfessionVariants>();
        stats.OnConfession -= ReceiveAbyss;
    }
}
