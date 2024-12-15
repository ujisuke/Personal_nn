using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class PoseMain : MonoBehaviour
    {
        private Text text;
        [SerializeField] private string _titleText;
        [SerializeField] private string _setVolumeText;
        [SerializeField] private string _backToLobbyText;
        [SerializeField] private string _exitGameText;
        private int index = 0;
        private bool isPushed = false;
        private List<Option> optionList = new();
        [SerializeField] private Color32 _targetTextColor;
        private static string _targetTextColorCode;
        public static string TargetTextColorCode => _targetTextColorCode;
        [SerializeField] private Color32 _nonTargetTextColor;
        private static string _nonTargetTextColorCode;
        public static string NonTargetTextColorCode => _nonTargetTextColorCode;
        private bool isSetVolumeSelected = false;
        public bool IsSetVolumeSelected => isSetVolumeSelected;
        private bool isBackToLobbySelected = false;
        public bool IsBackToLobbySelected => isBackToLobbySelected;
        private bool isExitGameSelected = false;
        public bool IsExitGameSelected => isExitGameSelected;
        public static bool IsInLobby = false;

        private void Awake()
        {
            text = GetComponent<Text>();
            _targetTextColorCode = ColorUtility.ToHtmlStringRGB(_targetTextColor);
            _nonTargetTextColorCode = ColorUtility.ToHtmlStringRGB(_nonTargetTextColor);
            ChangeTargetText();
        }

        private void OnEnable()
        {
            optionList.Clear();
            if(IsInLobby)
            {
                optionList.Add(new Option(_setVolumeText, () => isSetVolumeSelected = true));
                optionList.Add(new Option(_exitGameText, () => isExitGameSelected = true));
            }
            else
            {
                optionList.Add(new Option(_setVolumeText, () => isSetVolumeSelected = true));
                optionList.Add(new Option(_backToLobbyText, () => isBackToLobbySelected = true));
                optionList.Add(new Option(_exitGameText, () => isExitGameSelected = true));
            }
            index = 0;
            isPushed = true;
            isSetVolumeSelected = false;
            isBackToLobbySelected = false;
            isExitGameSelected = false;
            ChangeTargetText();
        }

        private void Update()
        {
            if(isPushed && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.Space))
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
            else if (Input.GetKey(KeyCode.Space))
                optionList[index].Action();
        }

        private void ChangeTargetText()
        {
            text.text = $"{_titleText}\n\n";
            for (int i = 0; i < optionList.Count; i++)
            {
                if (i == index)
                    text.text += $"<color=#{_targetTextColorCode}>>{optionList[i].Text}</color>\n";
                else
                    text.text += $"<color=#{_nonTargetTextColorCode}> {optionList[i].Text}</color>\n";
            }
        }
    }

    
    class Option
    {
        private readonly string text;
        public string Text => text;
        private readonly Action action;
        public Action Action => action;

        public Option(string text, Action action)
        {
            this.text = text;
            this.action = action;
        }
    }
}