
using SSync.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Tools.D2O
{
    internal sealed class Class16
    {
        private D2OReader D2oData;

        internal Class16(D2OReader d2oData, string string_1, string string_2)
        {
            this.string_0 = string_1;
            this.D2oData = d2oData;
        }

        internal DataClass method_0(string string_1, BigEndianReader dofusReader_0)
        {
            DataClass class2 = new DataClass { Name = this.string_0 };
            foreach (Class17 class3 in this.list_0)
            {
                var obj  =RuntimeHelpers.GetObjectValue(class3.delegate0_0.Invoke(string_1, dofusReader_0));
                class2.Fields.Add(class3.string_0, obj);
            }
            return class2;
        }

        internal void method_1(string string_1, BigEndianReader dofusReader_0)
        {
            Class17 item = new Class17(D2oData, string_1, dofusReader_0);
            this.list_0.Add(item);
        }


        // Fields
        public List<Class17> list_0 = new List<Class17>();
        public string string_0;
    }
}
