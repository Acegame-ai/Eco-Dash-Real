// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;

// public class RoadSpawn : MonoBehaviour

// {
//     public List<GameObject> roads;
//     private bool isSpawning = false;
//     private float groundSpawnDistance;
//     [SerializeField] private float roadLength;
//     public static RoadSpawn instance;

//     private void Awake()
//     {
//         instance = this;
//     }

//     private void Start()
//     {
     
//     }
//     public void SpawnRoad()
//     {
//        roads = roads.Skip(1).Concat(roads.Take(1)).ToList();
//         roads[roads.Count - 1].transform.position = roads[1].transform.position + new Vector3(0, 0, roadLength);
        
        
        
        
//     }
//  }
