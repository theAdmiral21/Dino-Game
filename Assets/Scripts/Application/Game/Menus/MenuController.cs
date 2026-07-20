using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using Game.Application.UI.Menus.Abstractions;
using Game.Application.UI.Menus.UICommands;
using Primitives.Menus.Commands;
using System.Text;
using Game.Core.UI.Menus.Abstractions;
using Primitives.Common.Scenes;
using Primitives.Common.Infrastructure;
using Primitives.GameState;
using Game.Core.Scenes;
using Game.Core.Execution;
using Primitives.Input.Enums;
using Game.Core.State.Services;

namespace Game.Application.UI.Menus
{
    public class MenuController
    {
        public IMenuPage CurrentPage => _pageStack.Count > 0 ? _pageStack.Peek() : null;
        private Dictionary<IPageToken, IMenuPage> _pageDict = new Dictionary<IPageToken, IMenuPage>();
        private readonly Stack<IMenuPage> _pageStack = new Stack<IMenuPage>();
        private IUIElement _lastHovered;
        // private bool fired = false;
        private IGameStateServices _gameStateServices;
        private IChangeGameStateService _changeStateService;
        private IRequestQuit _requestQuitService;
        private IChangeSceneService _changeSceneService;
        private IMenuPage _rootPage;
        private StringBuilder _sbDebug;
        public MenuController(Dictionary<IPageToken, IMenuPage> pageDict)
        {
            _pageDict = pageDict;
            _rootPage = _pageDict.Values.FirstOrDefault();
            _pageStack.Push(_rootPage);
            // _rootPage.Enter();
            HidePages();

            _sbDebug = new StringBuilder();
            _sbDebug.AppendLine($"MenuController has the following pages:");
            foreach (IPageToken page in pageDict.Keys)
            {
                _sbDebug.AppendLine($"{pageDict[page].PagePresenter.Token.PageName}");
            }
            Debug.Log(_sbDebug);
        }

        public void AddServices(IGameContext context)
        {
            _gameStateServices = context.GameStateServices;
            _changeStateService = _gameStateServices.ChangeGameState;
            _requestQuitService = context.QuitServices.RequestQuit;

            _gameStateServices.GameStateEvents.OnGameStateChanged += TogglePause;

            _changeSceneService = context.SceneServices.SceneChangeService;

            context.SceneServices.SceneEvents.SceneChangeComplete += HandleSceneStart;
        }

        private void TogglePause(GameState newState)
        {
            if (newState == GameState.InMenu || newState == GameState.Paused)
            {
                EnterStartPage();
            }
        }

        public void ExecuteCommand(IUICommand command)
        {
            switch (command)
            {
                case UIFocusCommand cmd:
                    {
                        if (_lastHovered != null && _lastHovered != cmd.Element)
                        {
                            _lastHovered.Deselect();
                        }
                        _lastHovered = cmd.Element;

                        int ndx = Array.IndexOf(CurrentPage.Elements, cmd.Element);
                        CurrentPage.SetSelection(ndx);
                        // Debug.Log($"Focusing {CurrentPage.CurrentElement}");
                        break;
                    }
                case ChangePageCommand cmd:
                    {
                        Debug.Log($"Got command: {cmd} with token: {cmd.Token.PageName}");
                        SetPage(cmd.Token);
                        break;
                    }
                case BackCommand cmd:
                    {
                        GoToPreviousPage();
                        break;
                    }
                case SubmitCommand cmd:
                    {
                        CurrentPage.CurrentElement.Submit();
                        break;
                    }
                case LocalNavigationCommand cmd:
                    {
                        // Debug.Log($"Got navigation command");
                        if (cmd.Input == UIInput.Select || cmd.Input == UIInput.Back || cmd.Input == UIInput.None)
                        {
                            Debug.LogError($"Local navigation handler got a LocalNavigationCommand with input {cmd.Input}. This is not allowed");
                            return;
                        }
                        if (_lastHovered != null)
                        {
                            _lastHovered.Deselect();
                            _lastHovered = null;
                        }
                        CurrentPage.ChangeSelection(cmd.Input);
                        break;
                    }
                case ChangeSceneCommand cmd:
                    {
                        // Debug.Log("Got scene change command");
                        _changeSceneService.ChangeScene(cmd.RequestedScene);
                        break;
                    }

                case ResumeCommand cmd:
                    {
                        _changeStateService.ChangeGameState(GameState.Gameplay);
                        break;
                    }

                case QuitCommand cmd:
                    {
                        _requestQuitService.RequestQuit();
                        break;
                    }
                case InputIntCommand cmd:
                    {
                        Debug.Log($"How do I route {cmd.IntValue} back to the display?");
                        break;
                    }
            }
        }

        private void HidePages()
        {
            foreach (IMenuPage page in _pageDict.Values)
            {
                page.PagePresenter.Hide();
            }
        }
        public IMenuPage GetPage(IPageToken token)
        {
            return _pageDict[token];
        }

        private void HandleSceneStart(SceneId _) => EnterStartPage();

        /// <summary>
        /// Method for starting the menu tween system. The menu controller enters the first page on construction but doesn't play any transitions. In order to make sure everything works as intended, this method must be called first when entering a new menu with transitions.
        /// </summary>
        public void EnterStartPage()
        {
            // if (!fired)
            // {
            EnterNewPage(CurrentPage);
            // fired = true;
            // }
        }
        public void SetPage(IPageToken token)
        {
            Debug.Log($"Set page {token.PageName}");

            // Check if the request page is on the stack
            if (_pageStack.Contains(GetPage(token)))
            {
                // if it is, route this to a back command.
                GoToPreviousPage();
                return;
            }

            ExitCurrentPage(token);
        }

        private void ExitCurrentPage(IPageToken token)
        {
            // Get the new page
            IMenuPage page = GetPage(token);

            // Play the exit transition and call... Enter new page?
            Debug.Log($"Calling exit fancy on {CurrentPage.PagePresenter.Token.PageName}");
            CurrentPage.ExitFancy(() => FinishExit(page));
        }

        private void EnterNewPage(IMenuPage page)
        {
            // Add the page to the stack
            if (!_pageStack.Contains(page))
            {
                _pageStack.Push(page);
            }
            else
            {
                if (page != _rootPage)
                {
                    Debug.LogError($"Unable to add {page.PagePresenter.Token.PageName} to stack.");
                }
            }
            Debug.Log($"Calling enter fancy on {CurrentPage.PagePresenter.Token.PageName}");
            // Make sure the new page is visible
            CurrentPage.Enter();
            CurrentPage.EnterFancy(FinishEntrance);
        }

        /// <summary>
        /// Method that cleans up anything left over from the enter transition. Is called AFTER the enter transition completes.
        /// </summary>
        private void FinishEntrance()
        {
            // Make sure the new page gets revealed
            // Debug.Log("Evaluate if you really need this method. It's only handy for things that need to happen after the transition. So maybe if you want to toggle input off during the transition it could help?");

        }
        /// <summary>
        /// Method that cleans up anything left over from the exit transition before entering the new page. Runs AFTER the exit transition and BEFORE the enter transition.
        /// </summary>
        /// <param name="page"></param>
        private void FinishExit(IMenuPage page)
        {
            // Debug.Log("Cleaning up exit");
            CurrentPage.Exit();
            // _pageStack.Push(page);
            EnterNewPage(page);
        }
        /// <summary>
        /// Method that cleans up after an exit fancy call. This method differs from FinishExit in that the page passed to FinishBack is the page you want to exit.
        /// </summary>
        /// <param name="page"></param>
        private void FinishBack(IMenuPage page)
        {
            page.Exit();
            EnterNewPage(CurrentPage);
        }

        public void GoToPreviousPage()
        {
            if (_pageStack.Count == 1) return;
            // Remember, removing a page from the stack automatically updates CurrentPage.
            if (_pageStack.TryPop(out IMenuPage prevPage))
            {
                // Debug.Log($"Going back from {prevPage.PagePresenter.Token}");
                prevPage.ExitFancy(() => FinishBack(prevPage));
            }
        }

        public void ResetStack()
        {
            _pageStack.Clear();
        }
    }
}