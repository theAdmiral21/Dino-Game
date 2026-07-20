using System.Collections.Generic;
using UnityEngine;
using Primitives.Unity.UI.Menus;
using Game.Application.UI.Menus.Abstractions;
using Game.Application.UI.Menus;
using Game.Core.UI.Menus.Abstractions;
using Primitives.Menus.Commands;
using Game.Unity.Audio;
using Infrastructure.Unity.Registries;
using Game.Core.Execution;

namespace Primitives.UI.Menus.Unity
{
    [RequireComponent(typeof(MenuRouter))]
    // [RequireComponent(typeof(MenuAudioBridge))]
    public class MenuOrchestrator : SelfRegister<IInitializable<IGameContext>>, IUICommandHandler, IInitializable<IGameContext>
    {
        [SerializeField] private int _priority = 0;
        public int Priority => _priority;
        private MenuController _menuController;
        private MenuRouter _menuRouter;

        private void Awake()
        {
            // Register with the scene boot strapper in order initialize in the correct order
            // SceneInitializationRegistry.Register(this);
            _menuRouter = GetComponent<MenuRouter>();
            base.Awake();


        }

        public void Initialize(IGameContext context)
        {
            // Get the pages in this menu
            Dictionary<IPageToken, IMenuPage> _pageDict = GetMenuPages();

            // Wire the commands
            foreach (IMenuPage page in _pageDict.Values)
            {
                WireCommands(page);
            }


            _menuController = new MenuController(_pageDict);

        }

        public void PostInitialize(IGameContext context)
        {
            // Should I break up these initialize and post initialize methods?
            _menuController.AddServices(context);
        }

        private void OnEnable()
        {
            _menuRouter.OnCommand += HandleCommand;
        }

        private void OnDisable()
        {
            _menuRouter.OnCommand -= HandleCommand;
        }


        public void HandleCommand(IUICommand command)
        {
            // Debug.Log($"Orchestrator got command: {command.CommandType}");
            _menuController.ExecuteCommand(command);
        }

        private Dictionary<IPageToken, IMenuPage> GetMenuPages()
        {
            // Get all of the page builders
            IPageBuilder[] builders = GetComponentsInChildren<IPageBuilder>();

            // Build the pages
            Dictionary<IPageToken, IMenuPage> pageDict = new Dictionary<IPageToken, IMenuPage>();
            foreach (IPageBuilder builder in builders)
            {
                // Build the menu page
                IMenuPage menuPage = builder.Build();
                IPagePresenter pagePresenter = menuPage.PagePresenter;

                // Build the directory
                Debug.Log($"Adding: {menuPage} with presenter: {pagePresenter.Token.PageName}");
                pageDict.TryAdd(pagePresenter.Token, menuPage);

            }
            Debug.Log($"Got {pageDict.Count} pages");

            return pageDict;
        }

        private void WireCommands(IMenuPage page)
        {
            // Wire the buttons
            foreach (IUIElement element in page.Elements)
            {
                element.OnSubmit += HandleCommand;
                element.OnFocus += HandleCommand;
            }
        }


    }
}