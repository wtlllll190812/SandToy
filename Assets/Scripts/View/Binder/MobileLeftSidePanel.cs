using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.View.Binder
{
    public class MobileLeftSidePanel : LeftSidePanel
    {
        [SerializeField] private Scrollbar brushSizeScrollbar;

        protected override void CreateBrushSizeButton()
        {
            brushSizeScrollbar.onValueChanged.AddListener(
                value => OnBrushSizeChange(brushSizePreset.GetSize(value)));
        }
    }
}