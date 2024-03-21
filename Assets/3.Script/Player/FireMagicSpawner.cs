using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMagicSpawner : MonoBehaviour
{
    [SerializeField] private GameObject FireMagic_Prefabs;
    [SerializeField] private GameObject player;

    private GameObject firemagic;
    private Vector3 playerposition;


    private void Start()
    {
        firemagic = new GameObject();
    }

    void Update()
    {
        playerposition = player.transform.position;
    }

    public void SpawnFireMagic()
    {
        firemagic = Instantiate(FireMagic_Prefabs, playerposition, Quaternion.identity);
        firemagic.transform.Rotate(0, 0, 90);
    }
}
