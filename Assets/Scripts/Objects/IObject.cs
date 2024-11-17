using UnityEngine;

namespace Assets.Scripts.Objects
{
    public interface IObject
    {
        public bool IsDamaging();

        public void DamagedBy(IObject obj);

        public (Vector3 minImPos3, Vector3 maxImPos3) GetImPos3s(); 

        public Vector3 GetRePos3();

        public void Destroy();
    }
}