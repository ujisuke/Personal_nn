using Assets.Scripts.Objects;
using UnityEngine;
using Assets.Scripts.Stage;
using Assets.ScriptableObjects;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour, IObject
    {
        [SerializeField] private ObjectParameter objectParameter;
        private HP hP;
        private PlayerAttack playerAttack;
        private bool isReady = false;
        public bool IsReady => isReady;

        private void Awake()
        {
            ObjectFacade.AddPlayer(this);
            hP = HP.Initialize(objectParameter.MaxHP);
            playerAttack = GetComponent<PlayerAttack>();
            GetComponent<ObjectMove>().Initialize(transform.position);
        }

        public void SetReady()
        {
            isReady = true;
        }

        public bool IsDead()
        {
            return hP.IsZero();
        }

        public bool IsDamaging()
        {
            return playerAttack.IsDamaging;
        }

        public void DamageTo(IObject obj)
        {
            obj.TakeDamage(objectParameter.AttackPower);
        }

        public void TakeDamage(float damage)
        {
            hP = hP.TakeDamage(damage);
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

        public void DestroyObject()
        {
            ObjectFacade.RemovePlayer();
            Destroy(gameObject);
        }
    }
}
