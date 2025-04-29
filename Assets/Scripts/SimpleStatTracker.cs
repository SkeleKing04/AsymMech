using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimpleStatTracker : MonoBehaviour
{
    public MechStats m_targetMech {get; private set;}
    public string m_statKey {get; private set;}
    public StatBlock m_targetBlock {get; private set;}
    public Image m_image;
    public TextMeshProUGUI m_statNum;
    public void GiveMech(MechStats mech)
    {
        m_targetMech = mech;
    }
    public void GetStat(MechStats mech, string key)
    {
        m_targetMech = mech;
        m_targetBlock = m_targetMech.GetStat(key);
        m_statNum.text = m_targetBlock.currentValue.ToString();
        CheckStat();
    }
    public void GetStat(string key)
    {
        if(m_targetMech != null)
            GetStat(m_targetMech, key);
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
