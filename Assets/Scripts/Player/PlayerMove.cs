using UnityEngine;
using Assets.Scripts.Objects;
using Assets.ScriptableObjects;

namespace Assets.Scripts.Player
{
    public class PlayerMove : MonoBehaviour
    {
        private PlayerParameter playerParameter;
        private ObjectMove objectMove;
        private bool isAfterAttack = true;
        private bool isAfterDash = true;
        private PlayerMain playerMain;

        public void Initialize(PlayerParameter playerParameter)
        {
            this.playerParameter = playerParameter;
            objectMove = GetComponent<ObjectMove>();
            playerMain = GetComponent<PlayerMain>();
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
            if(isAfterDash && !Input.GetMouseButton(1)) isAfterDash = false;
        }

        public bool CanAttack()
        {
            return Input.GetMouseButton(0) && !isAfterAttack && playerMain.CanUseEnergy(playerParameter.AttackEnergyConsumption);
        }

        public bool CanDash()
        {
            return Input.GetMouseButton(1) && !isAfterDash && playerMain.CanUseEnergy(playerParameter.DashEnergyConsumption);
        }

        public (bool isLookingPlusImX, bool isLookingMinusImX, bool isLookingPlusImY, bool isLookingMinusImY) GetLookingDirection()
        {
            return objectMove.GetLookingDirection();
        }
    }
}
