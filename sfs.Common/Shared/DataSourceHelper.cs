using SageFrame.Web.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;


namespace SageFrame.Web.Utilities
{
    /// <summary>
    /// Summary description for DataSourceHelper
    /// </summary>
    public class DataSourceHelper
    {
        public static ArrayList GetPropertyInfo(Type objType)
        {

            // Use the cache because the reflection used later is expensive
            ArrayList objProperties = null;

            if (objProperties == null)
            {
                objProperties = new ArrayList();
                foreach (PropertyInfo objProperty in objType.GetProperties())
                {
                    objProperties.Add(objProperty);
                }
            }

            return objProperties;

        }

        public static ArrayList FillCollection(IDataReader dr, Type objType)
        {

            ArrayList objFillCollection = new ArrayList();
            object objFillObject;

            // get properties for type
            ArrayList objProperties = GetPropertyInfo(objType);

            // get ordinal positions in datareader
            int[] arrOrdinals = GetOrdinals(objProperties, dr);

            // iterate datareader
            while (dr.Read())
            {
                // fill business object
                objFillObject = CreateObject(objType, dr, objProperties, arrOrdinals);
                // add to collection
                objFillCollection.Add(objFillObject);
            }

            // close datareader
            if ((dr != null))
            {
                dr.Close();
            }

            return objFillCollection;

        }

        private static int[] GetOrdinals(ArrayList objProperties, IDataReader dr)
        {

            int[] arrOrdinals = new int[objProperties.Count + 1];
            int intProperty;

            if ((dr != null))
            {
                for (intProperty = 0; intProperty <= objProperties.Count - 1; intProperty++)
                {
                    arrOrdinals[intProperty] = -1;
                    try
                    {
                        arrOrdinals[intProperty] = dr.GetOrdinal(((PropertyInfo)objProperties[intProperty]).Name);
                    }
                    catch (Exception e)
                    {
                        // property does not exist in datareader
                    }
                }
            }

            return arrOrdinals;

        }

        private static object CreateObject(Type objType, IDataReader dr, ArrayList objProperties, int[] arrOrdinals)
        {

            PropertyInfo objPropertyInfo;
            object objValue;
            Type objPropertyType = null;
            int intProperty;

            //objPropertyInfo.ToString() == BuiltyNumber
            object objObject = Activator.CreateInstance(objType);

            // fill object with values from datareader
            for (intProperty = 0; intProperty <= objProperties.Count - 1; intProperty++)
            {
                objPropertyInfo = (PropertyInfo)objProperties[intProperty];
                if (objPropertyInfo.CanWrite)
                {
                    objValue = Null.SetNull(objPropertyInfo);
                    if (arrOrdinals[intProperty] != -1)
                    {
                        if (System.Convert.IsDBNull(dr.GetValue(arrOrdinals[intProperty])))
                        {
                            // translate Null value
                            objPropertyInfo.SetValue(objObject, objValue, null);
                        }
                        else
                        {
                            try
                            {
                                if (((dr.GetValue(arrOrdinals[intProperty])).ToString().ToLower() == "true") || ((dr.GetValue(arrOrdinals[intProperty])).ToString().ToLower() == "false"))
                                {
                                    objPropertyInfo.SetValue(objObject, Convert.ToBoolean(dr.GetValue(arrOrdinals[intProperty])), null);
                                }
                                else if ((dr.GetValue(arrOrdinals[intProperty])).GetType().ToString() == "System.Int64")
                                {
                                    objPropertyInfo.SetValue(objObject, Convert.ToInt32(dr.GetValue(arrOrdinals[intProperty])), null);
                                }
                                else if ((dr.GetValue(arrOrdinals[intProperty])).GetType().ToString() == "System.Decimal")
                                {
                                    objPropertyInfo.SetValue(objObject, Convert.ToInt32(dr.GetValue(arrOrdinals[intProperty])), null);
                                }
                                else
                                {
                                    // try implicit conversion first
                                    objPropertyInfo.SetValue(objObject, dr.GetValue(arrOrdinals[intProperty]), null);
                                }
                            }
                            catch
                            {
                                // business object info class member data type does not match datareader member data type
                                try
                                {
                                    objPropertyType = objPropertyInfo.PropertyType;
                                    //need to handle enumeration conversions differently than other base types
                                    if (objPropertyType.BaseType.Equals(typeof(System.Enum)))
                                    {
                                        // check if value is numeric and if not convert to integer ( supports databases like Oracle )
                                        int test = 0;
                                        if (test.GetType() == dr.GetValue(arrOrdinals[intProperty]).GetType())
                                        {
                                            ((PropertyInfo)objProperties[intProperty]).SetValue(objObject, System.Enum.ToObject(objPropertyType, Convert.ToInt32(dr.GetValue(arrOrdinals[intProperty]))), null);
                                        }
                                        else
                                        {
                                            ((PropertyInfo)objProperties[intProperty]).SetValue(objObject, System.Enum.ToObject(objPropertyType, dr.GetValue(arrOrdinals[intProperty])), null);
                                        }
                                    }
                                    else if (objPropertyType.FullName.Equals("System.Guid"))
                                    {
                                        // guid is not a datatype common across all databases ( ie. Oracle )
                                        objPropertyInfo.SetValue(objObject, Convert.ChangeType(new Guid(dr.GetValue(arrOrdinals[intProperty]).ToString()), objPropertyType), null);
                                    }
                                    else
                                    {
                                        // try explicit conversion
                                        objPropertyInfo.SetValue(objObject, Convert.ChangeType(dr.GetValue(arrOrdinals[intProperty]), objPropertyType), null);
                                    }
                                }
                                catch
                                {
                                    objPropertyInfo.SetValue(objObject, Convert.ChangeType(dr.GetValue(arrOrdinals[intProperty]), objPropertyType), null);
                                }
                            }
                        }
                    }
                    else
                    {
                        // property does not exist in datareader
                    }
                }
            }

            return objObject;

        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Generic version of FillCollection fills a List custom business object of a specified type 
        /// from the supplied DataReader
        /// </summary>
        /// <typeparam name="T">The type of the business object</typeparam>
        /// <param name="dr">The IDataReader to use to fill the object</param>
        /// <returns>A List of custom business objects</returns>
        /// <remarks></remarks>
        /// <history>
        /// 	[cnurse]	10/10/2005	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public static List<T> FillCollection<T>(IDataReader dr)
        {

            List<T> objFillCollection = new List<T>();
            T objFillObject;

            // iterate datareader
            while (dr.Read())
            {
                // fill business object
                objFillObject = CreateObject<T>(dr);
                // add to collection
                objFillCollection.Add(objFillObject);
            }

            // close datareader
            if ((dr != null))
            {
                dr.Close();
            }

            return objFillCollection;

        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Generic version of FillCollection fills a List custom business object of a specified type 
        /// from the supplied DataTable
        /// </summary>
        /// <typeparam name="T">The type of the business object</typeparam>
        /// <param name="dt">The DataTable to use to fill the object</param>
        /// <returns>A List of custom business objects</returns>
        /// <remarks></remarks>
        /// <history>
        /// 	[dinesh]	4/22/2011	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public static List<T> FillCollection<T>(DataTable dt)
        {
            List<T> objFillCollection = new List<T>();
            T objFillObject;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                objFillObject = CreateObject<T>(dt.Rows[i]);
                objFillCollection.Add(objFillObject);
            }
            return objFillCollection;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Generic version of CreateObject creates an object of a specified type from the 
        /// provided DataReader
        /// </summary>
        /// <typeparam name="T">The type of the business object</typeparam>
        /// <param name="dr">The DataReader</param>
        /// <returns>The custom business object</returns>
        /// <remarks></remarks>
        /// <history>
        /// 	[cnurse]	10/10/2005	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        private static T CreateObject<T>(IDataReader dr)
        {

            PropertyInfo objPropertyInfo;
            object objValue;
            Type objPropertyType = null;
            int intProperty;

            T objObject = Activator.CreateInstance<T>();

            // get properties for type
            ArrayList objProperties = GetPropertyInfo(objObject.GetType());

            // get ordinal positions in datareader
            int[] arrOrdinals = GetOrdinals(objProperties, dr);

            // fill object with values from datareader
            for (intProperty = 0; intProperty <= objProperties.Count - 1; intProperty++)
            {
                objPropertyInfo = (PropertyInfo)objProperties[intProperty];
                if (objPropertyInfo.CanWrite)
                {
                    objValue = Null.SetNull(objPropertyInfo);
                    if (arrOrdinals[intProperty] != -1)
                    {
                        if (System.Convert.IsDBNull(dr.GetValue(arrOrdinals[intProperty])))
                        {
                            // translate Null value
                            objPropertyInfo.SetValue(objObject, objValue, null);
                        }
                        else
                        {
                            try
                            {
                                if (((dr.GetValue(arrOrdinals[intProperty])).ToString().ToLower() == "true") || ((dr.GetValue(arrOrdinals[intProperty])).ToString().ToLower() == "false"))
                                {
                                    objPropertyInfo.SetValue(objObject, Convert.ToBoolean(dr.GetValue(arrOrdinals[intProperty])), null);
                                }
                                else if ((dr.GetValue(arrOrdinals[intProperty])).GetType().ToString() == "System.Int64")
                                {
                                    objPropertyInfo.SetValue(objObject, Convert.ToInt32(dr.GetValue(arrOrdinals[intProperty])), null);
                                }
                                else if ((dr.GetValue(arrOrdinals[intProperty])).GetType().ToString() == "System.Decimal")
                                {
                                    if (objPropertyInfo.PropertyType.ToString() == "System.Nullable`1[System.Decimal]")
                                    {
                                        objPropertyInfo.SetValue(objObject, Convert.ToDecimal(dr.GetValue(arrOrdinals[intProperty])), null);
                                    }
                                    else
                                    {
                                        objPropertyInfo.SetValue(objObject, Convert.ToInt32(dr.GetValue(arrOrdinals[intProperty])), null);
                                        //objPropertyInfo.SetValue(objObject, Convert.ToDecimal(dr.GetValue(arrOrdinals[intProperty])), null);
                                    }
                                }
                                else
                                {
                                    // try implicit conversion first
                                    objPropertyInfo.SetValue(objObject, dr.GetValue(arrOrdinals[intProperty]), null);
                                }
                            }
                            catch
                            {
                                // business object info class member data type does not match datareader member data type
                                try
                                {
                                    objPropertyType = objPropertyInfo.PropertyType;
                                    //need to handle enumeration conversions differently than other base types
                                    if (objPropertyType.BaseType.Equals(typeof(System.Enum)))
                                    {
                                        // check if value is numeric and if not convert to integer ( supports databases like Oracle )
                                        int testint = 0;
                                        if (testint.GetType() == dr.GetValue(arrOrdinals[intProperty]).GetType())
                                        {
                                            ((PropertyInfo)objProperties[intProperty]).SetValue(objObject, System.Enum.ToObject(objPropertyType, Convert.ToInt32(dr.GetValue(arrOrdinals[intProperty]))), null);
                                        }
                                        else
                                        {
                                            ((PropertyInfo)objProperties[intProperty]).SetValue(objObject, System.Enum.ToObject(objPropertyType, dr.GetValue(arrOrdinals[intProperty])), null);
                                        }
                                    }
                                    else
                                    {
                                        // try explicit conversion
                                        objPropertyInfo.SetValue(objObject, Convert.ChangeType(dr.GetValue(arrOrdinals[intProperty]), objPropertyType), null);
                                    }
                                }
                                catch
                                {
                                    objPropertyInfo.SetValue(objObject, Convert.ChangeType(dr.GetValue(arrOrdinals[intProperty]), objPropertyType), null);
                                }
                            }
                        }
                    }
                    else
                    {
                        // property does not exist in datareader
                    }
                }
            }

            return objObject;

        }

        private static T CreateObject<T>(DataRow dr)
        {

            PropertyInfo objPropertyInfo;
            object objValue;
            Type objPropertyType = null;
            int intProperty;
            T objObject = Activator.CreateInstance<T>();
            ArrayList objProperties = GetPropertyInfo(objObject.GetType());
            for (intProperty = 0; intProperty <= objProperties.Count - 1; intProperty++)
            {
                objPropertyInfo = (PropertyInfo)objProperties[intProperty]; if (objPropertyInfo.CanWrite)
                {
                    objValue = Null.SetNull(objPropertyInfo);
                    try
                    {
                        if (System.Convert.IsDBNull(dr[objPropertyInfo.Name]))
                        {
                            objPropertyInfo.SetValue(objObject, objValue, null);
                        }
                        else
                        {
                            try
                            {
                                objPropertyInfo.SetValue(objObject, dr[objPropertyInfo.Name], null);
                            }
                            catch
                            {
                                try
                                {
                                    objPropertyType = objPropertyInfo.PropertyType;
                                    if (objPropertyType.BaseType.Equals(typeof(System.Enum)))
                                    {
                                        int testint = 0;
                                        if (testint.GetType() == dr[objPropertyInfo.Name].GetType())
                                        {
                                            ((PropertyInfo)objProperties[intProperty]).SetValue(objObject, System.Enum.ToObject(objPropertyType, Convert.ToInt32(dr[objPropertyInfo.Name])), null);
                                        }
                                        else
                                        {
                                            ((PropertyInfo)objProperties[intProperty]).SetValue(objObject, System.Enum.ToObject(objPropertyType, dr[objPropertyInfo.Name]), null);
                                        }
                                    }
                                    else
                                    {
                                        objPropertyInfo.SetValue(objObject, Convert.ChangeType(dr[objPropertyInfo.Name], objPropertyType), null);
                                    }
                                }
                                catch
                                {
                                    objPropertyInfo.SetValue(objObject, Convert.ChangeType(dr[objPropertyInfo.Name], objPropertyType), null);
                                }
                            }
                        }
                    }
                    catch
                    {
                        objPropertyInfo.SetValue(objObject, objValue, null);
                    }
                }
            }

            return objObject;

        }

        /// -----------------------------------------------------------------------------
    }

}