using UnityEngine;

namespace Assets.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Enemy3Parameter")]
    public class Enemy3Parameter : ObjectParameter
    {
        [SerializeField] private float _attackCoolDownTime;
        public float AttackCoolDownTime => _attackCoolDownTime;
        [SerializeField] private float _waveMoveTime;
        public float WaveMoveTime => _waveMoveTime;
    }
}