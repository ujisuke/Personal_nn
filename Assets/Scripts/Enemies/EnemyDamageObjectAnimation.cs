using UnityEngine;
using Assets.ScriptableObjects;

namespace Assets.Scripts.Enemies
{
    public class EnemyDamageObjectAnimation : MonoBehaviour
    {
        private EnemyDamageObject enemyDamageObject;
        private Animator animator;
        private bool isDamaging = false;
        private DamageObjectParameter _damageObjectParameter;

        public void Initialize(DamageObjectParameter damageObjectParameter)
        {
            enemyDamageObject = GetComponent<EnemyDamageObject>();
            animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = damageObjectParameter.AnimatorController;
            isDamaging = false;
            _damageObjectParameter = damageObjectParameter;
            animator.SetFloat("ReadySpeed" , 1f / _damageObjectParameter.ReadyTime);
        }

        private void FixedUpdate()
        {
            if (enemyDamageObject.IsDamaging() && !isDamaging)
            {
                animator.SetTrigger("StartDamage");
                animator.SetFloat("DamagingSpeed", 1f / _damageObjectParameter.DamagingTime);
                isDamaging = true;
            }
        }
    }
}