using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorMode : MonoBehaviour
{
    public GameObject editorMenu;
    public GameObject normalUI;

    public enum State { editor, normal };
    public State state = State.normal;
    void Update()
    {
        switch (state)
        {
            case State.normal:
                editorMenu.SetActive(false);
                normalUI.SetActive(true);
                break;
            case State.editor:
                editorMenu.SetActive(true);
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
