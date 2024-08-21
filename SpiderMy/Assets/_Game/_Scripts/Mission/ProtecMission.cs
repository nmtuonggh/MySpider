using System;
using System.Collections.Generic;
using _Game.Scripts.Event;
using SFRemastered._Game._Scripts.Enemy;
using SFRemastered._Game._Scripts.Enemy.State;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SFRemastered._Game._Scripts.Mission
{
    public class ProtecMission : BaseMission
    {
        [Header("Protec Mission  Information")]
        public ProtecMissionSO protecMissionSo;
        private GameObject _indicator;
        private GameObject _missionRange;
        private GameObject _victim;

        [Header("Protec Mission Event")]
        public GameEventListener onEnemyDead;
        public GameEventListener inCombatRange;
        public GameEventListener outCombatRange;

        private void OnEnable()
        {
            onEnemyDead.OnEnable();
            inCombatRange.OnEnable();
            outCombatRange.OnEnable();
        }
        
        private void OnDisable()
        {
            onEnemyDead.OnDisable();
            inCombatRange.OnDisable();
            outCombatRange.OnDisable();
        }

        public override void StartMission()
        {
            base.StartMission();
            protecMissionSo.currentWaveIndex = 0;
            SpawnMissionRange();
            DrawnIndicator();
            SetupWave();
        }

        public override void UpdateMission()
        {
            base.UpdateMission();
        }

        public override void CompleteMission()
        {
            Destroy(_indicator);
            Destroy(_missionRange);
            Destroy(_victim);
            base.CompleteMission();
        }

        public override void FailMission()
        {
            Destroy(_indicator);
            Destroy(_missionRange);
            Destroy(_victim);
            progressing = false;
            base.FailMission();
        }
        
        public void PlayerEnterCombatRange()
        {
            progressing = true;
            Destroy(_indicator);
        }
        
        public void PlayerLeaveCombatRange()
        {
            if (progressing)
            {
                protecMissionSo.currentWaveIndex = 0;
                protecMissionSo.currentWaveEnemyCount = 0;
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (var enemy in enemies)
                {
                    var obj = enemy.GetComponent<EnemyBlackBoard>();
                    if (obj.attacking)
                    {
                        obj.warningAttack.SetActive(false);
                        obj.attacking = false;
                    }
                    obj.enemyData.ReturnToPool(obj.enemyData.id, enemy);
                }
                Destroy(_victim);
                FailMission();
            }
        }

        private void SpawnMissionRange()
        {
            _missionRange = Instantiate(protecMissionSo.missionRangePrefab, protecMissionSo.SpawnPosition.position,
                Quaternion.identity);
            _missionRange.transform.SetParent(protecMissionSo.SpawnPosition);
        }

        private void DrawnIndicator()
        {
            _indicator = Instantiate(protecMissionSo.indicatorPrefab, protecMissionSo.SpawnPosition.position,
                Quaternion.identity);
            _indicator.transform.SetParent(protecMissionSo.SpawnPosition);
        }

        public void OnEnemyDie()
        {
            Debug.Log("Enemy Die");
            protecMissionSo.currentWaveEnemyCount--;
            if (protecMissionSo.currentWaveEnemyCount <= 0)
            {
                SetupWave();
            }
        }

        #region SpawnWave&Enemies
        private void SetupWave()
        {
            if (protecMissionSo.currentWaveIndex < protecMissionSo.listWaveCombat.Count)
            {
                SpawnWave(protecMissionSo.currentWaveIndex);
                protecMissionSo.currentWaveIndex++;
            }
            else
            {
                CompleteMission();
            }
        }
        
        private void SpawnWave(int waveIndex)
        {
            var waveCombat = protecMissionSo.listWaveCombat[waveIndex];
            protecMissionSo.currentWaveEnemyCount = 0;
            var position = protecMissionSo.SpawnPosition.position;
            
            //_victim = Instantiate(protecMissionSo.victim, protecMissionSo.SpawnPosition.position, Quaternion.identity);
            SpawnVictim(protecMissionSo.victim);
            SpawnEnemies(waveCombat.listGangster);
            SpawnEnemies(waveCombat.listMercenary);
            SpawnEnemies(waveCombat.listMafia);
        }

        private void SpawnEnemies<T>(List<T> enemies) where T : EnemySO
        {
            foreach (T enemy in enemies)
            {
                SpawnEnemy(enemy);
                protecMissionSo.currentWaveEnemyCount++;
            }
        }

        private void SpawnEnemy(EnemySO enemySo)
        {
            float angle = Random.Range(0f, Mathf.PI * 2);
            float distance = Random.Range(0f, protecMissionSo.spawnRange);

            var position = protecMissionSo.SpawnPosition.position;
            Vector3 spawnPos = new Vector3(
                position.x + Mathf.Cos(angle) * distance,
                position.y,
                position.z + Mathf.Sin(angle) * distance
            );
            
            enemySo.Spawn(spawnPos, Quaternion.identity, protecMissionSo.SpawnPosition, enemySo.id);
        }

        private void SpawnVictim(GameObject victim)
        {
            float angle = Random.Range(0f, Mathf.PI * 2);
            float distance = Random.Range(0f, protecMissionSo.spawnRange);

            var position = protecMissionSo.SpawnPosition.position;
            Vector3 spawnPos = new Vector3(
                position.x + Mathf.Cos(angle) * distance,
                position.y,
                position.z + Mathf.Sin(angle) * distance
            );
            
            _victim = Instantiate(victim, spawnPos, Quaternion.identity);
        }
        #endregion
    }
}