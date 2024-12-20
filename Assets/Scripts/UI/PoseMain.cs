using System;
using System.Collections.Generic;
using Assets.Scripts.Battle;
using Assets.Scripts.Room;
using Assets.Scripts.Sounds;
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
        [SerializeField] private string _setSeedText;
        [SerializeField] private string _exitGameText;
        private int index = 0;
        private bool isPushed = false;
        private readonly List<Option> optionList = new();
        [SerializeField] private Color32 _targetTextColor;
        private static string _targetTextColorCode;
        public static string TargetTextColorCode => _targetTextColorCode;
        [SerializeField] private Color32 _nonTargetTextColor;
        private static string _nonTargetTextColorCode;
        public static string NonTargetTextColorCode => _nonTargetTextColorCode;
        private bool isSetVolumeSelected = false;
        public bool IsSetVolumeSelected => isSetVolumeSelected;
        private bool isSetSeedSelected = false;
        public bool IsSetSeedSelected => isSetSeedSelected;
        public static bool IsInLobby = false;
        private AudioSource audioSource;

        private void Awake()
        {
            text = GetComponentInChildren<Text>();
            _targetTextColorCode = ColorUtility.ToHtmlStringRGB(_targetTextColor);
            _nonTargetTextColorCode = ColorUtility.ToHtmlStringRGB(_nonTargetTextColor);
            audioSource = GetComponent<AudioSource>();
            ChangeTargetText();
        }

        private void OnEnable()
        {
            optionList.Clear();
            if(IsInLobby)
            {
                optionList.Add(new Option(_setVolumeText, () => isSetVolumeSelected = true));
                optionList.Add(new Option(_setSeedText, () => isSetSeedSelected = true));
                optionList.Add(new Option(_exitGameText, () => { BattleData.Save(); Application.Quit(); }));   
            }
            else
            {
                optionList.Add(new Option(_setVolumeText, () => isSetVolumeSelected = true));
                optionList.Add(new Option(_backToLobbyText, () => { CanvasStorage.SingletonInstance.HidePoseCanvas(); LobbyState.IsBackingToLobby = true; }));
                optionList.Add(new Option(_exitGameText, () => { BattleData.Save(); Application.Quit(); }));   
            }
            index = 0;
            isPushed = true;
            isSetVolumeSelected = false;
            isSetSeedSelected = false;
            ChangeTargetText();
        }

        private void Update()
        {
            if(isPushed && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.Return))
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
            else if (Input.GetKey(KeyCode.Return))
            {
                SEPlayer.SingletonInstance.PlaySelect(audioSource);
                optionList[index].Action();
            }
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

    
    public class Option
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