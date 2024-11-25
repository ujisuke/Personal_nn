using UnityEditor.Animations;
using UnityEngine;

namespace Assets.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Enemy1Parameter")]
    public class Enemy1Parameter : ObjectParameter
    {
        [SerializeField] private float _stopMoveImDistanceFromPlayer;
        public float StopMoveImDistanceFromPlayer => _stopMoveImDistanceFromPlayer;
        [SerializeField] private float _attackImDistanceFromPlayer;
        public float AttackImDistanceFromPlayer => _attackImDistanceFromPlayer;

        [SerializeField] private int _attackPanelCount;
        public int AttackPanelCount => _attackPanelCount;
        [SerializeField] private int _attackPanelMinImRadius;
        public int AttackPanelMinImRadius => _attackPanelMinImRadius;
        [SerializeField] private int _attackPanelMaxImRadius;
        public int AttackPanelMaxImRadius => _attackPanelMaxImRadius;
        [SerializeField] private float _attackCoolDownTime;
        public float AttackCoolDownTime => _attackCoolDownTime;
    }
}
