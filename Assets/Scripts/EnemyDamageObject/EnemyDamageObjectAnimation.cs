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
        private EnemyDamageObjectParameter _enemyDamageObjectParameter;

        public void Initialize(EnemyDamageObjectParameter enemyDamageObjectParameter)
        {
            enemyDamageObjectMain = GetComponent<EnemyDamageObjectMain>();
            animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = enemyDamageObjectParameter.AnimatorController;
            isDamaging = false;
            _enemyDamageObjectParameter = enemyDamageObjectParameter;
            animator.SetFloat("ReadySpeed" , 1f / _enemyDamageObjectParameter.ReadyTime);
            GetComponent<SpriteRenderer>().sortingOrder = ObjectMove.CalculateSortingOrderFromRePos3(transform.position);
        }

        private void FixedUpdate()
        {
            if (!enemyDamageObjectMain.IsDamaging() || isDamaging) return;
            animator.SetTrigger("StartDamage");
            animator.SetFloat("DamagingSpeed", 1f / _enemyDamageObjectParameter.DamagingTime);
            isDamaging = true;
        }
    }
}