using System;
using CodeBase.Buildings;
using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class RequiredResourcesView : MonoBehaviour
    {
        public Building Building;
        public TextMeshProUGUI Minerals;
        public TextMeshProUGUI Energy;
        
        private void Awake()
        {
            if (Minerals != null)
                Minerals.text = Building.RequiredForBuilding.Minerals.ToString();

            if (Energy != null)
                Energy.text = Building.RequiredForBuilding.Energy.ToString();
        }
    }
}