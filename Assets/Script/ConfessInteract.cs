using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class ConfessInteract : MonoBehaviour
{
    [SerializeField] private Vector3 confessionalLoc;

    private delegate void ConfessionInteract(Vector3 rot, Vector3 pos);
    private event ConfessionInteract OnConfessing;

    public delegate void ConfessionEnd();
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

    public void GetNum(int pos)
    {
        confessionNum = pos;
    }
}
