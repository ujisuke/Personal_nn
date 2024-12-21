using Unity.Mathematics;

namespace Assets.Scripts.Objects
{
    public class HP
    {
        private readonly int _currentHP;
        public int CurrentHP => _currentHP;
        private readonly int _maxHP;
        public int MaxHP => _maxHP;

        public HP(int maxHP, int currentHP)
        {
            _maxHP = maxHP;
            _currentHP = math.min(maxHP, math.max(currentHP, 0));
        }

        public static HP Initialize(int maxHP)
        {
            return new HP(maxHP, maxHP);
        }

        public HP TakeDamage(int damage)
        {
            return new HP(_maxHP, _currentHP - damage);
        }

        public HP GetZero()
        {
            return new HP(_maxHP, 0);
        }

        public bool IsZero()
        {
            return _currentHP <= 0f;
        }

        public bool IsFatalDamage(int damage)
        {
            return _currentHP - damage <= 0f;
        }
    }
}