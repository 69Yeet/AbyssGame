using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class ConfessInteract : MonoBehaviour
{
    [SerializeField] private Vector3 confessionalLoc;

    private delegate void ConfessionInteract(Vector3 rot, Vector3 pos);
    private event ConfessionInteract OnConfessing;

    public delegate void ConfessionEnd(int num, priestEvent param);
    public event ConfessionEnd OnConfessingEnd;
    private int confessionNum;

    [SerializeField]private bool sinnerIn;

    [SerializeField] private Variants variant;

    private priestEvent priestAffliction = new priestEvent();

    private Transform sinnerSpawn;

    private bool priestRelated;

    void Awake()
    {
        priestRelated = false;
        StartCoroutine(SpawnNPC(variant.model, transform.position, 0.1f));
        
    }

    void Start()
    {
        //SpawnNPC(variant.model, transform.position, 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sinner"))
        {
            sinnerIn = true;
            Destroy(other.gameObject);
            return;
        }

        if (!sinnerIn)
        {
            return;
        }
        CameraRot camInstance = other.gameObject.GetComponentInParent<CameraRot>();
        PriestStats priestInstance = other.gameObject.GetComponent<PriestStats>();
        OnConfessing += camInstance.StartConfession;
        OnConfessing += camInstance.OverrideCam;
        OnConfessingEnd += camInstance.StopConfession;
        OnConfessingEnd += priestInstance.IncreaseAbyss;

        OnConfessing?.Invoke(Vector3.zero, confessionalLoc);
        OnConfessing = null;
    }

    public void SetNum(int pos)
    {
        confessionNum = pos;
    }

    private void OnDestroy()
    {
        OnConfessingEnd?.Invoke(confessionNum, priestAffliction);
        OnConfessingEnd = null;
    }

    private IEnumerator SpawnNPC(GameObject p_npc, Vector3 finalPos, float timeSec)
    {
        yield return new WaitForSeconds(timeSec);
        ConfessorAI instance = Instantiate(p_npc, sinnerSpawn.position, sinnerSpawn.rotation).GetComponent<ConfessorAI>();

        instance.SetPosition(finalPos);
    }

    public void SetSinnerSpawn(Transform spawnPos)
    {
        sinnerSpawn = spawnPos;
    }
}
