using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RX;
using Scrips.GameScene.Info;
using UnityEngine;
using WB.DI;

namespace Scrips.GameScene.Data
{
    public class SceneData:ISceneInfo
    {
        [Inject]
        public SceneData()
        {
            Debug.Log("SceneData instantiated");
        }

        public void Initialize(StageBook stageBook)
        {
            StageBook = stageBook;
            ended = false;
            _Enemies = new List<Enemy>();
            additionalEffects = new HashSet<SceneEffect>();
        }
        
        
        public StageBook StageBook { get; private set; }
        public float RemainTime { get; set; }
        private Subject<NoMean> _gameSet { get; } = new Subject<NoMean>(NoMean.Default);
        public IObservable<NoMean> GameSet => _gameSet;
        
        private bool ended;
        public void NewScene(List<Enemy> enemies)
        {
            _Enemies = new List<Enemy>(enemies);
        }

        public void AddEnemy(Enemy enemy)
        {
            _Enemies.Add(enemy);
        }

        private List<Enemy> _Enemies { get; set; } = new List<Enemy>();
        public ReadOnlyCollection<IEnemyInfo> Enemies => _Enemies.ConvertAll(e=>e as IEnemyInfo).AsReadOnly();
        public HashSet<SceneEffect> additionalEffects = new HashSet<SceneEffect>();
        private HashSet<SceneEffect> _SceneEffects
        {
            get
            {
                var effects = new HashSet<SceneEffect>();
                foreach (var enemy in _Enemies)
                {
                    if (enemy.Book.SceneEffect != SceneEffect.None)
                    {
                        effects.Add(enemy.Book.SceneEffect);
                    }
                }

                foreach (var e in additionalEffects)
                {
                    effects.Add(e);
                }

                return effects;
            }
        }
        public ReadOnlyCollection<SceneEffect> SceneEffects => _SceneEffects.ToList().AsReadOnly();

        private Subject<NoMean> _nextScene = new Subject<NoMean>(NoMean.Default);
        public IObservable<NoMean> NextScene => _nextScene;

        public void Remove(Enemy enemy)
        {
            if (_Enemies.Remove(enemy))
            {
                if (_Enemies.Count == 0)
                {
                    additionalEffects.Clear();
                    _nextScene.OnNext(NoMean.Default);
                }
            }
        }

        public void OnGameSet()
        {
            if (ended) return;
            _gameSet.OnNext(NoMean.Default);
            ended = true;
        }

        public void AddEffect(SceneEffect effect)
        {
            additionalEffects.Add(effect);
        }
    }
}
