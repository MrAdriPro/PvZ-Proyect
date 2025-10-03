using UnityEngine;

public class DeathZone : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Collision with: " + other.name);
        if (other.CompareTag("Zombie"))
        {
            //here we should add the logic for losing or ending the game
            Destroy(other.gameObject);
        }
    }

}
