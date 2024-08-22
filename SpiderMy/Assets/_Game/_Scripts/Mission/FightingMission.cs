using System;
using System.Collections.Generic;
using _Game.Scripts.Event;
using SFRemastered._Game._Scripts.Enemy;
using SFRemastered._Game._Scripts.Enemy.State;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SFRemastered._Game._Scripts.Mission
{
    public class FightingMission : BaseMission
    {
        [Header("Fighting Mission Information")]
        public FightingMissionSo fightingMissionSo;
        private GameObject _indicator;
        private GameObject _missionRange;

        [Header("Fighting Mission Event")]
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
            fightingMissionSo.currentWaveIndex = 0;
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
            base.CompleteMission();
        }

        public override void FailMission()
        {
            Destroy(_indicator);
            Destroy(_missionRange);
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
                fightingMissionSo.currentWaveIndex = 0;
                fightingMissionSo.currentWaveEnemyCount = 0;
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

                DrawnIndicator();
            }
        }

        private void SpawnMissionRange()
        {
            _missionRange = Instantiate(fightingMissionSo.missionRangePrefab, fightingMissionSo.SpawnPosition.position,
                Quaternion.identity);
            _missionRange.transform.SetParent(fightingMissionSo.SpawnPosition);
        }

        private void DrawnIndicator()
        {
            _indicator = Instantiate(fightingMissionSo.indicatorPrefab, fightingMissionSo.SpawnPosition.position,
                Quaternion.identity);
            _indicator.transform.SetParent(fightingMissionSo.SpawnPosition);
        }

        public void OnEnemyDie()
        {
            Debug.Log("Enemy Die");
            fightingMissionSo.currentWaveEnemyCount--;
            if (fightingMissionSo.currentWaveEnemyCount <= 0)
            {
                SetupWave();
            }
        }

        #region SpawnWave&Enemies
        private void SetupWave()
        {
            if (fightingMissionSo.currentWaveIndex < fightingMissionSo.listWaveCombat.Count)
            {
                SpawnWave(fightingMissionSo.currentWaveIndex);
                fightingMissionSo.currentWaveIndex++;
            }
            else
            {
                CompleteMission();
            }
        }
        
        private void SpawnWave(int waveIndex)
        {
            var waveCombat = fightingMissionSo.listWaveCombat[waveIndex];
            fightingMissionSo.currentWaveEnemyCount = 0;

            SpawnEnemies(waveCombat.listGangster);
            SpawnEnemies(waveCombat.listMercenary);
            SpawnEnemies(waveCombat.listMafia);
        }

        private void SpawnEnemies<T>(List<T> enemies) where T : EnemySO
        {
            foreach (T enemy in enemies)
            {
                SpawnEnemy(enemy);
                fightingMissionSo.currentWaveEnemyCount++;
            }
        }

        private void SpawnEnemy(EnemySO enemySo)
        {
            float angle = Random.Range(0f, Mathf.PI * 2);
            float distance = Random.Range(0f, fightingMissionSo.spawnRange);

            var position = fightingMissionSo.SpawnPosition.position;
            Vector3 spawnPos = new Vector3(
                position.x + Mathf.Cos(angle) * distance,
                position.y,
                position.z + Mathf.Sin(angle) * distance
            );
            
            enemySo.Spawn(spawnPos, Quaternion.identity, fightingMissionSo.SpawnPosition, enemySo.id);
        }
        #endregion
    }
}