using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMenuModel : MonoBehaviour
{
    private List<GameObject> _skins = new List<GameObject>();

    private void Start()
    {

        for (int i = 1; i <= 9; i++)
        {
            _skins.Add((GameObject)Resources.LoadAll("Skins/" + i + "/", typeof(GameObject))[0]);
        }

        if (PlayerPrefs.GetString("CurrentSkin") != "")
            ChangeModel(PlayerPrefs.GetString("CurrentSkin"));
        else
            ChangeModel(_skins[0].name);

    }

    public void ChangeModel(string objName)
    {

        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        foreach (var skin in _skins)
        {
            if (skin.name == objName)
            {
                var clone = Instantiate(skin, transform);
                clone.transform.localPosition = new Vector3(-4, 0, -9);
                break;
            }
        }

    }
}
