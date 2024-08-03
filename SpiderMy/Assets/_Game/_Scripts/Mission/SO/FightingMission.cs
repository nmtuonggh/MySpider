using System;
using System.Collections.Generic;
using _Game.Scripts.Event;
using SFRemastered._Game._Scripts.Enemy;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace SFRemastered._Game._Scripts.Mission
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Mission/FightingMission")]
    [System.Serializable]
    public class FightingMission : BaseMission
    {
        [Header("Fight Mission")]
        public GameObject warningRange;
        public float spawnRange;
        public GameObject indicator;
        public GameObject missionRange;
        

        [Header("Wave Data")]
        public int currentWaveIndex = 0;
        public int currentWaveEnemyCount = 0;
        public List<WaveCombat> listWaveCombat;

        public GameEventListener onEnemyDead;

        public override void StartMission()
        {
            //TODO : Bo cai nay luc lam xong
            currentWaveIndex = 0;
            base.StartMission();
            DrawnIndicator();
            SetupMissionRange();
            StartNextWave();
        }

        public override void UpdateMission()
        {
            base.UpdateMission();
        }
        
        public override void CompleteMission()
        {
            base.CompleteMission();
            //TODO : Pool
            ReMoveIndicator();
            Destroy(missionRange);
        }
        
        private void SetupMissionRange()
        {
            missionRange = Instantiate(missionRangePrefab, spawnPosition.position, Quaternion.identity);
            missionRange.transform.SetParent(spawnPosition);
        }
        
        private void DrawnIndicator()
        {
            indicator =
                Instantiate(indicatorPrefab, spawnPosition.position, Quaternion.identity);
            indicator.transform.SetParent(spawnPosition);
        }
        
        private void ReMoveIndicator()
        {
            Destroy(indicator);
        }

        public void PlayerInMissionRange()
        {
            indicator.SetActive(false);
        }
        
        public void PlayerOutOfMissionRange()
        {
            indicator.SetActive(true);
        }

        private void StartNextWave()
        {
            if (currentWaveIndex < listWaveCombat.Count)
            {
                SpawnWave(currentWaveIndex);
                currentWaveIndex++;
            }
            else
            {
                CompleteMission();
            }
        }

        public void OnEnemyDie()
        {
            currentWaveEnemyCount--;
            if (currentWaveEnemyCount <= 0)
            {
                StartNextWave();
            }
        }

        public void OnPlayerLeaveWarningRange()
        {
            FailMission();
        }

        private void SpawnWave(int waveIndex)
        {
            WaveCombat waveCombat = listWaveCombat[waveIndex];
            currentWaveEnemyCount = 0;

            SpawnEnemies(waveCombat.listGangster);
            SpawnEnemies(waveCombat.listMercenary);
            SpawnEnemies(waveCombat.listMafia);
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