using UnityEngine;
using Assets.Scripts.Objects;
using Assets.ScriptableObjects;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private PlayerParameter playerParameter;
        private ObjectMove objectMove;
        private bool isAttacking = true;
        public bool IsAttacking => isAttacking;
        private bool isDamaging = false;
        public bool IsDamaging => isDamaging;

        public void Initialize(PlayerParameter playerParameter)
        {
            this.playerParameter = playerParameter;
            objectMove = GetComponent<ObjectMove>();
        }

        private async void OnEnable()
        {
            isAttacking = true;
            isDamaging = true;
            PlayerMain.ConsumeEnergy(playerParameter.AttackEnergyConsumption);
            await UniTask.Delay(TimeSpan.FromSeconds(playerParameter.AttackingTime));
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
