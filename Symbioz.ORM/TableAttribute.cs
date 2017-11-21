using System;

namespace Symbioz.ORM
{
    public class TableAttribute : Attribute
    {
        public string tableName;
        public bool catchAll;
        public short readingOrder;

        public TableAttribute(string tableName, bool catchAll = true, short readingOrder = -1)
        {
            this.tableName = tableName;
            this.catchAll = catchAll;
            this.readingOrder = readingOrder;
        }
    }
}
