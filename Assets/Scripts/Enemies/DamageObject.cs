using Assets.Scripts.Objects;
using UnityEngine;
using Assets.Scripts.Stage;
using System.Collections;
using Assets.ScriptableObjects;

namespace Assets.Scripts.Enemies
{
    public class DamageObject : MonoBehaviour, IObject
    {
        [SerializeField] private DamageObjectParameter damageObjectParameter;
        private bool isDamaging = false;

        private void Awake()
        {
            ObjectFacade.AddEnemy(this);
            isDamaging = false;
            StartCoroutine(Suicide());
        }

        public void SetReady()
        {

        }

        private IEnumerator Suicide()
        {
            yield return new WaitForSeconds(damageObjectParameter.ReadyTime);
            isDamaging = true;
            yield return new WaitForSeconds(damageObjectParameter.DamagingTime);
            DestroyObject();
        }

        public bool IsDamaging()
        {
            return isDamaging;
        }

        public void DamageTo(IObject obj)
        {
            obj.TakeDamage(damageObjectParameter.AttackPower);
        }

        public void TakeDamage(float damage)
        {

        }

        public (Vector3 minImPos3, Vector3 maxImPos3) GetImPos3s()
        {
            Vector3 minRePos3 = transform.position - new Vector3(transform.localScale.x / 2f, 0f, 0f);
            Vector3 maxRePos3 = transform.position + new Vector3(transform.localScale.x / 2f, StageFacade._tileHeight, transform.localScale.y / StageFacade._tileHeight);
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
