using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    public float speed = 6f;
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    private void Awake() {
        // mendapatkan nilai mask dari layer yang bernama floor
        floorMask = LayerMask.GetMask("Floor");

        // mendapatkan komponen Animator
        anim = GetComponent<Animator>();

        // mendapatkan komponen Rigidbody
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        // mendapatkan nilai input horizontal (-1, 0, 1)
        float h = Input.GetAxisRaw("Horizontal");

        // mendapatkan nilai input vertical (-1, 0, 1)
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);
    }

    // movement player
    void Move(float h, float v) {
        // set nilai x dan y
        movement.Set(h, 0f, v);

        // menormalisasi nilai vector agar total panjang dari vector adalah 1
        movement = movement.normalized * speed * Time.deltaTime;

        // move to position
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning() {
        // buat ray dari posisi mouse di layar
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // buat raycast untuk floorhit
        RaycastHit floorHit;

        // lakukan raycast
        if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
            // mendapatkan vector dari posisi player dan posisi floorHit
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            // mendapatkan look rotation baru ke hit position
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // rotasi player
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v) {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }

}
