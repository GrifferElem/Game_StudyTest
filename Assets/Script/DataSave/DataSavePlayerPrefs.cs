using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataSavePlayerPrefs : MonoBehaviour
{
    public Transform player;

    private void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            print(Application.dataPath);
            Save();
        }
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            Load();
        }
    }

    private void Save()
    {
        print("SaveData");
        PlayerPrefs.SetFloat("PlayerX", player.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.position.y);
        PlayerPrefs.SetFloat("PlayerZ", player.position.z);
    }

    private void Load()
    {
        print("LoadData");
        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        float z = PlayerPrefs.GetFloat("PlayerZ");
        player.position = new Vector3(x, y, z);
    }
}
