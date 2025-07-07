using UnityEngine;

namespace _01.Scripts
{
    public enum TurnDirection
    {
        Left,
        Right
    }
    
    public class TurningZone : MonoBehaviour
    {
        [SerializeField] private TurnDirection _turnDirection;
        [SerializeField] private bool _isStartZone;
        public TurnDirection GetTurnDirection()
        {
            return _turnDirection;
        }

        public bool IsStartZone()
        {
            return _isStartZone;
        }
    }
}