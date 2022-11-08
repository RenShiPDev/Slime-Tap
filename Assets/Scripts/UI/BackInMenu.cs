using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackInMenu : MonoBehaviour
{

    [SerializeField] private float _moveSpeed;

    private void Update()
    {
        transform.localPosition = Vector3.Slerp(transform.localPosition, new Vector3(0, -340, 0), _moveSpeed * Time.deltaTime);
        transform.localPosition = new Vector3(0, transform.localPosition.y, 0);
    }

    public void OnClick()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
