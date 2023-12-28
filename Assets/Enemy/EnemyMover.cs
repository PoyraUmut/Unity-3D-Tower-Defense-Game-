using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<WayPoint> path = new List<WayPoint>();
    [SerializeField] [Range(0f,5f)]  float speed = 1f;

    Enemy enemy;
    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());

    }

    void Start(){
        enemy = GetComponent<Enemy>();
    }

    void FindPath(){
    path.Clear();
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Path");
        foreach(GameObject tile in tiles) 
        {
            WayPoint waypoint = tile.GetComponent<WayPoint>();

            if(waypoint != null)
            {
                path.Add(waypoint);
            }

        }
    

    
    
}

    void ReturnToStart(){
    if(path.Count > 0){
        transform.position = path[0].transform.position;
    } else {
        Debug.LogError("Path listesi boş. Bir yol belirtmelisin.");
    }
}

    void FinishPath(){
        enemy.StealdGold();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath(){

        if(path.Count == 0){
        Debug.LogError("Path listesi boş. Bir yol belirtmelisin.");
        yield break; // Fonksiyonu hemen sonlandır.
    }
    
        foreach(WayPoint wayPoint in path){
            Vector3 startPosition = transform.position;
            Vector3 endPosition = wayPoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while(travelPercent<1f){
                travelPercent+=Time.deltaTime*speed;
                transform.position = Vector3.Lerp(startPosition, endPosition,travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }
}
