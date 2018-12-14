using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;

    private CameraController cameraController;
    private InputController inputController;
    private Player player;

    private void Start()
    {
        cameraController = GetComponentInChildren<CameraController>();
        inputController = GetComponentInChildren<InputController>();
        player = GameObject.FindWithTag(gameSettings.playerTag).GetComponent<Player>();

        if(cameraController == null)
        {
            MissingControllerMessage(typeof(CameraController).Name);
            return;
        }

        if(inputController == null)
        {
            MissingControllerMessage(typeof(InputController).Name);
            return;
        }

        if(player == null)
        {
            MissingControllerMessage(typeof(Player).Name);
            return;
        }

        cameraController.Init(player.transform);
        inputController.Init(gameSettings, cameraController);
        player.Init();
    }

    private void MissingControllerMessage(string controllerName)
    {
        Debug.LogError(string.Format("{0} missing! Breaking out of startup.", controllerName));
    }
}
