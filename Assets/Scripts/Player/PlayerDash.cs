using UnityEngine;
using Assets.Scripts.Objects;
using Assets.ScriptableObjects;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.Player
{
    public class PlayerDash : MonoBehaviour
    {
        private PlayerParameter playerParameter;
        private ObjectMove objectMove;
        private bool isDashing = true;
        public bool IsDashing => isDashing;
        PlayerMain playerMain;
        
        public void Initialize(PlayerParameter playerParameter)
        {
            objectMove = GetComponent<ObjectMove>();
            playerMain = GetComponent<PlayerMain>();
            this.playerParameter = playerParameter;
        }

        private async void OnEnable()
        {
            isDashing = true;
            playerMain.ConsumeEnergy(playerParameter.DashEnergyConsumption);
            objectMove.StartDash(playerParameter.DashSpeedRatio);
            await UniTask.Delay(TimeSpan.FromSeconds(playerParameter.DashingTime));
            objectMove.StopDash();
            isDashing = false;
        }
    }
}
