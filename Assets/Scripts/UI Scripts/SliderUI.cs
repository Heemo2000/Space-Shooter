using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    [SerializeField]private Image slider;
    
    public void SetSliderPercent(float numerator,float denominator)
    {
        slider.fillAmount = numerator/denominator;
    }
}
