using UnityEngine;
using Assets.Scripts.Objects;
using Assets.ScriptableObjects;

namespace Assets.Scripts.Player
{
    public class PlayerMove : MonoBehaviour
    {
        private PlayerParameter _playerParameter;
        private ObjectMove objectMove;
        private bool isAfterAttack = true;
        private PlayerMain playerMain;

        public void Initialize(PlayerParameter playerParameter)
        {
            _playerParameter = playerParameter;
            objectMove = GetComponent<ObjectMove>();
            playerMain = GetComponent<PlayerMain>();
        }

        private void OnEnable()
        {
            isAfterAttack = true;
        }

        private void FixedUpdate()
        {
            objectMove.HeadToMinusImX(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W));
            objectMove.HeadToPlusImY(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D));
            objectMove.HeadToMinusImY(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A));
            objectMove.HeadToPlusImX(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S));
            objectMove.TryToJump(Input.GetKey(KeyCode.Space));

            if(isAfterAttack && !Input.GetKey(KeyCode.Return)) isAfterAttack = false;
        }

        public bool CanAttack()
        {
            return Input.GetKey(KeyCode.Return) && !isAfterAttack && playerMain.CanUseEnergy(_playerParameter.AttackEnergyConsumption);
        }

        public (bool isLookingPlusImX, bool isLookingMinusImX, bool isLookingPlusImY, bool isLookingMinusImY) GetLookingDirection()
        {
            return objectMove.GetLookingDirection();
        }
    }
}
