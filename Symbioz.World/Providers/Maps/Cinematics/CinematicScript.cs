using NLua;
using Symbioz.Core;
using Symbioz.World.Models.Entities;
using Symbioz.World.Providers.Criterias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Maps.Cinematics
{
    public class CinematicScript
    {
        Logger logger = new Logger();

        public int MapId
        {
            get;
            set;
        }
        public long NpcId
        {
            get;
            set;
        }
        private bool IsMapScript
        {
            get
            {
                return MapId != -1;
            }
        }
        private bool IsNpcScript
        {
            get
            {
                return NpcId != -1;
            }
        }
        public string Author
        {
            get;
            set;
        }
        private Lua Lua
        {
            get;
            set;
        }
        public short[] ObjectiveDones
        {
            get;
            private set;
        }
        public short[] ObjectivesNotDone
        {
            get;
            private set;
        }
        public string Criteria
        {
            get;
            private set;
        }
        public bool Reachable(Character character)
        {
            foreach (var objective in ObjectiveDones)
            {
                if (character.HasReachObjective(objective))
                    continue;
                else
                    return false;
            }
            foreach (var objective in ObjectivesNotDone)
            {
                if (!character.HasReachObjective(objective))
                    continue;
                else
                    return false;
            }

            return CriteriaProvider.EvaluateCriterias(character.Client, Criteria);
        }
        public CinematicScript(Lua lua)
        {
            this.Author = lua.GetString("author");
            this.MapId = int.Parse(lua.GetString("mapId"));
            this.NpcId = long.Parse(lua.GetString("npcId"));
            this.Criteria = lua.GetString("criteria");
            var table = lua.GetTable("doneObjectives").Values;

            var list = new List<short>();

            foreach (Double value in table)
            {
                list.Add((short)value);
            }

            ObjectiveDones = list.ToArray();


            table = lua.GetTable("notDoneObjectives").Values;

            list = new List<short>();

            foreach (Double value in table)
            {
                list.Add((short)value);
            }

            ObjectivesNotDone = list.ToArray();

            this.Lua = lua;
        }

        public void TalkToNpc(Character character)
        {
            CallFunction(character, "TalkToNpc");
        }
        private void CallFunction(Character character, string name)
        {
            try
            {

                Lua["env"] = new CinematicEnvironment(character, this);
              

                try
                {
                    LuaFunction functionMain = Lua.GetFunction(name);
                    functionMain.Call();
                }
                catch (Exception ex)
                {
                    logger.Error("LUA error: "+ex.ToString());
                }
            }
            catch (Exception ex)
            {
                logger.Error("LUA error: " + ex.ToString());
                character.Client.Disconnect();
            }
        }

        public void OnCriteriaWrong(Character character)
        {
            CallFunction(character, "CriteriaWrong");
        }
    }
}
