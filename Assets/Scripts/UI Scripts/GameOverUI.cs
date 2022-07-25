using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameOverUI : MonoBehaviour
{
    [SerializeField]private TMP_Text gameOverScoreText;
    [SerializeField]private ScoreTracker playerScoreData;
    
    private void Start() {
        ShowScoreAfterGameOver();
    }
    private void ShowScoreAfterGameOver()
    {
        gameOverScoreText.text = playerScoreData.CurrentScore.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(StringHolder.GameplaySceneName);
    }
    public void BackToMain()
    {
        SceneManager.LoadScene(StringHolder.MainMenuSceneName);
    }

    public void Exit()
    {
        Application.Quit();
    }    
}
