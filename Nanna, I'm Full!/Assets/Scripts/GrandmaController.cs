using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaController : MonoBehaviour
{
    public Animator animator;

    public GameObject Player;
    
    public PlatePlacer platePlacer;

    private float minLookTimer = 1f;
    private float maxLookTimer = 3f;
    
    private float minWorkTimer = 3f;
    private float maxWorkTimer = 5f;


    public float timeToHit = 5f;

    private GrandmaState _state = GrandmaState.NotLooking;

    public GrandmaState State
    {
        get => _state;
        set
        {
            _state = value;
            animator.SetBool("IsLooking", _state == GrandmaState.Looking);
        }
    }

    private Coroutine lookingCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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

    private void StartHitCheck()
    {
        if (lookingCoroutine != null)
        {
            StopCoroutine(lookingCoroutine);
        }

        lookingCoroutine = StartCoroutine(CheckForHit());
    }
}