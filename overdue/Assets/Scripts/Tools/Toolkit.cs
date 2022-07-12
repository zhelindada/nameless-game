using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Toolkit
{
    public static List<Entity> FindAllEntities() {
        Entity[] a = GameObject.FindObjectsOfType<Entity>();
        List<Entity> entities = new List<Entity>();
        foreach (Entity e in a) {
            entities.Add(e);
        }
        return entities;
    }

    public static Dictionary<Entity, float> GetEntityDistanceMap(Vector3 fromPos) {

        List<Entity> loe = FindAllEntities();
        Dictionary<Entity, float> doe = new Dictionary<Entity, float>();

        for (int i = 0; i < loe.Count; i++)
        {
            Entity e = loe[i];
            float f = (loe[i].transform.position - fromPos).magnitude;
            doe.Add(e, f);
        }

        return doe;
    }

    public static Entity FindClosestEnemy(Vector3 fromPos) {
        // TODO check
        Dictionary<Entity, float> doe = GetEntityDistanceMap(fromPos);
        Entity loweste = null;
        float lowest = float.MaxValue;
        foreach (KeyValuePair<Entity, float> kfp in doe)
        {
            if(kfp.Key.name.Equals("Player"))
            if (kfp.Value < lowest) {
                lowest = kfp.Value;
                loweste = kfp.Key;
            }
        }
        return loweste;
    }
    public static Entity FindRandomEnemy()
    {
        //TODO check
        List<Entity> l = FindAllEntities();
        int a;
        do
        {
            a = Random.Range(0, l.Count);
        } while (l[a].name.Equals("Player"));

        return l[a];
    }
}
