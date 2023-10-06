using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private GameObject camera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject currCamera = FindObjectOfType<PlayerCamera>().gameObject;

            LoadingSceneController.LoadScene("SafeHouse");

            Instantiate(camera, currCamera.transform.position, currCamera.transform.rotation);
        }
    }

}
