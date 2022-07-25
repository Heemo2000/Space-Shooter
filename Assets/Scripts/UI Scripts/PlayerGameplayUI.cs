using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerGameplayUI : MonoBehaviour
{
    [SerializeField]private SliderUI healthMeter;
    [SerializeField]private SliderUI shieldMeter;
    [SerializeField]private TMP_Text scoreText;

    [SerializeField]private ScoreTracker playerScoreData;
    private Player _player;
    private void Start() 
    {
        _player = FindObjectOfType<Player>();
        playerScoreData.OnScoreSet += SetScoreText;
        if(_player != null)
        {
            _player.HealthComp.OnCurrentHealthSet += healthMeter.SetSliderPercent;
            _player.ShieldComp.OnShieldAmountSet += shieldMeter.SetSliderPercent;
        }
    }
    private void SetScoreText(float value)
    {
        scoreText.text = value.ToString();
    }

    private void OnDisable()
    {
        playerScoreData.OnScoreSet -= SetScoreText;
    }   
    private void OnDestroy() 
    {
        
        if(_player != null)
        {
            _player.HealthComp.OnCurrentHealthSet -= healthMeter.SetSliderPercent;
            _player.ShieldComp.OnShieldAmountSet -= shieldMeter.SetSliderPercent;
        }   
    }
}
