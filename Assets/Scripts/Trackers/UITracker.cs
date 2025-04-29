using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class UITracker : SimpleStatTracker
{
    public Image m_image;
    public TextMeshProUGUI m_statNum;
    public TMP_Dropdown m_keySelector;
    public Gradient m_gradient;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        m_keySelector.ClearOptions();
        m_keySelector.AddOptions(m_targetMech.Stats.DumpKeys().ToList());
        GetStatDropdown(0);
    }

    public void GetStatVoid(string key)
    {
        GetStat(key);
    }
    public void GetStatDropdown(int index)
    {
        GetStat(m_keySelector.options[index].text);
    }
    public override void UpdateStat(float value)
    {
        m_statNum.text = m_targetBlock.UpdateValue(value).ToString();
        base.UpdateStat(value);
    }
    protected override void CheckStat()
    {
        m_statNum.text = m_targetBlock.currentValue.ToString();
        m_image.color = m_gradient.Evaluate(m_targetBlock.capPercent);
    }
}
