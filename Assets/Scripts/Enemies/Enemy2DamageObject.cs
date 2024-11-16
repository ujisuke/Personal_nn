using Assets.Scripts.Objects;
using UnityEngine;
using Assets.Scripts.Stage;
using System.Collections;

namespace Assets.Scripts.Enemies
{
    public class Enemy2DamageObject : MonoBehaviour, IObject
    {
        private void Awake()
        {
            ObjectFacade.AddEnemy(this);
            StartCoroutine(Suicide());
        }

        private IEnumerator Suicide()
        {
            yield return new WaitForSeconds(0.1f);
            ObjectFacade.RemoveEnemy(this);
            Destroy(gameObject);
        }

        public bool IsDamaging()
        {
            return true;
        }

        public void DamagedBy(IObject obj)
        {

        }

        public (Vector3 minImPos3, Vector3 maxImPos3) GetImPos3s()
        {
            Vector3 minRePos3 = transform.position - new Vector3(transform.localScale.x / 2f, transform.localScale.y / 2, 0f);
            Vector3 maxRePos3 = transform.position + new Vector3(transform.localScale.x / 2f, transform.localScale.y / 2f, transform.localScale.y / StageCreator._tileHeight);
            return (ObjectMove.ConvertToImPos3FromRePos3(minRePos3), ObjectMove.ConvertToImPos3FromRePos3(maxRePos3));
        }

        public Vector3 GetRePos3()
        {
            return transform.position;
        }
    }
}
