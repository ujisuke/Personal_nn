using UnityEngine;
using Assets.Scripts.Objects;

namespace Assets.Scripts.Player
{
    public class PlayerMove : MonoBehaviour
    {
        private ObjectMove objectMove;
        private bool isAfterAttack = true;
        private bool isAfterDash = true;

        public void Initialize()
        {
            objectMove = GetComponent<ObjectMove>();
        }

        private void OnEnable()
        {
            isAfterAttack = true;
            isAfterDash = true;
        }

        private void FixedUpdate()
        {
            objectMove.HeadToMinusImX(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W));
            objectMove.HeadToPlusImY(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D));
            objectMove.HeadToMinusImY(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A));
            objectMove.HeadToPlusImX(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S));
            objectMove.TryToJump(Input.GetKey(KeyCode.Space));

            if(isAfterAttack && !Input.GetMouseButton(0)) isAfterAttack = false;
            if(isAfterDash && !Input.GetKey(KeyCode.LeftShift)) isAfterDash = false;
        }

        public bool CanAttack()
        {
            return Input.GetMouseButton(0) && !isAfterAttack;
        }

        public bool CanDash()
        {
            return Input.GetKey(KeyCode.LeftShift) && !isAfterDash;
        }
    }
}
