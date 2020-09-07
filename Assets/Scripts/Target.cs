using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody rb;
    private GameController gameController;
    public int pointValue;

    public GameObject explosionFx;
    public GameObject collectionFx;
    public GameObject floatingText;

    private float timer;
    public float startTimer = 60f;

    public float timeOnScreen = 1.0f;
    public float speed = 1.5f;


    // Start is called before the first frame update
    void Start()
    {
        gameController= GameObject.Find("Game Manager").GetComponent<GameController>();
        
        speed *= gameController.playDifficulty;

        

        rb = GetComponent<Rigidbody>();

        if (gameController.isGameActive)
        {
            pointValue *= gameController.playDifficulty;
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector3.down * speed * Time.deltaTime);
    }
    
    // When target is clicked, destroy it, update score, and generate explosion
    private void OnHit()
    {
        if (gameController.isGameActive)
        {
            gameController.UpdateScore(pointValue);

            if (floatingText != null)
            {
                ShowFloatingText();
            }

            Explode();
        }
    }

    private void ShowFloatingText()
    {
        var fT = Instantiate(floatingText, transform.position, floatingText.transform.rotation);
        fT.GetComponent<TextMesh>().text = "+" + pointValue.ToString();
    }

    private void OnCollect()
    {
        if (gameController.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(collectionFx, transform.position, collectionFx.transform.rotation);
        }
               
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collector") || other.gameObject.CompareTag("Enemy"))
        {
            OnCollect();
        }

        if (other.gameObject.CompareTag("Arrow"))
        {
            OnHit();
        }

    }

    

    // Display explosion particle at object's position
    void Explode ()
    {
        Destroy(gameObject);
        Instantiate(explosionFx, transform.position, explosionFx.transform.rotation);
    }
}
