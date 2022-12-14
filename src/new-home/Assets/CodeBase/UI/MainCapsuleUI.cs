using UnityEngine.UI;

namespace CodeBase.UI
{
    public class MainCapsuleUI : PanelUI
    {
        public Button[] Buttons;

        public override void Activate()
        {
            foreach (var button in Buttons) 
                button.interactable = true;
        }

        public override void Deactivate()
        {
            foreach (var button in Buttons) 
                button.interactable = false;
        }
    }
}