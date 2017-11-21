using Symbioz.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Guilds
{
    public class ContextGuildEmblem
    {
        public ushort SymbolShape
        {
            get;
            set;
        }
        public int SymbolColor
        {
            get;
            set;
        }
        public sbyte BackgroundShape
        {
            get;
            set;
        }
        public int BackgroundColor
        {
            get;
            set;
        }
        public static ContextGuildEmblem New(GuildEmblem guildEmblem)
        {
            return new ContextGuildEmblem()
            {
                BackgroundColor = guildEmblem.backgroundColor,
                BackgroundShape = guildEmblem.backgroundShape,
                SymbolColor = guildEmblem.symbolColor,
                SymbolShape = guildEmblem.symbolShape,
            };
        }
        public GuildEmblem ToGuildEmblem()
        {
            return new GuildEmblem(SymbolShape, SymbolColor, BackgroundShape, BackgroundColor);
        }
        public override bool Equals(object obj)
        {
            var emblem = obj as ContextGuildEmblem;

            if (emblem != null)
            {
                return emblem.BackgroundColor == BackgroundColor && emblem.BackgroundShape == BackgroundShape
                    && emblem.SymbolColor == SymbolColor && emblem.SymbolShape == SymbolShape;
            }
            else
            {
                return false;
            }
        }
    }
}
