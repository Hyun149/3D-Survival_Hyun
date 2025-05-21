using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GoldSystem GoldSystem { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            GoldSystem = GetComponent<GoldSystem>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
