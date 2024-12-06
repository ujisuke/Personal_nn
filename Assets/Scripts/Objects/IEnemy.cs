using UnityEngine;

namespace Assets.Scripts.Objects
{
    public interface IEnemyMain
    {
        public void SetReady();
        public void TakeDamage(int damage);
        public (Vector3 minImPos3, Vector3 maxImPos3) GetImPos3s(); 

        public void DestroyDeadObject();
        public void DestroyAliveObject();
    }
}