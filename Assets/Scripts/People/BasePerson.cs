using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasePerson : MonoBehaviour
{
    public float timeScale = 0f;
    [Header("Needs")]
    public float health;
    public float hunger;
    public float thirst;
    public float tiredness;
    public float entertainment;

    [Space(10)]
    [Header("Max Needs")]
    public float maxHealth;
    public float maxHunger;
    public float maxThirst;
    public float maxTiredness;
    public float maxEntertained;

    [Space(10)]
    [Header("Satisfied Values")]
    public float satisfiedHealth;
    public float satisfiedHunger;
    public float satisfiedThirst;
    public float satisfiedTiredness;
    public float satisfiedEntertainment;

    [Space(10)]
    [Header("Components")]
    public Rigidbody rb;
    public NavMeshAgent navigation;

    [Space(10)]
    [Header("Findings")]
    public List<GameObject> drinkingLocations;
    public List<GameObject> foodLocations;
    public List<GameObject> sleepLocations;
    public List<GameObject> entertainmentLocations;

    protected float decayValue = 0.5f;
    public enum State { Idle, FindFood, FindDrink, FindSleep, FindEntertainment, Wander, Eating, Drinking, Sleeping, Entertaining };
    public State state;

    protected float wanderDelay = 0f;
    protected float wanderEvery = 5f;

    protected float eatTimer = 0f;
    protected float eatMax = 2f;

    protected float drinkTimer = 0f;
    protected float drinkMax = 3f;

    protected bool awake = true;
    protected bool decisionMade = false;
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        navigation = GetComponent<NavMeshAgent>();

        drinkingLocations = new List<GameObject>();
        foodLocations = new List<GameObject>();
        sleepLocations = new List<GameObject>();

        GameObject[] drinks = GameObject.FindGameObjectsWithTag("Drink");
        for (int i = 0; i < drinks.Length; i++)
        {
            drinkingLocations.Add(drinks[i]);
        }

        GameObject[] food = GameObject.FindGameObjectsWithTag("Food");
        for (int i = 0; i < food.Length; i++)
        {
            foodLocations.Add(food[i]);
        }

        GameObject[] sleep = GameObject.FindGameObjectsWithTag("Sleep");
        for (int i = 0; i < sleep.Length; i++)
        {
            sleepLocations.Add(sleep[i]);
        }

        GameObject[] entertainmentObj = GameObject.FindGameObjectsWithTag("Entertainment");
        for (int i = 0; i < entertainmentObj.Length; i++)
        {
            entertainmentLocations.Add(entertainmentObj[i]);
        }

        state = State.Wander;

        health = maxHealth;
        hunger = maxHunger;
        thirst = maxThirst;
        tiredness = maxTiredness;
        entertainment = maxEntertained;
    }


    public virtual void Update()
    {
        switch (state)
        {
            case State.Idle:
                Idle();
                break;
            case State.FindDrink:
                GoTo(drinkingLocations, 7);
                break;
            case State.FindFood:
                GoTo(foodLocations, 6);
                break;
            case State.FindSleep:
                GoTo(sleepLocations, 8);
                break;
            case State.FindEntertainment:
                GoTo(entertainmentLocations, 9);
                break;
            case State.Wander:
                Wander();
                break;
            case State.Eating:
                Eat(GetClosest(foodLocations).GetComponent<FoodBites>());
                break;
            case State.Drinking:
                Drink();
                break;
            case State.Sleeping:
                Sleep();
                break;
            case State.Entertaining:
                Entertain();
                break;

        }
        if (awake)
        {
            DeteriateSleep();
        }
        if (state != State.Sleeping)
        {
            awake = true;
        }

        Decision();

        DeteriateHunger();
        DeteriateThirst();
        DeteriateEntertainment();
    }

    public void Decision()
    {
        bool currentlyEating = state == State.FindFood;
        bool currentlyDrinking = state == State.FindDrink;
        bool currentlySleeping = state == State.FindSleep;
        bool currentlyEntertaining = state == State.FindEntertainment;

        if (!decisionMade)
        {
            if (tiredness > maxTiredness % 3 && !currentlySleeping)
            {
                if (satisfiedHunger > hunger && !currentlyEating)
                {
                    if (foodLocations.Count > 0)
                    {
                        state = State.FindFood;
                    }
                }
                else if (satisfiedThirst > thirst && !currentlyDrinking)
                {
                    if (drinkingLocations.Count > 0)
                    {
                        state = State.FindDrink;
                    }
                }
                else if (satisfiedEntertainment > entertainment && !currentlyEntertaining)
                {
                    if (entertainmentLocations.Count > 0)
                    {
                        state = State.FindEntertainment;
                    }
                }
            }
            else
            {
                state = State.FindSleep;
            }
        }
    }

    public void GoTo(List<GameObject> location, int desiredState)
    {
        decisionMade = true;
        if (location.Count == 0)
        {
            state = State.Wander;
        }
        else
        {
            navigation.destination = GetClosest(location).position;
            if (Vector3.Distance(transform.position, navigation.destination) == 1f)
            {
                state = (State)desiredState;
            }
        }
    }

    public void Eat(FoodBites food)
    {
        eatTimer += Time.deltaTime;
        if (eatTimer > eatMax)
        {
            hunger += food.foodValue;
            eatTimer = 0;
        }

        if (hunger > maxHunger)
        {
            hunger = maxHunger;
        }

        if (hunger > satisfiedHunger)
        {
            state = State.Wander;
        }

    }

    public void Drink()
    {
        drinkTimer += Time.deltaTime;
        if (drinkTimer > drinkMax)
        {
            thirst += 10f;
            drinkTimer = 0;
        }

        if (thirst > maxThirst)
        {
            thirst = maxThirst;
        }

        if (thirst > satisfiedThirst)
        {
            state = State.Wander;
        }
    }

    public void Entertain()
    {
        entertainment += 1.5f * Time.deltaTime;

        if (entertainment > satisfiedEntertainment)
        {
            state = State.Wander;
        }
    }

    public void Sleep()
    {
        awake = false;
        tiredness += 2f * Time.deltaTime;

        if (tiredness > satisfiedTiredness)
        {
            state = State.Wander;
        }
    }

    public void Wander()
    {
        decisionMade = false;
        wanderDelay += Time.deltaTime;
        if (wanderDelay > wanderEvery)
        {
            navigation.SetDestination(RandomSphere(gameObject.transform.position, 20));
            if (navigation.pathStatus == NavMeshPathStatus.PathComplete)
            {
                wanderDelay = 0;
            }
        }
    }

    public void Idle()
    {
        navigation.destination = transform.position;
    }

    public void DeteriateSleep()
    {
        //tiredness = Mathf.Lerp(maxTiredness, 0, Time.time * 0.01f);
        tiredness -= decayValue / 2 * Time.deltaTime;
    }

    public void DeteriateHunger()
    {
        //hunger = Mathf.Lerp(maxHunger, 0, Time.time * 0.01f);
        hunger -= (decayValue / 1.5f) * Time.deltaTime;
    }

    public void DeteriateThirst()
    {
        //thirst = Mathf.Lerp(maxThirst, 0, Time.time * 0.01f);
        thirst -= decayValue  * Time.deltaTime;
    }

    public void DeteriateEntertainment()
    {
        entertainment -= decayValue * Time.deltaTime;
    }
    Vector3 RandomSphere(Vector3 start, float range)
    {
        Vector3 randomPoint = Random.insideUnitSphere * range;

        randomPoint += start;

        NavMeshHit navMeshHit;

        NavMesh.SamplePosition(randomPoint, out navMeshHit, range, NavMesh.AllAreas);

        return navMeshHit.position;
    }

    public Transform GetClosest(List<GameObject> gameObject)
    {
        float closestDistance = Mathf.Infinity;
        Transform trns = null;
         
        foreach (GameObject enemy in gameObject)
        {
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                trns = enemy.transform;
            }
        }
        return trns;
    }

    IEnumerator FoodDelay(FoodBites food)
    {
        Debug.Log("Delay");
        yield return new WaitForSeconds(1);
        food.bites -= 1;
        hunger += food.foodValue;
        StopCoroutine(FoodDelay(food));
    }
}
