using UnityEngine;

public class ConfessionVariants : MonoBehaviour
{
    [SerializeField] private int abyssLevel;
    [SerializeField] private bool isPhysicalDamage;
    [SerializeField] private bool hasRelatedResponse;
    [SerializeField] private bool getsRelatedResponse;
    [SerializeField] private bool wasAngered;
    [SerializeField] private Texture2D texture;

    //public delegate void Choice(GameObject npc_model);
    //public event Choice OnChoice;

    //private void OnCollisionEnter(Collision collision)
    //{
    //    print("Collision");
    //    OnChoice?.Invoke(model);
    //}
}
