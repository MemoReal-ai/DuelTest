using System.Collections.Generic;
using UnityEngine;
using _project.Logic.Heroes;


namespace _project.Logic.Heroes
{
   public class Spawner : MonoBehaviour
   {
      [SerializeField] private  Heroes[] heroes;
      [SerializeField] private Transform[] spawnPoints;
      [SerializeField] private  int countHeroes = 2;
      
      private List<Heroes> _conteinerHeroes = new List<Heroes>();
      
      public IEnumerable<Heroes> ConteinerHeroes => _conteinerHeroes;
      private void Awake()
      {
         CreateRandomHero();
         SetTargetHero();
      }

      private void  CreateRandomHero()
      {
         for (int i = 0; i < countHeroes; i++)
         { var randomIndex = Random.Range(0, heroes.Length); 
            Heroes randomHero = Instantiate(heroes[randomIndex], spawnPoints[i].position, Quaternion.identity);
            _conteinerHeroes.Add(randomHero);
            
         }
      }

      private void SetTargetHero()
      {
         if (_conteinerHeroes.Count==countHeroes)
         {
            _conteinerHeroes[0].InitTarget(_conteinerHeroes[1]);
            _conteinerHeroes[1].InitTarget(_conteinerHeroes[0]);
         }
      }//Не  знаю я как сделать правильно соответственно хардкод 
   }
}
