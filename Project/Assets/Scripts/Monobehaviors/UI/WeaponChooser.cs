using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChooser : MonoBehaviour
{
    [SerializeField] ExperienceHandler experienceHandler;
    [SerializeField] ItemHolder playerItemHolder;
    [SerializeField] Button[] weaponChoices;
    [SerializeField] GameObject visuals;
    List<Gun> weaponPool;
    List<Gun> choices;
    int credits;

    void Start()
    {
        choices = new List<Gun>();
        weaponPool = new List<Gun>();
        weaponPool = DataLibrary.I.Guns.GetArray.ToList();
        weaponPool.Remove(DataLibrary.I.Guns["Starter Pistol"]);
        experienceHandler.LeveledUp += AddCredit;

        ReRoll();
    }

    void ReRoll()
    {
        for (int i = 0; i < weaponChoices.Length; i++)
        {
            if (weaponPool.Count == 0)
            {
                Destroy(weaponChoices[i]);
                continue;
            }

            Gun gun = weaponPool[Random.Range(0, weaponPool.Count - 1)];
            weaponPool.Remove(gun);

            choices.Add(gun);

            weaponChoices[i].transform.Find("Background").transform.GetChild(0).GetComponent<Image>().sprite = gun.Sprite;

            weaponChoices[i].onClick.RemoveAllListeners();
            weaponChoices[i].onClick.AddListener(() => Chose(gun));
        }
    }

    void Chose(Gun g)
    {
        credits--;
        choices.Remove(g);

        foreach (Gun gun in choices)
            weaponPool.Add(gun);

        choices.Clear();

        ReRoll();

        PickupSpawner.I.SpawnItem(playerItemHolder.ItemStack, playerItemHolder.transform.position);
        playerItemHolder.ChangeItem(new GunStack(g, 1));

        if (credits > 0)
            StartCoroutine(EnableAgain());

        visuals.SetActive(false);
    }

    IEnumerator EnableAgain()
    {
        yield return new WaitForSeconds(.25f);
        visuals.SetActive(true);
    }

    public void AddCredit()
    {
        visuals.SetActive(true);
        credits++;
    }

    private void OnDestroy()
    {
        experienceHandler.LeveledUp -= AddCredit;
    }
}
