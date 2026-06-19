using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestMenuItem : Editor
{
    [MenuItem("Tools/Test1 %r")]
    private static void Test1()
    {
        Debug.Log("<color=#88ff88>方法1</color>");
    }
    [MenuItem("Tools/Test2", false)]
    private static void Test2()
    {
        Debug.Log("<color=#88ff88>方法2</color>");
    }
    [MenuItem("Tools/Test3", false, 3)]
    private static void Test3()
    {
        Debug.Log("<color=#88ff88>方法2</color>");
    }
}
