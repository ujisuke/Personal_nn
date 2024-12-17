using UnityEngine;
using Assets.Scripts.Objects;
using Assets.ScriptableObjects;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private PlayerParameter _playerParameter;
        private ObjectMove objectMove;
        private bool isAttacking = true;
        public bool IsAttacking => isAttacking;
        private bool isDamaging = false;
        public bool IsDamaging => isDamaging;
        private PlayerMain playerMain;

        public void Initialize(PlayerParameter playerParameter)
        {
            _playerParameter = playerParameter;
            objectMove = GetComponent<ObjectMove>();
            playerMain = GetComponent<PlayerMain>();
        }

        private async void OnEnable()
        {
            isAttacking = true;
            isDamaging = true;
            playerMain.ConsumeEnergy(_playerParameter.AttackEnergyConsumption);
            await UniTask.Delay(TimeSpan.FromSeconds(_playerParameter.AttackingTime));
            isDamaging = false;
            isAttacking = false;
        }

        private void FixedUpdate()
        {
            objectMove.HeadToMinusImX(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W));
            objectMove.HeadToPlusImY(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D));
            objectMove.HeadToMinusImY(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A));
            objectMove.HeadToPlusImX(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S));
            objectMove.TryToJump(Input.GetKey(KeyCode.Space));
        }
    }
}
