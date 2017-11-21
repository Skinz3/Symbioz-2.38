using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using Symbioz.Tools.DLM;
using Symbioz.Tools.ELE;
using Symbioz.Tools.ELE.Repertory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Symbioz.Tools.D2O;
using Symbioz.SqlSync.Tables;
using Symbioz.Core;
using System.Collections;
using Symbioz.SqlSync.D2OTypes;
using Symbioz.Tools.D2I;
using System.Threading;

namespace Symbioz.SqlSync.MapTables
{
    class Helper
    {
        static Logger logger = new Logger();

        public static Dictionary<Type, List<ITable>> GetD2OTables(Type type, List<D2OReader> d2oFiles, D2IFile d2iFile)
        {
            Dictionary<Type, List<ITable>> tables = new Dictionary<Type, List<ITable>>();

            D2OAttribute d2oAttribute = type.GetCustomAttribute<D2OAttribute>();

            DataClass[] d2oData = GetD2OData(d2oFiles, d2oAttribute);

            if (d2oData.Length == 0)
            {
                logger.Color2("Unable to find d2o module : " + d2oAttribute.Module);
                Console.ReadKey();
                Environment.Exit(0);
            }

            logger.White("Loading D2OTable " + d2oAttribute.ToString() + "...");

            foreach (var data in d2oData)
            {
                ID2OTable table = Activator.CreateInstance(type) as ID2OTable;


                foreach (var field in GetD2OFields(table))
                {
                    var attribute = field.GetCustomAttribute<D2OFieldAttribute>();
                    if (!data.Fields.ContainsKey(attribute.FieldName))
                    {
                        logger.NewLine();
                        logger.Color2("Unable to get fieldValue for (" + attribute.FieldName + ") the field dosent exist in d2oFile (" + d2oAttribute.ToString() + ")");
                        logger.NewLine();
                        logger.Color1("D2O Fields for " + d2oAttribute.Module + ":");

                        foreach (var d2oField in d2oData[0].Fields.Keys)
                        {
                            logger.Color2("-" + d2oField, false);
                        }

                        Console.Read();
                        Environment.Exit(0);
                    }

                    object fieldValue = data.Fields[attribute.FieldName];

                    if (fieldValue == null)
                    {
                        if (field.FieldType == typeof(string))
                            field.SetValue(table, string.Empty);
                        else
                            field.SetValue(table, Activator.CreateInstance(field.FieldType));
                        continue;

                    }
                    var customMethod = type.GetMethods().FirstOrDefault(x => x.GetCustomAttribute<D2OFieldHandler>() != null);

                    if (customMethod != null)
                    {
                        var methodAttribute = customMethod.GetCustomAttribute<D2OFieldHandler>();
                        if (methodAttribute.Field == attribute.FieldName)
                        {
                            customMethod.Invoke(table, new object[] { fieldValue });
                            continue;
                        }
                    }

                    if (fieldValue is ArrayList)
                    {

                        var array = (fieldValue as ArrayList).ToArray();

                        if (array.Length > 0)
                        {
                            var obj = array[0];

                            if (obj is DataClass)
                            {
                                var d2otype = D2oTypeManager.GetType(obj as DataClass);

                                IList list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[] { d2otype }));

                                foreach (var value in array)
                                {
                                    list.Add(D2oTypeManager.Handle(value as DataClass));
                                }
                                try
                                {
                                    field.SetValue(table, list.XMLSerialize());
                                }
                                catch
                                {
                                    logger.Color2("Type for field " + field.Name + " in " + type.Name + " must be string");
                                    Console.Read();
                                    Environment.Exit(0);
                                }
                            }
                            else if (obj is ArrayList)
                            {

                                string str = string.Empty;

                                foreach (ArrayList subItem in array)
                                {
                                    foreach (var item in subItem)
                                    {
                                        str += item + ",";
                                    }

                                    str = str.Remove(str.Length - 1, 1);

                                    str += "|";
                                }

                                if (str != string.Empty)
                                    str = str.Remove(str.Length - 1, 1);
                                field.SetValue(table, str);

                            }
                            else
                            {
                                var list = Activator.CreateInstance(field.FieldType) as IList;

                                foreach (var value in array)
                                {
                                    var listType = field.FieldType.GenericTypeArguments[0];
                                    list.Add(Convert.ChangeType(value, listType));
                                }

                                field.SetValue(table, list);
                            }
                        }


                    }
                    else if (fieldValue is DataClass)
                    {
                        string value = D2oTypeManager.Handle(fieldValue as DataClass).XMLSerialize(); // idem
                        field.SetValue(table, value);
                    }
                    else
                    {

                        if (fieldValue != null)
                        {
                            if (fieldValue is String && fieldValue.ToString().ToLower() == "null")
                            {
                                fieldValue = string.Empty;
                            }

                            if (field.FieldType == typeof(String) && fieldValue is Int32)
                            {
                                if (field.GetCustomAttribute<i18nAttribute>() != null)
                                {
                                    fieldValue = d2iFile.GetText((int)fieldValue);
                                    fieldValue = fieldValue.ToString().Replace('\'', ' ');
                                }

                            }
                            field.SetValue(table, Convert.ChangeType(fieldValue, field.FieldType));
                        }
                    }
                }
                if (tables.ContainsKey(type))
                    tables[type].Add(table);
                else
                    tables.Add(type, new List<ITable> { table });
            }
            return tables;
        }
        private static FieldInfo[] GetD2OFields(ID2OTable table)
        {
            return table.GetType().GetFields().Where(x => x.GetCustomAttribute<D2OFieldAttribute>() != null).ToArray();
        }
        private static DataClass[] GetD2OData(List<D2OReader> d2oFiles, D2OAttribute attribute)
        {
            return d2oFiles.Find(x => x.FileName == attribute.FileName).GetObjects().Where(x => x.Name == attribute.Module).ToArray();
        }
        public static void GetElements(DlmMap map, Elements elements, out List<MapInteractiveElement> interactiveElements)
        {
            interactiveElements = new List<MapInteractiveElement>();
            foreach (var layer in map.Layers)
            {
                foreach (var cell in layer.Cells)
                {
                    foreach (var element in cell.Elements)
                    {
                        if (element is DlmGraphicalElement)
                        {
                            DlmGraphicalElement graphicalElement = element as DlmGraphicalElement;

                            if (graphicalElement.Identifier != 0)
                            {

                                var gfxElement = elements.ReadElement((int)graphicalElement.ElementId);

                                if (gfxElement.Type != EleGraphicalElementTypes.ENTITY)
                                {
                                    NormalGraphicalElementData normalElement = gfxElement as NormalGraphicalElementData;
                                    MapInteractiveElement interactiveTable = new MapInteractiveElement();
                                    interactiveTable.ElementId = (int)graphicalElement.Identifier;
                                    interactiveTable.MapId = map.Id;
                                    interactiveTable.CellId = (ushort)cell.Id;
                                    if (normalElement != null)
                                        interactiveTable.GfxId = normalElement.Gfx;
                                    interactiveTable.GfxBonesId = -1;
                                    interactiveElements.Add(interactiveTable);

                                }
                                else
                                {
                                    EntityGraphicalElementData entityElement = gfxElement as EntityGraphicalElementData;
                                    MapInteractiveElement interactiveTable = new MapInteractiveElement();
                                    interactiveTable.ElementId = (int)graphicalElement.Identifier;
                                    interactiveTable.MapId = map.Id;
                                    interactiveTable.CellId = (ushort)cell.Id;
                                    interactiveTable.GfxBonesId = ushort.Parse(entityElement.EntityLook.Replace("{", "").Replace("}", ""));
                                    interactiveTable.GfxId = -1;
                                    interactiveElements.Add(interactiveTable);


                                }


                            }
                        }
                    }
                }
            }

        }

    }
}


