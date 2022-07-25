using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Score Tracker Data", fileName = "Score Tracker Data")]

public class ScoreTracker : ScriptableObject
{

    private float _currentScore;
    public float CurrentScore { get => _currentScore;}

    public System.Action<float> OnScoreSet;

    void Start()
    {
        _currentScore = 0;
    }

    public void IncreaseScore(float increment)
    {
        _currentScore += increment;
        OnScoreSet?.Invoke(_currentScore);
    }
}
