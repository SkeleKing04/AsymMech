using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimpleStatTracker : MonoBehaviour
{
    public StatBlock m_targetBlock {get; private set;}
    public Image m_image;
    public TextMeshProUGUI m_statNum;
    public void GiveStat(StatBlock stats)
    {
        m_targetBlock = stats;
        m_statNum.text = m_targetBlock.currentValue.ToString();
        CheckStat();
    }

    public void UpdateStat(float value)
    {
        m_statNum.text = m_targetBlock.UpdateValue(value).ToString();
        CheckStat();
    }
    private void CheckStat()
    {
        if(m_targetBlock.reachedLowCap)
            m_image.color = Color.red;
        else if(m_targetBlock.reachedHighCap)
            m_image.color = Color.green;
        else
            m_image.color = Color.white;
    }
}
