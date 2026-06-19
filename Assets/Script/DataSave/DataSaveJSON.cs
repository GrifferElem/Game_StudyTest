using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataSaveJSON : MonoBehaviour
{
    public Transform player;
    public MeshRenderer meshRen;

    private void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
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
        SaveData saveData = new SaveData();
        saveData.playerPos = player.position;
        saveData.playerColor = meshRen.material.color;

        string json = JsonUtility.ToJson(saveData,true);//true是转json时自动把内容换行
        File.WriteAllText(Application.dataPath + "/SaveTest.txt", json);
    }

    private void Load()
    {
        print("LoadData");
        string json = File.ReadAllText(Application.dataPath + "/SaveTest.txt");
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

        player.position = saveData.playerPos;
        meshRen.material.color = saveData.playerColor;
    }
}

public class SaveData
{
    public Vector3 playerPos;
    public Color playerColor;
}
