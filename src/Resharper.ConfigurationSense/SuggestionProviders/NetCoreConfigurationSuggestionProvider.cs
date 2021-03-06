﻿using JetBrains.ReSharper.Feature.Services.CodeCompletion.Infrastructure;
using JetBrains.ReSharper.Feature.Services.CodeCompletion.Infrastructure.LookupItems;
using JetBrains.ReSharper.Feature.Services.CSharp.CodeCompletion.Infrastructure;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.Util;

using Resharper.ConfigurationSense.Components;
using Resharper.ConfigurationSense.Constants;
using Resharper.ConfigurationSense.Extensions;

namespace Resharper.ConfigurationSense.SuggestionProviders
{
    [Language(typeof(CSharpLanguage))]
    public class NetCoreConfigurationSuggestionProvider : ItemsProviderOfSpecificContext<CSharpCodeCompletionContext>
    {
        private readonly IGenericSettingsProvider _settingsProvider;

        public NetCoreConfigurationSuggestionProvider(IGenericSettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
        }

        protected override bool AddLookupItems(CSharpCodeCompletionContext context, IItemsCollector collector)
        {
            var lookupItems = _settingsProvider.GetJsonSettingsLookupItems(context, JsonSettingType.Value);
            if (!lookupItems.Any())
            {
                return false;
            }

            foreach (var lookupItem in lookupItems)
            {
                collector.Add(lookupItem);
            }

            return true;
        }

        protected override bool IsAvailable(CSharpCodeCompletionContext context)
        {
            return context.IsInsideAccessorType(ClrTypeConstants.NetCoreConfiguration)
                   || context.IsInsideMethodPath(ClrTypeConstants.NetCoreGetValue);
        }
    }
}
