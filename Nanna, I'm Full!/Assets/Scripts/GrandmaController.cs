using System.Collections;
using UnityEngine;

public class GrandmaController : MonoBehaviour
{
    public Animator animator;
    public GameObject player;
    public PlatePlacer platePlacer;
    private Vector3 originalPosition;
    public GameObject targetPositionObject;

    
    private float minLookTimer = 3f;
    private float maxLookTimer = 5f;
    
    private float minWorkTimer = 5f;
    private float maxWorkTimer = 7f;
    
    public float difficultyIncrease = 0.15f; // The amount by which the timers are decreased
    public float minTimerLimit = 1f; // Minimum limit for timers to prevent them from becoming too short
    public float maxTimerLimit = 3f;

    public float timeToHit = 5f;

    private GrandmaState state = GrandmaState.NotLooking;

    public GrandmaState State
    {
        get => state;
        set
        {
            state = value;
            animator.SetBool("IsLooking", state == GrandmaState.Looking);
            
            if (state == GrandmaState.HandlingPlates)
            {
                // Call a coroutine or method to handle the plates
                animator.SetBool("IsGetPlates", true);
                StartCoroutine(HandlePlatesRoutine());
            }
        }
    }

    private Coroutine lookingCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        originalPosition = transform.position;
        State = GrandmaState.NotLooking;
        StartCoroutine(GrandmaWorking());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartHitCheck();
        }
        
    }
    
    IEnumerator GrandmaLooking()
    {
        float lookInterval = Random.Range(minLookTimer, maxLookTimer);
        State = GrandmaState.Looking;
        platePlacer.Invoke("PlacePlate", 1);
        StartHitCheck();
        yield return new WaitForSeconds(lookInterval);
        
        // Decrease the timers, ensuring they don't go below the minimum limit
        minWorkTimer = Mathf.Max(minWorkTimer - difficultyIncrease, minTimerLimit);
        maxWorkTimer = Mathf.Max(maxWorkTimer - difficultyIncrease, maxTimerLimit);
        
        Debug.Log("Min Timer is: " + minWorkTimer);
        Debug.Log("Max Timer is: " + maxWorkTimer);
        StartCoroutine(GrandmaWorking());
    }
    
    IEnumerator GrandmaWorking()
    {
        float lookInterval = Random.Range(minWorkTimer, maxWorkTimer);
        State = GrandmaState.NotLooking;
        yield return new WaitForSeconds(lookInterval);
        StartCoroutine(GrandmaLooking());
    }

    IEnumerator CheckForHit()
    {
        yield return new WaitForSeconds(timeToHit);
        if (State == GrandmaState.NotLooking)
        {
            yield break;
        }
        
        if (!Input.GetKey(KeyCode.Space))
        {
            yield break; // Exit the coroutine if the space key is released
        }
        animator.SetBool("IsGoingToHit", true);
        player.GetComponent<PlayerController>().PlayerHurt(); // Call PlayerHurt method
        yield return new WaitForSeconds(2);
        animator.SetBool("IsGoingToHit", false);
    }
    
    private IEnumerator HandlePlatesRoutine()
    {
        // Move to plates, handle them, then return
        MoveToPlates();
        yield return new WaitForSeconds(2); // Adjust the time as needed
        ReturnToOriginalAndHandlePlates();
        yield return new WaitForSeconds(1); // Adjust the time as needed
        StartCoroutine(GrandmaWorking());
        // After handling plates, go back to working or looking state
        animator.SetBool("IsGetPlates", false);
        State = GrandmaState.NotLooking;
    }
    
    public void HandlePlatesState()
    {
        State = GrandmaState.HandlingPlates;
    }
    
    public void MoveToPlates()
    {
        State = GrandmaState.NotLooking;
        Debug.Log("Moving to plates");
        // Move the grandmother to the position behind the clean plates
        if (targetPositionObject != null)
        {
            // Move the grandmother to the position of the target object
            transform.position = targetPositionObject.transform.position;
        }
        else
        {
            Debug.LogError("Target position object is not set");
        }
    }

    public void ReturnToOriginalAndHandlePlates()
    {
        Debug.Log("Returning to original position and handling plates");

        // Move the grandmother back to her original position
        transform.position = originalPosition;
        animator.SetBool("IsGetPlates", false);

        // Call a method from PlatePlacer to handle clean plates
        platePlacer.HandleCleanPlates(); 
    }


    private void StartHitCheck()
    {
        if (lookingCoroutine != null)
        {
            StopCoroutine(lookingCoroutine);
        }

        lookingCoroutine = StartCoroutine(CheckForHit());
    }
}