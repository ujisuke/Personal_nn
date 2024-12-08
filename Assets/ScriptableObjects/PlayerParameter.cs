using UnityEngine;

namespace Assets.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerParameter")]
    public class PlayerParameter : ObjectParameter
    {
        [SerializeField] private int _maxEnergy;
        public int MaxEnergy => _maxEnergy;
        [SerializeField] private int _chargingEnergyTime;
        public int ChargingEnergyTime => _chargingEnergyTime;
        [SerializeField] private float _attackingTime;
        public float AttackingTime => _attackingTime;
        [SerializeField] private int _attackEnergyConsumption;
        public int AttackEnergyConsumption => _attackEnergyConsumption;
        [SerializeField] private float _dashSpeedRatio;
        public float DashSpeedRatio => _dashSpeedRatio;
        [SerializeField] private float _dashingTime;
        public float DashingTime => _dashingTime;
        [SerializeField] private int _dashEnergyConsumption;
        public int DashEnergyConsumption => _dashEnergyConsumption;
    }
}