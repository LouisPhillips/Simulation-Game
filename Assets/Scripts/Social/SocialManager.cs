using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialManager : MonoBehaviour
{
    public GameObject socialSpherePrefab;
    private GameObject socialSphere = null;

    public List<GameObject> socialAIs;
    private bool spawned = false;

    public GameObject[] ais;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnSocialZone()
    {
        if (!spawned)
        {
            ais = GameObject.FindGameObjectsWithTag("AI");
            for (int i = 0; i < ais.Length; i++)
            {
                socialAIs.Add(ais[i]);
                socialAIs[i].GetComponent<Person>().queueState.Contains(BasePerson.State.GoBeSocial);
                socialSphere = Instantiate(socialSpherePrefab, transform.position, transform.rotation);
                spawned = true;
            }
        }
        else
        {
            Destroy(socialSphere);
        }

    }
}
