using CodeBase.Buildings;
using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class ResourcePanel : MonoBehaviour
    {
        public MainCapsule Main;
        public TextMeshProUGUI MineralsCount;
        public TextMeshProUGUI EnergyCount;
        public TextMeshProUGUI OxygenCount;

        private void OnEnable()
        {
            Main.MineralsChanged += OnMineralsChanged;
            Main.EnergyChanged += OnEnergyChanged;
            Main.OxygenChanged += OnOxygenChanged;
            OnMineralsChanged(Main.MineralsStored);
            OnEnergyChanged();
            OnOxygenChanged(Main.OxygenStored);
        }

        private void OnDisable()
        {
            Main.MineralsChanged -= OnMineralsChanged;
            Main.EnergyChanged -= OnEnergyChanged;
            Main.OxygenChanged -= OnOxygenChanged;
        }

        private void OnMineralsChanged(int minerals) => 
            MineralsCount.text = minerals.ToString();

        private void OnEnergyChanged() => 
            EnergyCount.text = $"{Main.UsedEnergy} / {Main.AllEnergy}";

        private void OnOxygenChanged(int oxygen) =>
            OxygenCount.text = $"{Main.OxygenStored} / {Main.OxygenMax}";
    }
}