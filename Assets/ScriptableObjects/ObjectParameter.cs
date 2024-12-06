using UnityEngine;

namespace Assets.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ObjectParameter")]
    public class ObjectParameter : ScriptableObject
    {
        [SerializeField] private float _readyTime;
        public float ReadyTime => _readyTime;
        [SerializeField] private float _moveSpeed;
        public float MoveSpeed => _moveSpeed;
        [SerializeField] private float _jumpHeight;
        public float JumpHeight => _jumpHeight;
        [SerializeField] private float _jumpTime;
        public float JumpTime => _jumpTime;
        [SerializeField] private int _attackPower;
        public int AttackPower => _attackPower;
        [SerializeField] private int _maxHP;
        public int MaxHP => _maxHP;
        [SerializeField] private float _deadTime;
        public float DeadTime => _deadTime;
        [SerializeField] private EnemyDamageObjectParameter _enemyDamageObjectParameter;
        public EnemyDamageObjectParameter EnemyDamageObjectParameter => _enemyDamageObjectParameter;
    }
}