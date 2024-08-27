using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SFRemastered;
using SFRemastered._Game._Scripts.Player.State.Combat.Gadget;
using UnityEngine;

namespace TerrainScannerDEMO
{
    public class SensorDetector : MonoBehaviour
    {
        [SerializeField] BlackBoard blackBoard;
        
        [SerializeField] PoolObject chestMarker;
        
        [SerializeField] TerrainScanner.CameraEffect _sensorCameraEffect;

        [SerializeField] Material _sensorMaterial;

        [SerializeField] GameObject _sensorOrigin;

        [SerializeField] private float _maxDistance;
        [SerializeField] private float _speed;

        [SerializeField] private float _killTime;

        //private AudioSource _sensorStart;

        private GameObject marker;

        private bool _startSensor;
        private float _duration;
        private float _timer;

        private bool _killSensor;
        private float _timerKill;

        private float _EmissionCacher;

        private Vector3 _origin;

        private float _currentRadius;

        public LayerMask chestLayer;
        public List<GameObject> List = new List<GameObject>();

        public Vector3 Origin
        {
            get { return _origin; }
        }

        public bool SensorOn
        {
            get { return _startSensor; }
        }

        public float Radius
        {
            get { return _currentRadius; }
        }

        public float Duration
        {
            get { return _duration; }
        }

        private void Start()
        {
            _sensorCameraEffect.material = _sensorMaterial;
            _EmissionCacher = _sensorMaterial.GetFloat("_OverlayEmission");
            _sensorMaterial.SetFloat("_Radius", 0);
            //_sensorStart = GetComponent<AudioSource>();
            _sensorCameraEffect.enabled = false;
        }

        private void OnDisable()
        {
            _sensorMaterial.SetFloat("_OverlayEmission", _EmissionCacher);
            _sensorMaterial.SetFloat("_Radius", 0);
        }

        void Update()
        {
            if (_startSensor)
            {
                _currentRadius = Mathf.Min(_speed * _timer, _maxDistance);
                _sensorMaterial.SetFloat("_Radius", _currentRadius);

                if (_timer >= _duration)
                {
                    _timer = 0f;
                    _killSensor = true;
                    _startSensor = false;
                }

                _timer += Time.deltaTime;
            }

            if (_killSensor)
            {
                if (_timerKill >= _killTime)
                {
                    _timerKill = 0f;
                    _sensorMaterial.SetFloat("_Radius", 0);
                    _sensorMaterial.SetFloat("_OverlayEmission", _EmissionCacher);
                    _sensorCameraEffect.enabled = false;
                    _killSensor = false;
                    _currentRadius = 0;
                }

                _sensorMaterial.SetFloat("_OverlayEmission", Mathf.Lerp(_EmissionCacher, 0, _timerKill / _killTime));

                _timerKill += Time.deltaTime;
            }

            if (blackBoard.scan && blackBoard.playerMovement.IsGrounded())
            {
                StartSensor();
              
            }
        }

        private void Overlap()
        {
            List.Clear();
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 40, chestLayer);
            foreach (var hitCollider in hitColliders)   
            {
                marker = chestMarker.Spawn(hitCollider.transform.position, Quaternion.identity, hitCollider.transform);
                List.Add(marker);
            }
            DOVirtual.DelayedCall(8f, ReturnPool);
        }

        private void StartSensor()
        {
            if (_startSensor) { return; }
            if (_killSensor) { return; }
            blackBoard.playerMovement.SetMovementDirection(Vector3.zero);
            _sensorCameraEffect.enabled = true;
            //_sensorStart.Play();
            _duration = _maxDistance / _speed;
            _EmissionCacher = _sensorMaterial.GetFloat("_OverlayEmission");
            _sensorMaterial.SetVector("_RevealOrigin", transform.position);
            _origin = transform.position;
            DOVirtual.DelayedCall(1.5f, Overlap);
            _startSensor = true;
        }
        
        public void ReturnPool()
        {
            foreach (var var in List)
            {
                chestMarker.ReturnToPool(var); 
            }
        }
    }
}
