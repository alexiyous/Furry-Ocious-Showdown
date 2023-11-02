using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using TMPro;

public class BattleSystem : MonoBehaviour
{
    public event EventHandler OnBattleStart, OnBattleEnd;

    private enum State
    {
        Idle,
        Active,
        BattleEnd,
    }

    [SerializeField] private ColliderTrigger colliderTrigger;
    [SerializeField] private Wave[] waveArray;

    [SerializeField] private TextMeshProUGUI waveCountText;
    [SerializeField] private TextMeshProUGUI waveTimerText;

    private State state;
    private int currentWaveIndex;

    private Wave currentActiveWave;

    private void Awake()
    {
        state = State.Idle;
    }

    // Start is called before the first frame update
    void Start()
    {
        colliderTrigger.OnPlayerEnterTrigger += ColliderTrigger_OnPlayerEnterTrigger;
        
    }

    private void ColliderTrigger_OnPlayerEnterTrigger(object sender, EventArgs e)
    {
        if(state == State.Idle)
        {
            StartBattle();
            colliderTrigger.OnPlayerEnterTrigger -= ColliderTrigger_OnPlayerEnterTrigger;
        }
        
    }

    private void StartBattle()
    {
        Debug.Log("Start Battle");

        state = State.Active;


        currentWaveIndex = 0;
        currentActiveWave = waveArray[currentWaveIndex];

        currentActiveWave.SetCoroutineStarter(this);

        OnBattleStart?.Invoke(this, EventArgs.Empty);
    }

    private void Update()
    {
        switch(state)
        {
            case State.Active:
                    currentActiveWave.FixedUpdate();
                UpdateWaves();
                TestBattleOver();
                break;
        }
        
    }

    private void UpdateWaves()
    {
        if (currentWaveIndex < waveArray.Length)
        {
            waveArray[currentWaveIndex].FixedUpdate();

            if (waveArray[currentWaveIndex].GetWaveTimer() <= 0)
            {
                // If the timer reaches zero, update the current wave index
                currentWaveIndex++;

                if (currentWaveIndex < waveArray.Length)
                {
                    // Update the wave count text with the current wave number
                    waveCountText.text = "Wave " + (currentWaveIndex) + "/" + (waveArray.Length - 1);

                    currentActiveWave = waveArray[currentWaveIndex];
                    currentActiveWave.SetCoroutineStarter(this);
                }
                else
                {
                    waveTimerText.text = "";
                }
            }

            if(currentWaveIndex < waveArray.Length)
            {
                waveTimerText.text = "Next Wave In " + (int)waveArray[currentWaveIndex].GetWaveTimer() + " seconds";
            }
            
        }
    }

    private void TestBattleOver()
    {
        foreach(Wave wave in waveArray)
        {
            if(state == State.Active)
            {
                if(areWavesOver())
                {
                    Debug.Log("battle over");
                    StartCoroutine(Victory());
                    state = State.BattleEnd;
                    OnBattleEnd?.Invoke(this, EventArgs.Empty);
                }
            }

        }
    }

    private bool areWavesOver()
    {
        foreach (Wave wave in waveArray)
        {
            if (!wave.IsWaveOver())
            {
                return false;
            } 
        }

        return true;
    }

    public IEnumerator Victory()
    {
        if (!GameManager.instance.hasLost)
        {
            GameManager.instance.hasWon = true;
            PauseHandler.ableToPause = false;

            yield return new WaitForSecondsRealtime(3f);

            SceneTransitionHandler.instance.EndTransition("Victory");
        }


    }

    [System.Serializable]
    private class Wave
    {
        [InlineEditor][SerializeField] private Enemy[] enemyArray;
        [SerializeField] private float timer;
        [SerializeField] private float spawnRate = .5f;

        private MonoBehaviour coroutineStarter;
        public Wave(MonoBehaviour starter)
        {
            coroutineStarter = starter;
        }
        public void FixedUpdate()
        {
            if(timer >= 0)
            {
                timer -= Time.deltaTime * 0.5f;
                if (timer <= 0)
                {
                    coroutineStarter.StartCoroutine(SpawnEnemies());
                }
            }
            
        }
        private IEnumerator SpawnEnemies()
        {
            foreach (Enemy enemySpawn in enemyArray)
            {
                enemySpawn.Spawn();
                yield return new WaitForSeconds(spawnRate);
            }
        }

        public bool IsWaveOver()
        {
            if(timer < 0)
            {
                foreach(Enemy enemy in enemyArray)
                {
                    if(enemy.isAlive)
                    {
                        return false;
                    }
                }
                return true;
            } else
            {
                return false;
            }

            
        }

        public float GetWaveTimer()
        {
            return timer;
        }

        public void SetCoroutineStarter(MonoBehaviour starter)
        {
            coroutineStarter = starter;
        }

        

    }
}