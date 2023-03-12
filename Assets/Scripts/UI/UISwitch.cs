using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UISwitch : MonoBehaviour
{
    public GameObject editorMenu;
    public GameObject normalUI;
    public GameObject jobCard;
    public GameObject queueUI;

    public enum State { editor, normal, job };
    public State state = State.normal;

    private bool goToNormal = true;

    private TimeScaler ts;
    private GlobalDoings globalDoings;

    public bool runForCheck = false;
    private void Start()
    {
        ts = GetComponent<TimeScaler>();
        globalDoings = GetComponent<GlobalDoings>();
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
                globalDoings.destroying = false;
                editorMenu.SetActive(false);
                jobCard.SetActive(false);
                editorMenu.GetComponentInChildren<DestroyObjects>().clickIndex = 0;
                Destroy(editorMenu.GetComponentInChildren<BuildWallsV2>().wallPoint);
                normalUI.SetActive(true);
                break;
            case State.editor:
                if (!runForCheck)
                {
                    ts.timeScale = 0;
                }
                goToNormal = true;
                editorMenu.SetActive(true);
                jobCard.SetActive(false);
                //queueUI.SetActive(false);
                normalUI.SetActive(false);
                break;
            case State.job:
                ts.timeScale = 0;
                goToNormal = true;
                editorMenu.SetActive(false);
                jobCard.SetActive(true);
                //queueUI.SetActive(false);
                normalUI.SetActive(false);
                break;


        }

        if (Input.GetKeyDown(KeyCode.Escape) && state == State.normal)
        {
            state = State.editor;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && state == State.editor && !globalDoings.placing)
        {
            state = State.normal;
        }
    }
}
