﻿using Assets.Scripts.ConstructorBonusElement;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui
{
    public class UIData : MonoBehaviour
    {
        public static UIData instanse;
        public Image MainAim;
        public Image WriteAim;
        public Image RedAim;

        public Text Timer;
        public Image LevelBar;
        public Image[] hpbar;

        public Text Level;

        public GameObject EndPanel;

        public Text CountTimeSesion;
        public Text CountDeadEnemy;
        public Text CountRewardPlayer;


        public GameObject PanelUpgradeLevel;
        public UpdgradeUIData[] AllLevelUiData;
        public Bonus[] AllBonus;

        public GameObject BakeLevel;

        public GameObject DeadImage;
        public GameObject SurvivedImage;

        private void Awake()
        {
            if (instanse != null) Destroy(instanse);
            instanse = this;
        }

        private void Update()
        {
                if (Input.GetKeyDown(KeyCode.Mouse1)) MainAim.gameObject.SetActive(false);
            if (Input.GetKeyUp(KeyCode.Mouse1)) MainAim.gameObject.SetActive(true);
        }
    }
}