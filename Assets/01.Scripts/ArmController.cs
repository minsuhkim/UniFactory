using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    public Transform[] rigs;
    public Transform[] stopover1;
    public Transform[] stopover2;
    public Transform[] stopover3;
    public Transform[] stopover4;
    public Transform[] dests;
    private Quaternion[] _froms;
    private bool _isTurn = false;
    private int _state = 0;

    
    private Coroutine _turnRoutine;
    
    private void Awake()
    {
        _froms = new Quaternion[rigs.Length];
        for (int i = 0; i < rigs.Length; i++)
        {
            _froms[i] = rigs[i].localRotation;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // if (_isTurn == false)
            // {
            //     FirstRotate();
            // }
            // else
            // {
            //     TurnBack();
            // }
            if (_turnRoutine == null)
            {
                _turnRoutine = StartCoroutine(C_StartWork());
            }
        }
    }

    IEnumerator C_StartWork()
    {
        Debug.Log("from -> stopover1");
        StartCoroutine(C_RotateFromTo(_froms[0], stopover1[0].localRotation, rigs[0]));
        StartCoroutine(C_RotateFromTo(_froms[1], stopover1[1].localRotation, rigs[1]));
        StartCoroutine(C_RotateFromTo(_froms[2], stopover1[2].localRotation, rigs[2]));
        yield return StartCoroutine(C_RotateFromTo(_froms[3], stopover1[3].localRotation, rigs[3]));
        
        Debug.Log("stopover1 -> stopover2");
        StartCoroutine(C_RotateFromTo(stopover1[0].localRotation, stopover2[0].localRotation, rigs[0]));
        StartCoroutine(C_RotateFromTo(stopover1[1].localRotation, stopover2[1].localRotation, rigs[1]));
        StartCoroutine(C_RotateFromTo(stopover1[2].localRotation, stopover2[2].localRotation, rigs[2]));
        yield return StartCoroutine(C_RotateFromTo(stopover1[3].localRotation, stopover2[3].localRotation, rigs[3]));
        
        Debug.Log("stopover1 -> stopover2");
        StartCoroutine(C_RotateFromTo(stopover2[0].localRotation, stopover3[0].localRotation, rigs[0]));
        StartCoroutine(C_RotateFromTo(stopover2[1].localRotation, stopover3[1].localRotation, rigs[1]));
        StartCoroutine(C_RotateFromTo(stopover2[2].localRotation, stopover3[2].localRotation, rigs[2]));
        yield return StartCoroutine(C_RotateFromTo(stopover2[3].localRotation, stopover3[3].localRotation, rigs[3]));
        
        Debug.Log("stopover1 -> stopover2");
        StartCoroutine(C_RotateFromTo(stopover3[0].localRotation, stopover4[0].localRotation, rigs[0]));
        StartCoroutine(C_RotateFromTo(stopover3[1].localRotation, stopover4[1].localRotation, rigs[1]));
        StartCoroutine(C_RotateFromTo(stopover3[2].localRotation, stopover4[2].localRotation, rigs[2]));
        yield return StartCoroutine(C_RotateFromTo(stopover3[3].localRotation, stopover4[3].localRotation, rigs[3]));
        
        Debug.Log("stopover2 -> stopover3");
        StartCoroutine(C_RotateFromTo(stopover4[0].localRotation, dests[0].localRotation, rigs[0]));
        StartCoroutine(C_RotateFromTo(stopover4[1].localRotation, dests[1].localRotation, rigs[1]));
        StartCoroutine(C_RotateFromTo(stopover4[2].localRotation, dests[2].localRotation, rigs[2]));
        yield return StartCoroutine(C_RotateFromTo(stopover4[3].localRotation, dests[3].localRotation, rigs[3]));
        
        Debug.Log("dest -> from");
        StartCoroutine(C_RotateFromTo(dests[0].localRotation, _froms[0], rigs[0]));
        StartCoroutine(C_RotateFromTo(dests[1].localRotation, _froms[1], rigs[1]));
        StartCoroutine(C_RotateFromTo(dests[2].localRotation, _froms[2], rigs[2]));
        yield return StartCoroutine(C_RotateFromTo(dests[3].localRotation, _froms[3], rigs[3]));

        _turnRoutine = null;
    }

    private void FirstRotate()
    {
        _isTurn = true;
        for (int i = 0; i < rigs.Length; i++)
        {
            StartCoroutine(C_RotateFromTo(_froms[i], stopover1[i].localRotation, rigs[i]));
        }
    }

    private void TurnBack()
    {
        _isTurn = false;
        for (int i = 0; i < rigs.Length; i++)
        {
            StartCoroutine(C_RotateFromTo(stopover1[i].localRotation, _froms[i], rigs[i]));
        }
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