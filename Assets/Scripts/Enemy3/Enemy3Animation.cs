using Assets.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Enemy3
{
    public class Enemy3Animation : MonoBehaviour
    {
        private Animator animator;

        public void Initialize(Enemy3Parameter enemy3Parameter)
        {
            animator = GetComponent<Animator>();
            animator.SetFloat("DeadSpeed", 1f / enemy3Parameter.DeadTime);
            animator.SetFloat("ReadySpeed", 1f / enemy3Parameter.ReadyTime);
        }

        public void SetLookingDirection((bool plusImX, bool minusImX, bool plusImY, bool minusImY) isLooking)
        {
            animator.SetBool("IsLookingPlusImX", isLooking.plusImX);
            animator.SetBool("IsLookingMinusImX", isLooking.minusImX);
            animator.SetBool("IsLookingPlusImY", isLooking.plusImY);
            animator.SetBool("IsLookingMinusImY", isLooking.minusImY);
        }

        public void StartMove()
        {
            animator.SetBool("IsMoving", true);
        }

        public void StopMove()
        {
            animator.SetBool("IsMoving", false);
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