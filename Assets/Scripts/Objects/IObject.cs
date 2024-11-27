using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public interface IObject
    {
        public void SetReady();
        public bool IsDamaging();
        public void DamageTo(IObject obj);
        public void TakeDamage(int damage);
        public (Vector3 minImPos3, Vector3 maxImPos3) GetImPos3s(); 
        public Vector3 GetRePos3();
        public void DestroyDeadObject();
        public void DestroyAliveObject();
    }
}