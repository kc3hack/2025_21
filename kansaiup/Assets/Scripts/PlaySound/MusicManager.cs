using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;  // シングルトン用変数

    void Awake()
    {
        // このオブジェクトを保持する
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
