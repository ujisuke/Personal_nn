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

        public void SetHeadingDirection((bool PlusImX, bool MinusImX, bool PlusImY, bool MinusImY) isHeadingTo)
        {
            _animator.SetBool("IsHeadingToPlusImX", isHeadingTo.PlusImX);
            _animator.SetBool("IsHeadingToMinusImX", isHeadingTo.MinusImX);
            _animator.SetBool("IsHeadingToPlusImY", isHeadingTo.PlusImY);
            _animator.SetBool("IsHeadingToMinusImY", isHeadingTo.MinusImY);
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
