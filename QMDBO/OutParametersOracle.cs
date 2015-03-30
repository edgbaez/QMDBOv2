using System;
using System.Collections.Generic;
using System.Linq;
using Oracle.DataAccess.Client;

namespace QMDBO
{
    public class OutParametersOracle
    {
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
                else if (this.typeName.Equals("Decimal"))
                {
                    return OracleDbType.Decimal;
                }
                else if (this.typeName.Equals("Double"))
                {
                    return OracleDbType.Double;
                }
                else if (this.typeName.Equals("Int64"))
                {
                    return OracleDbType.Int64;
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
