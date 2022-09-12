using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Ui
{
    public class UpdateBonusPlayer : MonoBehaviour
    {
        //TODO Add branch
        public static Action<int> UpdateStatePlayer { get; set; }
        
        public void  UpdateBonus()
        {
            var UiData = UIData.instanse;

            for (int i = 0; i < UiData.AllLevelUiData.Length; i++)
            {
                UiData.AllLevelUiData[i].NameBonus.text = UiData.AllBonus[i].NameBonus;
                UiData.AllLevelUiData[i].Description.text = UiData.AllBonus[i].Description;
                UiData.AllLevelUiData[i].BonusImage.sprite = UiData.AllBonus[i].BonusImage;
                UiData.AllLevelUiData[i].Frame.sprite = UiData.AllBonus[i].Frame;
            }

        }

        public void ClickToCurrentBonus(int BonusID)
        {
            UpdateStatePlayer?.Invoke(BonusID);
        }
    }
}