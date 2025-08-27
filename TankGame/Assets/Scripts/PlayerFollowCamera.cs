using Unity.VisualScripting;
using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
    [SerializeField] private Transform player;


    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z - 25);
    }
}
