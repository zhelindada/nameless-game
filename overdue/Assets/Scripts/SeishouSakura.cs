using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeishouSakura : MonoBehaviour
{

    // static variables
    public static int maxNumSSS = 3;
    public static int currentNum = 0;
    private static int nextReuseCounter = 0;
    private static GameObject[] allSSS = new GameObject[maxNumSSS];
    private static GameObject ssSakura;

    [SerializeField] private DamageCollider _damage;

    public float despawnTimer;
    public float despawnTime;

    public float attackTimer;
    public float attackCooldown;


    private void OnEnable()
    {
        ResetVariables();
    }

    private void Update()
    {
        despawnTimer -= Time.deltaTime;
        if (despawnTimer < 0) {
            TurnOff();
        }
        attackTimer -= Time.deltaTime;
        if (attackTimer < 0)
        {
            Attack(Toolkit.FindRandomEnemy().transform.position);
            attackTimer = attackCooldown;
        }
    }

    private void ResetVariables() {
        despawnTimer = despawnTime;
        attackTimer = 1;
    }

    private void Attack(Vector3 target) {
        GameObject newS = Instantiate(_damage.gameObject);
        newS.transform.position = target;
    }

    private void TurnOff() {
        currentNum--;
        gameObject.SetActive(false);
    }

    private static void CountUp()
    {
        nextReuseCounter++;
        if (nextReuseCounter >= maxNumSSS) {
            nextReuseCounter -= maxNumSSS;
        }
        currentNum++;
    }

    public static void MakeNewSSS(Transform parent, Vector3 position) {

        if (allSSS == null)
            allSSS = new GameObject[maxNumSSS];
        
        GameObject go;
        if (allSSS[nextReuseCounter] == null)
        {
            go = Instantiate(ssSakura, parent);
            allSSS[nextReuseCounter] = go;
            go.name = "SSS" + nextReuseCounter;
        }
        else {
            go = allSSS[nextReuseCounter];
            go.SetActive(true);
        }

        go.GetComponent<SeishouSakura>().ResetVariables();
        go.transform.position = position;

        SeishouSakura s = go.GetComponent<SeishouSakura>();
        s.ResetVariables();

        CountUp();
    }

    public static void AssignGO(GameObject go) {
        if(ssSakura == null)
            ssSakura = go;
    }
    public static void DestroyAllSSS() {
        foreach (GameObject go in allSSS) {
            if (go == null)
                continue;
            if (!go.activeInHierarchy)
                continue;
            SeishouSakura sss = go.GetComponent<SeishouSakura>();
            sss.TurnOff();
        }
    }
}
