using DG.Tweening;
using Entitas;
using Taiga.Core;
using Taiga.Core.CharacterFactory;
using Taiga.Core.GameRound;
using Taiga.Core.Player;
using Taiga.Core.Unity.Demo.Providers;
using Taiga.Core.Unity.Preset;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Taiga
{
    public class GameInitializeSystem : IInitializeSystem
    {

        GameContext game;

        public GameInitializeSystem(Contexts contexts)
        {
            this.game = contexts.game;
        }

        public void Initialize()
        {
           //SceneManager.LoadScene("Taiga/Assets/Scenes/GameCenter01", LoadSceneMode.Additive);

            var gameDemoPreset = game.GetProvider<IGameDemoPreset>();
            string character1 = null;
            string character2 = null;

            if (gameDemoPreset.Mode == 1)
            {
                character1 = "kitagawa kenji";
                character2 = "tsuji nomoru";
            }
            else if (gameDemoPreset.Mode == 2)
            {
                character1 = "sarashi taiga";
                // character1 = "inoue masaru";
                character2 = "takahashi asaka";
            }
            else if (gameDemoPreset.Mode == 3)
            {
                character1 = "kuroki motoki";
                character2 = "arashi taiga";
            }
            else if (gameDemoPreset.Mode == 4)
            {
                //character1 = "M08";
                // character1 = "N16";
                // character1 = "aragaki";
                // character1 = "M04";
                // character2 = "M06";

                character1 = "M01";
                character2 = "M05";

                // character1 = "S01";
                // character2 = "S04";

                // character2 = "ueno yuji";
                //character2 = "inoue masaru";
            }
            else if (gameDemoPreset.Mode == 5)
            {
                character1 = "arashi taiga";
                character2 = "kitagawa kenji";
            }
            else
            {
                character1 = "ueno yuji";
                character2 = "aji";
            }

            Debug.LogFormat(">>game_demo_mode<< {0}", gameDemoPreset.Mode);

            game.AsPlayerContext()
                .CreatePlayer(0);
            game.AsPlayerContext()
                .CreatePlayer(1);

            game.AsCharacterFactoryContext().Create(
                ownerPlayerId: 0,
                architypeId: character1,
                level: 1,
                position: new Vector2Int(7, 1),
                direction: MapDirection.North
            );

            game.AsCharacterFactoryContext().Create(
                ownerPlayerId: 0,
                architypeId: character2,
                level: 2,
                position: new Vector2Int(6, 1),
                direction: MapDirection.North
            );


            if (gameDemoPreset.Mode == 4)
            {
                character1 = "kitagawa kenji";
                character2 = "tsuji nomoru";
                
                Debug.Log(">>create_enemies<<");

                // game.AsCharacterFactoryContext().Create(
                //     ownerPlayerId: 1,
                //     architypeId: "kitagawa kenji",
                //     level: 2,
                //     position: new Vector2Int(4, 3),
                //     direction: MapDirection.South
                // );

                game.AsCharacterFactoryContext().Create(
                    ownerPlayerId: 1,
                    architypeId: "M04",
                    level: 2,
                    position: new Vector2Int(1, 4),
                    direction: MapDirection.South
                );

                game.AsCharacterFactoryContext().Create(
                    ownerPlayerId: 1,
                    architypeId: "M09",
                    level: 2,
                    position: new Vector2Int(1, 3),
                    direction: MapDirection.South
                );

                game.AsCharacterFactoryContext().Create(
                    ownerPlayerId: 1,
                    architypeId: "M10",
                    level: 2,
                    position: new Vector2Int(1, 2),
                    direction: MapDirection.South
                );
            }
            //game.AsCharacterFactoryContext().Create(
            //    ownerPlayerId: 0,
            //    architypeId: "aji",
            //    level: 3,
            //    position: new Vector2Int(3, 1),
            //    direction: MapDirection.North
            //);

            //game.AsCharacterFactoryContext().Create(
            //    ownerPlayerId: 1,
            //    architypeId: "aji",
            //    level: 2,
            //    position: new Vector2Int(1, 7),
            //    direction: MapDirection.South
            //);

            game.AsGameRoundContext()
                .Start();
        }
    }

}
