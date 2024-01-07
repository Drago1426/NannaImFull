using System;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlatePlacer : MonoBehaviour
{
    public GameObject spawnPointFood;
    public GameObject spawnPointCleanPlate;
    public GameObject platePrefab;
    public GameObject cleanPlatePrefab;
    private Vector3 originalCleanPlateSpawnPointPosition;

    
    public GrandmaController grandmaController; // Add this public variable


    public int numOfPlates = 0;
    public int numOfCleanPlates = 0;

    public float plateHealth = 3;

    public List<GameObject> _plates = new List<GameObject>();
    public List<GameObject> _cleanPlates = new List<GameObject>();
    // Start is called before the first frame update

    public void Start()
    {
        plateHealth = 3;
        originalCleanPlateSpawnPointPosition = spawnPointCleanPlate.transform.position;

    }

    public void PlacePlate()
    {
        numOfPlates++;
        GameObject newPlate = Instantiate(platePrefab, spawnPointFood.transform.position, Quaternion.identity);
        newPlate.transform.parent = transform;
        _plates.Add(newPlate);
        spawnPointFood.transform.position += Vector3.up;
        Debug.Log("Number of Plates: " + numOfPlates);

        if (spawnPointFood.transform.position.y >= 5)
        {
            PlateCrashing();
        }
    }
    
    public void PlaceCleanPlate()
    {
        numOfCleanPlates++;
        GameObject newCleanPlate = Instantiate(cleanPlatePrefab, spawnPointCleanPlate.transform.position, Quaternion.identity);
        newCleanPlate.transform.parent = transform;
        _cleanPlates.Add(newCleanPlate);
        spawnPointCleanPlate.transform.position += Vector3.up;
        Debug.Log("Number of Plates: " + numOfCleanPlates);
        
        if (numOfCleanPlates >= 5)
        {
            if (grandmaController != null)
            {
                Debug.Log("It works!");
                grandmaController.HandlePlatesState();
                //Invoke("grandmaController.ReturnToOriginalAndHandlePlates", 2); // Wait 2 seconds then call the method
            }
        }
    }

    public void RemovePlate()
    {
        plateHealth = 3;
        numOfPlates--;
        if (_plates.Count > 0)
        {
            Destroy(_plates[0]);
            _plates.RemoveAt(0);
            spawnPointFood.transform.position += Vector3.down;
            foreach (GameObject plate in _plates)
            {
                plate.transform.position += Vector3.down; // Move the plate down (adjust the Vector3 as needed)
            }

        }
    }
    
    public void HandleCleanPlates()
    {
        foreach (GameObject cleanPlate in _cleanPlates)
        {
            Destroy(cleanPlate); // Remove the clean plate
        }
        _cleanPlates.Clear();
        numOfCleanPlates = 0;
        spawnPointCleanPlate.transform.position = originalCleanPlateSpawnPointPosition;
    }
    
    public void PlateCrashing()
    {
        Debug.Log("All the plates are crashing!!!!");
        //SceneManager.LoadScene(sceneToLoad);
    }
}
