using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningCurve : MonoBehaviour
{
    public GameObject DirectionLight;
    private Transform LightTransform;
    private Transform camTransform;

    // Start is called before the first frame update
    void Start()
    {

        DirectionLight = GameObject.Find("Directional Light");

        LightTransform = DirectionLight.GetComponent<Transform>();
        Debug.Log(LightTransform.localPosition);

        //camTransform = this.GetComponent<Transform>();
        //Debug.Log(camTransform.localPosition);

        //Character hero = new Character();
        //Character hero2 = new Character();
        //hero2 = hero;
        //Character heroine = new Character("Agatha");
        //hero2.name = "lol";


        //hero.PrintStatsInfo();
        //hero2.PrintStatsInfo();
        ////hero2.Reset();
        //heroine.PrintStatsInfo();

        //Weapon HuntingBow = new Weapon("Hunting Bow", 105);
        //Weapon WarBow = HuntingBow;
        //WarBow.name = "War Bow";
        //WarBow.damage = 200;
        //HuntingBow.PrintWeaponStats();
        //WarBow.PrintWeaponStats();

        //Paladin knight = new Paladin("sir arthur", HuntingBow);
        //knight.PrintStatsInfo();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
