using UnityEngine;

public class Delivery : MonoBehaviour
{
    bool hasPackage;
    [SerializeField] float destroyDelay = 0.3f;
    [SerializeField] GameObject packagePrefab;
    [SerializeField] GameObject customerPrefab;
    Vector3 packageSpawnPosition;
    Vector3 customerSpawnPosition;

    void Start()
    {
        GetComponent<ParticleSystem>().Stop();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //if(tag is package)
        //then (print picked up package to console)
        if(collision.CompareTag("Package") && !hasPackage) 
        {                                  //or hasPackage == false
            Debug.Log("Picked up package!");
            hasPackage = true;
            GetComponent<ParticleSystem>().Play();
            packageSpawnPosition = collision.transform.position;
            Destroy(collision.gameObject, destroyDelay);
            Invoke("RespawnPackage", 6f);
        }

        if(collision.CompareTag("Customer") && hasPackage)
        {
            Debug.Log("Delivered package!");
            hasPackage = false;
            GetComponent<ParticleSystem>().Stop();
            customerSpawnPosition = collision.transform.position;
            Destroy(collision.gameObject, destroyDelay);
            Invoke("RespawnCustomer", 6f);
        }

        if(collision.CompareTag("Customer") && !hasPackage)
        {
            Debug.Log("You don't have a package to deliver!");
        }
    }

    void RespawnPackage()
    {
        Instantiate(packagePrefab, packageSpawnPosition, Quaternion.identity);
    }

    void RespawnCustomer()
    {
        Instantiate(customerPrefab, customerSpawnPosition, Quaternion.identity);
    }
}
