using Assets.Scripts.Objects;
using UnityEngine;
using Assets.Scripts.Stage;
using System.Collections;
using Assets.ScriptableObjects;

namespace Assets.Scripts.Enemies
{
    public class Enemy1DamageObject : MonoBehaviour, IObject
    {
        [SerializeField] private ObjectParameter objectParameter;

        private void Awake()
        {
            ObjectFacade.AddEnemy(this);
            StartCoroutine(Suicide());
        }

        public void SetReady()
        {

        }

        private IEnumerator Suicide()
        {
            yield return new WaitForSeconds(0.1f);
            DestroyObject();
        }

        public bool IsDamaging()
        {
            return true;
        }

        public void DamageTo(IObject obj)
        {
            obj.TakeDamage(objectParameter.AttackPower);
        }

        public void TakeDamage(float damage)
        {

        }

        public (Vector3 minImPos3, Vector3 maxImPos3) GetImPos3s()
        {
            Vector3 minRePos3 = transform.position - new Vector3(transform.localScale.x / 2f, 0f, 0f);
            Vector3 maxRePos3 = transform.position + new Vector3(transform.localScale.x / 2f, 0f, transform.localScale.y / StageFacade._tileHeight);
            return (ObjectMove.ConvertToImPos3FromRePos3(minRePos3), ObjectMove.ConvertToImPos3FromRePos3(maxRePos3));
        }

        public Vector3 GetRePos3()
        {
            return transform.position;
        }

        public void DestroyObject()
        {
            ObjectFacade.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }
}
