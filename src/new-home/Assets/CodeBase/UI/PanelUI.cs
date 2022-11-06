using UnityEngine;

namespace CodeBase.UI
{
    public abstract class PanelUI : MonoBehaviour
    {
        public abstract void Activate();
        public abstract void Deactivate();
    }
}