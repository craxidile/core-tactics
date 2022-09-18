using System;
using System.Collections.Generic;
using System.Linq;
using Taiga.Core.Character.Placement;
using Taiga.Core.Map;
using UnityEngine;
using UnityEngine.Assertions;

namespace Taiga.Core.Character.Attack
{
    public class Area5x5_Attack_Strategy : Area_Attack_Strategy
    {
        public Area5x5_Attack_Strategy(CharacterEntity character, int attackPoint)
            : base(character, attackPoint, 5)
        {
        }
    }
}