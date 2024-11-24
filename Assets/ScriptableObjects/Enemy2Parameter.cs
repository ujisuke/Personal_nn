using UnityEngine;

namespace Assets.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Enemy2Parameter")]
    public class Enemy2Parameter : ObjectParameter
    {
        [SerializeField] private float _stopMoveImDistanceFromPlayer;
        public float StopMoveImDistanceFromPlayer => _stopMoveImDistanceFromPlayer;
        [SerializeField] private float _searchEnemy2Z;
        public float SearchEnemy2Z => _searchEnemy2Z;
        [SerializeField] private float _searchedTargetZ;
        public float SearchedTargetZ => _searchedTargetZ;
        [SerializeField] private float _attackCount;
        public float AttackCount => _attackCount;
        [SerializeField] private float _attackCoolDownTime;
        public float AttackCoolDownTime => _attackCoolDownTime;
        [SerializeField] private float _missingPlayerTime;
        public float MissingPlayerTime => _missingPlayerTime;
    }
}
