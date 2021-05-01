﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanterManager : MonoBehaviour
{
    public GameObject planterPrefab;
    public Mushroom startingMushroom;

    public static void AddPlanter()
    {
        PersistentData.planterData.Add(new Planter.Data());
    }

    public static void AddPoints(int quantity)
    {
        PersistentData.totalPoints += quantity;
    }

    public static void RemovePoints(int quantity)
    {
        PersistentData.totalPoints -= quantity;
    }

    public static void AddDiscoveredMushroom(Mushroom mushroom)
    {
        if (PersistentData.discoveredMushrooms.Contains(mushroom)) return;
        PersistentData.discoveredMushrooms.Add(mushroom);
    }

    public static float GameTimeRemaining()
    {
        return PersistentData.gameOverTime - Time.time;
    }

    public static void Init()
    {
        persistentData = new Data();
        AddPlanter();
        PersistentData.gameOverTime = Time.time + (30);
    }

    public class Data
    {
        public List<Planter.Data> planterData = new List<Planter.Data>();
        public int totalPoints;
        public List<Mushroom> discoveredMushrooms = new List<Mushroom>();
        public float gameOverTime = -1;
    }
    private static Data persistentData;

    public static Data PersistentData
    {
        get
        {
            if (persistentData == null)
            {
                Init();
            }
            return persistentData;
        }
    }

    private void Start()
    {
        for (int i = 0; i < PersistentData.planterData.Count; i++)
        {
            Transform spot = transform.GetChild(i);
            GameObject planter = Instantiate(planterPrefab, spot);
            planter.GetComponent<Planter>().data = PersistentData.planterData[i];
        }
        AddDiscoveredMushroom(startingMushroom);
    }

    private void Update()
    {
        if (PersistentData.gameOverTime <= Time.time)
        {
            SceneManager.LoadScene("MainMenu");
            Init();
        }
    }
}
