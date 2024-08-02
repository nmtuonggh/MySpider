using System.Collections.Generic;
using SFRemastered._Game._Scripts.Enemy;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered._Game._Scripts.Mission
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Mission/FightingMission")]
    public class FightingMission : BaseMission
    {
        [Header("Fight Mission")]
        public float warningRange;
        public float spawnRange;
        public List<Wave> listWave;
        public int currentWaveIndex = 0;
        public int currentWaveEnemyCount = 0;
        
        public override void StartMission()
        {
            //TODO : Bo cai nay luc lam xong
            currentWaveIndex = 0;
            base.StartMission();
            Debug.Log("StartMission FightingMission");
            StartNextWave();
        }
        
        public override void UpdateMission()
        {
            base.UpdateMission();
            Debug.Log("UpdateMission FightingMission");
        }
        
        private void StartNextWave()
        {
            if (currentWaveIndex < listWave.Count)
            {
                // Logic to spawn enemies in the current wave
                SpawnWave(currentWaveIndex);
                currentWaveIndex++;
            }
            else
            {
                CompleteMission();
            }
        }
        
        public void OnEnemyDefeated()
        {
            // Check if all enemies in the current wave are defeated
            // If yes, call StartNextWave()
        }

        public void OnPlayerLeaveWarningRange()
        {
            FailMission();
        }
        
        private void SpawnWave(int waveIndex)
        {
            Wave wave = listWave[waveIndex];
            currentWaveEnemyCount = 0;

            SpawnEnemies(wave.listGangster);
            SpawnEnemies(wave.listMercenary);
            SpawnEnemies(wave.listMafia);
        }

        private void SpawnEnemies<T>(List<T> enemies) where T : EnemySO
        {
            foreach (T enemy in enemies)
            {
                SpawnEnemy(enemy);
                currentWaveEnemyCount++;
            }
        }

        private void SpawnEnemy(EnemySO enemySo)
        {
            float angle = Random.Range(0f, Mathf.PI * 2);
            float distance = Random.Range(0f, spawnRange);
    
            Vector3 spawnPos = new Vector3(
                spawnPosition.position.x + Mathf.Cos(angle) * distance,
                spawnPosition.position.y,
                spawnPosition.position.z + Mathf.Sin(angle) * distance
            );

            Instantiate(enemySo.prefab, spawnPos, Quaternion.identity);
        }
    }
}