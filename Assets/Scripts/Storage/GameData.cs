using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Storage
{
    [Serializable]
    internal class GameData
    {
        public Vector2 playerPosition;
        public GameData()
        {
            playerPosition = Vector2.zero;
        }
    }
}
