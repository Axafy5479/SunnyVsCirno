using UnityEngine;

namespace Scrips.GameScene.Info
{

    [CreateAssetMenu(menuName = "Enemy", fileName = "Enemy")]
    public class EnemyBook : ScriptableObject
    {
        [SerializeField] private int enemyId;


        [SerializeField] private string enemyName;
        [SerializeField] private int hp = 1;
        [SerializeField,TextArea(3,7)] private string description;
        [SerializeField] private Sprite texture;
        [SerializeField] private SceneEffect sceneEffect = SceneEffect.None;
        [SerializeField] private int point = 100;
        [SerializeField] private Color charaColor;

        [SerializeField] private AudioClip killSe;

        public AudioClip KillSe => killSe;

        public Color CharaColor => charaColor;

        public string EnemyName => enemyName;

        public int Hp => hp;

        public string Description => description;

        public Sprite Texture => texture;

        public SceneEffect SceneEffect => sceneEffect;
        
        public int Point => point;
        
        public int EnemyId
        {
            get => enemyId;
        }
    }


}
