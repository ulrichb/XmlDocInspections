using System;
using System.Linq;
using JetBrains.Application;
using JetBrains.Application.DataContext;
using JetBrains.Application.Parts;
using JetBrains.Application.Settings;
using JetBrains.Application.Settings.Extentions;
using JetBrains.Diagnostics;
using JetBrains.Lifetimes;
using JetBrains.Metadata.Reader.Impl;
using JetBrains.ProjectModel.Settings.Cache;
using XmlDocInspections.Plugin.Settings;

namespace XmlDocInspections.Plugin
{
    [ShellComponent(Instantiation.DemandAnyThreadSafe)] // TODO: Check this out
    public class XmlDocInspectionsSettingsCache : ICachedSettingsReader<CachedXmlDocInspectionsSettings>
    {
        private readonly Lifetime _lifetime;
        private readonly SettingsCacheManager _settingsCacheManager;
        private readonly ISettingsOptimization _settingsOptimization;
        private readonly SettingsKey _settingsKey;

        public XmlDocInspectionsSettingsCache(
            Lifetime lifetime,
            SettingsCacheManager settingsCacheManager,
            ISettingsOptimization settingsOptimization,
            ISettingsStore settingsStore)
        {
            _lifetime = lifetime;
            _settingsCacheManager = settingsCacheManager;
            _settingsOptimization = settingsOptimization;
            _settingsKey = settingsStore.Schema.GetKey<XmlDocInspectionsSettings>();
        }

        public CachedXmlDocInspectionsSettings GetCachedSettings(Func<Lifetime, DataContexts, IDataContext> dataContext)
        {
            // Note that at the moment SettingsCacheManager seems to only support solution-specific caches, not project-specific.

            return _settingsCacheManager.GetCache(dataContext).GetData(_lifetime, this).NotNull();
        }

        SettingsKey ICachedSettingsReader<CachedXmlDocInspectionsSettings>.KeyExposed => _settingsKey;

        CachedXmlDocInspectionsSettings ICachedSettingsReader<CachedXmlDocInspectionsSettings>.ReadData(
            Lifetime _,
            IContextBoundSettingsStore store)
        {
            var settings = store.GetKey<XmlDocInspectionsSettings>(_settingsOptimization);

            var includeAttributeClrTypeNames =
                (from part in settings.IncludeAttributeFullNames.Split(',')
                 let fullName = part.Trim()
                 where !string.IsNullOrEmpty(fullName)
                 select new ClrTypeName(fullName)).ToList();

            return new CachedXmlDocInspectionsSettings(settings, includeAttributeClrTypeNames);
        }
    }
}
