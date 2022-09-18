using UnityEngine;

namespace Taiga
{
    public class GameRunner : MonoBehaviour
    {
        GameController gameController;

        private void Awake()
        {
            var contexts = Contexts.sharedInstance;
            gameController = new GameController(contexts);
        }

        void Start()
        {
            gameController.Initialize();
        }

        void Update()
        {
            gameController.Execute();
        }

        void OnDestroy()
        {
            gameController.TearDown();
        }
    }
}
