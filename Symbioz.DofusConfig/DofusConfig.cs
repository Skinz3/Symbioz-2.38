using Symbioz.DofusConfig.LineParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.DofusConfig
{
    /// <summary>
    /// Représente le fichier config.xml dans le répértoire /app/ du client Dofus
    /// </summary>
    public class DofusConfig
    {
        /// <summary>
        /// Emplacement actuel du fichier de configuration.
        /// </summary>
        public string Path
        {
            get;
            private set;
        }
        public Dictionary<int, string> Lines;

        [DofusConfigField("nom de la clef")]
        public ConfigLineField DemoField;
        /// <summary>
        /// Mode salon
        /// </summary>
        [DofusConfigField("maps.encryptionKey")]
        public ConfigLineField MapsEncryptionKey;

        [DofusConfigField("eventMode")]
        public ConfigLineField EventMode;

        [DofusConfigField("characterCreationAllowed")]
        public ConfigLineField CharacterCreationAllowed;

        [DofusConfigField("buildType")]
        public ConfigLineField BuildType;

        [DofusConfigField("automMaximize")]
        public ConfigLineField AutoMaximize;
        /// <summary>
        /// Constantes pour les Chemins basiques
        /// </summary>
        [DofusConfigField("root.path")]
        public ConfigLineField RootPath;

        [DofusConfigField("content.path")]
        public ConfigLineField ContentPath;

        [DofusConfigField("ui")]
        public ConfigLineField Ui;

        [DofusConfigField("mod.path")]
        public ConfigLineField ModPath;

        [DofusConfigField("theme.path")]
        public ConfigLineField ThemePath;

        [DofusConfigField("gfx.path")]
        public ConfigLineField GfxPath;

        [DofusConfigField("script.path")]
        public ConfigLineField ScriptPath;

        [DofusConfigField("data.path.root")]
        public ConfigLineField DataPathRoot;

        [DofusConfigField("audio")]
        public ConfigLineField Audio;
        /// <summary>
        /// Constantes pour l'accès au serveur
        /// </summary>
        /// <param name="path"></param>
        [DofusConfigField("connection.useSniffer")]
        public ConfigLineField UseSniffer;

        [DofusConfigField("connection.snifferHost")]
        public ConfigLineField SnifferHost;

        [DofusConfigField("connection.snifferPort")]
        public ConfigLineField SnifferPort;

        [DofusConfigField("connection.host")]
        public ConfigLineField ConnectionHost;

        [DofusConfigField("connection.host.signature")]
        public ConfigLineField ConnectionHostSignature;

        [DofusConfigField("connection.port")]
        public ConfigLineField ConnectionPort;
        /// <summary>
        ///  Constantes pour les interfaces  
        /// </summary>
        [DofusConfigField("ui.asset")]
        public ConfigLineField UIAsset;

        [DofusConfigField("ui.common")]
        public ConfigLineField UICommon;

        [DofusConfigField("ui.common.css")]
        public ConfigLineField UICommonCSS;

        [DofusConfigField("ui.common.test")]
        public ConfigLineField UICommonTest;

        [DofusConfigField("ui.common.logo")]
        public ConfigLineField UICommonLogo;

        [DofusConfigField("ui.common.fonts")]
        public ConfigLineField UICommonFonts;

        [DofusConfigField("ui.common.themes")]
        public ConfigLineField UICommonThemes;

        [DofusConfigField("ui.common.button")]
        public ConfigLineField UICommonButton;

        [DofusConfigField("ui.common.radio")]
        public ConfigLineField UICommonRadio;

        [DofusConfigField("ui.common.border")]
        public ConfigLineField UICommonBorder;

        [DofusConfigField("ui.common.texture")]
        public ConfigLineField UICommonTexture;

        [DofusConfigField("ui.common.checkbox")]
        public ConfigLineField UICommonCheckbox;

        [DofusConfigField("ui.common.scrollbar")]
        public ConfigLineField UICommonScrollbar;

        [DofusConfigField("ui.common.texture.spells")]
        public ConfigLineField UICommonTextureSpells;

        [DofusConfigField("ui.common.texture.icons")]
        public ConfigLineField UICommonTextureIcons;

        [DofusConfigField("ui.gfx.artworks")]
        public ConfigLineField UIGfxArtworks;

        [DofusConfigField("ui.definitions")]
        public ConfigLineField UIDefinitions;

        [DofusConfigField("ui.definitions.items")]
        public ConfigLineField UIDefinitionsItems;

        [DofusConfigField("ui.definitions.tooltips")]
        public ConfigLineField UIDefinitionsTooltips;

        public DofusConfig(string path)
        {
            Path = path;
            Lines = new Dictionary<int, string>();
            var lines = File.ReadAllLines(path);

            for (int i = 0; i < lines.Length; i++)
            {
                Lines.Add(i, lines[i]);
            }

            var fields = this.GetType().GetFields();

            foreach (var line in Lines)
            {
                if (ConfigLineField.IsValid(line.Value))
                {
                    ConfigLineField configLine = new ConfigLineField(this, line.Value, line.Key);

                    FieldInfo field = GetField(configLine.Key);

                    if (field != null)
                    {
                        field.SetValue(this, configLine);
                    }
                    else
                    {
                        Console.WriteLine("Cannot find field with key in the DofusConfig object: " + configLine.Key);
                    }
                }
            }
        }

        private FieldInfo GetField(string entry)
        {
            foreach (var field in this.GetType().GetFields())
            {
                DofusConfigFieldAttribute attribute = field.GetCustomAttribute<DofusConfigFieldAttribute>();

                if (attribute != null && attribute.FieldName == entry)
                    return field;
            }

            return default(FieldInfo);
        }

        public void Save()
        {
            File.WriteAllLines(Path, Lines.Values);
        }
    }
}
