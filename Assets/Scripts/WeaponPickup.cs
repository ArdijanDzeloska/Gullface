using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{

    public List<GameObject> weapons;
    public Transform player;
    public GameObject currentWeapon;
    public Transform hand;
    public int selectedWeapon = 0;
    public GameObject heavy;
    public GameObject water;
    public GameObject fish;


    // Start is called before the first frame update
    void Start()
    {
        (water.GetComponent<ShootWater>()).enabled = false;
        (heavy.GetComponent<ShootHeavy>()).enabled = false;
        (fish.GetComponent<ThrowFish>()).enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        int previousWeapon = selectedWeapon;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeapon = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedWeapon = 3;
        }

        if (previousWeapon != selectedWeapon)
        {
            SwitchWeapon();
        }
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("WaterGun") )
        {
            // save the weapon                
            weapons.Add(other.gameObject);

            // hides the weapon because it's now in our 'inventory'
            other.collider.gameObject.SetActive(false);
            //other.collider.transform.position = Vector3.zero;
            // now we can positioning the weapon at many other places.
            // but for this demonstration where we just want to show a weapon
            // in our hand at some point we do it now.

            other.transform.parent = hand;
            //other.transform.position = new Vector3(0, 0, 0);
            //other.transform.rotation = new Quaternion(0, 0, 0, 0);
            other.collider.transform.position = Vector3.zero;
            other.transform.rotation = Quaternion.identity;
            //other.transform.position = hand.transform.position;
            (water.GetComponent<ShootWater>()).enabled = true;
        }

        if (other.gameObject.CompareTag("HeavyGun"))
        {
            // save the weapon                
            weapons.Add(other.gameObject);

            // hides the weapon because it's now in our 'inventory'
            other.collider.gameObject.SetActive(false);
            //other.collider.transform.position = Vector3.zero;
            // now we can positioning the weapon at many other places.
            // but for this demonstration where we just want to show a weapon
            // in our hand at some point we do it now.

            other.transform.parent = hand;
            //other.transform.position = new Vector3(0, 0, 0);
            //other.transform.rotation = new Quaternion(0, 0, 0, 0);
            other.collider.transform.position = Vector3.zero;
            other.transform.rotation = Quaternion.identity;
            (heavy.GetComponent<ShootHeavy>()).enabled = true;
        }

        if (other.gameObject.CompareTag("Fish"))
        {
            // save the weapon                
            weapons.Add(other.gameObject);

            // hides the weapon because it's now in our 'inventory'
            other.collider.gameObject.SetActive(false);
            //other.collider.transform.position = Vector3.zero;

            other.transform.parent = hand;
            //other.transform.position = new Vector3(0, 0, 0);
            other.collider.transform.position = Vector3.zero;
            other.transform.rotation = Quaternion.identity;
            //other.transform.position = hand.transform.position;
            (fish.GetComponent<ThrowFish>()).enabled = true;
        }
    }

    void SelectWeapon(int index)
    {

        // Ensure we have a weapon in the wanted 'slot'
        if (weapons.Count > index && weapons[index].CompareTag("Weapon") == false)
        {

            // Check if we already is carrying a weapon
            if (currentWeapon != null)
            {
                // hide the weapon                
                currentWeapon.gameObject.SetActive(false);
            }

            // Add our new weapon
            currentWeapon = weapons[index];

            // Show our new weapon
            currentWeapon.SetActive(true);
        }
    }

    void SwitchWeapon()
    {
        int i = 1;
        foreach (GameObject item in weapons)
        {
            if (i == selectedWeapon)
                item.gameObject.SetActive(true);
            else
                item.gameObject.SetActive(false);
            i++;
        }
    }
}
