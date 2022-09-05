using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Ui
{
    public class ControllerUi : MonoBehaviour
    {
        private DateTime FinalDataTimer;
        private void Start()
        {
            FinalDataTimer = DateTime.Now.AddMinutes(10);
            StartCoroutine(Timer());
            PlayerStatesHolder.EventMinusPlayerHp += MinusHpPlayer;
            PlayerStatesHolder.EventToDeadPlayer += DeadPlayer;
        }
        private IEnumerator Timer()
        {
            while (true)
            {

                TimeSpan CurrentTimeLeft = FinalDataTimer - DateTime.Now;
                if (0 > CurrentTimeLeft.Seconds)
                {
                    StopCoroutine(Timer());
                    //TODO Add конец уровня.
                }
                UIData.instanse.Timer.text = $"{CurrentTimeLeft.Minutes}:{CurrentTimeLeft.Seconds}";
                yield return new WaitForSeconds(1f);

            }
        }
        private void MinusHpPlayer()
        {
            var DataUI = UIData.instanse;
            for (int i = DataUI.hpbar.Length -1; i < DataUI.hpbar.Length; i--)
            {
                if (0 > i)
                {
                    return;
                }
                if (DataUI.hpbar[i].enabled ==true)
                {
                    DataUI.hpbar[i].enabled = false;
                    return;
                }
            }
        }
        private void DeadPlayer()
        {
            UIData.instanse.DeadPanel.SetActive(true);
            Time.timeScale = 0;
        }

    }
}