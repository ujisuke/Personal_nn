using Assets.Scripts.Objects;
using UnityEngine;
using Assets.Scripts.Stage;
using System.Collections;
using Assets.ScriptableObjects;

namespace Assets.Scripts.Enemies
{
    public class EnemyDamageObject : MonoBehaviour, IObject
    {
        private DamageObjectParameter _damageObjectParameter;
        private bool isDamaging = false;

        public void Initialize(DamageObjectParameter _damageObjectParameter)
        {
            this._damageObjectParameter = _damageObjectParameter;
            ObjectFacade.AddEnemy(this);
            isDamaging = false;
            GetComponent<EnemyDamageObjectAnimation>().Initialize(_damageObjectParameter);
            StartCoroutine(Suicide());
        }

        public void SetReady()
        {

        }

        private IEnumerator Suicide()
        {
            yield return new WaitForSeconds(_damageObjectParameter.ReadyTime);
            isDamaging = true;
            yield return new WaitForSeconds(_damageObjectParameter.DamagingTime);
            DestroyDeadObject();
        }

        public bool IsDamaging()
        {
            return isDamaging;
        }

        public void DamageTo(IObject obj)
        {
            obj.TakeDamage(_damageObjectParameter.AttackPower);
        }

        public void TakeDamage(float damage)
        {

        }

        public (Vector3 minImPos3, Vector3 maxImPos3) GetImPos3s()
        {
            Vector3 minRePos3 = transform.position - new Vector3(transform.localScale.x / 2f, 0f, 0f);
            Vector3 maxRePos3 = transform.position + new Vector3(transform.localScale.x / 2f, transform.localScale.y * StageFacade._TileHeight, transform.localScale.y);
            return (ObjectMove.ConvertToImPos3FromRePos3(minRePos3), ObjectMove.ConvertToImPos3FromRePos3(maxRePos3));
        }

        public Vector3 GetRePos3()
        {
            return transform.position;
        }

        public void DestroyDeadObject()
        {
            ObjectFacade.RemoveEnemy(this);
            Destroy(gameObject);
        }

        public void DestroyAliveObject()
        {
            ObjectFacade.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }
}
