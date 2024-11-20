using UnityEngine;

namespace Assets.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/DamageObjectParameter")]
    public class DamageObjectParameter : ScriptableObject
    {
        [SerializeField] private float _attackPower;
        public float AttackPower => _attackPower;
        [SerializeField] private float _readyTime;
        public float ReadyTime => _readyTime;
        [SerializeField] private float _damagingTime;
        public float DamagingTime => _damagingTime;
    }
}