using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.View.Binder
{
    public class MobileLeftSidePanel : LeftSidePanel
    {
        [SerializeField] private Scrollbar brushSizeScrollbar;

        private float targetValue;

        protected override void CreateBrushSizeButton()
        {
            brushSizeScrollbar.onValueChanged.AddListener(
            value =>
            {
                int index = (int) Mathf.Round(value * brushSizePreset.Presets.Count);
                targetValue = index / (float) brushSizePreset.Presets.Count;
                OnBrushSizeChange(brushSizePreset.GetSize(index));
            });
        }

        private void Update()
        {
            brushSizeScrollbar.value = Mathf.Lerp(brushSizeScrollbar.value, targetValue, Time.deltaTime * 50);
        }
    }
}