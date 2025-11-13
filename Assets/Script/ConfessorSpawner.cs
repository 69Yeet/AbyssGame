using System.Collections.Generic;
using System.Linq;
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
        InitialSpawn();

    }
    private void ConfessorSpawn(GameObject confessor, Transform position, int num)
    {
        ConfessInteract instance = Instantiate(confessor, position.position, position.rotation).GetComponent<ConfessInteract>();
        instance.SetNum(num);
        instance.OnConfessingEnd += ContinueSpawn;
    }

    public void ContinueSpawn(int pos)
    {
        if (queue.Count > 0)
        {
            ConfessorSpawn(queue.Dequeue(), confessionalPos[pos], pos);
        }
    }

    private void InitialSpawn()
    {
        foreach (var pos in confessionalPos.Select((value, i) => (value, i)))
        {
            ConfessorSpawn(queue.Dequeue(), pos.value, pos.i);
        }
    }
}
