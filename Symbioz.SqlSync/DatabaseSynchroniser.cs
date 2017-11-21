using Symbioz.Core;
using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using Symbioz.SqlSync.D2OTypes;
using Symbioz.SqlSync.MapTables;
using Symbioz.SqlSync.Tables;
using Symbioz.Tools.D2I;
using Symbioz.Tools.D2O;
using Symbioz.Tools.D2P;
using Symbioz.Tools.DLM;
using Symbioz.Tools.ELE;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync
{
    public class DatabaseSynchroniser
    {
        static Assembly Assembly = Assembly.GetExecutingAssembly();


        public static Logger logger = new Logger();

        public static string D2O_DIRECTORY = Environment.CurrentDirectory + "/D2o/";

        public static string MAPS_D2P_DIRECTORY = Environment.CurrentDirectory + "/Maps/";

        public static string D2I_FILE_PATH = Environment.CurrentDirectory + "/i18n_fr.d2i";

        private List<D2OReader> D2OFiles = new List<D2OReader>();

        private Type[] D2OTableTypes;

        private Dictionary<Type, List<ITable>> Tables = new Dictionary<Type, List<ITable>>();

        private D2IFile d2iFile;

        private List<Map> m_maps = new List<Map>();

        private List<MapInteractiveElement> m_interactiveElements = new List<MapInteractiveElement>();

        private bool SyncD2O;

        private bool SyncMaps;

        public DatabaseSynchroniser(bool syncD2O, bool syncMaps)
        {
            this.SyncD2O = syncD2O;
            this.SyncMaps = syncMaps;

            if (syncD2O == false && syncMaps == false)
            {
                logger.Error("No objects to sync... ");
                return;
            }
            if (!Directory.Exists(D2O_DIRECTORY))
            {
                logger.Color2("D2O Directory dosent exist! (" + D2O_DIRECTORY + ")");
                return;
            }
            if (!Directory.Exists(MAPS_D2P_DIRECTORY))
            {
                logger.Color2("Maps Directory dosent exist! (" + MAPS_D2P_DIRECTORY + ")");
                return;
            }
            if (!File.Exists(MAPS_D2P_DIRECTORY + "maps0.d2p"))
            {
                logger.Color2("Unable to find " + MAPS_D2P_DIRECTORY + "maps0.d2p");
                return;
            }
            if (!File.Exists(D2I_FILE_PATH))
            {
                logger.Color2("D2I File dosent exist! (" + D2I_FILE_PATH + ")");
                return;
            }

        }

        public void Launch()
        {

            logger.Color1("--- Loading ITables ---");

            LoadD2I();

            if (SyncD2O)
            {
                LoadTypes();

                LoadD2OFiles();

                LoadD2OTables();
            }

            if (SyncMaps)
            {
                LoadMaps();
            }


        }

        public void Synchronize()
        {
            logger.Color1("--- Database Sync ---");
            this.SynchronizeD2O();
            this.SynchronizeMaps();
        }
        private void SynchronizeMaps()
        {
            DatabaseManager.GetInstance().CreateTable(typeof(Map));
            DatabaseManager.GetInstance().CreateTable(typeof(MapInteractiveElement));

            logger.Color2("Inserting Maps...");
            new DatabaseWriter<Map>(DatabaseAction.Add, m_maps.ToArray());

            logger.Color2("Inserting MapsInteractiveElements...");
            new DatabaseWriter<MapInteractiveElement>(DatabaseAction.Add, m_interactiveElements.ToArray());

        }
        private void SynchronizeD2O()
        {

            logger.Color2("Creating Tables...");
            var types = Tables.Keys.ToList();

            foreach (var type in types)
            {
                DatabaseManager.GetInstance().CreateTable(type);
            }



            foreach (var type in types)
            {
                List<ITable> elements;

                lock (Tables)
                    elements = Tables[type];
                logger.Color2("Inserting " + type.Name + "...");
                try
                {
                    //int x = 0;
                    //foreach (var element in elements)
                    //{
                    //    DatabaseManager.GetInstance().WriterInstance(type, DatabaseAction.Add, new ITable[] { element });
                    //    x++;
                    //    Console.Title = x.Percentage(elements.Count()) + "%";

                    //}

                    // fast but dangerous (lost packets)
                    DatabaseManager.GetInstance().WriterInstance(type, DatabaseAction.Add, elements.ToArray());
                }
                catch (Exception e) { logger.Error(e.ToString()); }

                lock (Tables)
                    Tables[type] = Tables[type].Skip(elements.Count).ToList();




            }

        }
        private void LoadTypes()
        {
            logger.White("Loading D2OTable Types...");
            this.D2OTableTypes = Assembly.GetTypes().Where(x => x.GetInterfaces().Contains(typeof(ID2OTable))).ToArray();

        }
        private void LoadD2OFiles()
        {
            logger.White("Reading D2o Files...");
            foreach (var type in this.D2OTableTypes)
            {
                D2OAttribute d2oAttribute = type.GetCustomAttribute<D2OAttribute>();

                if (D2OFiles.Find(x => x.FileName == d2oAttribute.FileName) == null)
                {
                    string path = D2O_DIRECTORY + d2oAttribute.FileName;

                    if (!File.Exists(path))
                    {
                        logger.Color2("Unable to find d2o File :" + path);
                        Console.Read();

                    }
                    else
                        D2OFiles.Add(new D2OReader(path));

                }
            }
        }
        private void LoadD2I()
        {
            d2iFile = new D2IFile(D2I_FILE_PATH);
            logger.White("D2I File loaded");
        }


        private void LoadMaps()
        {
            var mapFile = MAPS_D2P_DIRECTORY + "maps0.d2p"; // put this in const field

            string eleFile = MAPS_D2P_DIRECTORY + "elements.ele";

            if (!File.Exists(mapFile))
                return;

            logger.White("Loading Maps from d2p...");

            EleReader eleReader = new EleReader(eleFile);

            Elements elements = eleReader.ReadElements();

            D2pFile d2p = new D2pFile(mapFile);
            var datas = d2p.ReadAllFiles();

            UpdateLogger m_logger = new UpdateLogger();

            int x = 0;
            foreach (var data in datas)
            {
                DlmReader mapReader = new DlmReader(new MemoryStream(data.Value));
                mapReader.DecryptionKey = SqlSyncConfiguration.Instance.MapKey;
                ReadMap(mapReader, elements); //elements
                x++;
                m_logger.Update(x.Percentage(datas.Count) + "%");
            }
        }

        private void ReadMap(DlmReader reader, Elements elements = null)
        {
            var dlmMap = reader.ReadMap();

            m_maps.Add(Map.FromDlm(dlmMap));

            if (elements != null)
            {
                List<MapInteractiveElement> interactives;
                Helper.GetElements(dlmMap, elements, out interactives);
                m_interactiveElements.AddRange(interactives);
            }

        }
        private void LoadD2OTables()
        {
            foreach (var type in this.D2OTableTypes)
            {
                foreach (var data in Helper.GetD2OTables(type, D2OFiles, d2iFile))
                {
                    Tables.Add(data.Key, data.Value);
                }

            }
        }
        public T[] GetD2Os<T>() where T : ID2OTable, ITable
        {
            var table = Tables[typeof(T)];
            return table.Cast<T>().ToArray();
        }

    }
}
