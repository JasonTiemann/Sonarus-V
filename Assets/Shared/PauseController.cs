using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController: MonoBehaviour {
    public PlayerController player;
    public EnemyGroupController enemies;
    public tapedeckController boombox;


    public void Pause() {
        player.Pause();
        enemies.Pause();
        boombox.Pause();
    }

    public void Resume() {
        player.Resume();
        enemies.Resume();
        boombox.Resume();
    }
}
