using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject target1;
    [SerializeField] private GameObject target2;
    [SerializeField] private GameObject target3;
    [SerializeField] private Collisions_UI collisions_UI;
    public static bool hitted = false;// raycast gecisi oldugunda bir kere calissin diye ve aradan gecmeden gol atilirsa fail versin diye
    public static int shot_count; //raycast hit vurulamazsa bu degerle basarili sut sayisi karsilastirilacak,esit degilse gameover.
    public static bool accurated_shot; //hem isabetli hem isabetsiz atista shot_count'in artirilip oyunun bitmesine engel olsun diye olusturuldu.
    private int layerMask;
    private RaycastHit _hit;
    void Start()
    {
        shot_count = 0;
        layerMask = 1 << 6;
    }
    private void FixedUpdate()
    {
        Throwing();
        if (Collisions_UI.shot_success != shot_count)
        {
            collisions_UI.Unsuccess_Throw();
            shot_count--;//update icinden cikmasi icin
        }
    }
    private void Throwing()
    {
        Vector3 direction = (target2.transform.position - target1.transform.position).normalized;
        Vector3 direction2 = (target3.transform.position - target1.transform.position).normalized;
        Vector3 direction3 = (target3.transform.position - target2.transform.position).normalized;
        float distance = Vector3.Distance(target2.transform.position, target1.transform.position);
        float distance2 = Vector3.Distance(target3.transform.position, target1.transform.position);
        float distance3 = Vector3.Distance(target3.transform.position, target2.transform.position);

        if (Physics.Raycast(target1.transform.position, direction, out _hit, distance,layerMask))
        {
            if (_hit.transform.CompareTag("Player3") && !hitted)
            {
                collisions_UI.Success_Throw();
                hitted = true;
                BacktoNormalCollider();//bir onceki deaktif collider aktif olsun diye burada gecer gecmez cagrildi,bir onceki collider siradaki atisa hazir olacakken,yeni kullanýlan topun collideri ballstop fonksiyonunda deaktif olacak
            }
        }
        if (Physics.Raycast(target1.transform.position, direction2, out _hit, distance2,layerMask))
        {
            if (_hit.transform.CompareTag("Player2") && !hitted)
            {
                collisions_UI.Success_Throw();
                hitted = true;
                BacktoNormalCollider();
            }
        }
        if (Physics.Raycast(target2.transform.position, direction3, out _hit, distance3,layerMask))
        {
            if (_hit.transform.CompareTag("Player") && !hitted)
            {
                collisions_UI.Success_Throw();
                hitted = true;
                BacktoNormalCollider();
            }
        }
    }
    private void BacktoNormalCollider() // degdigi an collider kapaninca rakipleri, fileyi ve diger toplari algilayamiyodu; bu fonk ve hitted, bu yuzden olusturuldu
    {
        target1.GetComponent<BoxCollider>().enabled = true;
        target2.GetComponent<BoxCollider>().enabled = true;
        target3.GetComponent<BoxCollider>().enabled = true;
    }
}