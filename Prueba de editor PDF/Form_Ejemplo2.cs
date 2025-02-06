using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace Prueba_de_editor_PDF
{
    public partial class Form_Ejemplo2 : Form
    {
        private DataTable productos;
        private string tempPdfPath = Path.Combine(Path.GetTempPath(), "preview_factura.pdf");
        public Form_Ejemplo2()
        {
            InitializeComponent();
            InicializarProductos();
        }

        // Inicializar la lista de productos
        private void InicializarProductos()
        {
            productos = new DataTable();
            productos.Columns.Add("Cantidad", typeof(int));
            productos.Columns.Add("Descripcion", typeof(string));
            productos.Columns.Add("PrecioUnitario", typeof(decimal));
            productos.Columns.Add("Importe", typeof(decimal));

            productos.Rows.Add(2, "Producto 1", 10.50m, 21.00m);
            productos.Rows.Add(1, "Producto 2", 20.00m, 20.00m);
            productos.Rows.Add(3, "Producto 3", 15.75m, 47.25m);
            productos.Rows.Add(2, "Producto 1", 10.50m, 21.00m);
            productos.Rows.Add(1, "Producto 2", 20.00m, 20.00m);
            productos.Rows.Add(3, "Producto 3", 15.75m, 47.25m);
            productos.Rows.Add(2, "Producto 1", 10.50m, 21.00m);
            productos.Rows.Add(1, "Producto 2", 20.00m, 20.00m);
            productos.Rows.Add(3, "Producto 3", 15.75m, 47.25m);

            dataGridViewProductos.DataSource = productos;
            dataGridViewProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnGenerarFactura_Click(object sender, EventArgs e)
        {
            // Crear el documento PDF
            Document doc = new Document(PageSize.A4, 20, 20, 20, 20);

            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(tempPdfPath, FileMode.Create));
            doc.Open();
            
            // Fuentes
            BaseFont helvetica = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font tituloFont = new iTextSharp.text.Font(helvetica, 18, iTextSharp.text.Font.BOLD, BaseColor.DARK_GRAY);
            iTextSharp.text.Font normalFont = new iTextSharp.text.Font(helvetica, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font negritaFont = new iTextSharp.text.Font(helvetica, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font BlacaFont = new iTextSharp.text.Font(helvetica, 10, iTextSharp.text.Font.BOLD, BaseColor.WHITE);

            // Encabezado
            PdfPTable headerTable = new PdfPTable(3);
            headerTable.WidthPercentage = 100;
            headerTable.SetWidths(new float[] { 20, 55, 25 });

            PdfContentByte canvas = writer.DirectContent;

            // Cargar la imagen
            string imagePath = "DSC03427.JPG"; // Cambia esto por la ruta de tu imagen
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imagePath);
            iTextSharp.text.Image Fondo = iTextSharp.text.Image.GetInstance(imagePath);
            // Ajustar el tamaño de la imagen (opcional)
            logo.ScaleToFit(50f, 50f); // Cambia los valores según el tamaño deseado

            // Crear una celda con la imagen
            PdfPCell imageCell = new PdfPCell(logo)
            {
                Border = iTextSharp.text.Rectangle.NO_BORDER, // Sin bordes
                HorizontalAlignment = Element.ALIGN_CENTER,   // Centrar horizontalmente
                VerticalAlignment = Element.ALIGN_MIDDLE     // Centrar verticalmente
            };

            // Agregar la celda con la imagen a la tabla
            headerTable.AddCell(imageCell);

            // Ajustar el tamaño de la imagen (opcional)
            Fondo.ScaleToFit(300f, 300f); // Cambia los valores según el tamaño deseado

            // Posicionar la imagen en el centro de la página
            float xPosition = (PageSize.A4.Width - Fondo.ScaledWidth) / 2; // Centrar horizontalmente
            float yPosition = (PageSize.A4.Height - Fondo.ScaledHeight) / 2; // Centrar verticalmente
            Fondo.SetAbsolutePosition(xPosition, yPosition);
            canvas.SaveState();
            // Hacer la imagen semi-transparente
            PdfGState gState = new PdfGState
            {
                FillOpacity = 0.3f, // Opacidad del relleno (0.0 = completamente transparente, 1.0 = completamente opaco)
                StrokeOpacity = 0.3f // Opacidad del borde
            };
            canvas.SetGState(gState);

            // Agregar la imagen al lienzo
            canvas.AddImage(Fondo);
            canvas.RestoreState();
            // Obtener el lienzo para dibujar


            // Configurar colores
            canvas.SetColorStroke(BaseColor.WHITE); // Color del borde
            canvas.SetRGBColorFill(0, 167, 255); // Color de relleno (púrpura)
                                                 // Dimensiones de la página
            float pageWidth = PageSize.A4.Width;  // Ancho de la página (595 puntos)
            float pageHeight = PageSize.A4.Height; // Altura de la página (842 puntos)
            
            // Dimensiones del trapecio
            float baseMayor = 300f; // Longitud de la base mayor
            float altura = 70f;    // Altura del trapecio

            // Coordenadas para centrar el trapecio
            float centerX = 448;//pageWidth / 2;  // Centro horizontal de la página
            float centerY = 800;//pageHeight / 2; // Centro vertical de la página

            float x1 = centerX + baseMayor / 2; // Vértice inferior izquierdo (base mayor)
            float y1 = centerY - altura / 2;    // Vértice inferior izquierdo
            float x2 = 420;// centerX - baseMayor / 2; // Vértice inferior derecho (base mayor)
            float y2 = centerY - altura / 2;    // Vértice inferior derecho
            float x3 = 350;//centerX + baseMenor / 2; // Vértice superior derecho (base menor)
            float y3 = centerY + altura / 2;    // Vértice superior derecho
            float x4 = x1;                      // Vértice superior izquierdo (perpendicular al lado izquierdo)
            float y4 = y3;

            // Dibujar el trapecio
            canvas.MoveTo(x1, y1); // Mover al primer vértice
            canvas.LineTo(x2, y2); // Línea hacia el segundo vértice
            canvas.LineTo(x3, y3); // Línea hacia el tercer vértice
            canvas.LineTo(x4, y4); // Línea hacia el cuarto vértice
            canvas.ClosePathFillStroke(); // Cerrar el camino, rellenar y dibujar el borde

            float x12 = centerX + baseMayor / 2; // Vértice inferior izquierdo (base mayor)
            float y12 = 750 - 20f / 2;    // Vértice inferior izquierdo
            float x22 = 447;// centerX - baseMayor / 2; // Vértice inferior derecho (base mayor)
            float y22 = 750 - 20f / 2;    // Vértice inferior derecho
            float x32 = 424;//centerX + baseMenor / 2; // Vértice superior derecho (base menor)
            float y32 = 750 + 20f / 2;    // Vértice superior derecho
            float x42 = x12;                      // Vértice superior izquierdo (perpendicular al lado izquierdo)
            float y42 = y32;

            canvas.MoveTo(x12, y12); // Mover al primer vértice
            canvas.LineTo(x22, y22); // Línea hacia el segundo vértice
            canvas.LineTo(x32, y32); // Línea hacia el tercer vértice
            canvas.LineTo(x42, y42); // Línea hacia el cuarto vértice
            canvas.ClosePathFillStroke(); // Cerrar el camino, rellenar y dibujar el borde
            
            // Celda vacía (izquierda)
            //headerTable.AddCell(new PdfPCell(new Phrase("")) { Border = iTextSharp.text.Rectangle.NO_BORDER });

            // Centro: Datos del emisor
            PdfPCell centerCell = new PdfPCell();
            centerCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            centerCell.AddElement(new Paragraph(""));
            headerTable.AddCell(centerCell);


            // Derecha: Número de factura
            PdfPCell rightCell = new PdfPCell();
            rightCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            rightCell.AddElement(new Paragraph(" ", new iTextSharp.text.Font(helvetica, 28, iTextSharp.text.Font.BOLD, BaseColor.WHITE)) { Alignment = Element.ALIGN_TOP });
            rightCell.AddElement(new Paragraph(" ", normalFont) { Alignment = Element.ALIGN_CENTER });
            rightCell.AddElement(new Paragraph("N°: FAC-000000000 ", new iTextSharp.text.Font(helvetica, 12, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)) { Alignment = Element.ALIGN_CENTER });

            headerTable.AddCell(rightCell);

            doc.Add(headerTable);


            // Configurar la fuente y el tamaño del texto
            BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            canvas.SetFontAndSize(baseFont, 28); // Fuente Helvetica, tamaño 12

            // Configurar el color del texto (opcional)
            canvas.SetColorFill(BaseColor.WHITE);

            // Posicionar el texto en una ubicación específica
            string texto = "FACTURA";
            float x = 438f; // Coordenada X (horizontal)
            float y = 790f; // Coordenada Y (vertical)
            canvas.BeginText();
            canvas.ShowTextAligned(Element.ALIGN_LEFT, texto, x, y, 0); // Alineación izquierda, sin rotación
            canvas.EndText();

            canvas.SetFontAndSize(baseFont, 14); // Fuente Helvetica, tamaño 12
            canvas.SetColorFill(BaseColor.BLACK);
            string Nombre_Empresa = "CORPORACION DE PRUEBA, C.A.";
            x = 108f; // Coordenada X (horizontal)
            y = 810f; // Coordenada Y (vertical)
            canvas.BeginText();
            canvas.ShowTextAligned(Element.ALIGN_LEFT, Nombre_Empresa, x, y, 0); // Alineación izquierda, sin rotación
            canvas.EndText();

            string DireccionFiscal = "Dirección Fiscal:";
            string RestoTexto = " VDA 5 Y 6 con calle Tacagua Qta Atamare Nro 7 Urb. La Atlántida, CATIA LA MAR, VARGAS. ZONA POSTAL 1162.";
            x = 110f; // Coordenada X (horizontal)
            y = 805f; // Coordenada Y (vertical)
            float width = 200f; // Ancho del área de texto
            float height = 50f; // Altura del área de texto
            ColumnText columnText = new ColumnText(canvas);
            iTextSharp.text.Font boldFont = new iTextSharp.text.Font(helvetica, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK); // Fuente en negrita
            iTextSharp.text.Font normal = new iTextSharp.text.Font(helvetica, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK); // Fuente normal
            Phrase phrase = new Phrase(7f);
            phrase.Add(new Chunk(DireccionFiscal, boldFont)); // Texto en negrita
            phrase.Add(new Chunk(RestoTexto, normal)); // Texto normal
            columnText.SetSimpleColumn(x, y - height, x + width, y); // Define el área del texto
            columnText.AddElement(phrase); // Agrega el texto
            columnText.Go(); // Renderiza el texto

            canvas.SetFontAndSize(baseFont, 7); // Fuente Helvetica, tamaño 12
            canvas.SetColorFill(BaseColor.BLACK);
            string Web = "www.google.com.ve.ejemplo.com";
            x = 110f; // Coordenada X (horizontal)
            y = 775f; // Coordenada Y (vertical)
            canvas.BeginText();
            canvas.ShowTextAligned(Element.ALIGN_LEFT, Web, x, y, 0); // Alineación izquierda, sin rotación
            canvas.EndText();

            canvas.SetFontAndSize(baseFont, 7); // Fuente Helvetica, tamaño 12
            canvas.SetColorFill(BaseColor.BLACK);
            string Telefono = "0424-123-4567";
            x = 110f; // Coordenada X (horizontal)
            y = 765f; // Coordenada Y (vertical)
            canvas.BeginText();
            canvas.ShowTextAligned(Element.ALIGN_LEFT, Telefono, x, y, 0); // Alineación izquierda, sin rotación
            canvas.EndText();

            // Espacio
            doc.Add(new Paragraph(" "));

            // Datos del cliente
            PdfPTable clienteTable = new PdfPTable(4);
            clienteTable.WidthPercentage = 100;
            clienteTable.SetWidths(new float[] { 15, 25, 15, 45 });

            iTextSharp.text.Font normalFontPq = new iTextSharp.text.Font(helvetica, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font negritaFontPq = new iTextSharp.text.Font(helvetica, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            // Derecha: Número de factura
            PdfPCell LeftCellCliente = new PdfPCell();
            LeftCellCliente.Border = iTextSharp.text.Rectangle.NO_BORDER;
            LeftCellCliente.AddElement(new Paragraph("Cliente:", negritaFontPq) { Alignment = Element.ALIGN_CENTER });
            LeftCellCliente.AddElement(new Paragraph("Documento:", negritaFontPq) { Alignment = Element.ALIGN_CENTER });
            LeftCellCliente.AddElement(new Paragraph("Teléfono:", negritaFontPq) { Alignment = Element.ALIGN_CENTER });
            LeftCellCliente.AddElement(new Paragraph("Dirección:", negritaFontPq) { Alignment = Element.ALIGN_CENTER });

            clienteTable.AddCell(LeftCellCliente);

            // Derecha: Número de factura
            PdfPCell LeftCellCliente2 = new PdfPCell();
            LeftCellCliente2.Border = iTextSharp.text.Rectangle.NO_BORDER;
            LeftCellCliente2.AddElement(new Paragraph("Hoswald", normalFontPq) { Alignment = Element.ALIGN_LEFT });
            LeftCellCliente2.AddElement(new Paragraph("V-30266399", normalFontPq) { Alignment = Element.ALIGN_LEFT });
            LeftCellCliente2.AddElement(new Paragraph("0424-5359972", normalFontPq) { Alignment = Element.ALIGN_LEFT });
            LeftCellCliente2.AddElement(new Paragraph("Barrio el Carmen Guerrera Ana Soto", normalFontPq) { Alignment = Element.ALIGN_LEFT });

            clienteTable.AddCell(LeftCellCliente2);

            // Derecha: Número de factura
            PdfPCell rightCellCliente = new PdfPCell();
            rightCellCliente.Border = iTextSharp.text.Rectangle.NO_BORDER;
            rightCellCliente.AddElement(new Paragraph("Vendedor:", negritaFontPq) { Alignment = Element.ALIGN_CENTER });
            rightCellCliente.AddElement(new Paragraph("Documento:", negritaFontPq) { Alignment = Element.ALIGN_CENTER });
            rightCellCliente.AddElement(new Paragraph("Fecha Emisión:", negritaFontPq) { Alignment = Element.ALIGN_CENTER });
            rightCellCliente.AddElement(new Paragraph("Fecha Vencimiento:", negritaFontPq) { Alignment = Element.ALIGN_CENTER });
            rightCellCliente.AddElement(new Paragraph("N° Control:", negritaFontPq) { Alignment = Element.ALIGN_CENTER });

            clienteTable.AddCell(rightCellCliente);

            // Derecha: Número de factura
            PdfPCell rightCellCliente2 = new PdfPCell();
            rightCellCliente2.Border = iTextSharp.text.Rectangle.NO_BORDER;
            rightCellCliente2.AddElement(new Paragraph("Hoswald", normalFontPq) { Alignment = Element.ALIGN_LEFT });
            rightCellCliente2.AddElement(new Paragraph("19/03/2024", normalFontPq) { Alignment = Element.ALIGN_LEFT });
            rightCellCliente2.AddElement(new Paragraph("19/03/2024", normalFontPq) { Alignment = Element.ALIGN_LEFT });
            rightCellCliente2.AddElement(new Paragraph("V-30266399", normalFontPq) { Alignment = Element.ALIGN_LEFT });
            rightCellCliente2.AddElement(new Paragraph("FAC-000000000", normalFontPq) { Alignment = Element.ALIGN_LEFT });

            clienteTable.AddCell(rightCellCliente2);

            doc.Add(clienteTable);

            // Espacio
            doc.Add(new Paragraph(" "));

            // Detalles de la factura
            PdfPTable detallesTable = new PdfPTable(4);
            detallesTable.WidthPercentage = 100;
            detallesTable.SetWidths(new float[] { 15, 55, 15, 15 });

            // Encabezados de la tabla
            detallesTable.AddCell(new PdfPCell(new Phrase("Cant.", BlacaFont)) { BackgroundColor = new BaseColor(0, 167, 255), HorizontalAlignment = Element.ALIGN_CENTER, FixedHeight = 18f, Border = iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER });
            detallesTable.AddCell(new PdfPCell(new Phrase("Descripción", BlacaFont)) { BackgroundColor = new BaseColor(0, 167, 255), HorizontalAlignment = Element.ALIGN_CENTER, FixedHeight = 18f, Border = iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER });
            detallesTable.AddCell(new PdfPCell(new Phrase("P. Unitario", BlacaFont)) { BackgroundColor = new BaseColor(0, 167, 255), HorizontalAlignment = Element.ALIGN_CENTER, FixedHeight = 18f, Border = iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER });
            detallesTable.AddCell(new PdfPCell(new Phrase("Importe", BlacaFont)) { BackgroundColor = new BaseColor(0, 167, 255), HorizontalAlignment = Element.ALIGN_CENTER, FixedHeight = 18f, Border = iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER });


            // Filas de productos
            decimal total = 0;
            foreach (DataGridViewRow row in dataGridViewProductos.SelectedRows)
            {
                int cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value);
                string descripcion = row.Cells["Descripcion"].Value.ToString();
                decimal precioUnitario = Convert.ToDecimal(row.Cells["PrecioUnitario"].Value);
                decimal importe = Convert.ToDecimal(row.Cells["Importe"].Value);

                detallesTable.AddCell(new PdfPCell(new Phrase(cantidad.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, FixedHeight = 18f, Border = iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER, BorderColor = new BaseColor(188, 188, 188) });
                detallesTable.AddCell(new PdfPCell(new Phrase(descripcion, normalFont)) { HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 18f, Border = iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER, BorderColor = new BaseColor(188, 188, 188) });
                detallesTable.AddCell(new PdfPCell(new Phrase(precioUnitario.ToString("N2"), normalFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, FixedHeight = 18f, Border = iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER, BorderColor = new BaseColor(188, 188, 188) });
                detallesTable.AddCell(new PdfPCell(new Phrase(importe.ToString("N2"), normalFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, FixedHeight = 18f, Border = iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER, BorderColor = new BaseColor(188, 188, 188) });

                total += importe;
            }

            // Total
            detallesTable.AddCell(new PdfPCell(new Phrase(" ")) { Border = iTextSharp.text.Rectangle.NO_BORDER });
            detallesTable.AddCell(new PdfPCell(new Phrase(" ")) { Border = iTextSharp.text.Rectangle.NO_BORDER });
            detallesTable.AddCell(new PdfPCell(new Phrase("Sub Total", normalFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = iTextSharp.text.Rectangle.NO_BORDER });
            detallesTable.AddCell(new PdfPCell(new Phrase(total.ToString("N2"), normalFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, FixedHeight = 18f, Border = iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER, BorderColor = new BaseColor(188, 188, 188) });

            detallesTable.AddCell(new PdfPCell(new Phrase(" ")) { Border = iTextSharp.text.Rectangle.NO_BORDER });
            detallesTable.AddCell(new PdfPCell(new Phrase(" ")) { Border = iTextSharp.text.Rectangle.NO_BORDER });
            detallesTable.AddCell(new PdfPCell(new Phrase("Monto Base Imponible", normalFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = iTextSharp.text.Rectangle.NO_BORDER });
            detallesTable.AddCell(new PdfPCell(new Phrase(total.ToString("N2"), normalFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, FixedHeight = 18f, Border = iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER, BorderColor = new BaseColor(188, 188, 188) });

            detallesTable.AddCell(new PdfPCell(new Phrase(" ")) { Border = iTextSharp.text.Rectangle.NO_BORDER });
            detallesTable.AddCell(new PdfPCell(new Phrase(" ")) { Border = iTextSharp.text.Rectangle.NO_BORDER });
            detallesTable.AddCell(new PdfPCell(new Phrase("Monto IVA", normalFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = iTextSharp.text.Rectangle.NO_BORDER });
            detallesTable.AddCell(new PdfPCell(new Phrase(total.ToString("N2"), normalFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, FixedHeight = 18f, Border = iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER, BorderColor = new BaseColor(188, 188, 188) });

            detallesTable.AddCell(new PdfPCell(new Phrase(" ")) { Border = iTextSharp.text.Rectangle.NO_BORDER });
            detallesTable.AddCell(new PdfPCell(new Phrase(" ")) { Border = iTextSharp.text.Rectangle.NO_BORDER });
            detallesTable.AddCell(new PdfPCell(new Phrase("Monto Exento", normalFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = iTextSharp.text.Rectangle.NO_BORDER });
            detallesTable.AddCell(new PdfPCell(new Phrase(total.ToString("N2"), normalFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, FixedHeight = 18f, Border = iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER, BorderColor = new BaseColor(188, 188, 188) });


            detallesTable.AddCell(new PdfPCell(new Phrase(" ")) { Border = iTextSharp.text.Rectangle.NO_BORDER });
            detallesTable.AddCell(new PdfPCell(new Phrase(" ")) { Border = iTextSharp.text.Rectangle.NO_BORDER });
            detallesTable.AddCell(new PdfPCell(new Phrase("Monto Base IGTF", normalFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = iTextSharp.text.Rectangle.NO_BORDER });
            detallesTable.AddCell(new PdfPCell(new Phrase(total.ToString("N2"), normalFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, FixedHeight = 18f, Border = iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER, BorderColor = new BaseColor(188, 188, 188) });

            detallesTable.AddCell(new PdfPCell(new Phrase(" ")) { Border = iTextSharp.text.Rectangle.NO_BORDER });
            detallesTable.AddCell(new PdfPCell(new Phrase(" ")) { Border = iTextSharp.text.Rectangle.NO_BORDER });
            detallesTable.AddCell(new PdfPCell(new Phrase("Total a pagar", normalFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = iTextSharp.text.Rectangle.NO_BORDER });
            detallesTable.AddCell(new PdfPCell(new Phrase(total.ToString("N2"), BlacaFont)) { BackgroundColor = new BaseColor(0, 167, 255), HorizontalAlignment = Element.ALIGN_RIGHT, FixedHeight = 18f, Border = iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER, BorderColor = new BaseColor(188, 188, 188) });

            doc.Add(detallesTable);

            // Cerrar el documento
            doc.Close();

            // Previsualizar el PDF
            webBrowserPreview.Navigate(tempPdfPath);
            btnExportarPDF.Enabled = true;
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files|*.pdf";
            saveFileDialog.Title = "Guardar Factura";
            saveFileDialog.FileName = "factura.pdf";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.Copy(tempPdfPath, saveFileDialog.FileName, overwrite: true);
                MessageBox.Show("Factura exportada correctamente.");
            }
        }
    }
}
