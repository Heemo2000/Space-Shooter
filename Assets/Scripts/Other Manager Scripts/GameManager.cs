using UnityEngine;
using UnityEngine.Events;

public class GameManager : GenericSingleton<GameManager>
{
    [SerializeField]private AudioSource musicAudioSource;
    [SerializeField]private ScoreTracker playerScoreData;
    private bool _isGamePlaying;

    
    public UnityEvent OnGameOver;

    public UnityEvent OnGameStart;

    public UnityEvent OnGamePaused;
    
    public UnityEvent OnGameResumed;

    public bool IsGamePlaying { get => _isGamePlaying; }

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start() 
    {
        OnGameOver.AddListener(MakeGameOver);
        InitializeLevel();        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && _isGamePlaying)
        {
            if(Time.timeScale == 1.0f)
            {
                PauseGame();
            }
            else
            {
                UnPauseGame();
            }
        }
    }


    private void InitializeLevel()
    {
        _isGamePlaying = true;
        SoundManager.Instance.Play(SoundType.Theme,musicAudioSource);
        playerScoreData.OnScoreSet?.Invoke(0f);
        OnGameStart?.Invoke();
    }

    private void OnDestroy() {
        OnGamePaused.RemoveListener(PauseGame);
        OnGameResumed.RemoveListener(UnPauseGame);
    }
    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        SoundManager.Instance.Pause(musicAudioSource);
        OnGamePaused?.Invoke();
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1.0f;
        SoundManager.Instance.Resume(musicAudioSource);
        OnGameResumed?.Invoke();
    }

    private void MakeGameOver()
    {
        _isGamePlaying = false;
        SoundManager.Instance.Stop(musicAudioSource);
    } 
}
