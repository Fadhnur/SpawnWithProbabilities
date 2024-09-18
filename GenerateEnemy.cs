// using System.Collections;
// using System.Collections.Generic;
// using System.Numerics;
using UnityEngine;

[System.Serializable]
public class Enemy{
    public string Name;
    public GameObject Prefab;   //Tempat menyimpan prefab
    [Range (0f, 100f)] public float Chance = 100f;  //Range slider antara 0 - 100, dengan nilai awal 100
    [HideInInspector] public double _weight;
}

public class GenerateEnemy : MonoBehaviour
{
    [SerializeField] private Enemy[] enemies;   //Menyimpan list dari banyaknya Enemy yang akan digunakan
    private double accumulatedWeights;
    private System.Random rand = new System.Random();

    private void Awake() {
        CalculateWeights();
    }

    void Start()
    {
        for(int i = 0; i < 200; i++){
            SpawnRandomEnemy(new Vector2 (Random.Range(-8f, 8f), Random.Range(-5f, 5f)));   //Random.Range digunakan untuk mengatur seberapa panjang/lebar posisi yang digunakan untuk spawn
        }
    }

    private void SpawnRandomEnemy(Vector2 pos){
        Enemy randomEnemy = enemies [GetRandomEnemyIndex()];
        Instantiate(randomEnemy.Prefab, pos, Quaternion.identity, transform);   //Instantiate digunakan untuk "menduplikasi" prefab yang telah dimasukkan sesuai dengan posisi yang sudah diatur
    }
    
    private int GetRandomEnemyIndex(){
        double r = rand.NextDouble() * accumulatedWeights;

        for(int i = 0; i < enemies.Length; i++){
            if(enemies[i]._weight >= r){
                return i;
            }
        }
        return 0;
    }

    private void CalculateWeights(){
        accumulatedWeights = 0f;
        foreach(Enemy enemy in enemies){
            accumulatedWeights += enemy.Chance;
            enemy._weight = accumulatedWeights;
        }
    }
}
