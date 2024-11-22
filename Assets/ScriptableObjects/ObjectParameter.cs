using UnityEngine;

namespace Assets.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ObjectParameter")]
    public class ObjectParameter : ScriptableObject
    {
        [SerializeField] private float _moveSpeed;
        public float MoveSpeed => _moveSpeed;
        [SerializeField] private float _jumpHeight;
        public float JumpHeight => _jumpHeight;
        [SerializeField] private float _jumpTime;
        public float JumpTime => _jumpTime;
        [SerializeField] private float _attackPower;
        public float AttackPower => _attackPower;
        [SerializeField]private float maxHP;
        public float MaxHP => maxHP;
        [SerializeField] private DamageObjectParameter _damageObjectParameter;
        public DamageObjectParameter DamageObjectParameter => _damageObjectParameter;
    }
}