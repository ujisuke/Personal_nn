using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Enemy1Animation : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetLookingDirection((bool PlusImX, bool MinusImX, bool PlusImY, bool MinusImY) isLooking)
        {
            _animator.SetBool("IsLookingPlusImX", isLooking.PlusImX);
            _animator.SetBool("IsLookingMinusImX", isLooking.MinusImX);
            _animator.SetBool("IsLookingPlusImY", isLooking.PlusImY);
            _animator.SetBool("IsLookingMinusImY", isLooking.MinusImY);
        }

        public void StartWalk()
        {
            _animator.SetBool("IsWalking", true);
        }

        public void StopWalk()
        {
            _animator.SetBool("IsWalking", false);
        }

        public void StartAttack()
        {
            _animator.SetBool("IsAttacking", true);
        }

        public void StopAttack()
        {
            _animator.SetBool("IsAttacking", false);
        }
    }
}
