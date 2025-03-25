using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveChair : MonoBehaviour
{
    public bool isStartChair;
    public string sceneName;
    private Animator animator;

    private bool interactable;
    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        animator = GetComponent<Animator>();
        if (isStartChair)
        {
            GameManager.Instance.lastSaveChairScene = sceneName;
        }
    }

    //进入交互区域内，保存，将GameManager的保存椅子场景名索引改成目前的场景名，这样就可以找到这个椅子了复活了。一个场景最多一把保存椅子。





    private void OnTriggerEnter2D(Collider2D collision)
    {
        interactable = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactable = false;
    }

    private void Update()
    {
        if (interactable && Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("ccc");
            animator.Play("Brake");
            GameManager.Instance.lastSaveChairScene = sceneName;
            GameManager.Instance.lastSavePoint = this.transform;
            PlayerDisplayData.Instance.playerDestructable.CurrHealth = PlayerDisplayData.Instance.playerDestructable.maxHealth;
        }
    }
}
