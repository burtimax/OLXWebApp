using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace OLXWebApp.Code.Helpers
{
    public class ExcelAdParser
    {
        private Application app = null;
        private Workbook wb = null;
        private Worksheet infoSheet = null;
        private Worksheet nameDescSheet = null;


        public ExcelAdParser(string filename)
        {
            this.app = new Application();
            wb = app.Workbooks.Open(filename);
            infoSheet = (Worksheet) wb.Worksheets[0];
            nameDescSheet = (Worksheet) wb.Worksheets[1];
        }


    }
}
