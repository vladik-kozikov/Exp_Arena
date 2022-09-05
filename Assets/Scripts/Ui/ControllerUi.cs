using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Ui
{
    public class ControllerUi : MonoBehaviour
    {
        public static ControllerUi instanse;

        private DateTime FinalDataTimer;
        private UIData _uIData;

        private bool _isAcriveWriteImage;
        private bool _isAcriveRedImage;

        private void Awake()
        {
            if (instanse != null) Destroy(instanse);
            instanse = this;
        }

        private void Start()
        {
            _uIData = UIData.instanse;
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
        //TODO Присмотреться, возможно отключать при этом writeAim.
        public void SetRedAim()
        {
            if (_isAcriveRedImage)
            {
                StopCoroutine(SetRedImage(_uIData.RedAim.gameObject, _isAcriveRedImage));
                _uIData.WriteAim.gameObject.SetActive(true);
            }

            StartCoroutine(SetRedImage(_uIData.RedAim.gameObject, _isAcriveRedImage));
        }
        public void SetWriteAim()
        {
            if (_isAcriveRedImage)
            {
                StopCoroutine(SetRedImage(_uIData.WriteAim.gameObject, _isAcriveWriteImage));
                _uIData.WriteAim.gameObject.SetActive(false);
            }
            StartCoroutine(SetRedImage(_uIData.WriteAim.gameObject, _isAcriveWriteImage));
        }
        
        private IEnumerator SetRedImage(GameObject AimImage,bool isActive)
        {
            isActive = true;
            AimImage.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            AimImage.SetActive(false);
            isActive = false;
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