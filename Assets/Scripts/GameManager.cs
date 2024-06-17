using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUNPaused;
  private enum  State
  {
    WaitingTostart,
    CountdownToStart,
    GamePlaying,
    GameOver
  }

  private State state;
  private float waitingTostartTimer = 1f;
  private float countDownToStart = 3f;
  private float gamePalyingTimer;
  private float max = 30f;
  private bool isGamePaused = false;


  private void Awake()
  {
    Instance = this;
    state = State.WaitingTostart;
  }

  private void Start()
  {
    GameInput.Instance.OnpauseAction += GameInput_OnPause;
  }

  private void GameInput_OnPause(object sender, EventArgs e)
  {
    PauseGameStart();
  }

  private void Update()
  {
    switch (state)
    {
      case State.WaitingTostart:
        waitingTostartTimer -=Time.deltaTime;
        if (waitingTostartTimer <= 0f)
        {
          state = State.CountdownToStart;
          OnStateChanged?.Invoke(this,EventArgs.Empty);
        }
        break;
      case State.CountdownToStart:
        countDownToStart -= Time.deltaTime;
        if (countDownToStart <= 0f)
        {
          state = State.GamePlaying;
          gamePalyingTimer = max;
          OnStateChanged?.Invoke(this,EventArgs.Empty);
        }

        break;
      case State.GamePlaying:
        gamePalyingTimer -= Time.deltaTime;
        if (gamePalyingTimer <= 0f)
        {
          state = State.GameOver;
          OnStateChanged?.Invoke(this,EventArgs.Empty);
        }
        break;
      case State.GameOver:
        break;
    }
   
  }

  public  bool  IsPlaying()
  {
    return state == State.GamePlaying;
  }

  public bool IsCountDownToStartAtcitv()
  {
    return state == State.CountdownToStart;
  }

  public bool IsGameOver()
  {
    return state == State.GameOver;
  }

  public float GetGamePlayingTimerNormalized()
  {
    return 1 - (gamePalyingTimer / max);
  }

  public void PauseGameStart()
  {
    isGamePaused = !isGamePaused;
    if (isGamePaused)
    {
      Time.timeScale = 0f;
      OnGamePaused?.Invoke(this,EventArgs.Empty);
    }
    else
    {
      Time.timeScale = 1f;
      OnGameUNPaused?.Invoke(this,EventArgs.Empty);
    }
  }
}