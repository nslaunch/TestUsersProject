using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace TestUsersProject.Localization;

public static class TestUsersProjectLocalizationConfigurer
{
    public static void Configure(ILocalizationConfiguration localizationConfiguration)
    {
        localizationConfiguration.Sources.Add(
            new DictionaryBasedLocalizationSource(TestUsersProjectConsts.LocalizationSourceName,
                new XmlEmbeddedFileLocalizationDictionaryProvider(
                    typeof(TestUsersProjectLocalizationConfigurer).GetAssembly(),
                    "TestUsersProject.Localization.SourceFiles"
                )
            )
        );
    }
}
