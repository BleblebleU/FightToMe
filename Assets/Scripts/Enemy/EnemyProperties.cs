using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties : MonoBehaviour {

    public float health;
    public float damageAmount = 5;

    private float initialEnemyHealth;

    [SerializeField] private GameObject[] itemsToDrop = new GameObject[4];
    [SerializeField] private GameObject directionJuiceDrop;
    [SerializeField] private Transform dropTransform;

    void Start () {
        initialEnemyHealth = health;
        for (int i = 0; i < itemsToDrop.Length; i++)
        {
            itemsToDrop[i].GetComponent<PickUpFall>().rb2d = itemsToDrop[i].GetComponent<Rigidbody2D>();
        }
	}
	
	void Update () {
        if (health <= 0)
        {
            health = initialEnemyHealth;
            DropItem();
            Destroy(gameObject);
        }
	}
    public void DamageTaken(float dmgFromPlayer)
    {
        health -= dmgFromPlayer;
        //Debug.Log("DamageTaken");
    }

    private void DropItem()
    {
        int randomItemIndex = Random.Range(0, itemsToDrop.Length - 1);
        Vector3 dropPosition = new Vector3(dropTransform.position.x + Random.Range(-0.5f , 0.5f), dropTransform.position.y + Random.Range(-0.5f, 0.5f), dropTransform.position.z);
        GameObject randomItem = Instantiate(itemsToDrop[randomItemIndex], dropPosition, Quaternion.identity);
        GameObject directionJuice = Instantiate(directionJuiceDrop, dropTransform.position, Quaternion.identity);

        PickUpFall randomPickUpFall = randomItem.GetComponent<PickUpFall>();
        PickUpFall randomJuicePickUpFall = directionJuice.GetComponent<PickUpFall>();
        float fallXVel = Random.Range(-6, 8);
        randomPickUpFall.fallSpeed = new Vector2(fallXVel, 5);
        randomJuicePickUpFall.fallSpeed = new Vector2(fallXVel + 1, 5);

        //Debug.Log(itemsToDrop[randomItemIndex].name);
    }
}
