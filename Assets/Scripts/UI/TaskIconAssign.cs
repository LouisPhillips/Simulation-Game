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
        /*for (int j = 0; j < transform.childCount; j++)
        {
            for (int i = 0; i < selectedAI.selectedAI.queueState.Count; i++)
            {
                if (selectedAI.selectedAI.queueState[i] == BasePerson.State.Wander)
                {
                    transform.GetChild(j).GetComponent<RawImage>().texture = wanderTexture;
                }
                if (selectedAI.selectedAI.queueState[i] == BasePerson.State.Eating)
                {
                    transform.GetChild(j).GetComponent<RawImage>().texture = foodTexture;
                }
                if (selectedAI.selectedAI.queueState[i] == BasePerson.State.Drinking)
                {
                    transform.GetChild(j).GetComponent<RawImage>().texture = drinkTexture;
                }
                if (selectedAI.selectedAI.queueState[i] == BasePerson.State.Sleeping)
                {
                    transform.GetChild(j).GetComponent<RawImage>().texture = sleepTexture;
                }
                if (selectedAI.selectedAI.queueState[i] == BasePerson.State.Entertaining)
                {
                    transform.GetChild(j).GetComponent<RawImage>().texture = entertainTexture;
                }
                if (selectedAI.selectedAI.queueState[i] == BasePerson.State.GoBeSocial)
                {
                    transform.GetChild(j).GetComponent<RawImage>().texture = goToSocialTexture;
                }
                if (selectedAI.selectedAI.queueState[i] == BasePerson.State.Socialize)
                {
                    transform.GetChild(j).GetComponent<RawImage>().texture = socialTexture;
                }
                if (selectedAI.selectedAI.queueState[i] == BasePerson.State.Work)
                {
                    transform.GetChild(j).GetComponent<RawImage>().texture = workTexture;
                }
            }
        }*/
        for (int i = 0; i < transform.childCount; i++)
        {
            if (selectedAI.selectedAI.queueState[i] == BasePerson.State.Wander)
            {
                transform.GetChild(i).GetComponent<RawImage>().texture = wanderTexture;
            }
            if (selectedAI.selectedAI.queueState[i] == BasePerson.State.Eating)
            {
                transform.GetChild(i).GetComponent<RawImage>().texture = foodTexture;
            }
            if (selectedAI.selectedAI.queueState[i] == BasePerson.State.Drinking)
            {
                transform.GetChild(i).GetComponent<RawImage>().texture = drinkTexture;
            }
            if (selectedAI.selectedAI.queueState[i] == BasePerson.State.Sleeping)
            {
                transform.GetChild(i).GetComponent<RawImage>().texture = sleepTexture;
            }
            if (selectedAI.selectedAI.queueState[i] == BasePerson.State.Entertaining)
            {
                transform.GetChild(i).GetComponent<RawImage>().texture = entertainTexture;
            }
            if (selectedAI.selectedAI.queueState[i] == BasePerson.State.GoBeSocial)
            {
                transform.GetChild(i).GetComponent<RawImage>().texture = goToSocialTexture;
            }
            if (selectedAI.selectedAI.queueState[i] == BasePerson.State.Socialize)
            {
                transform.GetChild(i).GetComponent<RawImage>().texture = socialTexture;
            }
            if (selectedAI.selectedAI.queueState[i] == BasePerson.State.Work)
            {
                transform.GetChild(i).GetComponent<RawImage>().texture = workTexture;
            }
        }
    }
}
