using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasePerson : MonoBehaviour
{
    public float timeScale = 0f;
    [Header("Needs")]
    public float hunger;
    public float thirst;
    public float tiredness;
    public float entertainment;

    [Space(10)]
    [Header("Max Needs")]
    public float maxHunger;
    public float maxThirst;
    public float maxTiredness;
    public float maxEntertained;

    [Space(10)]
    [Header("Satisfied Values")]
    public float satisfiedHunger;
    public float satisfiedThirst;
    public float satisfiedTiredness;
    public float satisfiedEntertainment;

    [Space(10)]
    [Header("Critical Values")]
    public float criticalHunger;
    public float criticalThirst;
    public float criticalTiredness;
    public float criticalEntertainment;

    [Space(10)]
    [Header("Components")]
    public Rigidbody rb;
    public NavMeshAgent navigation;
    private GameObject gameManager;
    private GlobalLocations gl;
    private TimeScaler ts;

    [Space(10)]
    [Header("Job Info")]
    public float jobStartTime;
    public float jobFinishTime;
    public float paidPerHour;
    public string jobTitle;
    public enum Employment { Unemployed, Employed };
    public Employment employment;

    public enum State { Idle, FindFood, FindDrink, FindSleep, FindEntertainment, Wander, Eating, Drinking, Sleeping, Entertaining, GoToWork, Work };
    public State state;

    public enum Gender { Male, Female };
    public Gender gender;

    public enum Age { Child, Teenager, Young_Adult, Adult, Elder }
    public Age age;

    public Transform destination;

    protected float decayValue = 0.5f;

    protected float wanderDelay = 0f;
    protected float wanderEvery = 5f;

    protected float eatTimer = 0f;
    protected float eatMax = 2f;

    protected float drinkTimer = 0f;
    protected float drinkMax = 3f;

    protected bool awake = true;
    protected bool decisionMade = false;

    protected bool getAgeDay = true;
    protected int agedDay = 0;
    [SerializeField] protected bool canMakeDecision = true;
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        navigation = GetComponent<NavMeshAgent>();

        state = State.Wander;
        gender = Random.value < 0.5f ? Gender.Male : Gender.Female;
        employment = Employment.Unemployed;
        age = Age.Young_Adult;

        hunger = maxHunger;
        thirst = maxThirst;
        tiredness = maxTiredness;
        entertainment = maxEntertained;

        gameManager = GameObject.FindGameObjectWithTag("GameController");
        gl = gameManager.GetComponent<GlobalLocations>();
        ts = gameManager.GetComponent<TimeScaler>();

    }


    public virtual void Update()
    {
        switch (state)
        {
            case State.Idle:
                Idle();
                break;
            case State.FindDrink:
                GoTo(gl.drinkingLocations, 7);
                break;
            case State.FindFood:
                GoTo(gl.foodLocations, 6);
                break;
            case State.FindSleep:
                GoTo(gl.sleepLocations, 8);
                break;
            case State.FindEntertainment:
                GoTo(gl.entertainmentLocations, 9);
                break;
            case State.GoToWork:
                GoTo(gl.doorLocation, 11);
                break;
            case State.Wander:
                Wander();
                break;
            case State.Eating:
                Eat(GetClosest(gl.foodLocations).GetComponent<FoodBites>());
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
            case State.Work:
                Work();
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

        if (canMakeDecision)
        {
            Decision();
        }


        DeteriateHunger();
        DeteriateThirst();
        DeteriateEntertainment();

        CheckDeath();

        AgePerson();

    }

    public void Decision()
    {
        bool currentlyEating = state == State.FindFood;
        bool currentlyDrinking = state == State.FindDrink;
        bool currentlySleeping = state == State.FindSleep;
        bool currentlyEntertaining = state == State.FindEntertainment;
        bool currentlyWorking = state == State.Work;

        if (!decisionMade)
        {
            if (tiredness > maxTiredness / 3 && !currentlySleeping)
            {
                if (jobStartTime == ts.hour && !currentlyWorking || ts.hour == jobStartTime + 1)
                {
                    if (employment == Employment.Employed)
                    {
                        canMakeDecision = false;
                        state = State.GoToWork;

                    }
                }
                else if (satisfiedHunger > hunger && !currentlyEating || hunger < criticalHunger)
                {
                    if (gl.foodLocations.Count > 0)
                    {
                        state = State.FindFood;
                    }
                }
                else if (satisfiedThirst > thirst && !currentlyDrinking || thirst < criticalThirst)
                {
                    if (gl.drinkingLocations.Count > 0)
                    {
                        state = State.FindDrink;
                    }
                }
                else if (satisfiedEntertainment > entertainment && !currentlyEntertaining)
                {
                    if (gl.entertainmentLocations.Count > 0)
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
        entertainment += 3f * Time.deltaTime;

        if (entertainment > satisfiedEntertainment)
        {
            state = State.Wander;
        }
    }

    public void Sleep()
    {
        awake = false;
        tiredness += 1f * Time.deltaTime;

        if (tiredness > satisfiedTiredness)
        {
            state = State.Wander;
        }
    }

    public void Work()
    {
        // finished work
        if (jobFinishTime <= ts.hour)
        {
            GlobalValues.money = GlobalValues.money + ((jobFinishTime - jobStartTime) * paidPerHour);
            state = State.Wander;
            canMakeDecision = true;
        }

        // lunch break
        if (ts.hour == jobStartTime + ((jobFinishTime - jobStartTime) / 2))
        {
            thirst += 30;
            hunger += 30;
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
        // 2.75f take away brackets - for some reason broke game loop
        tiredness -= (decayValue / 1.25f) * Time.deltaTime;
        if (tiredness < 0)
        {
            tiredness = 0;
        }
    }

    public void DeteriateHunger()
    {
        hunger -= (decayValue / 1.5f) * Time.deltaTime;
    }

    public void DeteriateThirst()
    {
        thirst -= decayValue * Time.deltaTime;
    }

    public void DeteriateEntertainment()
    {
        entertainment -= decayValue * Time.deltaTime;
        if (entertainment < 0)
        {
            entertainment = 0;
        }
    }

    public void AgePerson()
    {
        if (getAgeDay)
        {
            agedDay = ts.day;
            getAgeDay = false;
        }
        if (ts.day == agedDay + 14)
        {
            age = age + 1;
            getAgeDay = true;
        }


    }

    public void CheckDeath()
    {
        if (hunger < 0 || thirst < 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);

        // will cause issues with selected AI
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

        foreach (GameObject obj in gameObject)
        {
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, obj.transform.position);
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                trns = obj.transform;
                destination = trns;
            }
        }
        return trns;
    }

    IEnumerator FoodDelay(FoodBites food)
    {
        yield return new WaitForSeconds(1);
        food.bites -= 1;
        hunger += food.foodValue;
        StopCoroutine(FoodDelay(food));
    }
}
