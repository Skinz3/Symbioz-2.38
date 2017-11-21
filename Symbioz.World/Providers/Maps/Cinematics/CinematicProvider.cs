using NLua;
using Symbioz.Core;
using Symbioz.Core.DesignPattern;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.World.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Symbioz 2.38 13/06/2017
/// </summary>
namespace Symbioz.World.Providers.Maps.Cinematics
{
    /// <summary>
    /// Provide an instance to host each lua script.
    /// </summary>
    public class CinematicProvider : Singleton<CinematicProvider>
    {
        /// <summary>
        /// Environment.CurrentDirectory + Path
        /// </summary>
        public const string Path = "/Cinematics/";

        /// <summary>
        /// Lua extension
        /// </summary>
        public const string Extension = ".lua";

        /// <summary>
        /// A logger to log infos in the console
        /// </summary>
        Logger logger = new Logger();

        /// <summary>
        /// The dictionnary , containing lua scripts loaded, Key is the file path, value is the script object.
        /// </summary>
        Dictionary<string, CinematicScript> Scripts = new Dictionary<string, CinematicScript>();
        /// <summary>
        /// A file watcher, it permit to see if a file has been modified an to reload this file, during runtime.
        /// </summary>
        FileSystemWatcher Watcher = new FileSystemWatcher();
        /// <summary>
        /// Load all the Lua script (this method is call at startup or via a ConsoleCommand)
        /// </summary>
        /// <param name="contextPath">Path of the directory</param>
        private void LoadDirectory(string contextPath)
        {
            Scripts.Clear();

            foreach (var file in Array.FindAll(Directory.GetFiles(contextPath), x => System.IO.Path.GetExtension(x) == Extension))
            {
                try
                {
                    Scripts.Add(file, LoadFile(file));
                }
                catch (Exception ex)
                {
                    logger.Error("Cannot build Lua Script: (" + System.IO.Path.GetFileNameWithoutExtension(file) + ") : " + ex);
                }


            }
            logger.Gray("Lua Scripts loaded");
        }
        /// <summary>
        /// Load a single file
        /// </summary>
        /// <param name="path">Path of the file</param>
        /// <returns></returns>
        private CinematicScript LoadFile(string path)
        {
            Lua lua = new Lua();
            Thread.Sleep(50); // this bug is really weird, we need to put a delay between each load (see KeraLua.dll & Lua.dll) ?
            lua.DoFile(path);
            CinematicScript script = new CinematicScript(lua);
            return script;
        }
        /// <summary>
        /// Reload a lua script (called when the file is modified, see FileSystemWatcher)
        /// </summary>
        /// <param name="path"></param>
        private void ReloadFile(string path)
        {
            string fileName = System.IO.Path.GetFileNameWithoutExtension(path);
            try
            {
                Scripts[path] = LoadFile(path);
                logger.Gray(fileName + " reloaded");
            }
            catch (Exception ex)
            {
                logger.Error("Cannot build Lua Script: (" + fileName + ") : " + ex);
            }

        }
        /// <summary>
        /// This method is called at startup, instantiate all the objects.
        /// </summary>
        [StartupInvoke("LUA Cinematics", StartupInvokePriority.Eighth)]
        public void Initialize()
        {


            string contextPath = Environment.CurrentDirectory + Path;


            if (!Directory.Exists(contextPath))
            {
                Directory.CreateDirectory(contextPath);
                logger.Color1("Map Cinematics Path (" + Path + ") created!");
            }


            Watcher.Path = contextPath;


            Watcher.NotifyFilter = NotifyFilters.LastWrite;

            Watcher.EnableRaisingEvents = true;
            Watcher.Filter = "*" + Extension;

            Watcher.Changed += Watcher_Changed;

            LoadDirectory(contextPath);

        }

        /// <summary>
        /// Watcher.EnlableRaisingEvents (cf microsoft website)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                Watcher.EnableRaisingEvents = false;
                LoadDirectory(Environment.CurrentDirectory + Path);
            }

            finally
            {
                Watcher.EnableRaisingEvents = true;
            }

        }

        private CinematicScript GetScript(int mapId)
        {
            return Scripts.FirstOrDefault(x => x.Value.MapId == mapId).Value;
        }
        public CinematicScript GetScript(int mapId, long npcId)
        {
            return Scripts.FirstOrDefault(x => x.Value.MapId == mapId && x.Value.NpcId == npcId).Value;
        }
        /// <summary>
        /// Does this npc works with lua script or database informations?
        /// </summary>
        /// <param name="character">Player witch interact with the npc</param>
        /// <param name="npcId">The NpcSpawnRecord Id</param>
        /// <returns></returns>
        public bool IsNpcHandled(Character character, long npcId)
        {
            var script = GetScript(character.Map.Id, npcId);

            if (script == null)
                return false;

            if (Criterias.CriteriaProvider.EvaluateCriterias(character.Client, script.Criteria))
            {
                return true;
            }
            else
            {
                script.OnCriteriaWrong(character);
                return false;
            }
        }
        /// <summary>
        /// Play the CinematicScript async
        /// </summary>
        /// <param name="character"></param>
        /// <param name="spawnId"></param>
        public void TalkToNpc(Character character, long spawnId)
        {
            CinematicScript script = GetScript(character.Map.Id, spawnId);
            script.TalkToNpc(character);
        }
     
        /// <summary>
        /// Is the script reachable for this character
        /// </summary>
        /// <param name="character"></param>
        /// <param name="npcSpawnId"></param>
        /// <returns></returns>
        public bool ExistForCharacter(Character character, int npcSpawnId)
        {
            var script = GetScript(npcSpawnId);
            return script != null && script.Reachable(character);
        }
    }
}
