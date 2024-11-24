using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Enemy2Animation : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetLookingDirection((bool PlusImX, bool MinusImX, bool PlusImY, bool MinusImY) isHeadingTo)
        {
            _animator.SetBool("IsLookingPlusImX", isHeadingTo.PlusImX);
            _animator.SetBool("IsLookingMinusImX", isHeadingTo.MinusImX);
            _animator.SetBool("IsLookingPlusImY", isHeadingTo.PlusImY);
            _animator.SetBool("IsLookingMinusImY", isHeadingTo.MinusImY);
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

        public void StartStand()
        {
            _animator.SetBool("IsStanding", true);
        }

        public void StopStand()
        {
            _animator.SetBool("IsStanding", false);
        }
    }
}