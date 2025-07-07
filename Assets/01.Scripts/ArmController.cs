using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    public Transform[] rigs;
    public Transform[] dests;
    private Quaternion[] _froms;
    private int _state = 0;

    public float workTime = 2f;
    
    private Coroutine _turnRoutine;
    
    private void Awake()
    {
        _froms = new Quaternion[rigs.Length];
        for (int i = 0; i < rigs.Length; i++)
        {
            _froms[i] = rigs[i].localRotation;
        }
    }

    
    public IEnumerator C_StartWork()
    {
        Debug.Log("from -> stopover1");
        StartCoroutine(C_RotateFromTo(_froms[0], dests[0].localRotation, rigs[0]));
        StartCoroutine(C_RotateFromTo(_froms[1], dests[1].localRotation, rigs[1]));
        StartCoroutine(C_RotateFromTo(_froms[2], dests[2].localRotation, rigs[2]));
        yield return StartCoroutine(C_RotateFromTo(_froms[3], dests[3].localRotation, rigs[3]));
        
        Debug.Log("dest -> from");
        StartCoroutine(C_RotateFromTo(dests[0].localRotation, _froms[0], rigs[0]));
        StartCoroutine(C_RotateFromTo(dests[1].localRotation, _froms[1], rigs[1]));
        StartCoroutine(C_RotateFromTo(dests[2].localRotation, _froms[2], rigs[2]));
        yield return StartCoroutine(C_RotateFromTo(dests[3].localRotation, _froms[3], rigs[3]));

        _turnRoutine = null;
    }
    
    IEnumerator C_RotateFromTo(Quaternion from,  Quaternion to, Transform rotateTransform)
    {
        Debug.Log("C_Rotate");
        float elapsedTime = 0;
        float duration = 1f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            
            rotateTransform.localRotation = Quaternion.Slerp(from, to, elapsedTime / duration);
            yield return null;
        }
        rotateTransform.localRotation = to;
    }
}