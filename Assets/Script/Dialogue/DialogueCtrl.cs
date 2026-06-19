using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueCtrl : MonoBehaviour
{
    private ConversationSO currentConv;
    private int currentSentIndex;

    public ConversationSO conversation;

    [SerializeField] CanvasGroup canvasGroup;//对话框的消失用
    [SerializeField] TMP_Text contentText;
    [SerializeField] TMP_Text avtorName;
    [SerializeField] Image avtorImage;

    public Sprite avtor1;
    public Sprite avtor2;

    private void Start()
    {
        canvasGroup.alpha = 0;
    }
    private void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            StartDialogue(conversation);
        }
        if (currentConv!=null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            UpdateDialogue();
        }
    }

    public void StartDialogue(ConversationSO c)
    {
        currentConv = c;
        currentSentIndex = 0;

        canvasGroup.DOFade(1f, 1f);
        UpdateDialogueUI();
    }

    private void UpdateDialogueUI()
    {
        contentText.text = currentConv.sentences[currentSentIndex].content;
        avtorName.text = currentConv.sentences[currentSentIndex].name;
        avtorImage.sprite = currentConv.sentences[currentSentIndex].avtorPic;
    }

    public void UpdateDialogue()
    {
        currentSentIndex++;
        if (currentSentIndex == currentConv.sentences.Count)
        {
            EndDialogue();
            return;
        }
        UpdateDialogueUI();
    }
    public void EndDialogue()
    {
        canvasGroup.DOFade(0, 1f);
        currentConv = null;
    }
}

[Serializable]
public class Sentence
{
    public string content;
    public string name;
    public Sprite avtorPic;
} 