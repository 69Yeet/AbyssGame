using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class ConfessInteract : MonoBehaviour
{
    [SerializeField] private Vector3 confessionalLoc;

    private delegate void ConfessionInteract(Vector3 rot, Vector3 pos);
    private event ConfessionInteract OnConfessing;

    public delegate void ConfessionEnd(int num);
    public event ConfessionEnd OnConfessingEnd;
    private int confessionNum;

    ConfessionVariants variant;

    private void OnTriggerEnter(Collider other)
    {
        CameraRot camInstance = other.gameObject.GetComponentInParent<CameraRot>();
        PriestStats priestInstance = other.gameObject.GetComponent<PriestStats>();
        OnConfessing += camInstance.StartConfession;
        OnConfessing += camInstance.OverrideCam;
        OnConfessingEnd += camInstance.StopConfession;

        OnConfessing?.Invoke(Vector3.zero, confessionalLoc);
        OnConfessing = null;
    }

    public void SetNum(int pos)
    {
        confessionNum = pos;
    }

    private void OnDestroy()
    {
        OnConfessingEnd?.Invoke(confessionNum);
    }
}
