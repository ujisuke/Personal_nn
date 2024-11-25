using UnityEngine;
using Assets.ScriptableObjects;
using Assets.Scripts.Objects;

namespace Assets.Scripts.EnemyDamageObject
{
    public class EnemyDamageObjectAnimation : MonoBehaviour
    {
        private EnemyDamageObjectMain enemyDamageObjectMain;
        private Animator animator;
        private bool isDamaging = false;
        private DamageObjectParameter _damageObjectParameter;

        public void Initialize(DamageObjectParameter damageObjectParameter)
        {
            enemyDamageObjectMain = GetComponent<EnemyDamageObjectMain>();
            animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = damageObjectParameter.AnimatorController;
            isDamaging = false;
            _damageObjectParameter = damageObjectParameter;
            animator.SetFloat("ReadySpeed" , 1f / _damageObjectParameter.ReadyTime);
            GetComponent<SpriteRenderer>().sortingOrder = ObjectMove.CalculateSortingOrderFromRePos3(transform.position);
        }

        private void FixedUpdate()
        {
            if (!enemyDamageObjectMain.IsDamaging() || isDamaging) return;
            animator.SetTrigger("StartDamage");
            animator.SetFloat("DamagingSpeed", 1f / _damageObjectParameter.DamagingTime);
            isDamaging = true;
        }
    }
}