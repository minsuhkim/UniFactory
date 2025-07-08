using System;
using System.Collections;
using UnityEngine;

namespace _01.Scripts
{

    public class ProductController : MonoBehaviour
    {
        [SerializeField] private float _speed = 0.05f;
        [SerializeField] private LayerMask _turnLayer;
        [SerializeField] private float _turnTime = 1f;
        
        private bool _isMoving = true;
        private bool _isPerformedFirstTurn = false;

        private float _workTime = 2f;
        
        private void Awake()
        {
            Debug.Log(transform.forward);
        }

        private void Update()
        {
            if (_isMoving == true)
            {
                transform.Translate(Vector3.forward * _speed * Time.deltaTime, Space.Self);
            }
        }

        private IEnumerator C_Turn(TurnDirection direction)
        {
            Quaternion current = transform.rotation;
            Quaternion dest = transform.rotation * (direction == TurnDirection.Left ? Quaternion.Euler(0,-90,0) : Quaternion.Euler(0,90,0));
            
            Debug.Log($"Current: {current.eulerAngles}, Destination: {dest.eulerAngles}");
            float elapsedtime = 0f;
            while (elapsedtime < _turnTime)
            {
                elapsedtime += Time.deltaTime;
                transform.rotation = Quaternion.Slerp(current, dest, elapsedtime / _turnTime);
                yield return null;
            }
            // transform.rotation = dest;
            Debug.Log(transform.forward);
        }

        private IEnumerator C_Wait()
        {
            _isMoving = false;
            yield return new WaitForSeconds(_workTime);
            _isMoving = true;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 10)
            {
                Debug.Log("Turn Start!");
                TurningZone zone = other.gameObject.GetComponent<TurningZone>();
                if (zone.IsStartZone())
                {
                    if (_isPerformedFirstTurn == true)
                    {
                        return;
                    }
                    else
                    {
                        _isPerformedFirstTurn = true;
                    }
                }
                TurnDirection direction = zone.GetTurnDirection();
                StartCoroutine(C_Turn(direction));
            }
            
            else if (other.gameObject.layer == 11)
            {
                Debug.Log("Destroy!");
                Destroy(gameObject);
            }
            
            else if (other.gameObject.layer == 12)
            {
                ArmController armController = other.gameObject.GetComponent<ArmController>();
                _workTime = armController.workTime;
                StartCoroutine(armController.C_StartWork());
                StartCoroutine(C_Wait());
            }
            
            else if (other.gameObject.layer == 13)
            {
                MachineController machineController = other.gameObject.GetComponent<MachineController>();
                StartCoroutine(machineController.C_Work(_workTime));
                StartCoroutine(C_Wait());
            }
        }
    }
}

