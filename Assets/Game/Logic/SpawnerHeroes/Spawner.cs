using System.Collections.Generic;
using UnityEngine;

namespace Game.Logic.SpawnerHeroes
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Heroes.Heroes[] _heroes;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private int _countHeroes = 2;

        private readonly List<Heroes.Heroes> _conteinerHeroes = new();

        public IEnumerable<Heroes.Heroes> ConteinerHeroes => _conteinerHeroes;

        private void Awake()
        {
            CreateRandomHero();
            SetTargetHero();
        }

        private void CreateRandomHero()
        {
            for (var i = 0; i < _countHeroes; i++)
            {
                var randomIndex = Random.Range(0, _heroes.Length);
                var randomHero = Instantiate(_heroes[randomIndex], _spawnPoints[i].position, Quaternion.identity);
                _conteinerHeroes.Add(randomHero);
            }
        }

        private void SetTargetHero()
        {
            if (_conteinerHeroes.Count == _countHeroes)
            {
                _conteinerHeroes[0].InitTarget(_conteinerHeroes[1]);
                _conteinerHeroes[1].InitTarget(_conteinerHeroes[0]);
            }
        } //Не  знаю я как сделать правильно соответственно хардкод 
    }
}