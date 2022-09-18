using System;
using System.Collections.Generic;
using System.Linq;
using Taiga.Core.Character.Placement;
using Taiga.Core.Map;
using UnityEngine;
using UnityEngine.Assertions;

namespace Taiga.Core.Character.Attack
{
    public class Area2x2_Attack_Strategy : Area_Attack_Strategy
    {
        public Area2x2_Attack_Strategy(CharacterEntity character, int attackPoint)
            : base(character, attackPoint, 2)
        {
        }
    }
}