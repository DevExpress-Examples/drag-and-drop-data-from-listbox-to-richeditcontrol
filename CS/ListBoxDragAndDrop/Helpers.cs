using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraRichEdit.API.Native;
using System.Windows;
using DevExpress.Xpf.RichEdit;
using DevExpress.Office.Utils;
using System.Collections.ObjectModel;

namespace ListBoxDragAndDrop {
    public static class DataHelper { 
        public static ObservableCollection<Customer> GenerateCustomers() {
            return new  ObservableCollection<Customer>() { 
                new Customer(){ FirstName = "John", LastName = "Cassano"}, 
                new Customer(){ FirstName = "Michael", LastName = "Osvald" },
                new Customer(){ FirstName = "Miles", LastName = "Keaton"},
                new Customer(){ FirstName = "Tim", LastName = "Devenport"}
            };
        }
    }

    public static class RichEditHelper {
        public static DocumentPosition GetDocumentPositionFromWindowsPoint(RichEditControl richEditControl, Point windowsPoint) {
            System.Drawing.PointF drawingPoint = new System.Drawing.PointF(
                Units.PixelsToDocumentsF((float)windowsPoint.X, richEditControl.DpiX), 
                Units.PixelsToDocumentsF((float)windowsPoint.Y, richEditControl.DpiY));
            DocumentPosition pos = richEditControl.GetPositionFromPoint(drawingPoint);
            
            return pos;
        }

        public static System.Drawing.Rectangle GetRectangleFromDocumentPosition(RichEditControl richEditControl, DocumentPosition pos) {
            return Units.DocumentsToPixels(richEditControl.GetBoundsFromPosition(pos),
                richEditControl.DpiX, richEditControl.DpiY);
        }

    }
}
