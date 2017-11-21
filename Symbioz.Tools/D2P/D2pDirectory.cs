using System.Collections.Generic;
using System.Linq;

namespace Symbioz.Tools.D2P
{
    /// <summary>
    ///  From Stump
    /// </summary>
    public class D2pDirectory
    {
        private D2pDirectory m_parent;
        private List<D2pEntry> m_entries = new List<D2pEntry>();
        private List<D2pDirectory> m_directories = new List<D2pDirectory>();

        public string Name
        {
            get;
            set;
        }

        public string FullName
        {
            get;
            set;
        }

        public D2pDirectory Parent
        {
            get
            {
                return this.m_parent;
            }
            set
            {
                this.m_parent = value;
                this.UpdateFullName();
            }
        }

        public List<D2pEntry> Entries
        {
            get
            {
                return this.m_entries;
            }
            set
            {
                this.m_entries = value;
            }
        }

        public List<D2pDirectory> Directories
        {
            get
            {
                return this.m_directories;
            }
            set
            {
                this.m_directories = value;
            }
        }

        public bool IsRoot
        {
            get
            {
                return this.Parent == null;
            }
        }

        public D2pDirectory(string name)
        {
            this.Name = name;
            this.FullName = name;
        }

        private void UpdateFullName()
        {
            D2pDirectory current = this;
            this.FullName = current.Name;
            while (current.Parent != null)
            {
                this.FullName = this.FullName.Insert(0, current.Parent.Name + "\\");
                current = current.Parent;
            }
        }

        public bool HasDirectory(string directory)
        {
            return this.m_directories.Any((D2pDirectory entry) => entry.Name == directory);
        }

        public D2pDirectory TryGetDirectory(string directory)
        {
            return this.m_directories.SingleOrDefault((D2pDirectory entry) => entry.Name == directory);
        }

        public bool HasEntry(string entryName)
        {
            return this.m_entries.Any((D2pEntry entry) => entry.FullFileName == entryName);
        }

        public D2pEntry TryGetEntry(string entryName)
        {
            return this.m_entries.SingleOrDefault((D2pEntry entry) => entry.FullFileName == entryName);
        }
    }
}