using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlatePlacer : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject platePrefab;

    public int numOfPlates = 0;

    public float plateHealth = 3;

    public List<GameObject> _plates = new List<GameObject>();
    // Start is called before the first frame update
    
    
    public void PlacePlate()
    {
        numOfPlates++;
        GameObject newPlate = Instantiate(platePrefab, spawnPoint.transform.position, Quaternion.identity);
        newPlate.transform.parent = transform;
        _plates.Add(newPlate);
        spawnPoint.transform.position += Vector3.up;
        Debug.Log("Number of Plates: " + numOfPlates);

        if (spawnPoint.transform.position.y >= 5)
        {
            PlateCrashing();
        }
    }

    public void RemovePlate()
    {
        numOfPlates--;
        if (_plates.Count > 0)
        {
            Destroy(_plates[0]);
            _plates.RemoveAt(0);
            spawnPoint.transform.position += Vector3.down;
            foreach (GameObject plate in _plates)
            {
                plate.transform.position += Vector3.down; // Move the plate down (adjust the Vector3 as needed)
                plateHealth = 3;
            }

        }
    }
    
    public void PlateCrashing()
    {
        Debug.Log("All the plates are crashing!!!!");
        //SceneManager.LoadScene(sceneToLoad);
    }
}
