using UnityEngine;

public class Enemy : MonoBehaviour   //trashegohet nga MonoBehaviour
{

    // Deklarimi i një variabli private për objektin e parashikuar të particle efekteve
    // SerializeField është një atribut që bën të mundur që një fushë private të shfaqet në redaktorin
    // e Unity dhe të jetë e arritshme për ndryshime nga përdoruesi.
    [SerializeField] private GameObject _cloudParticlePrefab;


    // Metoda e thirrur kur ndodh një përplasje 2D
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kontrollon nëse objekti me të cilin është përplasur është një zog (Bird)
        Bird bird = collision.collider.GetComponent<Bird>();
        if (bird != null)
        {
            // Nëse është përplasur me një bird, instanco particle efektin dhe pastaj shkatërro këtë objektin (Enemy)
            Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return; // Kthehet nga metoda për të mos ekzekutuar kodin më poshtë
        }


        // Kontrollon nëse objekti me të cilin është përplasur është një 'Enemy' tjetër, nese po nuk ben asgje
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        if(enemy != null)
        {
            return;// Nëse përplasemi me një 'Enemy' tjetër, kthehemi nga metoda
        }

        // Nëse objekti përplaset me një sipërfaqe të përmbysur (normal.y < -0.5),
        // instanco particle efektin dhe pastaj shkatërro këtë objektin (Enemy)
        if (collision.contacts[0].normal.y < -0.5)
        {
            Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }
    }
}
