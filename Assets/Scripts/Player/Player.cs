using Assets.Scripts.Objects;
using UnityEngine;
using Assets.Scripts.Stage;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour, IObject
    {

        private void Awake()
        {
            ObjectFacade.SetPlayer(this);
        }

        public bool IsDamaging()
        {
            return false;
        }

        public void DamagedBy(IObject obj)
        {
            Debug.Log("Damaged");
        }

        public (Vector3 minImPos3, Vector3 maxImPos3) GetImPos3s()
        {
            Vector3 minRePos3 = transform.position - new Vector3(transform.localScale.x / 4f, 0f, 0f);
            Vector3 maxRePos3 = transform.position + new Vector3(transform.localScale.x / 4f, transform.localScale.y, transform.localScale.y / StageCreator._tileHeight);
            return (ObjectMove.ConvertToImPos3FromRePos3(minRePos3), ObjectMove.ConvertToImPos3FromRePos3(maxRePos3));
        }

        public Vector3 GetRePos3()
        {
            return transform.position;
        }
    }
}
