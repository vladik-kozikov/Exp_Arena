using Assets.Scripts.ConstructorBonusElement;
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

        public GameObject DeadPanel;

        public Text CountTimeSesion;
        public Text CountDeadEnemy;

        public GameObject PanelUpgradeLevel;
        public UpdgradeUIData[] AllLevelUiData;
        public Bonus[] AllBonus;

        public GameObject BakeLevel;

        private void Awake()
        {
            if (instanse != null) Destroy(instanse);
            instanse = this;
        }
       
    }
}