using UnityEngine;

public class ConfessionVariants : MonoBehaviour
{
    private int abyssLevel;
    private bool isPhysicalDamage;
    private bool hasRelatedResponse;
    private bool getsRelatedResponse;
    private bool wasAngered;
    [SerializeField] private GameObject model;

    //public delegate void Choice(GameObject npc_model);
    //public event Choice OnChoice;

    //private void OnCollisionEnter(Collision collision)
    //{
    //    print("Collision");
    //    OnChoice?.Invoke(model);
    //}
}
