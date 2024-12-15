using Assets.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerAttackEffectAnimation : MonoBehaviour
    {
        private Animator animator;

        public void Initialize(PlayerParameter playerParameter)
        {
            animator = GetComponent<Animator>();
            animator.SetFloat("AttackSpeed", 1f / playerParameter.AttackingTime);
        }

        public void StartAttack()
        {
            animator.SetTrigger("IsAttacking");
        }
    }
}   