using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaController : MonoBehaviour
{
    public Animator animator;
    public GameObject Player;
    public PlatePlacer platePlacer;
    private Vector3 originalPosition;

    private float minLookTimer = 3f;
    private float maxLookTimer = 5f;
    
    private float minWorkTimer = 5f;
    private float maxWorkTimer = 7f;


    public float timeToHit = 5f;

    private GrandmaState _state = GrandmaState.NotLooking;

    public GrandmaState State
    {
        get => _state;
        set
        {
            _state = value;
            animator.SetBool("IsLooking", _state == GrandmaState.Looking);
            
            if (_state == GrandmaState.HandlingPlates)
            {
                // Call a coroutine or method to handle the plates
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
        Player.GetComponent<PlayerController>().PlayerHurt(); // Call PlayerHurt method
    }
    
    private IEnumerator HandlePlatesRoutine()
    {
        // Move to plates, handle them, then return
        MoveToPlates();
        yield return new WaitForSeconds(2); // Adjust the time as needed
        ReturnToOriginalAndHandlePlates();
        yield return new WaitForSeconds(1); // Adjust the time as needed

        // After handling plates, go back to working or looking state
        State = GrandmaState.NotLooking;
    }
    
    public void HandlePlatesState()
    {
        State = GrandmaState.HandlingPlates;
        MoveToPlates();
    }
    
    public void MoveToPlates()
    {
        Debug.Log("Moving to plates");
        // Move the grandmother to the position behind the clean plates
        Vector3 platesPosition = platePlacer.spawnPointCleanPlate.transform.position; // Adjust as needed
        transform.position = new Vector3(platesPosition.x, platesPosition.y, transform.position.z);
    }

    public void ReturnToOriginalAndHandlePlates()
    {
        Debug.Log("Returning to original position and handling plates");

        // Move the grandmother back to her original position
        transform.position = originalPosition;

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