﻿using System.IO;
using JetBrains.Annotations;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Daemon.CSharp.Errors;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.FeaturesTestFramework.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.TestFramework;
using XmlDocInspections.Plugin.Settings;

namespace XmlDocInspections.Plugin.Tests.Integrative.Highlighting
{
    [TestNetFramework46]
    public abstract class MissingXmlDocHighlightingTestsBase : CSharpHighlightingTestBase
    {
        protected override void DoTestSolution([NotNull] params string[] fileSet)
        {
            ExecuteWithinSettingsTransaction(settingsStore =>
            {
                RunGuarded(() => MutateSettings(settingsStore));
                base.DoTestSolution(fileSet);
            });
        }

        protected abstract void MutateSettings(IContextBoundSettingsStore settingsStore);

        protected static void EnableAllXmlDocOptions(IContextBoundSettingsStore settingsStore)
        {
            settingsStore.SetValue((XmlDocInspectionsSettings s) => s.TypeAccessibility, AccessibilitySettingFlags.All);
            settingsStore.SetValue((XmlDocInspectionsSettings s) => s.TypeMemberAccessibility, AccessibilitySettingFlags.All);
        }

        protected override bool HighlightingPredicate(
            IHighlighting highlighting,
            [NotNull] IPsiSourceFile sourceFile,
            [CanBeNull] IContextBoundSettingsStore settingsStore
        ) => base.HighlightingPredicate(highlighting, sourceFile, settingsStore) &&
             !(highlighting is RedundantDisableWarningCommentWarning);
    }

    public abstract class MissingXmlDocHighlightingDirectoryTestsBase : MissingXmlDocHighlightingTestsBase
    {
        protected override string RelativeTestDataPath => "Highlighting";

        protected override string GetGoldTestDataPath([NotNull] string fileName) => base.GetGoldTestDataPath(GetRelativeGoldFilePath(fileName));

        protected virtual string GetRelativeGoldFilePath(string fileName) => Path.Combine(GetType().Name, fileName);
    }
}
