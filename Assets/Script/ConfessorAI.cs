using UnityEngine;
using UnityEngine.AI;

public class ConfessorAI : MonoBehaviour
{
    private Vector3 finalDestination;
    private NavMeshAgent agent;
    [SerializeField] private string sinTag = "Sinner";
    void Start()
    {
        gameObject.tag = sinTag;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.destination = finalDestination;
    }
    public void SetPosition(Vector3 pos)
    {
        finalDestination = pos;
    }
}
