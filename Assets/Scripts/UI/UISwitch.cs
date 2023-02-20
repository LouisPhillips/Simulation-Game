using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISwitch : MonoBehaviour
{
    public GameObject editorMenu;
    public GameObject normalUI;
    public GameObject jobCard;

    public enum State { editor, normal, job };
    public State state = State.normal;

    private bool goToNormal = true;

    private TimeScaler ts;

    private void Start()
    {
        ts = GetComponent<TimeScaler>();
    }
    void Update()
    {
        switch (state)
        {
            case State.normal:
                if (goToNormal)
                {
                    ts.timeScale = 1f;
                    goToNormal = false;
                }
                editorMenu.SetActive(false);
                jobCard.SetActive(false);
                normalUI.SetActive(true);
                break;
            case State.editor:
                ts.timeScale = 0;
                goToNormal = true;
                editorMenu.SetActive(true);
                jobCard.SetActive(false);
                normalUI.SetActive(false);
                break;
            case State.job:
                ts.timeScale = 0;
                goToNormal = true;
                editorMenu.SetActive(false);
                jobCard.SetActive(true);
                normalUI.SetActive(false);
                break;


        }

        if (Input.GetKeyDown(KeyCode.Escape) && state == State.normal)
        {
            state = State.editor;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && state == State.editor)
        {
            state = State.normal;
        }
    }
}
