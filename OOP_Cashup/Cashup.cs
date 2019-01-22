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

namespace OOP_Cashup
{
    class Cashup
    {

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

        private Decimal wssCash;
        private Decimal cashDiscrepancy;

        public Decimal WSSCash { get { return WssCard; } set { wssCash = value; } }
        public Decimal CashDiscrepancy { get { return cashDiscrepancy; } set { cashDiscrepancy = value; } }

        public Decimal CardBanked { get => cardBanked; set => cardBanked = value; }
        public Decimal WssCard { get => wssCard; set => wssCard = value; }
        public Decimal CardDiscrepancy { get => cardDiscrepancy; set => cardDiscrepancy = value; }

        private Decimal cardBanked;
        private Decimal wssCard;
        private Decimal cardDiscrepancy;

        #endregion

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Cashup() {
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

            Total = subTotal - drop;
            this.droppedTotal = 0.00M;
            correction = 0.0M;

            cardBanked = 0.0M;
            cardDiscrepancy = 0.0M;
            wssCard = 0.0M;

            wssCash = 0.0M;
            cashDiscrepancy = 0.0M;
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
            date = DateTime.Today.ToString("dddd  dd MMMM yyyy");

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

            cardBanked = 0.0M;
            cardDiscrepancy = 0.0M;
            wssCard = 0.0M;

            wssCash = 0.0M;
            cashDiscrepancy = 0.0M;

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

            cardBanked = 0.0M;
            cardDiscrepancy = 0.0M;
            wssCard = 0.0M;

            wssCash = 0.0M;
            cashDiscrepancy = 0.0M;

        }

        //Summary Drop Calculation
        //takes each Denomination and figures out how much of each denomination it needs to 
        //remove from the till in order to make the next days float
        //
        private int Drop(decimal amt, int dropAmt, int actualAmt, ref bool flagerror) {

            while ((dropTemp - amt >= 0) && dropAmt < actualAmt && !(dropAmt > actualAmt)) {
                dropAmt++;
                dropTemp -= amt;
                Thread.Sleep(0);
            }

            if (dropTemp == amt && actualAmt == 0)
                flagerror = true;

            return dropAmt;

        }

        //Summary
        //public Drop method that assigns the variables to be sent to the form and report
        public void Drop() {
            bool errorFlag = false;//if this is false there are no errors.
            Update();
            while (drop > droppedTotal) {

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
                if (errorFlag) {
                    MessageBox.Show("Error trying to drop something when its not available.", "Manual Intervention Required");
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
            
            cardDiscrepancy = drop - wssCash;
            cashDiscrepancy = cardBanked - wssCard;


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
        }

        private void timer_Tick(object sender, EventArgs e) {
            Update();
        }

        public void btnPrint_Click(object sender, EventArgs e) {

            frmPrintPreview rpt = new frmPrintPreview();
            ReportViewer rv = rpt.reportViewer1;
            rv.SetDisplayMode(DisplayMode.PrintLayout);
            rv.ZoomMode = ZoomMode.PageWidth;
            rv.LocalReport.ReportPath = "CashupReport.rdlc";

            rv.LocalReport.DataSources.Add(new ReportDataSource("Cashup", new BindingSource(this, null)));

            Export(rv.LocalReport);
            printPdf(rv.LocalReport);
            Print();

        }

        #region Printing


        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek) {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        // Export the given report as an EMF (Enhanced Metafile) file.
        private void Export(LocalReport report) {
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
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        private void PrintPage(object sender, PrintPageEventArgs ev) {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
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
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            PrintDialog pd = new PrintDialog();
            var result = pd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel || result == System.Windows.Forms.DialogResult.No) { return; }
            printDoc.PrinterSettings = pd.PrinterSettings;
            if (!printDoc.PrinterSettings.IsValid) {
                MessageBox.Show("Printer Settings Not Found");

            } else {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }



        }

        private void printPdf(LocalReport report) {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;
            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            byte[] bytes = report.Render("PDF", null, out mimeType, out encoding, out filenameExtension,
                out streamids, out warnings);



            if (!Directory.Exists(Path.Combine(assemblyPath, "archive"))) {

                Directory.CreateDirectory(Path.Combine(assemblyPath, "archive"));

            } else {
                Console.WriteLine("WHAT?");
            }

            using (FileStream fs = new FileStream(Path.Combine(assemblyPath, "archive/" + date + ".pdf"),
                FileMode.Create)) {

                fs.Write(bytes, 0, bytes.Length);

            }
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

        }


    }
}
