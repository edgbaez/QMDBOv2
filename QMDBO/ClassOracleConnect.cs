using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace QMDBO
{
    public class ClassOracleConnect
    {
        private string textExceptionConnection = "Connect failed.";
        private string textExceptionCommand = "Statement failed.";

        public string OracleConnString(string host, string port, string servicename, string user, string pass)
        {
            return String.Format(
              "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})" +
              "(PORT={1}))(CONNECT_DATA=(SERVICE_NAME={2})));User Id={3};Password={4};",
              host,
              port,
              servicename,
              user,
              pass);
        }

        public string OracleTest(string ConnectionString)
        {
            string status;
            OracleConnection OracleCon = new OracleConnection(ConnectionString);
            try
            {
                OracleCon.Open();
                status = "OK";
            }
            catch (OracleException oe)
            {
                status = this.textExceptionConnection + ' ' + oe.Message;
            }
            finally
            {
                if (OracleCon != null)
                {
                    OracleCon.Close();
                    OracleCon.Dispose();
                }
            }
            return status;
        }

        public string[] OracleQuery(string ConnectionString, string sSQL, int typeExecute, string obj_name)
        {
            string[] result = new string[4];
            string[] status = new string[3];
            sSQL = sSQL.Replace("\r\n", "\n");
            OracleConnection OracleCon = new OracleConnection(ConnectionString);
            try
            {
                OracleCon.Open();
                OracleCommand OracleCom = new OracleCommand();
                OracleCom.Connection = OracleCon;
                OracleCom.CommandText = sSQL;
                try
                {
                    if (typeExecute == 1)
                    {
                        result[0] = Convert.ToString(OracleCom.ExecuteNonQuery());
                        status = this.OracleObjectStatus(OracleCom, obj_name);
                    }
                    else
                    {
                        result[0] = Convert.ToString(OracleCom.ExecuteScalar());
                        status = this.OracleObjectStatus(OracleCom, obj_name);
                    }
                    result[1] = status[0];
                    result[2] = status[1];
                    result[3] = status[2];
                    OracleCom.Dispose();
                }
                catch (OracleException oe)
                {
                    result[0] = this.textExceptionCommand + ' ' + oe.Message;
                }
            }
            catch (OracleException oe)
            {
                result[0] = this.textExceptionConnection + ' ' + oe.Message;
            }
            finally
            {
                if (OracleCon != null)
                {
                    OracleCon.Close();
                    OracleCon.Dispose();
                }
            }
            return result;
        }

        private string[] OracleObjectStatus(OracleCommand OracleCom, string obj_name)
        {
            string[] result = new string[3];

            if (!string.IsNullOrEmpty(obj_name))
            {
                string obj_sql = string.Format(@"
SELECT WM_CONCAT(T.OBJECT_TYPE),
      WM_CONCAT(T.STATUS),
      WM_CONCAT(to_char(T.LAST_DDL_TIME, 'DD.MM.YYYY HH24:MI:SS'))
  FROM SYS.ALL_OBJECTS T
 WHERE T.OBJECT_NAME = upper('{0}')
 GROUP BY T.OBJECT_NAME", obj_name);

                obj_sql = obj_sql.Replace("\r\n", "\n");

                OracleCom.CommandText = obj_sql;
                OracleDataReader reader = OracleCom.ExecuteReader();
                while (reader.Read())
                {
                    result[0] = reader.GetString(0);
                    result[1] = reader.GetString(1);
                    result[2] = reader.GetString(2);
                }
                reader.Dispose();
            }
            return result;
        }

        public List<ParametersOracle> OracleProcedure(string ConnectionString, string procedureName, List<ParametersOracle> inParams, List<ParametersOracle> outParams)
        {
            List<ParametersOracle> resultList = new List <ParametersOracle>();
            OracleConnection OracleCon = new OracleConnection(ConnectionString);
            try
            {
                OracleCon.Open();
                OracleCommand cmd = new OracleCommand(procedureName);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = OracleCon;
                cmd.BindByName = true;

                /* In параметры */
                foreach (ParametersOracle inParam in inParams)
                {
                    if (inParam.type == OracleDbType.Date)
                    {
                        DateTime dt;
                        if (String.IsNullOrEmpty(inParam.value))
                        {
                            dt = DateTime.Now;
                        }
                        else
                        {
                            dt = Convert.ToDateTime(inParam.value);
                        }
                        cmd.Parameters.Add(inParam.name, inParam.type).Value = dt;
                    }
                    else
                    {
                        cmd.Parameters.Add(inParam.name, inParam.type).Value = inParam.value;
                    }
                }

                /* Out параметры */
                foreach (ParametersOracle outParam in outParams)
                {
                    cmd.Parameters.Add(outParam.name, outParam.type, outParam.size).Direction = ParameterDirection.Output;
                }

                try
                {
                    cmd.ExecuteNonQuery();
                    foreach (ParametersOracle outParam in outParams)
                    {
                        ParametersOracle resultParam = new ParametersOracle();
                        resultParam.name = outParam.name;
                        resultParam.value = (cmd.Parameters[outParam.name].Value ?? String.Empty).ToString();
                        resultList.Add(resultParam);
                    }
                    cmd.Dispose();
                }
                catch (OracleException oe)
                {
                    ParametersOracle resultParam = new ParametersOracle();
                    resultParam.name = "error";
                    resultParam.value = this.textExceptionCommand + ' ' + oe.Message;
                    resultList.Add(resultParam);
                }
            }
            catch (OracleException oe)
            {
                ParametersOracle resultParam = new ParametersOracle();
                resultParam.name = "error";
                resultParam.value = this.textExceptionConnection + ' ' + oe.Message;
                resultList.Add(resultParam);
            }
            finally
            {
                if (OracleCon != null)
                {
                    OracleCon.Close();
                    OracleCon.Dispose();
                }
            }
            return resultList;
        }


    }
}
