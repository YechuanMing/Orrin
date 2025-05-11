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
    public List<DialogueEvent> Dialogue1;
    public List<DialogueEvent> Dialogue2;
    public List<DialogueEvent> Dialogue3;

    private List<DialogueEvent> currDialogue;
    public int currDialogueIndex;
    public int currSentenceIndex;

    private Tween dialogueTween;
    public KeyCode InteractKey = KeyCode.E;
    public bool talkAble = true;

    //场景对话UI组件
    public GameObject dialogueUIObject;
    public Text dialogueText;

    public bool isDialogueActive;

    public GameObject Store;
    public bool storeOpen;

    public AudioClip[] TalkSounds;
    public void Start()
    {
        currDialogueIndex = GameManager.Instance.NPCDialogueIndex;
    }

    public void Talk()
    {
        if(TalkSounds.Length>0)
        {
            AudioManager.Instance.PlaySoundEffect(TalkSounds[UnityEngine.Random.Range(0, TalkSounds.Length - 1)]);
        }
    }

    public void SetCurrentDialogue()
    {
        switch(currDialogueIndex)
        {
            case 0:
                currDialogue = Dialogue0;
                break;
            case 1:
                currDialogue = Dialogue1;
                break;
            case 2:
                currDialogue = Dialogue2;
                break;
            case 3:
                currDialogue = Dialogue3;
                break;

        }
    }
    public void InitializeDialogue()
    {
        InteractHint.SetActive(false);
        //禁止玩家移动
        PlayerController.Instance.SetFreeze(true);
        PlayerAttackControl.Instance.SetFreeze(true);
        //启用对话面板
        dialogueUIObject.SetActive(true);
        SetCurrentDialogue();
        isDialogueActive = true;
        currSentenceIndex = 0;
        dialogueText.text = string.Empty;
        dialogueTween = dialogueText.DOText(currDialogue[currSentenceIndex].text, currDialogue[currSentenceIndex].text.Length * 0.1f).OnComplete(() =>
          { currDialogue[currSentenceIndex].OnDisplay.Invoke(); });
    }

    public void SwitchToNext()
    {
        dialogueTween.Kill();
        currDialogue[currSentenceIndex].OnSwitch.Invoke();
        if (currSentenceIndex == currDialogue.Count - 1)
        {
            EndDialogue();
            return;
        }
        else
        {
            currSentenceIndex += 1;
        }
        dialogueText.text = string.Empty;
        dialogueTween = dialogueText.DOText(currDialogue[currSentenceIndex].text, currDialogue[currSentenceIndex].text.Length * 0.1f).OnComplete(() =>
            { currDialogue[currSentenceIndex].OnDisplay.Invoke(); });
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

    public void SwitchDialogueIndex(int index)
    {
        currDialogueIndex = index;
        GameManager.Instance.NPCDialogueIndex= index;
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
            storeOpen = false;
            talkAble = true;
        }
    }
}
