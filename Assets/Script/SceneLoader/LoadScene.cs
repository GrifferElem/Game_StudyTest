using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public string sceneName;
    public GameObject loading;
    public TMP_Text prograssText;
    public Slider prograssSlider;

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            StartCoroutine(LoadSceneAsync(sceneName));
        }
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        loading.SetActive(true);//显示加载界面
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);//发出异步加载的指令，并且获取当前进度的变量

        asyncLoad.allowSceneActivation = false;//不让引擎自动切换到新场景，这可以让我们更好地控制加载流程
        float fakePrograss = 0;
        while (!asyncLoad.isDone)
        {
            fakePrograss += Time.deltaTime * 0.3f;
            float prograss = Mathf.Clamp01(asyncLoad.progress / 0.9f);///获取到真正的加载进度！（要除以0.9)
            prograssText.text = fakePrograss.ToString("p2");//p代表percentage，2代表小数精度;将当前进度转换为百分数形式，并在text组件上显示

            prograssSlider.value = fakePrograss;//用进度条显示当前进度
            if (fakePrograss >= 1f&& prograss>=1f)//当场景加载完毕时
            {
                asyncLoad.allowSceneActivation = true;//切换到新的场景
            }
            yield return null;  
        }
    }
}
