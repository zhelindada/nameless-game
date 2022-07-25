using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public PlayerEntity playerEntity;

    public void PlyrResCorStarter() {
        StartCoroutine(Instance.PlayerRespawn());
    }

    public IEnumerator PlayerRespawn()
    { 
        yield return new WaitForSeconds(1);
        playerEntity.gameObject.SetActive(true);
        playerEntity.gameObject.transform.position = Vector3.zero;

        playerEntity.HealBy(playerEntity.GetMaxHealth());
    }
}
