using Assets.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Enemy1Animation : MonoBehaviour
    {
        private Animator animator;
        private Enemy1Parameter _enemy1Parameter;

        public void Initialize(Enemy1Parameter enemy1Parameter)
        {
            _enemy1Parameter = enemy1Parameter;
            animator = GetComponent<Animator>();
            animator.SetFloat("DeadSpeed" , 1f / _enemy1Parameter.DeadTime);
        }

        public void SetLookingDirection((bool PlusImX, bool MinusImX, bool PlusImY, bool MinusImY) isLooking)
        {
            animator.SetBool("IsLookingPlusImX", isLooking.PlusImX);
            animator.SetBool("IsLookingMinusImX", isLooking.MinusImX);
            animator.SetBool("IsLookingPlusImY", isLooking.PlusImY);
            animator.SetBool("IsLookingMinusImY", isLooking.MinusImY);
        }

        public void StartWalk()
        {
            animator.SetBool("IsWalking", true);
        }

        public void StopWalk()
        {
            animator.SetBool("IsWalking", false);
        }

        public void StartAttack()
        {
            animator.SetBool("IsAttacking", true);
        }

        public void StopAttack()
        {
            animator.SetBool("IsAttacking", false);
        }

        public void StartDead()
        {
            animator.SetBool("IsDead", true);
        }
    }
}
