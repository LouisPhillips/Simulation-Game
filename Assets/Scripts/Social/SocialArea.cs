using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialArea : MonoBehaviour
{
    public GameObject[] ais;
    public float radius = 10f;
    private bool createdSocialSphere = false;
    private float conversationTimer = 0f;

    private SocialManager sm;

    private void Awake()
    {
        sm = GameObject.FindGameObjectWithTag("GameController").GetComponent<SocialManager>();
    }
    private void Update()
    {

        if (!createdSocialSphere)
        {
            for (int i = 0; i < sm.ais.Length; i++)
            {
                float distance = (Vector3.Distance(sm.ais[i].transform.position, transform.position));

                if (distance < radius)
                {
                    if (!sm.ais[i].GetComponent<Person>().queueState.Contains(BasePerson.State.Socialize))
                    {
                        if (!sm.ais[i].GetComponent<Person>().queueState.Contains(BasePerson.State.GoBeSocial))
                        {
                            sm.ais[i].GetComponent<Person>().previousState = sm.ais[i].GetComponent<Person>().queueState[0];
                        }
                        for (int j = 0; j < sm.ais[i].GetComponent<Person>().addedToQueue.Length; j++)
                        {
                            sm.ais[i].GetComponent<Person>().addedToQueue[j] = false;
                        }
                        sm.ais[i].GetComponent<Person>().queueState.Add(BasePerson.State.Socialize);
                        sm.ais[i].GetComponent<Person>().queueState.Remove(BasePerson.State.GoBeSocial);
                        sm.ais[i].GetComponent<Person>().queueState.Insert(0, sm.ais[i].GetComponent<Person>().previousState);
                        createdSocialSphere = true;
                    }
                }
            }
        }
        else
        {
            conversationTimer += Time.deltaTime;
            if (conversationTimer > 5f)
            {
                for (int i = 0; i < sm.ais.Length; i++)
                {
                    for (int j = 0; j < sm.ais[i].GetComponent<Person>().friends.Count; j++)
                    {
                        sm.ais[i].GetComponent<Person>().friendScore[j] += 1;
                    }
                }

                if (Random.value > 0.9f)
                {
                    for (int i = 0; i < sm.ais.Length; i++)
                    {
                        if (sm.ais[i].GetComponent<Person>().queueState.Count > 1)
                        {
                            sm.ais[i].GetComponent<Person>().queueState.RemoveAt(0);
                            sm.ais[i].GetComponent<Person>().socializingWith = null;
                            Destroy(gameObject);
                        }
                        else
                        {
                            sm.ais[i].GetComponent<Person>().queueState.Add(BasePerson.State.Wander);
                            sm.ais[i].GetComponent<Person>().queueState.RemoveAt(0);
                        }
                    }
                }
                else
                {
                    conversationTimer = 0f;
                }
            }
        }
    }
}
