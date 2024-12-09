using UnityEngine;

namespace Assets.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Enemy4Parameter")]
    public class Enemy4Parameter : ObjectParameter
    {
        [SerializeField] private float _attackCoolDownTime;
        public float AttackCoolDownTime => _attackCoolDownTime;
    }
}