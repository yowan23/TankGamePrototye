using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("MovementData")]
    [SerializeField] private float MoveSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float VertialInput;
    [SerializeField] private float HorizontalInput;
    [SerializeField] private float RotationalSpeed;

    [Header("Aim Data")]
    [SerializeField] private LayerMask WhichLayerMask;
    [SerializeField] private Transform AimShereTransform;

    [Header("Shoot Data")]
    [SerializeField] private Transform Aimpoint;
    [SerializeField] private float BulletSpeed;
    [SerializeField] private GameObject BulletPrefab;



    [Header("TowerData")]
    [SerializeField] private Transform TowerAimTransform;
    [SerializeField] private float TowerRotationSpeed;



    void Start()
    {

    }


    void Update()
    {
        ManageInput();
        UpdateAim();
        
    }

    private void ManageInput()
    {
        VertialInput = Input.GetAxis("Vertical");
        HorizontalInput = Input.GetAxis("Horizontal");
        if (VertialInput < 0)
        {
            HorizontalInput = -Input.GetAxis("Horizontal");
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            shoot();
        }
        
    }

    void FixedUpdate()
    {
        ApplyMovement();
        ApplyBodyRotation();
        ApplyTowerRotation();

    }

    private void ApplyTowerRotation()
    {
        Vector3 Direction = AimShereTransform.position - TowerAimTransform.position;
        Direction.y = 0;
        Quaternion TragetRotation = Quaternion.LookRotation(Direction);
        TowerAimTransform.rotation = Quaternion.RotateTowards(TowerAimTransform.rotation, TragetRotation, TowerRotationSpeed);
    }

    private void ApplyBodyRotation()
    {
        transform.Rotate(0, RotationalSpeed * HorizontalInput, 0);
    }

    private void ApplyMovement()
    {
        Vector3 Movement = transform.forward * MoveSpeed * VertialInput;
        rb.linearVelocity = Movement;
    }

    void UpdateAim()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, WhichLayerMask))
        {
            Debug.Log(hit.point);

            float FixedY = AimShereTransform.position.y;
            AimShereTransform.position = new Vector3(hit.point.x, FixedY, hit.point.z);
        }
    }

    void shoot()
    {
        GameObject bullet = Instantiate(BulletPrefab, Aimpoint.position, Aimpoint.rotation);
        bullet.GetComponent<Rigidbody>().linearVelocity = Aimpoint.forward * BulletSpeed;

        Destroy(bullet, 7);
        
    }
    
}
