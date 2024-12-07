using UnityEditor.Animations;
using UnityEngine;

namespace Assets.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/EnemyDamageObjectParameter")]
    public class EnemyDamageObjectParameter : ScriptableObject
    {
        [SerializeField] private int _attackPower;
        public int AttackPower => _attackPower;
        [SerializeField] private float _readyTime;
        public float ReadyTime => _readyTime;
        [SerializeField] private float _damagingTime;
        public float DamagingTime => _damagingTime;
        [SerializeField] private AnimatorController _animatorController;
        public AnimatorController AnimatorController => _animatorController;
    }
}