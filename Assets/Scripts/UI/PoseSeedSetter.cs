using System.Collections.Generic;
using Assets.Scripts.Battle;
using Assets.Scripts.Sounds;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class PoseSeedSetter : MonoBehaviour
    {
        [SerializeField] private string _titleText;
        [SerializeField] private string _returnText;
        private bool isReturnSelected = false;
        public bool IsReturnSelected => isReturnSelected;
        private Text text;
        private int inputSeed;
        private bool isPushed = false;
        private int index = 0;
        private readonly List<Option> optionList = new();
        private AudioSource audioSource;

        private void Awake()
        {
            text = GetComponentInChildren<Text>();
            audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            inputSeed = BattleData.Seed;
            optionList.Clear();
            optionList.Add(new Option(inputSeed.ToString(), () => {}));
            optionList.Add(new Option(_returnText, () => isReturnSelected = true));
            isReturnSelected = false;
            isPushed = true;
            index = 0;
            ChangeTargetText();
        }

        private void Update()
        {
            if(isPushed && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.Return) && !IsInputtingSeed())
                isPushed = false;
            if(isPushed)
                return;
            if (Input.GetKey(KeyCode.S))
            {
                isPushed = true;
                index = math.min(index + 1, optionList.Count - 1);
                ChangeTargetText();
            }
            else if (Input.GetKey(KeyCode.W))
            {
                isPushed = true;
                index = math.max(index - 1, 0);
                ChangeTargetText();
            }
            else if(index == 0)
                AdjustInputNumber();
            else if (Input.GetKey(KeyCode.Return))
            {
                BattleData.ReplaceSeed(inputSeed);
                SEPlayer.SingletonInstance.PlaySelect(audioSource);
                isPushed = true;
                isReturnSelected = true;
            }
        }

        private void ChangeTargetText()
        {
            text.text = $"{_titleText}\n\n";
            for (int i = 0; i < optionList.Count; i++)
            {
                if (i == index)
                    text.text += $"<color=#{PoseMain.TargetTextColorCode}>>{optionList[i].Text}</color>\n";
                else
                    text.text += $"<color=#{PoseMain.NonTargetTextColorCode}> {optionList[i].Text}</color>\n";
            }
        }

        private void AdjustInputNumber()
        {
            if(isPushed)
                return;
            if (Input.GetKey(KeyCode.Backspace))
                inputSeed = inputSeed / 10;
            else if(inputSeed >= 9999)
                return;
            if (Input.GetKey(KeyCode.Alpha0) && inputSeed > 0)    
                inputSeed = inputSeed * 10;
            else if (Input.GetKey(KeyCode.Alpha1))
                inputSeed = inputSeed * 10 + 1;
            else if (Input.GetKey(KeyCode.Alpha2))
                inputSeed = inputSeed * 10 + 2;
            else if (Input.GetKey(KeyCode.Alpha3))
                inputSeed = inputSeed * 10 + 3;
            else if (Input.GetKey(KeyCode.Alpha4))
                inputSeed = inputSeed * 10 + 4;
            else if (Input.GetKey(KeyCode.Alpha5))
                inputSeed = inputSeed * 10 + 5;
            else if (Input.GetKey(KeyCode.Alpha6))
                inputSeed = inputSeed * 10 + 6;
            else if (Input.GetKey(KeyCode.Alpha7))
                inputSeed = inputSeed * 10 + 7;
            else if (Input.GetKey(KeyCode.Alpha8))
                inputSeed = inputSeed * 10 + 8;
            else if (Input.GetKey(KeyCode.Alpha9))
                inputSeed = inputSeed * 10 + 9;
            if(IsInputtingSeed())
                isPushed = true;
            optionList[0] = new Option(inputSeed.ToString(), () => {});
            ChangeTargetText();
        }

        private static bool IsInputtingSeed()
        {
            return Input.GetKey(KeyCode.Alpha0) || Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Alpha4) || Input.GetKey(KeyCode.Alpha5) || Input.GetKey(KeyCode.Alpha6) || Input.GetKey(KeyCode.Alpha7) || Input.GetKey(KeyCode.Alpha8) || Input.GetKey(KeyCode.Alpha9) || Input.GetKey(KeyCode.Backspace);
        }
    }
}