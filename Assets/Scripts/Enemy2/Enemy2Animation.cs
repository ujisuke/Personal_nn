using Assets.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Enemy2
{
    public class Enemy2Animation : MonoBehaviour
    {
        private Animator animator;
        private Enemy2Parameter _enemy2Parameter;

        public void Initialize(Enemy2Parameter enemy2Parameter)
        {
            _enemy2Parameter = enemy2Parameter;
            animator = GetComponent<Animator>();
            animator.SetFloat("DeadSpeed", 1f / _enemy2Parameter.DeadTime);
            animator.SetFloat("ReadySpeed", 1f / _enemy2Parameter.ReadyTime);
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

        public void StartStand()
        {
            animator.SetBool("IsStanding", true);
        }

        public void StopStand()
        {
            animator.SetBool("IsStanding", false);
        }

        public void StartDead()
        {
            animator.SetBool("IsDead", true);
        }
    }
}