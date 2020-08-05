using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySongNotifier : MonoBehaviour
{
    public CameraMover cameraPosisiton;
    public Dictionary<Vector2, List<EnemyModel>> enemyList = new Dictionary<Vector2, List<EnemyModel>>();

    public Transform playerPosition;
    public PlayerController playerControl;

    public void RegisterEnemy(EnemyModel enemy, Vector2 mapChunk) {
        if (!enemyList.ContainsKey(mapChunk))
            enemyList[mapChunk] = new List<EnemyModel>();

        enemyList[mapChunk].Add(enemy);
    }


    public void PlayingSong(TapeColor songColor, bool playing){
        if (!enemyList.ContainsKey(cameraPosisiton.currentMapChunk)) return;

        enemyList[cameraPosisiton.currentMapChunk]?
            .ForEach(e => {
                if (e.enemyColor == songColor)
                    e.SetSoothed(playing);
            });
        if (songColor != TapeColor.blue)
            enemyList[cameraPosisiton.currentMapChunk]?
            .ForEach(e => {
                if (e.enemyColor == TapeColor.blue)
                    e.SetAngered(playing);
            });
    }

    public void Rewinding(bool rewinding) {
        if (!enemyList.ContainsKey(cameraPosisiton.currentMapChunk)) return;

        enemyList[cameraPosisiton.currentMapChunk]?
            .ForEach(e => e.SetAngered(rewinding));
    }
}
