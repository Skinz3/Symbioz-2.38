using SSync.IO;
using Symbioz.Core;
using Symbioz.World.Records.Items;
using System;
using System.Collections.Generic;
using Symbioz.ORM;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Core.DesignPattern.StartupEngine;
using System.IO;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Entities.Look;

namespace Symbioz.World.Modules
{
    /// <summary>
    /// Represente une ressource Item du site 
    /// http://www.dofus.tools/
    /// </summary>
    public class MA3Item
    {
        public uint Id;

        public short TypeId;

        public string Name;

        public short Level;

        public uint Skin;

        public uint IconId;

        public string Look;

        public bool IsCameleon;

        public void Deserialize(BigEndianReader reader)
        {
            this.Id = reader.ReadUInt();
            this.TypeId = reader.ReadShort();
            this.Name = reader.ReadUTF();
            this.Level = reader.ReadShort();
            this.IconId = reader.ReadUInt();
            this.Skin = reader.ReadUInt();
            this.Look = reader.ReadUTF();
        }
    }
    public class MA3Mount
    {

        public short Id;

        public string Name;

        public string Look;

        public void Deserialize(BigEndianReader reader)
        {
            this.Id = reader.ReadShort();
            this.Name = reader.ReadUTF();
            this.Look = reader.ReadUTF();
        }
    }
    /// <summary>
    /// http://www.dofus.tools/myAvatar3/assets/data/Items.ma3
    /// assets/emotes/
    /// http://www.dofus.tools/myAvatar3/assets/data/Mounts.ma3
    /// Permet de récuperer l'appearanceId des items de dofus.
    /// </summary>
    public class ItemSkinModule
    {
        public static string ItemsPath = Environment.CurrentDirectory + "/Items.ma3";


        static Logger logger = new Logger();


        //  [StartupInvoke("Item SkinModule", StartupInvokePriority.Modules)] 
        public static void SynchronizeItems()
        {
            BigEndianReader reader = new BigEndianReader(File.ReadAllBytes(ItemsPath));

            List<MA3Item> items = new List<MA3Item>();

            while (reader.BytesAvailable > 0)
            {
                MA3Item item = new MA3Item();
                item.Deserialize(reader);
                items.Add(item);
            }

            foreach (var item in items)
            {
                ItemRecord record = ItemRecord.GetItem((ushort)item.Id);

                if (record != null && record.AppearanceId != item.Skin && item.Skin != 0)
                {
                    if (record.Weapon)
                    {
                        var weaponRecord = WeaponRecord.GetWeapon((ushort)item.Id);
                        weaponRecord.AppearanceId = (ushort)item.Skin;
                        weaponRecord.UpdateInstantElement();

                    }
                    else
                    {
                        record.AppearanceId = (ushort)item.Skin;
                        record.UpdateInstantElement();
                    }
                    logger.Gray("Fixed: " + record.Name);
                }

            }
        }



        public static string MountsPath = Environment.CurrentDirectory + "/Mounts.ma3";

        [StartupInvoke("Mount SkinModule", StartupInvokePriority.Modules)]
        public static void SynchnronizeMounts()
        {
            BigEndianReader reader = new BigEndianReader(File.ReadAllBytes(MountsPath));

            while (reader.BytesAvailable > 0)
            {
                MA3Mount mount = new MA3Mount();
                mount.Deserialize(reader);

                var record = MountRecord.GetMount(mount.Id);

                if (record == null)
                {

                    var itemRecord = ItemRecord.Items.Find(x => x.Name == mount.Name);
                    MountRecord newRecord = new MountRecord(mount.Id, mount.Name, ContextActorLook.Parse(mount.Look), itemRecord.Id, new List<Models.Effects.EffectInstance>());
                    newRecord.AddInstantElement();
                    logger.Gray(mount.Name + " added to mount records.");
                }

            }
        }


    }
}
