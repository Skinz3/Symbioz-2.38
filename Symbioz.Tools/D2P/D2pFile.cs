using SSync.IO;
using Symbioz.Tools.D2P;
using Symbioz.MapLoader.IO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Symbioz.Tools.IO;

namespace Symbioz.Tools.D2P
{
    /// <summary>
    ///  From Stump
    /// </summary>
    public class D2pFile : INotifyPropertyChanged, IDisposable
    {
        private readonly Dictionary<string, D2pEntry> m_entries = new Dictionary<string, D2pEntry>();
        private readonly List<D2pFile> m_links = new List<D2pFile>();
        private readonly Queue<D2pFile> m_linksToSave = new Queue<D2pFile>();
        private readonly List<D2pProperty> m_properties = new List<D2pProperty>();
        private readonly List<D2pDirectory> m_rootDirectories = new List<D2pDirectory>();
        private bool m_isDisposed;
        private IDataReader m_reader;

        public event Action<D2pFile, int> ExtractPercentProgress;

        public event PropertyChangedEventHandler PropertyChanged;

        //public Stream Stream
        //{
        //    get
        //    {
        //        return (this.m_reader != null) ? this.m_reader.BaseStream : null;
        //    }
        //}

        private D2pIndexTable _IndexTable;

        public D2pIndexTable IndexTable
        {
            get
            {
                return this._IndexTable;
            }

            private set
            {
                if (this._IndexTable == value)
                {
                    return;
                }
                this._IndexTable = value;
                this.OnPropertyChanged("IndexTable");
            }
        }

        public ReadOnlyCollection<D2pProperty> Properties
        {
            get
            {
                return this.m_properties.AsReadOnly();
            }
        }

        public IEnumerable<D2pEntry> Entries
        {
            get
            {
                return this.m_entries.Values;
            }
        }

        public ReadOnlyCollection<D2pFile> Links
        {
            get
            {
                return this.m_links.AsReadOnly();
            }
        }

        public ReadOnlyCollection<D2pDirectory> RootDirectories
        {
            get
            {
                return this.m_rootDirectories.AsReadOnly();
            }
        }

        private string _FilePath;

        public string FilePath
        {
            get
            {
                return this._FilePath;
            }

            private set
            {
                if (string.Equals(this._FilePath, value))
                {
                    return;
                }
                this._FilePath = value;
                this.OnPropertyChanged("FilePath");
            }
        }

        public D2pEntry this[string fileName]
        {
            get
            {
                return this.m_entries[fileName];
            }
        }

        private void OnExtractPercentProgress(int percent)
        {
            Action<D2pFile, int> handler = this.ExtractPercentProgress;
            if (handler != null)
            {
                handler(this, percent);
            }
        }

        public D2pFile()
        {
            this.IndexTable = new D2pIndexTable(this);
            this.m_reader = new FastBigEndianReader(new byte[0]);
        }

        public D2pFile(string file)
        {
            this.FilePath = file;


            try
            {
                this.m_reader = new FastBigEndianReader(File.ReadAllBytes(file));
            }
            catch
            {
                Console.WriteLine("CACTHED!!");
                FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read);
                this.m_reader = new BigEndianReader(stream);
            }
            this.InternalOpen();
        }

        public void Dispose()
        {
            if (!this.m_isDisposed)
            {
                this.m_isDisposed = true;
                if (this.m_reader != null)
                {
                    //this.m_reader.Dispose();
                }
                if (this.m_links != null)
                {
                    foreach (D2pFile link in this.m_links)
                    {
                        link.Dispose();
                    }
                }
            }
        }

        public bool HasFilePath()
        {
            return !string.IsNullOrEmpty(this.FilePath);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propd = this.PropertyChanged;
            if (propd != null)
            {
                propd(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void InternalOpen()
        {
            //if (!this.Stream.CanSeek)
            //{
            //    this.m_reader = new FastBigEndianReader(this.m_reader.ReadBytes((int)(this.m_reader as FastBigEndianReader).BytesAvailable));
            //}
            if (this.m_reader.ReadByte() != 2 || this.m_reader.ReadByte() != 1)
            {
                throw new FileLoadException("Corrupted d2p header");
            }
            this.ReadTable();
            this.ReadProperties();
            this.ReadEntriesDefinitions();
        }

        private void ReadTable()
        {
            this.m_reader.Seek(-24, SeekOrigin.End);
            this.IndexTable = new D2pIndexTable(this);
            this.IndexTable.ReadTable(this.m_reader);
        }

        private void ReadProperties()
        {
            this.m_reader.Seek(this.IndexTable.PropertiesOffset, SeekOrigin.Begin);
            for (int i = 0; i < this.IndexTable.PropertiesCount; i++)
            {
                D2pProperty property = new D2pProperty();
                property.ReadProperty(this.m_reader);
                if (property.Key == "link")
                {
                    this.InternalAddLink(property.Value);
                }
                this.m_properties.Add(property);
            }
        }

        private void ReadEntriesDefinitions()
        {
            this.m_reader.Seek(this.IndexTable.EntriesDefinitionOffset, SeekOrigin.Begin);
            for (int i = 0; i < this.IndexTable.EntriesCount; i++)
            {
                D2pEntry entry = D2pEntry.CreateEntryDefinition(this, this.m_reader);
                this.InternalAddEntry(entry);
            }
        }

        public void AddProperty(D2pProperty property)
        {
            if (property.Key == "link")
            {
                this.InternalAddLink(property.Value);
            }
            this.InternalAddProperty(property);
        }

        public bool RemoveProperty(D2pProperty property)
        {
            if (property.Key == "link")
            {
                D2pFile link = this.m_links.FirstOrDefault((D2pFile entry) => Path.GetFullPath(this.GetLinkFileAbsolutePath(property.Value)) == Path.GetFullPath(entry.FilePath));
                if (link == null || !this.InternalRemoveLink(link))
                {
                    throw new Exception(string.Format("Cannot remove the associated link {0} to this property", property.Value));
                }
            }
            bool result;
            if (this.m_properties.Remove(property))
            {
                this.OnPropertyChanged("Properties");
                this.IndexTable.PropertiesCount--;
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        private void InternalAddProperty(D2pProperty property)
        {
            this.m_properties.Add(property);
            this.OnPropertyChanged("Properties");
            this.IndexTable.PropertiesCount++;
        }

        public void AddLink(string linkFile)
        {
            this.InternalAddLink(linkFile);
            this.InternalAddProperty(new D2pProperty
            {
                Key = "link",
                Value = linkFile
            });
        }

        private void InternalAddLink(string linkFile)
        {
            string path = this.GetLinkFileAbsolutePath(linkFile);
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(linkFile);
            }
            D2pFile link = new D2pFile(path);
            foreach (D2pEntry entry in link.Entries)
            {
                this.InternalAddEntry(entry);
            }
            this.m_links.Add(link);
            this.OnPropertyChanged("Links");
        }

        private string GetLinkFileAbsolutePath(string linkFile)
        {
            string result;
            if (File.Exists(linkFile))
            {
                result = linkFile;
            }
            else
            {
                if (this.HasFilePath())
                {
                    string absolutePath = Path.Combine(Path.GetDirectoryName(this.FilePath), linkFile);
                    if (File.Exists(absolutePath))
                    {
                        result = absolutePath;
                        return result;
                    }
                }
                result = linkFile;
            }
            return result;
        }

        public bool RemoveLink(D2pFile file)
        {
            D2pProperty property = this.m_properties.FirstOrDefault((D2pProperty entry) => Path.GetFullPath(this.GetLinkFileAbsolutePath(entry.Value)) == Path.GetFullPath(file.FilePath));
            bool result2;
            if (property == null)
            {
                result2 = false;
            }
            else
            {
                bool result = this.InternalRemoveLink(file) && this.m_properties.Remove(property);
                if (result)
                {
                    this.OnPropertyChanged("Properties");
                }
                result2 = result;
            }
            return result2;
        }

        private bool InternalRemoveLink(D2pFile link)
        {
            bool result;
            if (this.m_links.Remove(link))
            {
                this.OnPropertyChanged("Links");
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public D2pEntry[] GetEntriesOfInstanceOnly()
        {
            return (
                from entry in this.m_entries.Values
                where entry.Container == this
                select entry).ToArray<D2pEntry>();
        }

        public D2pEntry GetEntry(string fileName)
        {
            return this.m_entries[fileName];
        }

        public D2pEntry TryGetEntry(string fileName)
        {
            D2pEntry entry;
            D2pEntry result;
            if (this.m_entries.TryGetValue(fileName, out entry))
            {
                result = entry;
            }
            else
            {
                result = null;
            }
            return result;
        }

        public string[] GetFilesName()
        {
            return this.m_entries.Keys.ToArray<string>();
        }

        public void AddEntry(D2pEntry entry)
        {
            entry.State = D2pEntryState.Added;
            this.InternalAddEntry(entry);
            this.IndexTable.EntriesCount++;
            this.OnPropertyChanged("Entries");
        }

        private void InternalAddEntry(D2pEntry entry)
        {
            this.m_entries.Add(entry.FullFileName, entry);
            this.InternalAddDirectories(entry);
        }

        private void InternalAddDirectories(D2pEntry entry)
        {
            string[] directories = entry.GetDirectoriesName();
            if (directories.Length != 0)
            {
                D2pDirectory current = null;
                if (!this.HasDirectory(directories[0]))
                {
                    current = new D2pDirectory(directories[0]);
                    this.m_rootDirectories.Add(current);
                }
                else
                {
                    current = this.TryGetDirectory(directories[0]);
                }
                current.Entries.Add(entry);
                foreach (string directory in directories.Skip(1))
                {
                    if (!current.HasDirectory(directory))
                    {
                        D2pDirectory dir = new D2pDirectory(directory)
                        {
                            Parent = current
                        };
                        current.Directories.Add(dir);
                        current = dir;
                    }
                    else
                    {
                        current = current.TryGetDirectory(directory);
                    }
                    current.Entries.Add(entry);
                }
                entry.Directory = current;
            }
        }

        public bool RemoveEntry(D2pEntry entry)
        {
            bool result;
            if (entry.Container != this)
            {
                if (!entry.Container.RemoveEntry(entry))
                {
                    result = false;
                    return result;
                }
                if (!this.m_linksToSave.Contains(entry.Container))
                {
                    this.m_linksToSave.Enqueue(entry.Container);
                }
            }
            if (this.m_entries.Remove(entry.FullFileName))
            {
                entry.State = D2pEntryState.Removed;
                this.InternalRemoveDirectories(entry);
                this.OnPropertyChanged("Entries");
                if (entry.Container == this)
                {
                    this.IndexTable.EntriesCount--;
                }
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        private void InternalRemoveDirectories(D2pEntry entry)
        {
            for (D2pDirectory current = entry.Directory; current != null; current = current.Parent)
            {
                current.Entries.Remove(entry);
                if (current.Parent != null && current.Entries.Count == 0)
                {
                    current.Parent.Directories.Remove(current);
                }
                else
                {
                    if (current.IsRoot && current.Entries.Count == 0)
                    {
                        this.m_rootDirectories.Remove(current);
                    }
                }
            }
        }

        public bool Exists(string fileName)
        {
            return this.m_entries.ContainsKey(fileName);
        }
        public bool Contains(string fileName)
        {
            var data = this.m_entries.Keys.FirstOrDefault(x => x.Contains(fileName));

            bool result = data != null;

            if (result)
            {

            }

            return result;
        }

        public Dictionary<D2pEntry, byte[]> ReadAllFiles()
        {
            Dictionary<D2pEntry, byte[]> result = new Dictionary<D2pEntry, byte[]>();
            foreach (KeyValuePair<string, D2pEntry> entry in this.m_entries)
            {
                result.Add(entry.Value, this.ReadFile(entry.Value));
            }
            return result;
        }

        public byte[] ReadFile(D2pEntry entry)
        {
            byte[] result;
            if (entry.Container != this)
            {
                result = entry.Container.ReadFile(entry);
            }
            else
            {
                if (entry.Index >= 0 && this.IndexTable.OffsetBase + entry.Index >= 0)
                {
                    this.m_reader.Seek(this.IndexTable.OffsetBase + entry.Index, SeekOrigin.Begin);
                }
                byte[] data = entry.ReadEntry(this.m_reader);
                result = data;
            }
            return result;
        }
        public byte[] ReadFile(string fileName)
        {
            if (!this.Exists(fileName))
            {
                throw new FileNotFoundException(fileName);
            }
            D2pEntry entry = this.GetEntry(fileName);
            return this.ReadFile(entry);
        }

        public void ExtractFile(string fileName, bool overwrite = false)
        {
            if (!this.Exists(fileName))
            {
                throw new FileNotFoundException(fileName);
            }
            D2pEntry entry = this.GetEntry(fileName);
            string dest = Path.Combine("./", entry.FullFileName);
            if (!Directory.Exists(Path.GetDirectoryName(dest)))
            {
                Directory.CreateDirectory(dest);
            }
            this.ExtractFile(fileName, dest, overwrite);
        }

        public void ExtractFile(string fileName, string destination, bool overwrite = false)
        {
            byte[] bytes = this.ReadFile(fileName);
            FileAttributes attr = File.GetAttributes(destination);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                destination = Path.Combine(destination, Path.GetFileName(fileName));
            }
            if (File.Exists(destination) && !overwrite)
            {
                throw new InvalidOperationException(string.Format("Cannot overwrite {0}", destination));
            }
            if (!Directory.Exists(Path.GetDirectoryName(destination)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(destination));
            }
            File.WriteAllBytes(destination, bytes);
        }

        public void ExtractDirectory(string directoryName, string destination)
        {
            if (!this.HasDirectory(directoryName))
            {
                throw new InvalidOperationException(string.Format("Directory {0} does not exist", directoryName));
            }
            D2pDirectory directory = this.TryGetDirectory(directoryName);
            if (!Directory.Exists(Path.GetDirectoryName(Path.Combine(destination, directory.FullName))))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Path.Combine(destination, directory.FullName)));
            }
            foreach (D2pEntry entry in directory.Entries)
            {
                this.ExtractFile(entry.FullFileName, Path.Combine(destination, entry.FullFileName), false);
            }
            foreach (D2pDirectory pDirectory in directory.Directories)
            {
                this.ExtractDirectory(pDirectory.FullName, destination);
            }
        }

        public void ExtractAllFiles(string destination, bool overwrite = false, bool progress = false)
        {
            foreach (string[] dir in (
                from entry in this.m_entries
                select entry.Value.GetDirectoriesName()).Distinct<string[]>())
            {

                string dest = Path.Combine(Path.GetFullPath(destination), Path.Combine(dir));

                Directory.CreateDirectory(dest);
            }

            double i = 0.0;
            int progressPercent = 0;
            foreach (KeyValuePair<string, D2pEntry> entry2 in this.m_entries)
            {

                string dest = Path.Combine(Path.GetFullPath(destination), entry2.Value.FullFileName);

                File.WriteAllBytes(dest, this.ReadFile(entry2.Value));
                //     datas.Add(new D2PVirtualFile(, ));
                i += 1.0;
                if (progress)
                {
                    if ((int)(i / (double)this.m_entries.Count * 100.0) != progressPercent)
                    {
                        this.OnExtractPercentProgress(progressPercent = (int)(i / (double)this.m_entries.Count * 100.0));
                    }
                }
            }
            //  return datas;
        }

        public D2pEntry AddFile(string file)
        {
            byte[] bytes = File.ReadAllBytes(file);
            string dest = file;
            if (this.HasFilePath())
            {
                dest = this.GetRelativePath(file, Path.GetDirectoryName(this.FilePath));
            }
            return this.AddFile(dest, bytes);
        }

        public D2pEntry AddFile(string fileName, byte[] data)
        {
            D2pEntry entry = new D2pEntry(this, fileName, data);
            this.AddEntry(entry);
            return entry;
        }

        public bool RemoveFile(string file)
        {
            D2pEntry entry = this.TryGetEntry(file);
            return entry != null && this.RemoveEntry(entry);
        }

        public bool ModifyFile(string file, byte[] data)
        {
            D2pEntry entry = this.TryGetEntry(file);
            bool result;
            if (entry == null)
            {
                result = false;
            }
            else
            {
                entry.ModifyEntry(data);
                if (entry.Container != this && !this.m_linksToSave.Contains(entry.Container))
                {
                    this.m_linksToSave.Enqueue(entry.Container);
                }
                result = true;
            }
            return result;
        }

        private string GetRelativePath(string file, string directory)
        {
            Uri uri = new Uri(Path.GetFullPath(file));
            Uri currentUri = new Uri(Path.GetFullPath(directory));
            return currentUri.MakeRelativeUri(uri).ToString();
        }

        public void Save()
        {
            if (this.HasFilePath())
            {
                this.SaveAs(this.FilePath, true);
                return;
            }
            throw new InvalidOperationException("Cannot perform Save : No path defined, use SaveAs instead");
        }

        public void SaveAs(string destination, bool overwrite = true)
        {
            while (this.m_linksToSave.Count > 0)
            {
                D2pFile link = this.m_linksToSave.Dequeue();
                link.Save();
            }
            Stream stream;
            if (!File.Exists(destination))
            {
                stream = File.Create(destination);
            }
            else
            {
                if (!overwrite)
                {
                    throw new InvalidOperationException("Cannot perform SaveAs : file already exist, notify overwrite parameter to true");
                }
                stream = File.OpenWrite(destination);
            }
            using (BigEndianWriter writer = new BigEndianWriter(stream))
            {
                writer.WriteByte(2);
                writer.WriteByte(1);
                D2pEntry[] entries = this.GetEntriesOfInstanceOnly();
                int offset = 2;
                D2pEntry[] array = entries;
                for (int i = 0; i < array.Length; i++)
                {
                    D2pEntry entry = array[i];
                    byte[] data = this.ReadFile(entry);
                    entry.Index = (int)writer.Position - offset;
                    writer.WriteBytes(data);
                }
                int entriesDefOffset = (int)writer.Position;
                array = entries;
                for (int i = 0; i < array.Length; i++)
                {
                    D2pEntry entry = array[i];
                    entry.WriteEntryDefinition(writer);
                }
                int propertiesOffset = (int)writer.Position;
                foreach (D2pProperty property in this.m_properties)
                {
                    property.WriteProperty(writer);
                }
                this.IndexTable.OffsetBase = offset;
                this.IndexTable.EntriesCount = entries.Length;
                this.IndexTable.EntriesDefinitionOffset = entriesDefOffset;
                this.IndexTable.PropertiesCount = this.m_properties.Count;
                this.IndexTable.PropertiesOffset = propertiesOffset;
                this.IndexTable.Size = this.IndexTable.EntriesDefinitionOffset - this.IndexTable.OffsetBase;
                this.IndexTable.WriteTable(writer);
            }
        }

        public bool HasDirectory(string directory)
        {
            string[] directoriesName = directory.Split(new char[]
            {
                '/',
                '\\'
            }, StringSplitOptions.RemoveEmptyEntries);
            bool result;
            if (directoriesName.Length == 0)
            {
                result = false;
            }
            else
            {
                D2pDirectory current = this.m_rootDirectories.SingleOrDefault((D2pDirectory entry) => entry.Name == directoriesName[0]);
                if (current == null)
                {
                    result = false;
                }
                else
                {
                    foreach (string dir in directoriesName.Skip(1))
                    {
                        if (!current.HasDirectory(dir))
                        {
                            result = false;
                            return result;
                        }
                        current = current.TryGetDirectory(dir);
                    }
                    result = true;
                }
            }
            return result;
        }

        public D2pDirectory TryGetDirectory(string directory)
        {
            string[] directoriesName = directory.Split(new char[]
            {
                '/',
                '\\'
            }, StringSplitOptions.RemoveEmptyEntries);
            D2pDirectory result;
            if (directoriesName.Length == 0)
            {
                result = null;
            }
            else
            {
                D2pDirectory current = this.m_rootDirectories.SingleOrDefault((D2pDirectory entry) => entry.Name == directoriesName[0]);
                if (current == null)
                {
                    result = null;
                }
                else
                {
                    foreach (string dir in directoriesName.Skip(1))
                    {
                        if (!current.HasDirectory(dir))
                        {
                            result = null;
                            return result;
                        }
                        current = current.TryGetDirectory(dir);
                    }
                    result = current;
                }
            }
            return result;
        }

        public D2pDirectory[] GetDirectoriesTree(string directory)
        {
            List<D2pDirectory> result = new List<D2pDirectory>();
            string[] directoriesName = directory.Split(new char[]
            {
                '/',
                '\\'
            }, StringSplitOptions.RemoveEmptyEntries);
            D2pDirectory[] result2;
            if (directoriesName.Length == 0)
            {
                result2 = new D2pDirectory[0];
            }
            else
            {
                D2pDirectory current = this.m_rootDirectories.SingleOrDefault((D2pDirectory entry) => entry.Name == directoriesName[0]);
                if (current == null)
                {
                    result2 = new D2pDirectory[0];
                }
                else
                {
                    result.Add(current);
                    foreach (string dir in directoriesName.Skip(1))
                    {
                        if (!current.HasDirectory(dir))
                        {
                            result2 = result.ToArray();
                            return result2;
                        }
                        current = current.TryGetDirectory(dir);
                        result.Add(current);
                    }
                    result2 = result.ToArray();
                }
            }
            return result2;
        }
    }
}