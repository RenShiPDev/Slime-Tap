using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsButtonSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _skinButtonPrefab;

    [SerializeField] private GameObject _buttonsObject;

    [SerializeField] private GameObject _markObject;
    [SerializeField] private MarkMover _markMover;

    private List<GameObject> _skins = new List<GameObject>();

    private void Start()
    {

        int skinId = 0;
        int maxLevel = PlayerPrefs.GetInt("MaxLevel");
        int unlockLevel = 0;

        for(int i = 1; i <= 9; i++)
        {
            _skins.Add( (GameObject)Resources.LoadAll("Skins/" + i + "/", typeof(GameObject))[0] );
        }

        for(int i = -1; i < 2; i++)
            for(int j = -1; j < 2; j++)
            {
                unlockLevel = (i + 1) * 200 + (j + 2) * 50 - 50;

                var clone = Instantiate(_skinButtonPrefab, _buttonsObject.transform);
                clone.transform.localPosition = new Vector3(220 * j, -i * 220, 0);

                clone.GetComponent<ChangeSkinButton>().SetSkinPrefab(_skins[skinId]);
                clone.GetComponent<ChangeSkinButton>().ChangeText(unlockLevel);

                if(unlockLevel > maxLevel)
                    clone.GetComponent<ChangeSkinButton>().SetLock();
                else
                    clone.GetComponent<ChangeSkinButton>().Unlock();

                skinId++;
            }
    }
}
