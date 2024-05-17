using System.Collections;
using System.Collections.Generic;
using Character.ComponentContainer;
using UI;
using UnityEngine;

namespace Global
{
    public class DeathManager : MonoBehaviour
    {
        [SerializeField] private ScreenFader _blackScreen;
        [SerializeField] private TextFader _diedText;

        private PersonContainer _player;
        private List<PersonContainer> _deadUnits = new List<PersonContainer>();

        private void Awake()
        {
            EventBus.OnPlayerDied.AddListener(HandleDeath);
            EventBus.OnUnitDied.AddListener(AddDeadUnit);
        }

        private void OnDestroy()
        {
            EventBus.OnPlayerDied.RemoveListener(HandleDeath);
            EventBus.OnUnitDied.RemoveListener(AddDeadUnit);
        }

        private void AddDeadUnit(PersonContainer unit)
        {
            if (!_deadUnits.Contains(unit)) _deadUnits.Add(unit);
        }

        private void HandleDeath(PersonContainer playerUnit)
        {
            _player = playerUnit;
            _blackScreen.OnFaderIsDone += ExecuteCoroutine;
            _blackScreen.Mute();
            _diedText.Mute();
        }

        private void ExecuteCoroutine()
        {
            _blackScreen.OnFaderIsDone -= ExecuteCoroutine;
            StartCoroutine(RespawnDelay());
        }

        private IEnumerator RespawnDelay()
        {
            yield return new WaitForSeconds(_player.Config.TimeToRespawn);
            UnmuteDiedText();
        }

        private void UnmuteDiedText()
        {
            _diedText.OnFaderIsDone += Death;
            _diedText.Unmute();
        }

        private void Death()
        {
            _diedText.OnFaderIsDone -= Death;
            RespawnEnemies(); // + выжившие
            RespawnPlayer();
        }

        private void RespawnEnemies()
        {
            if (_deadUnits.Count > 0)
                for (int i = 0; i < _deadUnits.Count; i++)
                    RespawnUnit(_deadUnits[i]);
        }

        private void RespawnPlayer()
        {
            RespawnUnit(_player);
            _blackScreen.Unmute();
        }

        private void RespawnUnit(PersonContainer unit)
        {
            unit.IsDeath = false;
            unit.Health.TakeFullHeal();
            unit.Movement.SetBodyType(RigidbodyType2D.Dynamic);
            SpawnUnit(unit);
        }

        private void SpawnUnit(PersonContainer unit) => unit.transform.position =
            unit.IsPlayer ? GetNearestPoint(_player.Config.SavePoints) : unit.StartSpawnPoint;

        private Vector2 GetNearestPoint(List<Vector2> points)
        {
            if (points.Count <= 0) return GlobalConstants.StartPointPosition;

            var length = points.Count;
            var position = _player.transform.position;
            var point = _player.StartSpawnPoint;
            var distance = Vector2.Distance(position, point);

            for (int i = 0; i < length; i++)
            {
                var newPoint = points[i];
                var newDistance = Vector2.Distance(position, newPoint);

                if (newDistance > distance) continue;

                point = newPoint;
                distance = newDistance;
            }

            return point;
        }
    }
}