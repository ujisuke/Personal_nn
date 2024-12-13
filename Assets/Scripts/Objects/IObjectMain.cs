using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public interface IObjectMain
    {
        public void SetReady();
        public void TakeDamage(int damage);
        public UniTask BecomeInvincible();
        public bool IsDead();
        public (Vector3 minImPos3, Vector3 maxImPos3) GetImPos3s();
        public void SetCleaned();
        public void DestroyDeadObject();
        public void DestroyAliveObject();
    }
}