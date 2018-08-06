using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class FindAlliesAndEnemies : MonoBehaviour
{
    public BaseAgent owner;
    static int numberOfPlayers=3;
    List<BaseAgent>[] foundAgents;
    int enemies;
    int allies;
    int neutral;
    //Lista opisująca stosunki wobec innych graczy 
    // enemyAndAllyFractions[0]=ENEMY oznacza, że gracz 0 jest wrogiem
    public int[] enemyAndAllyFractions;
    static int NEUTRAL = 0;
    static int ENEMY = 1;
    static int ALLY = 2;
    void Awake()
    {
        foundAgents = new List<BaseAgent>[numberOfPlayers];
        for(int i = 0 ; i < numberOfPlayers; ++i)
        {
            foundAgents[i] = new List<BaseAgent>();
        }
    }
    void Start()
    {

    }
    void Update()
    {
        enemies = NumberOfEnemies();
        allies = NumberOfAllies();
        neutral = NumberOfNeutral();
        removeNULLs();
    }
    public BaseAgent NearestEnemy()
    {
        return NearestUnitOfType(ENEMY);
    }
    public BaseAgent NearesAlly()
    {
        return NearestUnitOfType(ALLY);
    }
    public BaseAgent NearestNeutral()
    {
        return NearestUnitOfType(NEUTRAL);
    }
    public BaseAgent NearestUnitOfType(int type)
    {
        float minDistance = 999999;
        BaseAgent nearest=null;
        for (int i = 0; i < numberOfPlayers; ++i)
        {
            if (enemyAndAllyFractions[i] == type)
            {
                for (int j= 0; j< foundAgents[i].Count;++j)
                {
                    BaseAgent ba = foundAgents[i][j];
                    if(ba==null)
                    {
                        foundAgents[i].Remove(ba);
                        continue;
                    }
                    float sqMag = (ba.transform.position - transform.position).sqrMagnitude;
                    if (sqMag < minDistance)
                    {
                        nearest = ba; 
                    }
                }
            }
        }
        return nearest;
    }
    public int NumberOfEnemies()
    {
        return NumberOfUnitsOfType(ENEMY);
    }
    public int NumberOfAllies()
    {
        return NumberOfUnitsOfType(ALLY);
    }
    public int NumberOfNeutral()
    {
        return NumberOfUnitsOfType(NEUTRAL);
    }
    int NumberOfUnitsOfType(int type)//type is NEUTRAL or ENEMY or ALLY
    {
        int counter = 0;
        for(int i=0;i<numberOfPlayers;++i)
        {
            if (enemyAndAllyFractions[i] == type)
                counter += foundAgents[i].Count;
        }
        return counter;
    }
    void OnCollisionEnter(Collision collision)
    {
        GameObject cg = collision.gameObject;
        if (cg == owner.gameObject)
        {
            return;
        }
        BaseAgent ba= cg.GetComponent<BaseAgent>();
        if(ba)
        {
            foundAgents[ba.fraction].Add(ba);
        }
    }
    void OnCollisionExit(Collision collisionInfo)
    {
        GameObject cg=collisionInfo.gameObject;
        BaseAgent ba = cg.GetComponent<BaseAgent>();
        if(ba)
        {
            foundAgents[ba.fraction].Remove(ba);
        }
    }
    void removeNULLs()
    {
        for (int i = 0; i < numberOfPlayers; ++i)
        {
            for (int j = 0; j < foundAgents[i].Count; ++j)
            {
                BaseAgent ba = foundAgents[i][j];
                if (ba == null)
                {
                    foundAgents[i].Remove(ba);
                    continue;
                }
            }
        }
    }
}
