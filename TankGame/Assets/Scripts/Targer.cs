using UnityEngine;

public class Targer : MonoBehaviour
{
    [SerializeField] private Material HitMaterial;




   private void OnCollisionEnter(Collision collision)
    {
        if( collision.gameObject.tag == "bullet")
        {
            GetComponent<MeshRenderer>().material = HitMaterial;
        }
    }
}
