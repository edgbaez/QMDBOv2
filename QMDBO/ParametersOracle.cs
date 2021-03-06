using System;
using System.Collections.Generic;
using System.Linq;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace QMDBO
{
    public class ParametersOracle
    {
        public const int In = 1;
        public const int Out = 2;
        public string name { get; set; }
        public string typeName { get; set; }
        public OracleDbType type
        {
            get
            {
                if (this.typeName.Equals("Char"))
                {
                    return OracleDbType.Char;
                }
                else if (this.typeName.Equals("Date"))
                {
                    return OracleDbType.Date;
                }
                else if (this.typeName.Equals("Double"))
                {
                    return OracleDbType.Double;
                }
                else if (this.typeName.Equals("Int32"))
                {
                    return OracleDbType.Int32;
                }
                else if (this.typeName.Equals("Varchar2"))
                {
                    return OracleDbType.Varchar2;
                }
                else
                {
                    return this.type;
                }
            }
            set
            {
                this.type = this.type;
            }
        }
        public int size { get; set; }
        public string value { get; set; }
    }
}
