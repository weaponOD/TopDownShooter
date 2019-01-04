using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Data/Game Settings", fileName = "New Game Settings")]
public class GameSettings : ScriptableObject {
    [Header("Tags")]
    public string playerTag;

    [Header("PC Input")]
    public KeyCode shootKey;
    public KeyCode dodgeKey;
    public KeyCode reloadKey;
}
