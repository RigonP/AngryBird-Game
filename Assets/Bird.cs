
using UnityEngine;  
using UnityEngine.SceneManagement;          //Ketu importohen dy librari te Unity per te manipuluar elementet e lojes dhe per te kontrolluar nderhyrjet ne skene.

public class Bird : MonoBehaviour
{
    private Vector3 _initialPosition; //ruan pozicionin fillestar të bird.
    private bool _birdWasLaunched; //mban gjurmët nëse bird është lëshuar apo jo
    private float _timesSittingAround; //përdoret për të numëruar sa kohë ka qëndruar bird i palëvizshëm pasi është lëshuar.
    [SerializeField] private float _launchPower = 500; //është fuqia e përdorur për të nisur bird, i cili mund të vendoset në redaktorin Unity.

    //[SerializeField] është një atribut në C# që përdoret në Unity për të bërë variablat private të një klase të shihen në editorin e Unity
    //dhe të ndryshueshme në kohë realisht. Kjo është e rëndësishme për shkak të mënyrës se si Unity menaxhon parametrat e objekteve dhe
    //mundëson ndryshime gjatë zhvillimit të lojës pa ndryshuar kodin.

    //E krijojm metoden Awake
    private void Awake()
    {
        _initialPosition = transform.position;
        
    }
    //Metoda Awake() thirret kur objekti i lojës është inicializuar për herë të parë. Ajo vendos pozicionin fillestar të objektit (bird).

    //is called once per framed
    private void Update()
    {
        //Metoda Update() thirret për çdo frame të lojës. Ajo përditëson vijën që tregon drejtimin e fluturimit të bird.
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);
        GetComponent<LineRenderer>().SetPosition(0, transform.position);


        //Ky kod kontrollon nëse bird është lëshuar dhe po qëndron pa lëvizur për një kohë të caktuar.
        if (_birdWasLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1)
        {
            _timesSittingAround += Time.deltaTime;
        }

        //Kjo pjesë kontrollon nëse bird ka dalë jashtë kufijve të skenës ose ka qëndruar pa lëvizur për
        //një kohë të gjatë, dhe nëse kjo ndodh, skena ringarkohet.
        if (transform.position.y > 10 || transform.position.y < -10 ||
            transform.position.x > 10 || transform.position.x < -13 ||
            _timesSittingAround > 3)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }


    //Kjo metodë thirret kur përdoruesi klikon mbi bird. Ajo vendos ngjyrën e bird në te gjelbër dhe
    //aktivizon LineRenderer për të shfaqur drejtimin e fluturimit.
    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        GetComponent<LineRenderer>().enabled = true;
    }

    //Kjo metodë thirret kur përdoruesi leshon butonin e mausit mbi bird. Ajo vendos forcën
    //dhe drejtimin e fluturimit të bird bazuar në pozicionin fillestar dhe pozicionin aktual të bird.
    //Përcakton gjithashtu që bird është lëshuar dhe fshin LineRenderer.
    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        Vector2 directionToInitialPosition = _initialPosition - transform.position;
        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition* _launchPower);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        _birdWasLaunched = true;

        GetComponent<LineRenderer>().enabled = false;
    }


    //Kjo metodë thirret kur përdoruesi lëviz bird me mausin në ekran. Ajo lëviz pozicionin e bird sipas pozicionit të mausit në ekran.
    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y);
    }
}
