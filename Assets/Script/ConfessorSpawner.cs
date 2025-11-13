using System.Collections.Generic;
using UnityEngine;

public class ConfessorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] confessor;
    [SerializeField] private Transform[] confessionalPos;
    private Queue<GameObject> queue;

    void Awake()
    {
        queue = new Queue<GameObject>();
        foreach (GameObject obj in confessor)
        {
            queue.Enqueue(obj); 
        }


    }
    private void ConfessorSpawn(GameObject confessor, Transform position)
    {
        ConfessInteract instance = Instantiate(confessor, position).GetComponent<ConfessInteract>();
        instance.OnConfessingEnd += ContinueSpawn;
    }

    public void ContinueSpawn()
    {
        if (queue.Count > 0)
        {
            ConfessorSpawn(queue.Dequeue(), confessionalPos[0]);
        }
    }

    private void InitialSpawn()
    {
        foreach (Transform pos in confessionalPos)
        {
            ConfessorSpawn(queue.Dequeue(), pos);
        }
    }
}
