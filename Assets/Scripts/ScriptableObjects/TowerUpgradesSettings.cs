using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDeffence
{
    //[CreateAssetMenu(fileName = "TowersSettings", menuName = "TowerConfig")]
    public class Settings : ScriptableObject
    {
        [SerializeField]
        private string _spritePath = "Sprites//";
        [SerializeField]
        private string _audioPath = "Audio//";

        [SerializeField]
        private List<string> _sprites;

        public Sprite LoadSpriteByID(string id)
        {
            /*
            TowerModification mod = default;
            switch (mod.Id)
            {
                case "_buff_line_one_monkey_sec_in_damage":
                    break;
                case "_buff_line_one_monkey_sec_in_damage1":
                    break;
                case "_buff_line_one_monkey_sec_in_damage2":
                    break;
            }
            */

            return Resources.Load<Sprite>(_spritePath + id);
        }

        public AudioClip LoadAudioClipByID(string id)
        {
            return Resources.Load<AudioClip>(_audioPath + id);
        }
    }
}