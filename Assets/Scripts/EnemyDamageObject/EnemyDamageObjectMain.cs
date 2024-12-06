using Assets.Scripts.Objects;
using UnityEngine;
using Assets.Scripts.Stage;
using Assets.ScriptableObjects;
using Assets.Scripts.Player;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.EnemyDamageObject
{
    public class EnemyDamageObjectMain : MonoBehaviour
    {
        private DamageObjectParameter _damageObjectParameter;
        private bool isDamaging = false;

        public async UniTask Initialize(DamageObjectParameter _damageObjectParameter, IEnemyMain enemy)
        {
            this._damageObjectParameter = _damageObjectParameter;
            ObjectStorage.AddEnemyDamageObject(this, enemy);
            isDamaging = false;
            GetComponent<EnemyDamageObjectAnimation>().Initialize(_damageObjectParameter);
            await UniTask.Delay(TimeSpan.FromSeconds(_damageObjectParameter.ReadyTime));
            isDamaging = true;
            await UniTask.Delay(TimeSpan.FromSeconds(_damageObjectParameter.DamagingTime));
            DestroyObject();
        }

        public bool IsDamaging()
        {
            return isDamaging;
        }

        public void DamageTo(PlayerMain player)
        {
            player.TakeDamage(_damageObjectParameter.AttackPower);
        }

        public (Vector3 minImPos3, Vector3 maxImPos3) GetImPos3s()
        {
            Vector3 minRePos3 = transform.position - new Vector3(transform.localScale.x / 2f, 0f, 0f);
            Vector3 maxRePos3 = transform.position + new Vector3(transform.localScale.x / 2f, transform.localScale.y * StageFacade.TileHeight, transform.localScale.y);
            return (ObjectMove.ConvertToImPos3FromRePos3(minRePos3), ObjectMove.ConvertToImPos3FromRePos3(maxRePos3));
        }

        public void DestroyObject()
        {
            ObjectStorage.RemoveEnemyDamageObject(this);
            Destroy(gameObject);
        }
    }
}
