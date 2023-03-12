using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasePerson : MonoBehaviour
{
    [Header("Needs")]
    public float hunger;
    public float thirst;
    public float tiredness;
    public float entertainment;
    public float social;

    [Space(10)]
    [Header("Max Needs")]
    public float maxHunger;
    public float maxThirst;
    public float maxTiredness;
    public float maxEntertained;
    public float maxSocial;

    [Space(10)]
    [Header("Satisfied Values")]
    public float satisfiedHunger;
    public float satisfiedThirst;
    public float satisfiedTiredness;
    public float satisfiedEntertainment;
    public float satisfiedSocial;

    [Space(10)]
    [Header("Critical Values")]
    public float criticalHunger;
    public float criticalThirst;
    public float criticalTiredness;
    public float criticalEntertainment;
    public float criticalSocial;

    [Space(10)]
    [Header("InteractionTime")]
    public float hungerInteractionTime;
    public float thirstInteractionTime;
    public float sleepInteractionTime;
    public float entertainmentInteractionTime;
    public float socialInteractionTime;

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
    private bool hadLunchBreak = false;


    [Space(10)]
    [Header("Social")]
    public List<GameObject> friends;
    public List<float> friendScore;
    public GameObject socializingWith;

    public enum State { Idle, Wander, Eating, Drinking, Sleeping, Entertaining, Work, GoBeSocial, Socialize };
    public State state;
    public State previousState;
    public List<State> queueState;
    public bool[] addedToQueue;
    protected bool performing;
    private AddToTaskList addToTaskList;
    public enum Gender { Male, Female };
    public Gender gender;

    public enum Age { Child, Teenager, Young_Adult, Adult, Elder }
    public Age age;

    public Transform destination;

    protected float decayValue = 0.25f;

    protected float wanderDelay = 0f;
    protected float wanderEvery = 5f;

    protected float eatTimer = 0f;
    protected float drinkTimer = 0f;
    protected float entertainmentTimer = 0f;
    protected float sleepTimer = 0f;

    protected bool awake = true;
    protected bool decisionMade = false;

    protected bool getAgeDay = true;
    protected int agedDay = 0;
    [SerializeField] protected bool canMakeDecision = true;


    //public Queue queueTasks;
    public virtual void Start()
    {
        //queueTasks = new Queue();
        rb = GetComponent<Rigidbody>();
        navigation = GetComponent<NavMeshAgent>();

        state = State.Wander;
        previousState = State.Wander;
        gender = Random.value < 0.5f ? Gender.Male : Gender.Female;
        employment = Employment.Unemployed;
        age = Age.Young_Adult;

        hunger = maxHunger;
        thirst = maxThirst;
        tiredness = maxTiredness;
        entertainment = maxEntertained;
        social = maxSocial;

        gameManager = GameObject.FindGameObjectWithTag("GameController");
        gl = gameManager.GetComponent<GlobalLocations>();
        ts = gameManager.GetComponent<TimeScaler>();
        addToTaskList = GameObject.FindObjectOfType<AddToTaskList>();

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("AI"))
        {
            friends.Add(obj);
            friendScore.Add(GameObject.FindGameObjectsWithTag("AI").Length);
            friends.Remove(gameObject);
        }
        friendScore.RemoveAt(0);
        for (int i = 0; i < friends.Count; i++)
        {
            friendScore[i] = 0;
        }


        addedToQueue = new bool[6];

        queueState.Add(State.Wander);

        //queueTasks.Enqueue(State.Wander);
    }


    public virtual void Update()
    {
        switch (state)
        {
            case State.Idle:
                Idle();
                break;
            case State.Wander:
                Wander();
                break;
            case State.Eating:
                GoTo(gl.unocFoodLocations, gl.ocFoodLocations);
                break;
            case State.Drinking:
                GoTo(gl.unocDrinkingLocations, gl.ocDrinkingLocations);
                break;
            case State.Sleeping:
                GoTo(gl.unocSleepLocations, gl.ocSleepLocations);
                break;
            case State.Entertaining:
                GoTo(gl.unocEntertainmentLocations, gl.ocEntertainmentLocations);
                break;
            case State.Work:
                Work();
                break;
            case State.GoBeSocial:
                FindDesiredFriend();
                break;
            case State.Socialize:
                Socialize();
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
        DeteriateSocial();

        CheckDeath();

        AgePerson();

        if (!decisionMade)
        {
            state = queueState[0];
        }

    }
    public State GetTasks(IEnumerable queue)
    {
        foreach (State q_state in queue)
        {
            state = q_state;
        }
        return state;
    }

    public void Decision()
    {
        bool currentlyEating = state == State.Eating;
        bool currentlyDrinking = state == State.Drinking;
        bool currentlySleeping = state == State.Sleeping;
        bool currentlyEntertaining = state == State.Entertaining;
        bool currentlyWorking = state == State.Work;
        bool currentlySocializing = state == State.Socialize;



        if (jobStartTime == ts.hour && !currentlyWorking || ts.hour == jobStartTime + 1)
        {
            if (employment == Employment.Employed)
            {
                if (!addedToQueue[1])
                {
                    queueState.Add(State.Work);
                    addedToQueue[1] = true;
                }


            }
        }
        if (satisfiedHunger > hunger && !currentlyEating || hunger < criticalHunger)
        {
            if (gl.unocFoodLocations.Count > 0)
            {
                if (!addedToQueue[2])
                {
                    queueState.Add(State.Eating);
                    addedToQueue[2] = true;
                }

            }
        }
        if (satisfiedThirst > thirst && !currentlyDrinking || thirst < criticalThirst)
        {
            if (gl.unocDrinkingLocations.Count > 0)
            {
                if (!addedToQueue[3])
                {
                    queueState.Add(State.Drinking);
                    addedToQueue[3] = true;
                }

            }
        }
        if (satisfiedEntertainment > entertainment && !currentlyEntertaining)
        {
            if (gl.unocEntertainmentLocations.Count > 0)
            {
                if (!addedToQueue[4])
                {
                    queueState.Add(State.Entertaining);
                    addedToQueue[4] = true;
                }

            }
        }
        if (criticalTiredness > tiredness && !currentlySleeping)
        {
            if (gl.unocSleepLocations.Count > 0)
            {
                if (!addedToQueue[0])
                {
                    queueState.Add(State.Sleeping);
                    addedToQueue[0] = true;
                }
            }
        }
        if (criticalSocial > social && !currentlySocializing)
        {
            if (!addedToQueue[5])
            {
                queueState.Add(State.GoBeSocial);
                addedToQueue[5] = true;
            }
        }
    }

    public void GoTo(List<GameObject> uncoLocation, List<GameObject> ocLocation)
    {
        if (uncoLocation.Count == 0 && GetClosest(ocLocation).GetComponent<IsOccupied>().occupiedSlot != gameObject && !queueState.Contains(State.Wander))
        {
            queueState.Insert(0, State.Wander);
        }


        if (uncoLocation.Count > 0)
        {
            if (GetClosest(uncoLocation).GetComponent<IsOccupied>().occupiedSlot == null)
            {
                GetClosest(uncoLocation).GetComponent<IsOccupied>().occupiedSlot = gameObject;
                GetClosest(uncoLocation).GetComponent<IsOccupied>().AddOcuppied();
            }
        }



        if (GetClosest(ocLocation).GetComponent<IsOccupied>().occupiedSlot == gameObject)
        {
            //Debug.Log(Vector3.Distance(transform.position, navigation.destination));
            navigation.destination = GetClosest(ocLocation).position;
            if (Vector3.Distance(transform.position, navigation.destination) <= 1.1f)
            {
                switch (state)
                {
                    case State.Eating:
                        Eat(GetClosest(ocLocation).GetComponent<FoodBites>(), ocLocation);
                        break;
                    case State.Drinking:
                        Drink(ocLocation);
                        break;
                    case State.Entertaining:
                        Entertain(ocLocation);
                        break;
                    case State.Sleeping:
                        Sleep(ocLocation);
                        break;
                }
            }
        }

    }

    public void Eat(FoodBites food, List<GameObject> location)
    {
        eatTimer += Time.deltaTime;
        if (eatTimer < hungerInteractionTime)
        {
            hunger += 9f * Time.deltaTime;
        }
        else
        {
            eatTimer = 0;
            decisionMade = false;
            addedToQueue[2] = false;
            GetClosest(location).GetComponent<IsOccupied>().RemoveOccupied();
            if (queueState.Count > 1)
            {
                queueState.RemoveAt(0);
            }
            else
            {
                queueState.Add(State.Wander);
                queueState.RemoveAt(0);
            }
        }

        if (hunger > maxHunger)
        {
            hunger = maxHunger;
        }
    }

    public void Drink(List<GameObject> location)
    {
        drinkTimer += Time.deltaTime;
        if (drinkTimer < thirstInteractionTime)
        {
            thirst += 12f * Time.deltaTime;
        }
        else
        {
            drinkTimer = 0f;
            decisionMade = false;
            addedToQueue[3] = false;
            GetClosest(location).GetComponent<IsOccupied>().RemoveOccupied();
            if (queueState.Count > 1)
            {
                queueState.RemoveAt(0);
            }
            else
            {
                queueState.Add(State.Wander);
                queueState.RemoveAt(0);
            }
        }

        if (thirst > maxThirst)
        {
            thirst = maxThirst;
        }
    }

    public void Entertain(List<GameObject> location)
    {
        entertainmentTimer += Time.deltaTime;
        if (entertainmentTimer < entertainmentInteractionTime)
        {
            entertainment += 2f * Time.deltaTime;
        }
        else
        {
            entertainmentTimer = 0f;
            decisionMade = false;
            addedToQueue[4] = false;
            GetClosest(location).GetComponent<IsOccupied>().RemoveOccupied();
            if (queueState.Count > 1)
            {
                queueState.RemoveAt(0);
            }
            else
            {
                queueState.Add(State.Wander);
                queueState.RemoveAt(0);
            }
        }

        if (entertainment > maxEntertained)
        {
            entertainment = maxEntertained;
        }
    }

    public void Sleep(List<GameObject> location)
    {
        awake = false;
        if (tiredness < sleepInteractionTime)
        {
            tiredness += 1f * Time.deltaTime;
        }
        else
        {
            sleepTimer = 0f;
            decisionMade = false;
            addedToQueue[0] = false;
            GetClosest(location).GetComponent<IsOccupied>().RemoveOccupied();
            if (queueState.Count > 1)
            {
                queueState.RemoveAt(0);
            }
            else
            {
                queueState.Add(State.Wander);
                queueState.RemoveAt(0);
            }
        }

        if (tiredness > maxTiredness)
        {
            tiredness = maxTiredness;
        }
    }

    public void Work()
    {
        navigation.destination = GetClosest(gl.doorLocation).position;
        if (Vector3.Distance(transform.position, navigation.destination) <= 1.05f)
        {
            if (ts.hour < jobFinishTime)
            {
                GetComponent<Renderer>().enabled = false;
            }

            // finish
            if (jobFinishTime <= ts.hour)
            {
                GetComponent<Renderer>().enabled = true;
                GlobalValues.money = GlobalValues.money + ((jobFinishTime - jobStartTime) * paidPerHour);
                decisionMade = false;
                addedToQueue[1] = false;
                hadLunchBreak = false;
                if (queueState.Count > 1)
                {
                    queueState.RemoveAt(0);
                }
                else
                {
                    queueState.Add(State.Wander);
                    queueState.RemoveAt(0);
                }
            }

            // lunch break
            if (ts.hour == jobStartTime + ((jobFinishTime - jobStartTime) / 2) && !hadLunchBreak)
            {
                thirst += 30;
                hunger += 30;
                hadLunchBreak = true;
            }
        }
        if (thirst > maxThirst)
        {
            thirst = maxThirst;
        }
        if (hunger > maxHunger)
        {
            hunger = maxHunger;
        }

    }

    public void Socialize()
    {
        social += 3 * Time.deltaTime;

        if (social > maxSocial)
        {
            social = maxSocial;
        }

    }

    public void FindDesiredFriend()
    {
        socializingWith = friends[Random.RandomRange(0, friends.Count)];
        navigation.destination = socializingWith.transform.position;

        //Debug.Log(Vector3.Distance(transform.position, navigation.destination));

        if (Vector3.Distance(transform.position, navigation.destination) <= 2f)
        {
            navigation.destination = transform.position;
            gameManager.GetComponent<SocialManager>().SpawnSocialZone();
        }
    }

    public void Wander()
    {
        wanderDelay += Time.deltaTime;
        if (wanderDelay > wanderEvery)
        {
            navigation.SetDestination(RandomSphere(gameObject.transform.position, 20));
            if (navigation.pathStatus == NavMeshPathStatus.PathComplete)
            {
                wanderDelay = 0;
                if (queueState.Count > 1)
                {
                    queueState.RemoveAt(0);
                }
                /*else
                {
                    if (Random.value > 0.5)
                    {
                        // could be interesting to include personalities here. Introvert would rather entertain while extrovert would rather socialise 
                        if (Random.value > 0.2)
                        {
                            queueState.Add(State.Entertaining);
                        }
                        else
                        {
                            queueState.Add(State.GoBeSocial); 
                        }

                    }
                else
                {
                    waderDelay = 0;
                }

                }*/
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

    public void DeteriateSocial()
    {
        social -= decayValue * Time.deltaTime;
        if (social < 0)
        {
            social = 0;
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
