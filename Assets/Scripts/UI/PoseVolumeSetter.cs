using System;
using System.Collections.Generic;
using Assets.Scripts.Sounds;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class PoseVolumeSetter : MonoBehaviour
    {
        private Text text;
        [SerializeField] private string _titleText;
        [SerializeField] private string _bgmVolumeText;
        [SerializeField] private string _seVolumeText;
        [SerializeField] private string _returnText;
        private int index = 0;
        private bool isPushed = false;
        private readonly List<SoundOption> soundOptionList = new();
        private bool isReturnSelected = false;
        public bool IsReturnSelected => isReturnSelected;

        private void OnEnable()
        {
            isReturnSelected = false;
            index = 0;
            isPushed = true;
            ChangeTargetText();
        }

        private void Awake()
        {
            text = GetComponent<Text>();
            soundOptionList.Add(new SoundOption(_bgmVolumeText, (int)BGMPlayer.SingletonInstance.GetBGMVolume(), volume => { BGMPlayer.SingletonInstance.SetBGMVolume(volume); }));
            soundOptionList.Add(new SoundOption(_seVolumeText, (int)SEPlayer.SingletonInstance.GetSEVolume(), volume => { SEPlayer.SingletonInstance.SetSEVolume(volume); }));
        }
        
        private void Update()
        {
            if(isPushed && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.Return) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                isPushed = false;
            if(isPushed)
                return;
            if (Input.GetKey(KeyCode.S))
            {
                isPushed = true;
                index = math.min(index + 1, soundOptionList.Count);
                ChangeTargetText();
            }
            else if (Input.GetKey(KeyCode.W))
            {
                isPushed = true;
                index = math.max(index - 1, 0);
                ChangeTargetText();
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if (index == soundOptionList.Count)
                    return;
                isPushed = true;
                soundOptionList[index].IncreaseVolume();
                ChangeTargetText();
            }
            else if (Input.GetKey(KeyCode.A))
            {
                if (index == soundOptionList.Count)
                    return;
                isPushed = true;
                soundOptionList[index].DecreaseVolume();
                ChangeTargetText();
            }
            else if (Input.GetKey(KeyCode.Return))
            {
                if (index != soundOptionList.Count)
                    return;
                isPushed = true;
                isReturnSelected = true;
            }
        }

        private void ChangeTargetText()
        {
            text.text = $"{_titleText}\n\n";
            for (int i = 0; i < soundOptionList.Count; i++)
            {
                if (i == index)
                    text.text += $"<color=#{PoseMain.TargetTextColorCode}>>{soundOptionList[i].Text} {soundOptionList[i].Volume}%</color>\n";
                else
                    text.text += $"<color=#{PoseMain.NonTargetTextColorCode}> {soundOptionList[i].Text} {soundOptionList[i].Volume}%</color>\n";
            }
            if (index == soundOptionList.Count)
                text.text += $"<color=#{PoseMain.TargetTextColorCode}>>{_returnText}</color>\n";
            else
                text.text += $"<color=#{PoseMain.NonTargetTextColorCode}> {_returnText}</color>\n";
        }
    }

    class SoundOption
    {
        private readonly string text;
        public string Text => text;
        private int volume;
        public int Volume => volume;
        private readonly Action<float> setFloatAction;

        public SoundOption(string text, int volume, Action<float> setFloatAction)
        {
            this.text = text;
            this.volume = volume;
            this.setFloatAction = setFloatAction;
        }

        public void IncreaseVolume()
        {
            volume = math.min(volume + 5, 100);
            setFloatAction(volume);
        } 

        public void DecreaseVolume()
        {
            volume = math.max(volume - 5, 0);
            setFloatAction(volume);
        }
    }
}
