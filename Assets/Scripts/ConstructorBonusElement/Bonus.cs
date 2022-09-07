using UnityEngine;

namespace Assets.Scripts.ConstructorBonusElement
{
    [CreateAssetMenu(fileName = "Data", menuName = "Bonus", order = 1)]
    public class Bonus : ScriptableObject
    {
        public string NameBonus;
        public string Description;
        public Sprite BonusImage;
        public Sprite Frame;
    }
}