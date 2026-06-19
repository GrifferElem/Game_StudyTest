using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Backpack : MonoBehaviour
{
    public InventoryUIManager uiMgr;

    private void Start()
    {
        InventoryManager.AddItem(0, 5);
        InventoryManager.AddItem(1, 6);
    }

    private void Update()
    {
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            uiMgr.UpdateUIDisplay();
        }
    }
}
