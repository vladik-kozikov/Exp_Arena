using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Ui
{
    public class ControllerUi : MonoBehaviour
    {
        
        public static ControllerUi instanse;

        private UpdateBonusPlayer _updateBonusPlayer;

        private DateTime FinalDataTimer;
        private DateTime StartDataTimer;
        private UIData _uIData;

        private bool _isAcriveWriteImage;
        private bool _isAcriveRedImage;

        private int CountPlayerExp; //Max = 100;
        private int LevelExp = 1;

        private const int CountAddLevel = 1;
        private int CountDeadEnemy;


        private void Awake()
        {
            if (instanse != null) Destroy(instanse);
            instanse = this;
            _updateBonusPlayer = GetComponent<UpdateBonusPlayer>();
        }

        private void Start()
        {
            _uIData = UIData.instanse;
            FinalDataTimer = DateTime.Now.AddMinutes(10);
            StartDataTimer = DateTime.Now;
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
                    EndGame();
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

            CountDeadEnemy++;
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
            EndGame();
        }

        private void EndGame()
        {
            TimeSpan CurrentTimeLeft = (DateTime.Now - StartDataTimer);
            _uIData.CountTimeSesion.text = $"Final Time: { CurrentTimeLeft.Minutes}:{ CurrentTimeLeft.Seconds}";
            _uIData.CountDeadEnemy.text = $"Dead Enemy: {CountDeadEnemy}";

            StopAllCoroutines();
            OnDisableCursor();
        }
        
        public void ChangesLevelBar()
        {
           int AddPlayerExp = CountAddLevel;
            CountPlayerExp += AddPlayerExp;

            if (CountPlayerExp >= 10)
            {
                CountPlayerExp -= 10;
                LevelExp++;
                _uIData.Level.text = LevelExp.ToString();
                UpdateUpLevel();
            }

           
            float CountAddExp = CountPlayerExp;
            CountAddExp = CountAddExp / 10;
            _uIData.LevelBar.fillAmount = CountAddExp;
        }

        public void ResetScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }

        public void UpdateUpLevel()
        {
            UIData.instanse.BakeLevel.SetActive(true);

            _updateBonusPlayer.UpdateBonus();
            _uIData.PanelUpgradeLevel.SetActive(true);

            this.Invoke("FalsePanel", 0.1f);
            Time.timeScale = 0.1f;
        }

        public void ClickToLevelUpBonus(int CurrentBonus)
        {
            _updateBonusPlayer.ClickToCurrentBonus(CurrentBonus);
            _uIData.PanelUpgradeLevel.SetActive(false);
            OnDisableCursor();
            
            Time.timeScale = 1;

        }
        public void FalsePanel()
        {
            UIData.instanse.BakeLevel.SetActive(false);

            OnEnableCursor();
        }

        private void OnDisableCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnEnableCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

    }
}