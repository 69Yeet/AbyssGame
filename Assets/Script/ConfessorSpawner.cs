using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConfessorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] confessor;
    [SerializeField] private Transform[] confessionalPos;
    [SerializeField] private Transform sinnerSpawn;
    [SerializeField] private GameObject dialogue;

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
        instance.SetSinnerSpawn(sinnerSpawn);
        instance.OnConfessingEnd += ContinueSpawn;
        instance.OnConfessing += dialogue.GetComponentInChildren<Dialogue>().StartConfessing;
        instance.OnConfessingEnd += dialogue.GetComponentInChildren<Dialogue>().EndConfessing;
        instance.SetCanva(dialogue);
    }

    public void ContinueSpawn(int pos, priestEvent param)
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
