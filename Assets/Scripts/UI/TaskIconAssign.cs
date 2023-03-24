using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskIconAssign : MonoBehaviour
{
    public Texture foodTexture;
    public Texture drinkTexture;
    public Texture sleepTexture;
    public Texture entertainTexture;
    public Texture goToSocialTexture;
    public Texture socialTexture;
    public Texture workTexture;
    public Texture wanderTexture;

    private SelectedInScene selectedAI;
    void Awake()
    {
        selectedAI = GameObject.FindGameObjectWithTag("GameController").GetComponent<SelectedInScene>();

    }

    public void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (selectedAI.selectedAI.queueState[i] == BasePerson.State.Wander)
            {
                transform.GetChild(i).GetComponent<RawImage>().texture = wanderTexture;
                transform.GetChild(i).GetComponent<DesignatedState>().state = BasePerson.State.Wander;
            }
            if (selectedAI.selectedAI.queueState[i] == BasePerson.State.Eating)
            {
                transform.GetChild(i).GetComponent<RawImage>().texture = foodTexture;
                transform.GetChild(i).GetComponent<DesignatedState>().state = BasePerson.State.Eating;
            }
            if (selectedAI.selectedAI.queueState[i] == BasePerson.State.Drinking)
            {
                transform.GetChild(i).GetComponent<RawImage>().texture = drinkTexture;
                transform.GetChild(i).GetComponent<DesignatedState>().state = BasePerson.State.Drinking;
            }
            if (selectedAI.selectedAI.queueState[i] == BasePerson.State.Sleeping)
            {
                transform.GetChild(i).GetComponent<RawImage>().texture = sleepTexture;
                transform.GetChild(i).GetComponent<DesignatedState>().state = BasePerson.State.Sleeping;
            }
            if (selectedAI.selectedAI.queueState[i] == BasePerson.State.Entertaining)
            {
                transform.GetChild(i).GetComponent<RawImage>().texture = entertainTexture;
                transform.GetChild(i).GetComponent<DesignatedState>().state = BasePerson.State.Entertaining;
            }
            if (selectedAI.selectedAI.queueState[i] == BasePerson.State.GoBeSocial)
            {
                transform.GetChild(i).GetComponent<RawImage>().texture = goToSocialTexture;
                transform.GetChild(i).GetComponent<DesignatedState>().state = BasePerson.State.GoBeSocial;
            }
            if (selectedAI.selectedAI.queueState[i] == BasePerson.State.Socialize)
            {
                transform.GetChild(i).GetComponent<RawImage>().texture = socialTexture;
                transform.GetChild(i).GetComponent<DesignatedState>().state = BasePerson.State.Socialize;
            }
            if (selectedAI.selectedAI.queueState[i] == BasePerson.State.Work)
            {
                transform.GetChild(i).GetComponent<RawImage>().texture = workTexture;
                transform.GetChild(i).GetComponent<DesignatedState>().state = BasePerson.State.Work;
            }
        }
    }
}
