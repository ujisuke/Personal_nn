using Assets.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Enemy1
{
    public class Enemy1Animation : MonoBehaviour
    {
        private Animator animator;

        public void Initialize(Enemy1Parameter enemy1Parameter)
        {
            animator = GetComponent<Animator>();
            animator.SetFloat("DeadSpeed" , 1f / enemy1Parameter.DeadTime);
            animator.SetFloat("ReadySpeed", 1f / enemy1Parameter.ReadyTime);
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
