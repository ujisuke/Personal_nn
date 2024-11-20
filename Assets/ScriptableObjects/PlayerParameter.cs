using UnityEngine;

namespace Assets.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerParameter")]
    public class PlayerParameter : ObjectParameter
    {
        [SerializeField] private float attackingTime;
        public float AttackingTime => attackingTime;
        [SerializeField] private float _dashSpeed;
        public float DashSpeedRatio => _dashSpeed;
        [SerializeField] private float _dashingTime;
        public float DashingTime => _dashingTime;
    }
}