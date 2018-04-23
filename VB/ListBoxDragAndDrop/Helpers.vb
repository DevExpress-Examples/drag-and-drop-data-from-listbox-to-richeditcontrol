Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.XtraRichEdit.API.Native
Imports System.Windows
Imports DevExpress.Xpf.RichEdit
Imports DevExpress.Office.Utils
Imports System.Collections.ObjectModel

Namespace ListBoxDragAndDrop
	Public NotInheritable Class DataHelper
		Private Sub New()
		End Sub
		Public Shared Function GenerateCustomers() As ObservableCollection(Of Customer)
            Return New ObservableCollection(Of Customer) _
                (New Customer() {
                 New Customer() With {.FirstName = "John", .LastName = "Cassano"},
                 New Customer() With {.FirstName = "Michael", .LastName = "Osvald"},
                 New Customer() With {.FirstName = "Miles", .LastName = "Keaton"},
                 New Customer() With {.FirstName = "Tim", .LastName = "Devenport"}
             })
		End Function
	End Class

	Public NotInheritable Class RichEditHelper
		Private Sub New()
		End Sub
		Public Shared Function GetDocumentPositionFromWindowsPoint(ByVal richEditControl As RichEditControl, ByVal windowsPoint As Point) As DocumentPosition
			Dim drawingPoint As New System.Drawing.PointF(Units.PixelsToDocumentsF(CSng(windowsPoint.X), richEditControl.DpiX), Units.PixelsToDocumentsF(CSng(windowsPoint.Y), richEditControl.DpiY))
			Dim pos As DocumentPosition = richEditControl.GetPositionFromPoint(drawingPoint)

			Return pos
		End Function

		Public Shared Function GetRectangleFromDocumentPosition(ByVal richEditControl As RichEditControl, ByVal pos As DocumentPosition) As System.Drawing.Rectangle
			Return Units.DocumentsToPixels(richEditControl.GetBoundsFromPosition(pos), richEditControl.DpiX, richEditControl.DpiY)
		End Function

	End Class
End Namespace
