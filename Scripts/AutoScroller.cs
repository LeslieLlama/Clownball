using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScroller : MonoBehaviour
{
    [SerializeField] private List<GameObject> holePrefabs = new List<GameObject>();
    private List<GameObject> objectsToScroll = new List<GameObject>();
    [SerializeField] private GameObject GemPrefab;
    [Range(0.0f, 10.0f)]
    public float scrollSpeed;
    private bool isScrolling;
    private float holeSpawnTimer;
    private float gemSpawnTimer = 4.5f;

    private void OnEnable()
    {
        Collector.OnHoleDeath += DestroyObjectFromScroll;
        Collector.OnGemDeath += DestroyObjectFromScroll;
        Coin.OnCoinPickup += DestroyObjectFromScroll;
        Clownball.OnPlayerDeath += StopScrolling;
    }
    private void OnDisable()
    {
        Collector.OnHoleDeath -= DestroyObjectFromScroll;
        Collector.OnGemDeath += DestroyObjectFromScroll;
        Coin.OnCoinPickup -= DestroyObjectFromScroll;
        Clownball.OnPlayerDeath -= StopScrolling;
    }

    private void Start()
    {
        isScrolling = true;
    }

    // Update is called once per frame
    void Update()
    {
        holeSpawnTimer -= Time.deltaTime;
        gemSpawnTimer -= Time.deltaTime;

        if (holeSpawnTimer <= 0)
        {
            SpawnHole();
            holeSpawnTimer += Random.Range(5,10);
        }
        if(gemSpawnTimer <= 0)
        {
            SpawnGem();
            gemSpawnTimer += Random.Range(8, 14);
        }

        ScrollDownwards();
    }

    void SpawnHole()
    {
        float randomXPosition = Random.Range(-3, 3);
        int randomHole = Random.Range(0, holePrefabs.Count);

        GameObject holeToSpawn = Instantiate(holePrefabs[randomHole]);
        holeToSpawn.transform.position = new Vector3(randomXPosition, 7, 0);
        objectsToScroll.Add(holeToSpawn);
    }

    void DestroyObjectFromScroll(GameObject objectToRemove)
    {
        objectsToScroll.Remove(objectToRemove);
        Destroy(objectToRemove);
    }

    void StopScrolling()
    {
        isScrolling = false;
    }

    void SpawnGem()
    {
        float randomXPosition = Random.Range(-2, 2);

        GameObject gemToSpawn = Instantiate(GemPrefab);
        gemToSpawn.transform.position = new Vector3(randomXPosition, 7, 0);
        objectsToScroll.Add(gemToSpawn);
    }

    void ScrollDownwards()
    {
        if (objectsToScroll.Count <= 0 || isScrolling == false)
        {
            return;
        }
        foreach (GameObject item in objectsToScroll)
        {
            item.transform.position += new Vector3(0, -scrollSpeed, 0) * Time.deltaTime;
        }
    }



}
