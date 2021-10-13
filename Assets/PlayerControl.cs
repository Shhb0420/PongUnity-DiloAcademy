using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControl : MonoBehaviour
{
    // Tombol untuk menggerakkan ke atas
    public KeyCode upButton = KeyCode.W;
 
    // Tombol untuk menggerakkan ke bawah
    public KeyCode downButton = KeyCode.S;
 
    // Kecepatan gerak
    public float speed = 10.0f;
 
    // Batas atas dan bawah game scene (Batas bawah menggunakan minus (-))
    public float yBoundary = 9.0f;
 
    // Rigidbody 2D raket ini
    private Rigidbody2D rigidBody2D;
 
    // Skor pemain
    private int score;

    // Titik tumbukan terakhir dengan bola, untuk menampilkan variabel-variabel fisika terkait tumbukan tersebut
    private ContactPoint2D lastContactPoint;

    // Titik asal lintasan bola saat ini
    private Vector2 trajectoryOrigin;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        trajectoryOrigin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Dapatkan Kecepatan Raket Sekarang
        Vector2 velocity = rigidBody2D.velocity;

        // Kontrol Pemain
        if (Input.GetKey(upButton)) {
            velocity.y = speed;
        } else if(Input.GetKey(downButton)) {
            velocity.y = -speed;
        } else {
            velocity.y = 0.0f;
        }

        rigidBody2D.velocity = velocity;

        // Dapatkan Posisi raket sekarang
        Vector3 position = transform.position;

        // Posisi control
        if (position.y > yBoundary) {
            position.y = yBoundary;
        } else if(position.y < -yBoundary) {
            position.y = -yBoundary;
        }
        
        // Posisi transform
        transform.position = position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Equals("Ball"))
        {
            lastContactPoint = collision.GetContact(0);
        }
    }

        // Menaikkan skor sebanyak 1 poin
        public void IncrementScore()
        {
            score++;
        }
        
        // Mengembalikan skor menjadi 0
        public void ResetScore()
        {
            score = 0;
        }
    
        // Mendapatkan nilai skor
        public int Score
        {
            get { return score; }
        }

        public ContactPoint2D LastContactPoint
        {
            get { return lastContactPoint; }
        }

        public Vector2 TrajectoryOrigin
        {
            get { return trajectoryOrigin; }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            trajectoryOrigin = transform.position;
        }
}
