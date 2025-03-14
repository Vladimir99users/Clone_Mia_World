using Assets.Scripts.Controller;
using Assets.Scripts.Input;
using UnityEngine;

namespace Assets.Scripts.GlobalScripts
{
    public class SceneInitializer : MonoBehaviour
    {
        [SerializeField] private ItemController itemController;
        [SerializeField] private SceneController sceneController;

        private void Start()
        {
            OnStart();
        }
        private void OnStart()
        {
            IManipulating manipulating = GetManipulator();
            InitializeController(manipulating);
        }
        private IManipulating GetManipulator()
        {
            var inputService = new InputService();
            inputService.Initialize();
            return inputService;
        }
        private void InitializeController(IManipulating manipulating)
        {
            itemController.Initialize(manipulating);
            sceneController.Initialize(manipulating);
        }

    }
}
