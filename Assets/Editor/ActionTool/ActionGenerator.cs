using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class ActionGenerator
    {
        private const string REQUEST_PATH = @"C:\Users\Thomas\Documents\GameDev\Dino-Game\Assets\Scripts\Core\Movement\ActionRequests\";
        private const string RESULT_PATH = @"C:\Users\Thomas\Documents\GameDev\Dino-Game\Assets\Scripts\Core\Movement\ActionResults\";

        private const string RULE_PATH = @"C:\Users\Thomas\Documents\GameDev\Dino-Game\Assets\Scripts\Core\Movement\Rules\";

        private const string DISPATCH_PATH = @"C:\Users\Thomas\Documents\GameDev\Dino-Game\Assets\Scripts\Application\Movement\Dispatchers";

        [MenuItem("Tools/Actions/Create New Action")]
        public static void OpenWindow()
        {
            ActionGeneratorWindow.Open();
        }

        public static void CreateAction(string actionName, bool createRequest, bool createResult, bool createRule)
        {
            actionName = actionName.Trim();
            if (string.IsNullOrWhiteSpace(actionName))
            {
                Debug.LogError("Invalid action name");
                return;
            }

            if (createRequest)
            {
                WriteRequestTemplate(actionName);
                WriteDispatchTemplate(actionName);
            }

            if (createResult)
                WriteResultTemplate(actionName);

            if (createRule)
                WriteRuleTemplate(actionName);

            AssetDatabase.Refresh();
        }

        private static void WriteRequestTemplate(string actionName)
        {
            string filePath = $"{REQUEST_PATH}{actionName}Request.cs";
            VerifyUnique(filePath);
            File.WriteAllText(filePath, TemplateProvider.GetRequestTemplate(actionName));
        }

        private static void WriteResultTemplate(string actionName)
        {
            string filePath = $"{RESULT_PATH}{actionName}Result.cs";
            VerifyUnique(filePath);
            File.WriteAllText(filePath, TemplateProvider.GetResultTemplate(actionName));
        }

        private static void WriteRuleTemplate(string actionName)
        {
            string filePath = $"{RULE_PATH}{actionName}Rules.cs";
            VerifyUnique(filePath);
            File.WriteAllText(filePath, TemplateProvider.GetRuleTemplate(actionName));
        }

        private static void WriteDispatchTemplate(string actionName)
        {
            string filePath = $"{DISPATCH_PATH}/{actionName}Dispatcher.cs";
            VerifyUnique(filePath);
            File.WriteAllText(filePath, TemplateProvider.GetDispatchTemplate(actionName));
        }

        private static void VerifyUnique(string filePath)
        {
            if (File.Exists(filePath))
            {
                throw new IOException();
            }
        }
    }
}