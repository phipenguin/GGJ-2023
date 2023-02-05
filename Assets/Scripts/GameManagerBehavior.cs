using System.Collections;
using System.Collections.Generic;
using Micosmo.SensorToolkit;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerBehavior : MonoBehaviour
{
    public static GameManagerBehavior Instance { get; private set; }

    [SerializeField] private TriggerSensor trigger;

    [SerializeField] private GameObject player;
    [SerializeField] private int maxPlayerHealth = 3;
    private void Awake()
    {
        if (Instance == null && Instance != this)
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        player = GameObject.FindWithTag("Plant");
        trigger.OnDetected.AddListener(SwitchToFirstBoss);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToFirstBoss(GameObject gameObject, Sensor sensor)
    {
        SceneManager.LoadScene("First Boss Area");
        player.GetComponent<Health>().playerHealth = maxPlayerHealth;
    }

    public void SwitchToSecondBoss()
    {
        SceneManager.LoadScene("Second Boss Area");
        player.GetComponent<Health>().playerHealth = maxPlayerHealth;
    }
}
