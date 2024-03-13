using UnityEngine;
using UnityEngine.SceneManagement;
//Këto rreshta importojnë librarinë e Unity dhe pjesën e menaxhimit të skenave që nevojiten për operacionet që janë përdorur në kod.

public class LevelController : MonoBehaviour  //trashegohet nga MonoBehaviour
{
    private static int _nextLevelIndex = 1; //Ky është një integer statik privat që tregon indeksin e nivelit të ardhshëm. Fillon nga nivel 1.
    private Enemy[] _enemies; //Ky është një varg privat i enemies (klasa Enemy) që gjendet në nivelin aktual.


    private void OnEnable() //Kjo metodë thirret kur skripti bëhet i aktivizuar. Ajo gjen të gjithë enemies në nivel dhe i ruan ato në vargun _enemies
    {
        _enemies = FindObjectsOfType<Enemy>();
    }

    // Update is called once per frame
    //Ky është një metodë e thirrur automatikisht nga Unity në çdo frame të ekzekutimit.Kjo pjesë e kodit kontrollon nëse të gjithë 
    //  armiqtë janë eliminuar. Nëse janë, ai kalon në nivelin tjetër.
    void Update() //
    {
        foreach(Enemy enemy in _enemies) //Kjo është një foreach loop që kalon përmes çdo enemy në vargun _enemies.
        {
            if(enemy != null) //Kontrollon nëse enemy aktual është ende i gjallë (nuk është null).
            {
                return; //Nëse një enemy është i gjalle (jo null), kjo kthen menjëherë ekzekutimin pasi nuk ka nevojë për të vazhduar kontrollin.
            }
        }

        _nextLevelIndex++; //Rrit indeksin e nivelit për të kaluar në nivelin e ardhshëm.
        string nextLevelName = "Level" + _nextLevelIndex; //Përcakton emrin e skenës së nivelit të ardhshëm duke përdorur indeksin e ardhshëm të nivelit.
        SceneManager.LoadScene(nextLevelName); //Ngarkon skenën e nivelit të ardhshëm në lojë.
    }
}
