using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI
{
    public class HoveredTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string Title;
        public string Description;
        public Window Window;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            Window.Show(Title, Description);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Window.Hide();
        }
    }
}