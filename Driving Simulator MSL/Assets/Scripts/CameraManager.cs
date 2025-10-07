using Unity.Cinemachine;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField] private CinemachineCamera[] cameras;

    private int currentCameraIndex = 0;

    [SerializeField] private CinemachineCamera backCam;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {

        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == 0); //Turns all cameras off except for the one at index 0
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCam();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && currentCameraIndex == 0)
        {
            if (backCam.gameObject.activeSelf)
            {
                cameras[currentCameraIndex].gameObject.SetActive(true);
                backCam.gameObject.SetActive(false);
            } else
            {
                cameras[currentCameraIndex].gameObject.SetActive(false);
                backCam.gameObject.SetActive(true);
            }
        }
    }
    public void SwitchCam()
    {
        cameras[currentCameraIndex].gameObject.SetActive(false);

        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        cameras[currentCameraIndex].gameObject.SetActive(true);
    }
}
