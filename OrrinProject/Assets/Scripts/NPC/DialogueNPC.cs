using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using DG.Tweening;

public class DialogueNPC : MonoBehaviour
{



    [Serializable]
    public struct DialogueEvent
    {
        public string text;
        public UnityEvent OnDisplay;
        public UnityEvent OnSwitch;
        //public float duration;
    }

    //"按E交互"
    public GameObject InteractHint;

    //本地对话数据
    public List<DialogueEvent> Dialogue0;
    private int currDialogueIndex;
    private Tween dialogueTween;
    public KeyCode InteractKey = KeyCode.E;
    public bool talkAble = true;

    //场景对话UI组件
    public GameObject dialogueUIObject;
    public Text dialogueText;

    public bool isDialogueActive;

    public GameObject Store;
    public bool storeOpen;


    public void InitializeDialogue()
    {
        InteractHint.SetActive(false);
        //禁止玩家移动
        PlayerController.Instance.SetFreeze(true);
        PlayerAttackControl.Instance.SetFreeze(true);
        //启用对话面板
        dialogueUIObject.SetActive(true);
        isDialogueActive = true;
        currDialogueIndex = 0;
        dialogueText.text = string.Empty;
        dialogueTween = dialogueText.DOText(Dialogue0[currDialogueIndex].text, Dialogue0[currDialogueIndex].text.Length * 0.1f).OnComplete(() =>
          { Dialogue0[currDialogueIndex].OnDisplay.Invoke(); });
    }

    public void SwitchToNext()
    {
        dialogueTween.Kill();
        Dialogue0[currDialogueIndex].OnSwitch.Invoke();
        if (currDialogueIndex == Dialogue0.Count - 1)
        {
            EndDialogue();
            return;
        }
        else
        {
            currDialogueIndex += 1;
        }
        dialogueText.text = string.Empty;
        dialogueTween = dialogueText.DOText(Dialogue0[currDialogueIndex].text, Dialogue0[currDialogueIndex].text.Length * 0.1f).OnComplete(() =>
            { Dialogue0[currDialogueIndex].OnDisplay.Invoke(); });
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
        //关闭对话面板
        dialogueUIObject.SetActive(false);
        //恢复玩家移动
        PlayerController.Instance.SetFreeze(false);
        PlayerAttackControl.Instance.SetFreeze(false);

        InteractHint.SetActive(true);

    }

    private void Update()
    {
        //当当前对话在显示，且受到交互指令“E”
        if (talkAble && Input.GetKeyDown(InteractKey))
        {
            if (isDialogueActive)
            {
                SwitchToNext();
            }
            else
            {
                InitializeDialogue();
            }

        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            CloseStore();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        InteractHint.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InteractHint.SetActive(false);
    }

    public void OpenStore()
    {
        Store.SetActive(true);
        talkAble = false;
        storeOpen = true;
    }

    public void CloseStore()
    {
        if (storeOpen)
        {
            Store.SetActive(false);
            talkAble = true;
        }
    }
}
