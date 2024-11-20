using Unity.Mathematics;

namespace Assets.Scripts.Player
{
    public class Energy
    {
        private readonly int _currentEnergy;
        private readonly int _maxEnergy;

        public Energy(int maxEnergy, int currentEnergy)
        {
            _maxEnergy = maxEnergy;
            _currentEnergy = math.min(maxEnergy, math.max(currentEnergy, 0));
        }

        public static Energy Initialize(int maxEnergy)
        {
            return new Energy(maxEnergy, maxEnergy);
        }

        public Energy Consume(int consumption)
        {
            return new Energy(_maxEnergy, _currentEnergy - consumption);
        }

        public Energy Charge(int charge)
        {
            return new Energy(_maxEnergy, _currentEnergy + charge);
        }

        public bool IsFull()
        {
            return _currentEnergy >= _maxEnergy;
        }

        public bool CanUse(int consumption)
        {
            return _currentEnergy >= consumption;
        }
    }
}