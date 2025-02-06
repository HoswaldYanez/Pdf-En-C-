using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
namespace Prueba_de_editor_PDF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Document doc = new Document(PageSize.A4);
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream("Factura.pdf", FileMode.Create));
            doc.Open();

            // Dibujar el rectángulo azul pequeño en la esquina superior derecha
            PdfContentByte cb = writer.DirectContent;
            cb.SetColorFill(new BaseColor(0, 102, 204));  // Azul oscuro
            cb.Rectangle(doc.PageSize.Width - 150, doc.PageSize.Height - 80, 150, 80);
            cb.Fill();

            // Agregar el texto "INVOICE" en el rectángulo azul
            iTextSharp.text.Font titleFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 28, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
            ColumnText.ShowTextAligned(cb, Element.ALIGN_CENTER, new Phrase("INVOICE", titleFont), doc.PageSize.Width - 75, doc.PageSize.Height - 40, 0);

            // Datos de la factura
            PdfPTable infoTable = new PdfPTable(2);
            infoTable.WidthPercentage = 100;
            infoTable.SetWidths(new float[] { 50f, 50f });
            infoTable.SpacingBefore = 20f;

            PdfPCell leftCell = new PdfPCell(new Phrase("Invoice No: #LL93784\nDate: 01.07.2022"));
            leftCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            leftCell.HorizontalAlignment = Element.ALIGN_LEFT;

            PdfPCell rightCell = new PdfPCell(new Phrase("Pay To:\nLaralink Ltd\n86-90 Paul Street, London\nEngland EC2A 4NE\ndemo@gmail.com"));
            rightCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            rightCell.HorizontalAlignment = Element.ALIGN_RIGHT;

            infoTable.AddCell(leftCell);
            infoTable.AddCell(rightCell);
            doc.Add(infoTable);

            // Datos del cliente
            PdfPTable clientTable = new PdfPTable(1);
            clientTable.WidthPercentage = 100;
            clientTable.SpacingBefore = 20f;

            PdfPCell clientCell = new PdfPCell(new Phrase("Invoice To:\nLowell H. Dominguez\n84 Spilman Street, London\nUnited Kingdom\nlowell@gmail.com"));
            clientCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            clientCell.HorizontalAlignment = Element.ALIGN_LEFT;

            clientTable.AddCell(clientCell);
            doc.Add(clientTable);

            // Detalles de los productos/servicios
            PdfPTable itemTable = new PdfPTable(4);
            itemTable.WidthPercentage = 100;
            itemTable.SetWidths(new float[] { 10f, 40f, 20f, 20f, 20f });
            itemTable.SpacingBefore = 20f;

            // Encabezados con fondo azul
            BaseColor headerColor = new BaseColor(0, 102, 204);
            AddHeaderCell(itemTable, "Item", headerColor);
            AddHeaderCell(itemTable, "Description", headerColor);
            AddHeaderCell(itemTable, "Price", headerColor);
            AddHeaderCell(itemTable, "Qty", headerColor);
            AddHeaderCell(itemTable, "Total", headerColor);

            // Agregar filas
            AddItemRow(itemTable, "1.", "Website Design\nSix web page designs and three times revision", "$350", "1", "$350");
            AddItemRow(itemTable, "2.", "Web Development\nConvert pixel-perfect frontend and make it dynamic", "$600", "1", "$600");
            AddItemRow(itemTable, "3.", "App Development\nAndroid & iOS Application Development", "$200", "2", "$400");
            AddItemRow(itemTable, "4.", "Digital Marketing\nFacebook, Youtube and Google Marketing", "$100", "3", "$300");

            doc.Add(itemTable);

            // Totales
            PdfPTable totalTable = new PdfPTable(2);
            totalTable.WidthPercentage = 40;
            totalTable.HorizontalAlignment = Element.ALIGN_RIGHT;
            totalTable.SetWidths(new float[] { 50f, 50f });
            totalTable.SpacingBefore = 20f;

            AddTotalRow(totalTable, "Subtotal", "$1650");
            AddTotalRow(totalTable, "Tax (5%)", "$82");

            PdfPCell grandTotalCell = new PdfPCell(new Phrase("Grand Total", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.WHITE)));
            grandTotalCell.BackgroundColor = headerColor;
            grandTotalCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            grandTotalCell.Padding = 5;
            totalTable.AddCell(grandTotalCell);

            PdfPCell grandTotalValueCell = new PdfPCell(new Phrase("$1732", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.WHITE)));
            grandTotalValueCell.BackgroundColor = headerColor;
            grandTotalValueCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            grandTotalValueCell.Padding = 5;
            totalTable.AddCell(grandTotalValueCell);

            doc.Add(totalTable);

            doc.Close();
            Console.WriteLine("Factura generada con éxito.");

        }

        static void AddHeaderCell(PdfPTable table, string text, BaseColor backgroundColor)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.WHITE)));
            cell.BackgroundColor = backgroundColor;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Padding = 5;
            table.AddCell(cell);
        }

        static void AddItemRow(PdfPTable table, string item, string desc, string price, string qty, string total)
        {
            table.AddCell(new PdfPCell(new Phrase(item)));
            table.AddCell(new PdfPCell(new Phrase(desc)));
            table.AddCell(new PdfPCell(new Phrase(price)));
            table.AddCell(new PdfPCell(new Phrase(qty)));
            table.AddCell(new PdfPCell(new Phrase(total)));
        }

        static void AddTotalRow(PdfPTable table, string label, string value)
        {
            PdfPCell labelCell = new PdfPCell(new Phrase(label));
            labelCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            labelCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            labelCell.Padding = 5;
            table.AddCell(labelCell);

            PdfPCell valueCell = new PdfPCell(new Phrase(value));
            valueCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            valueCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            valueCell.Padding = 5;
            table.AddCell(valueCell);
        }
    }
}
