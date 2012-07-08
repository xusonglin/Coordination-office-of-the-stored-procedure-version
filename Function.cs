using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace JiaoShiXinXiTongJi
{
   public static  class Function
    {
       public static  string GetJSONString(DataTable dt,int Total)
       {
           StringBuilder jsonBuilder = new StringBuilder();
           jsonBuilder.Append("{\"total\":");
           jsonBuilder.Append(Total);
           jsonBuilder.Append(",\"rows\":[");
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               jsonBuilder.Append("{");
               for (int j = 0; j < dt.Columns.Count; j++)
               {
                   jsonBuilder.Append("\"");
                   jsonBuilder.Append(dt.Columns[j].ColumnName);
                   jsonBuilder.Append("\":\"");
                   jsonBuilder.Append(dt.Rows[i][j].ToString());
                   jsonBuilder.Append("\",");
               }
               jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
               jsonBuilder.Append("},");
           }
           jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
           jsonBuilder.Append("]");
           jsonBuilder.Append("}");
           return jsonBuilder.ToString();
       }
    }
}
