using System;
using System.Collections;
using UnityEngine;

public class MachineController : MonoBehaviour
{
    private Vector3[] defaultPositions;
    [SerializeField] private Transform[] machinePositions;
    [SerializeField] private Transform[] dests;

    private void Awake()
    {
        defaultPositions = new Vector3[machinePositions.Length];
        for (int i = 0; i < defaultPositions.Length; i++)
        {
            defaultPositions[i] = machinePositions[i].localPosition;
        }
    }

    public IEnumerator C_Work(float workTime)
    {
        float time = workTime / 4;

        StartCoroutine(C_MoveFromTo(time, defaultPositions[0], dests[0].localPosition, 0));
        yield return StartCoroutine(C_MoveFromTo(time, defaultPositions[1], dests[1].localPosition, 1));
        
        yield return StartCoroutine(C_MoveFromTo(time, defaultPositions[2], dests[2].localPosition, 2));

        yield return StartCoroutine(C_MoveFromTo(time, dests[2].localPosition, defaultPositions[2], 2));

        StartCoroutine(C_MoveFromTo(time, dests[1].localPosition, defaultPositions[1], 1));
        yield return StartCoroutine(C_MoveFromTo(time, dests[0].localPosition, defaultPositions[0], 0));
    }
    
    private IEnumerator C_MoveFromTo(float workTime, Vector3 from, Vector3 to, int machineIndex)
    {
        float elapsedTime = 0;
        while (elapsedTime < workTime)
        {
            elapsedTime += Time.deltaTime;
            machinePositions[machineIndex].localPosition = Vector3.Lerp(from, to, elapsedTime / workTime);
            yield return null;
        }
        machinePositions[machineIndex].localPosition = to;
    }
}
