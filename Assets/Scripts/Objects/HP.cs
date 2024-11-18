using Unity.Mathematics;

namespace Assets.Scripts.Objects
{
    public class HP
    {
        private readonly float _currentHP;
        public float CurrentHP => _currentHP;
        private readonly float _maxHP;

        public HP(float maxHP, float currentHP)
        {
            _maxHP = maxHP;
            _currentHP = math.min(maxHP, math.max(currentHP, 0f));
        }

        public static HP Initialize(float maxHP)
        {
            return new HP(maxHP, maxHP);
        }

        public HP TakeDamage(float damage)
        {
            return new HP(_maxHP, _currentHP - damage);
        }

        public bool IsZero()
        {
            return _currentHP <= 0f;
        }

    }
}