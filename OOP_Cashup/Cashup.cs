using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Drawing;
using Microsoft.Reporting.WinForms;
using System.Reflection;
using System.Data.Odbc;
using Hounds;

namespace OOP_Cashup
{
    class Cashup
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region variables

        #region till variables

        public Decimal R200 { get; set; }
        public Decimal R100 { get; set; }
        public Decimal R50 { get; set; }
        public Decimal R20 { get; set; }
        public Decimal R10 { get; set; }
        public Decimal R5 { get; set; }
        public Decimal R2 { get; set; }
        public Decimal R1 { get; set; }
        public Decimal c50 { get; set; }
        public Decimal c20 { get; set; }
        public Decimal c10 { get; set; }
        public Decimal c5 { get; set; }

        public int R200Amt { get; set; }
        public int R100Amt { get; set; }
        public int R50Amt { get; set; }
        public int R20Amt { get; set; }
        public int R10Amt { get; set; }
        public int R5Amt { get; set; }
        public int R2Amt { get; set; }
        public int R1Amt { get; set; }
        public int c50Amt { get; set; }
        public int c20Amt { get; set; }
        public int c10Amt { get; set; }
        public int c5Amt { get; set; }

        #endregion

        #region Drop Variables

        public int R200Drop { get; set; }
        public int R100Drop { get; set; }
        public int R50Drop { get; set; }
        public int R20Drop { get; set; }
        public int R10Drop { get; set; }
        public int R5Drop { get; set; }
        public int R2Drop { get; set; }
        public int R1Drop { get; set; }
        public int c50Drop { get; set; }
        public int c20Drop { get; set; }
        public int c10Drop { get; set; }
        public int c5Drop { get; set; }


        public Decimal R200DropTotal { get { return R200Drop * 200.00M; } set { R200DropTotal = value; } }
        public Decimal R100DropTotal { get { return R100Drop * 100.00M; } set { R100DropTotal = value; } }
        public Decimal R50DropTotal { get { return R50Drop * 50.00M; } set { R50DropTotal = value; } }
        public Decimal R20DropTotal { get { return R20Drop * 20.00M; } set { R20DropTotal = value; } }
        public Decimal R10DropTotal { get { return R10Drop * 10.00M; } set { R10DropTotal = value; } }
        public Decimal R5DropTotal { get { return R5Drop * 5.00M; } set { R5DropTotal = value; } }
        public Decimal R2DropTotal { get { return R2Drop * 2.00M; } set { R2DropTotal = value; } }
        public Decimal R1DropTotal { get { return R1Drop * 1.00M; } set { R1DropTotal = value; } }
        public Decimal c50DropTotal { get { return c50Drop * 0.50M; } set { c50DropTotal = value; } }
        public Decimal c20DropTotal { get { return c20Drop * 0.20M; } set { c20DropTotal = value; } }
        public Decimal c10DropTotal { get { return c10Drop * 0.10M; } set { c10DropTotal = value; } }
        public Decimal c5DropTotal { get { return c5Drop * 0.05M; } set { c5DropTotal = value; } }

        public Decimal drop { get; set; }
        private Decimal dropTemp;

        #endregion

        private Decimal cashFloat;
        public Decimal CashFloat { get { return cashFloat; } set { cashFloat = value; } }

        private Decimal Total;
        public Decimal total { get { return Total; } }

        public Decimal skimmed { set; get; }

        public Decimal subTotal { get; set; }

        private string tillNum;
        public string TillNum { get { return tillNum; } set { tillNum = value; } }

        private Decimal droppedTotal;
        public Decimal DroppedTotal { get { return droppedTotal; } set { droppedTotal = value; } }

        public int NumChecks { get; set; }
        public Decimal ChecksValue { get; set; }

        private string date;
        public string Date { get { return date; } }

        private string name;
        public string Name { get { return name; } set { name = value; } }

        private Decimal dropTotal;
        public Decimal DropTotal { get { return droppedTotal; } set { droppedTotal = value; } }

        private IList<Stream> m_streams;
        private int m_currentPageIndex;

        private Decimal correction;
        public decimal Correction {
            get {
                return correction;
            }
        }

        #endregion

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Cashup() {

            On_Cashup_Load(this, null);
            date = DateTime.Now.ToString("dddd  dd MMMM yyyy");

            R200Amt = 0;
            R100Amt = 0;
            R50Amt = 0;
            R20Amt = 0;
            R10Amt = 0;
            R5Amt = 0;
            R2Amt = 0;
            R1Amt = 0;
            c50Amt = 0;
            c20Amt = 0;
            c10Amt = 0;
            c5Amt = 0;

            R200 = R200Amt * 200.00m;
            R100 = R100Amt * 100.00m;
            R50 = R50Amt * 50.00m;
            R20 = R20Amt * 20.00m;
            R10 = R10Amt * 10.00m;
            R5 = R5Amt * 5.00m;
            R2 = R2Amt * 2.00m;
            R1 = R1Amt * 1.00m;
            c50 = c50Amt * 0.50m;
            c20 = c20Amt * 0.20m;
            c10 = c10Amt * 0.10m;
            c5 = c5Amt * 0.05m;

            R200Drop = 0;
            R100Drop = 0;
            R50Drop = 0;
            R20Drop = 0;
            R10Drop = 0;
            R5Drop = 0;
            R2Drop = 0;
            R1Drop = 0;
            c50Drop = 0;
            c20Drop = 0;
            c10Drop = 0;
            c5Drop = 0;

            subTotal = R200 + R100 + R50 + R20 + R10 + R5 + R2 +
                R1 + c50 + c20 + c10 + c5;

            drop = subTotal - cashFloat + skimmed;
            dropTemp = drop;

            Total = subTotal - drop;
            this.droppedTotal = 0.00M;
            correction = 0.0M;

            log.Debug("cashup object created. with default constructor.");
        }

        /// <summary>
        /// Paramatised constructor 
        /// </summary>
        /// <param name="amt200"></param>
        /// <param name="amt100"></param>
        /// <param name="amt50"></param>
        /// <param name="amt20"></param>
        /// <param name="amt10"></param>
        /// <param name="amt5"></param>
        /// <param name="amt2"></param>
        /// <param name="amt1"></param>
        /// <param name="amt50c"></param>
        /// <param name="amt20c"></param>
        /// <param name="amt10c"></param>
        /// <param name="amt5c"></param>
        /// <param name="DroppedTotal"></param>
        /// <param name="Register"></param>
        public Cashup(int amt200, int amt100, int amt50, int amt20, int amt10, int amt5,
            int amt2, int amt1, int amt50c, int amt20c, int amt10c, int amt5c, Decimal DroppedTotal,
            string Register) {
            On_Cashup_Load(this, null);

            log.Debug("cashup object timer started.");

            date = DateTime.Now.ToString("dddd  dd MMMM yyyy");

            R200Amt = amt200;
            R100Amt = amt100;
            R50Amt = amt50;
            R20Amt = amt20;
            R10Amt = amt10;
            R5Amt = amt5;
            R2Amt = amt2;
            R1Amt = amt1;
            c50Amt = amt50c;
            c20Amt = amt20c;
            c10Amt = amt10c;
            c5Amt = amt5c;

            R200 = R200Amt * 200.00m;
            R100 = R100Amt * 100.00m;
            R50 = R50Amt * 50.00m;
            R20 = R20Amt * 20.00m;
            R10 = R10Amt * 10.00m;
            R5 = R5Amt * 5.00m;
            R2 = R2Amt * 2.00m;
            R1 = R1Amt * 1.00m;
            c50 = c50Amt * 0.50m;
            c20 = c20Amt * 0.20m;
            c10 = c10Amt * 0.10m;
            c5 = c5Amt * 0.05m;

            subTotal = R200 + R100 + R50 + R20 + R10 + R5 + R2 +
                 R1 + c50 + c20 + c10 + c5;

            drop = subTotal - cashFloat + skimmed;
            dropTemp = drop;

            Total = subTotal - drop;
            this.droppedTotal = DroppedTotal;
            this.dropTotal = DroppedTotal + ChecksValue;

            tillNum = Register;

            log.Debug("cashup object created using constructor 1(value initiation)");

        }

        /// <summary>
        /// paramatised constructor 2
        /// </summary>
        /// <param name="DroppedTotal"></param>
        /// <param name="Register"></param>
        public Cashup(Decimal DroppedTotal, string Register) {
            On_Cashup_Load(this, null);
            date = DateTime.Today.ToString("dddd  dd MMMM yyyy");

            R200Amt = 0;
            R100Amt = 0;
            R50Amt = 0;
            R20Amt = 0;
            R10Amt = 0;
            R5Amt = 0;
            R2Amt = 0;
            R1Amt = 0;
            c50Amt = 0;
            c20Amt = 0;
            c10Amt = 0;
            c5Amt = 0;

            R200 = R200Amt * 200.00m;
            R100 = R100Amt * 100.00m;
            R50 = R50Amt * 50.00m;
            R20 = R20Amt * 20.00m;
            R10 = R10Amt * 10.00m;
            R5 = R5Amt * 5.00m;
            R2 = R2Amt * 2.00m;
            R1 = R1Amt * 1.00m;
            c50 = c50Amt * 0.50m;
            c20 = c20Amt * 0.20m;
            c10 = c10Amt * 0.10m;
            c5 = c5Amt * 0.05m;

            R200Drop = 0;
            R100Drop = 0;
            R50Drop = 0;
            R20Drop = 0;
            R10Drop = 0;
            R5Drop = 0;
            R2Drop = 0;
            R1Drop = 0;
            c50Drop = 0;
            c20Drop = 0;
            c10Drop = 0;
            c5Drop = 0;

            subTotal = R200 + R100 + R50 + R20 + R10 + R5 + R2 +
                R1 + c50 + c20 + c10 + c5;

            drop = subTotal - cashFloat + skimmed;
            dropTemp = drop;
            this.droppedTotal = DroppedTotal;

            Total = subTotal - drop;
            tillNum = Register;
            log.Debug("cashup object created using constructor 1(drop total and register number)");
        }

        //Summary Drop Calculation
        //takes each Denomination and figures out how much of each denomination it needs to 
        //remove from the till in order to make the next days float
        //
        private int Drop(decimal amt, int dropAmt, int actualAmt, ref bool flagerror) {
            log.Debug("dropping R" + amt);
            while((dropTemp - amt >= 0) && dropAmt < actualAmt && !(dropAmt > actualAmt)) {
                dropAmt++;
                dropTemp -= amt;
                Thread.Sleep(0);
            }

            if(dropTemp == amt && actualAmt == 0)
                flagerror = true;

            log.Debug("calculated amount to drop for R" + amt + " is " + dropAmt);
            log.Debug("amount left to drop: R" + dropTemp);
            return dropAmt;

        }

        //Summary
        //public Drop method that assigns the variables to be sent to the form and report
        public void Drop() {
            bool errorFlag = false;//if this is false there are no errors.
            Update();
            while(drop > droppedTotal) {

                R200Drop = Drop(200, R200Drop, R200Amt, ref errorFlag);
                R100Drop = Drop(100, R100Drop, R100Amt, ref errorFlag);
                R50Drop = Drop(50, R50Drop, R50Amt, ref errorFlag);
                R20Drop = Drop(20, R20Drop, R20Amt, ref errorFlag);
                R10Drop = Drop(10, R10Drop, R10Amt, ref errorFlag);
                R5Drop = Drop(5, R5Drop, R5Amt, ref errorFlag);
                R2Drop = Drop(2, R2Drop, R2Amt, ref errorFlag);
                R1Drop = Drop(1, R1Drop, R1Amt, ref errorFlag);
                c50Drop = Drop(0.50M, c50Drop, c50Amt, ref errorFlag);
                c20Drop = Drop(0.20M, c20Drop, c20Amt, ref errorFlag);
                c10Drop = Drop(0.10M, c10Drop, c10Amt, ref errorFlag);
                c5Drop = Drop(0.05M, c5Drop, c5Amt, ref errorFlag);
                if(errorFlag) {
                    MessageBox.Show("Error trying to drop something when its not available.", "Manual Intervention Required");
                    log.Error("error dropping");
                    break;
                }
                Update();
            }



        }

        /// <summary>
        /// The update method to update all values
        /// </summary>
        private void Update() {
            R200 = R200Amt * 200.00m;
            R100 = R100Amt * 100.00m;
            R50 = R50Amt * 50.00m;
            R20 = R20Amt * 20.00m;
            R10 = R10Amt * 10.00m;
            R5 = R5Amt * 5.00m;
            R2 = R2Amt * 2.00m;
            R1 = R1Amt * 1.00m;
            c50 = c50Amt * 0.50m;
            c20 = c20Amt * 0.20m;
            c10 = c10Amt * 0.10m;
            c5 = c5Amt * 0.05m;

            subTotal = R200 + R100 + R50 + R20 + R10 + R5 + R2 +
                R1 + c50 + c20 + c10 + c5;

            drop = subTotal - cashFloat + skimmed;
            dropTemp = drop;

            this.Total = subTotal - drop;
            this.droppedTotal = R200Drop * 200 + R100Drop * 100 + R50Drop * 50 + R20Drop * 20 + R10Drop * 10 +
                R5Drop * 5 + R2Drop * 2 +
                R1Drop * 1 + c50Drop * 0.50M + c20Drop * 0.2M + c10Drop * 0.1M + c5Drop * 0.05M;
            Thread.Sleep(0);
        }

        /// <summary>
        /// starts the update loop on a timmer to update every 20ms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void On_Cashup_Load(object sender, EventArgs e) {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = (20); // 20 milisecs
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            log.Debug("cashup object timer started");
        }

        private void timer_Tick(object sender, EventArgs e) {
            Update();
        }

        /// <summary>
        /// Prints the Document, saves a PDF of the Document and writes all the properties of the object to the DB.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnPrint_Click(object sender, EventArgs e) {

            frmPrintPreview rpt = new frmPrintPreview();
            ReportViewer rv = rpt.reportViewer1;
            rv.SetDisplayMode(DisplayMode.Printayout);
            rv.ZoomMode = ZoomMode.PageWidth;
            rv.LocalReport.ReportPath = "CashupReport.rdlc";

            rv.LocalReport.DataSources.Add(new ReportDataSource("Cashup", new BindingSource(this, null)));

            Export(rv.LocalReport);
            printPdf(rv.LocalReport);
            Print();
            SaveToDB();

        }

        /// <summary>
        /// Only Prints the Document.
        /// </summary>
        public void PrintOnly() {

            frmPrintPreview rpt = new frmPrintPreview();
            ReportViewer rv = rpt.reportViewer1;
            rv.SetDisplayMode(DisplayMode.Printayout);
            rv.ZoomMode = ZoomMode.PageWidth;
            rv.LocalReport.ReportPath = "CashupReport.rdlc";

            rv.LocalReport.DataSources.Add(new ReportDataSource("Cashup", new BindingSource(this, null)));

            Export(rv.LocalReport);
            Print();

        }

        /// <summary>
        /// Only Saves the PDF.
        /// </summary>
        public void SavePDF() {

            frmPrintPreview rpt = new frmPrintPreview();
            ReportViewer rv = rpt.reportViewer1;
            rv.SetDisplayMode(DisplayMode.Printayout);
            rv.ZoomMode = ZoomMode.PageWidth;
            rv.LocalReport.ReportPath = "CashupReport.rdlc";

            rv.LocalReport.DataSources.Add(new ReportDataSource("Cashup", new BindingSource(this, null)));

            printPdf(rv.LocalReport);

        }

        #region Printing

        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek) {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        /// <summary>
        /// Export the given report as an EMF (Enhanced Metafile) file.
        /// </summary>
        /// <param name="report"></param>
        private void Export(LocalReport report) {
            log.Debug("exporting data to EMF file for printing");
            string deviceInfo =
              @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>210mm</PageWidth>
                <PageHeight>280mm</PageHeight>
                <MarginTop>1cm</MarginTop>
                <MarginLeft>1cm</MarginLeft>
                <MarginRight>1cm</MarginRight>
                <MarginBottom>1cm</MarginBottom>
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream,
               out warnings);
            foreach(Stream stream in m_streams)
                stream.Position = 0;
        }

        private void PrintPage(object sender, PrintPageEventArgs ev) {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int) ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int) ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        private void Print() {
            if(m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            PrintDialog pd = new PrintDialog();
            var result = pd.ShowDialog();
            if(result == System.Windows.Forms.DialogResult.Cancel || result == System.Windows.Forms.DialogResult.No) { return; }
            printDoc.PrinterSettings = pd.PrinterSettings;
            if(!printDoc.PrinterSettings.IsValid) {
                MessageBox.Show("Printer Settings Not Found");
                log.Error("printer settings not Found.");

            } else {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
                log.Debug("printing");
                log.Info("Document Printed");
            }



        }
        
        /// <summary>
        /// saves the report as a pdf.
        /// </summary>
        /// <param name="report">local report  of document to be saved as PDF.</param>
        private void printPdf(LocalReport report) {

            log.Info("creating PDF");

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;
            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            byte[] bytes = report.Render("PDF", null, out mimeType, out encoding, out filenameExtension,
                out streamids, out warnings);



            if(!Directory.Exists(Path.Combine(assemblyPath, "archive"))) {

                log.Info("archive does not exist creating");
                Directory.CreateDirectory(Path.Combine(assemblyPath, "archive"));

            } else {
                log.Debug("archive folder exists");
            }

            using(FileStream fs = new FileStream(Path.Combine(assemblyPath, "archive/" + date + ".pdf"),
                FileMode.Create)) {

                fs.Write(bytes, 0, bytes.Length);

            }

            log.Info("pdf saved");

        }
        #endregion

        public void ClearDrop() {
            this.R200Amt = 0;
            this.R100Amt = 0;
            this.R50Amt = 0;
            this.R20Amt = 0;
            this.R10Amt = 0;
            this.R5Amt = 0;
            this.R2Amt = 0;
            this.R1Amt = 0;
            this.c50Amt = 0;
            this.c20Amt = 0;
            this.c10Amt = 0;
            this.c5Amt = 0;

            R200Drop = 0;
            R100Drop = 0;
            R50Drop = 0;
            R20Drop = 0;
            R10Drop = 0;
            R5Drop = 0;
            R2Drop = 0;
            R1Drop = 0;
            c50Drop = 0;
            c20Drop = 0;
            c10Drop = 0;
            c5Drop = 0;
            log.Debug("drop data cleared.");
        }

        /// <summary>
        /// saves all the properties of this object to a DB specified in the settings.
        /// </summary>
        public void SaveToDB() {
            log.Info("saving data to Database");
            using(OdbcConnection con = new OdbcConnection(RuntimeSettings.conString)) {
                try {
                    con.Open();
                    log.Debug("opened connection to DB");
                } catch(Exception ex) {
                    MessageBox.Show("Could not connect to the Database");
                    log.Error("cannot connect to Database.", ex);
                }

                string query = string.Format(@"INSERT INTO `Cashup_test`.`Cashup_data`(`cashup_TillNum`, `cashup_CashierName`,`cashup_Date`, `cashup_float`,`cashup_R200Value`,`cashup_R100Value`, `cashup_R50Value`,`cashup_R20Value`,`cashup_R10Value`, `cashup_R5Value`,`cashup_R2Value`, `cashup_R1Value`,`cashup_50cValue`, `cashup_20cValue`, `cashup_10cValue`,`cashup_5cValue`, `cashup_R200Amount`, `cashup_R100Amount`, `cashup_R50Amount`, `cashup_R20Amount`,`cashup_R10Amount`, `cashup_R5Amount`, `cashup_R2Amount`, `cashup_R1Amount`, `cashup_50cAmount`,`cashup_20cAmount`, `cashup_10cAmount`, `cashup_5cAmount`, `cashup_R200DropValue`,`cashup_R100DropValue`, `cashup_R50DropValue`, `cashup_R20DropValue`, `cashup_R10DropValue`,`cashup_R5DropValue`, `cashup_R2DropValue`, `cashup_R1DropValue`, `cashup_50cDropValue`, `cashup_20cDropValue`, `cashup_10cDropValue`, `cashup_5cDropValue`, `cashup_R200DropAmount`, `cashup_R100DropAmount`, `cashup_R50DropAmount`, `cashup_R20DropAmount`, `cashup_R10DropAmount`, `cashup_R5DropAmount`, `cashup_R2DropAmount`, `cashup_R1DropAmount`, `cashup_50cDropAmount`,`cashup_20cDropAmount`, `cashup_10cDropAmount`, `cashup_5cDropAmount`, `cashup_NumCheques`,`cashup_ChequesValue`)VALUES({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39},{40},{41},{42},{43},{44},{45},{46},{47},{48},{49},{50},{51},{52},{53});",
tillNum.Replace("Register #", ""), "\"" + name + "\"", "\"" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "\"", cashFloat, R200, R100, R50, R20, R10, R5,
R2, R1, c50, c20, c10, c5, R200Amt, R100Amt, R50Amt, R20Amt, R10Amt, R5Amt, R2Amt, R1Amt, c50Amt,
c20Amt, c10Amt, c5Amt, R200DropTotal, R100DropTotal, R50DropTotal, R20DropTotal, R10DropTotal,
R5DropTotal, R2DropTotal, R1DropTotal, c50DropTotal, c20DropTotal, c10DropTotal, c5DropTotal,
R200Drop, R100Drop, R50Drop, R20Drop, R10Drop, R5Drop, R2Drop, R1Drop, c50Drop, c20Drop, c10Drop,
c5Drop, NumChecks, ChecksValue);

                log.Debug("sql Query: " + query);
                OdbcCommand cmd = new OdbcCommand(query, con);
                try {

                    cmd.ExecuteNonQuery();
                    log.Debug("successfully excecuted mysql query.");

                } catch(Exception ex) {
                    log.Error("could not write to DB", ex);
                }
                con.Close();
                log.Debug("Closed Connection to DB");

            }
        }

        public bool LoadFromDB() {



            return true;
        }

    }
}
