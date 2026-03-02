using UnityEngine;

public class ChangeRound : MonoBehaviour
{

    [SerializeField] PlayerController player;
    [SerializeField] GameObject GoNextRoundPoint;
    [SerializeField] GameObject RoundSpawnPoint;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.transform.position = RoundSpawnPoint.transform.position;
        }
    }

}
