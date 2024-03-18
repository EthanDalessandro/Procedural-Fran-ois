using TMPro;
using UnityEngine;

public class IncreaseDecreaseValue : MonoBehaviour
{
    public TextMeshProUGUI gridXText;
    public TextMeshProUGUI gridYText;
    public TextMeshProUGUI cooldownStepByStepText;

    public ScriptableObjectSettings gridX;
    public ScriptableObjectSettings gridY;
    public ScriptableObjectSettings cooldownStepByStep;

    private void Start()
    {
        Refresh();
    }

    public void IncreaseGridX()
    {
        gridX.floatToStock += 1;
        Refresh();
    }

    public void DecreaseGridX()
    {
        gridX.floatToStock -= 1;
        if(gridX.floatToStock <= 1)
        {
            gridX.floatToStock = 1;
        }
        Refresh();
    }

    public void IncreaseGridY()
    {
        gridY.floatToStock += 1;
        Refresh();
    }

    public void DecreaseGridY()
    {
        gridY.floatToStock -= 1;
        if (gridY.floatToStock <= 1)
        {
            gridY.floatToStock = 1;
        }
        Refresh();
    }

    public void IncreaseCooldownStepByStep()
    {
        cooldownStepByStep.floatToStock += 0.25f;
        Refresh();
    }

    public void DecreaseCooldownStepByStep()
    {
        cooldownStepByStep.floatToStock -= 0.25f;
        if (cooldownStepByStep.floatToStock <= 0)
        {
            cooldownStepByStep.floatToStock = 0;
        }
        Refresh();
    }

    public void Refresh()
    {
        gridXText.text = gridX.floatToStock.ToString();
        gridYText.text = gridY.floatToStock.ToString();
        cooldownStepByStepText.text = cooldownStepByStep.floatToStock.ToString();
    }
}
