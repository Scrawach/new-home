using CodeBase.Buildings;
using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class RequiredResourcesForBotView : MonoBehaviour
    {
        public MainCapsule Main;
        public TextMeshProUGUI Minerals;
        public TextMeshProUGUI Energy;

        private void Awake()
        {
            Minerals.text = Main.RobotCost.Minerals.ToString();
            Energy.text = Main.RobotCost.Energy.ToString();
        }
    }
}