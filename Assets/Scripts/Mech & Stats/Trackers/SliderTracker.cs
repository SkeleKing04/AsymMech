using UnityEngine;
using UnityEngine.UI;

public class SliderTracker : SimpleStatTracker
{
    [SerializeField] Image m_slider;
    public override void CheckStat()
    {
        m_slider.fillAmount = m_targetBlock.capPercent;
    }
}
