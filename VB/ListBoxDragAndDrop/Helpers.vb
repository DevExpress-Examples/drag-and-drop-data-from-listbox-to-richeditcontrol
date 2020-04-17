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
	Public Module DataHelper
		Public Function GenerateCustomers() As ObservableCollection(Of Customer)
			Return New ObservableCollection(Of Customer)() From {
				New Customer() With {
					.FirstName = "John",
					.LastName = "Cassano"
				},
				New Customer() With {
					.FirstName = "Michael",
					.LastName = "Osvald"
				},
				New Customer() With {
					.FirstName = "Miles",
					.LastName = "Keaton"
				},
				New Customer() With {
					.FirstName = "Tim",
					.LastName = "Devenport"
				}
			}
		End Function
	End Module

	Public Module RichEditHelper
		Public Function GetDocumentPositionFromWindowsPoint(ByVal richEditControl As RichEditControl, ByVal windowsPoint As Point) As DocumentPosition
			Dim drawingPoint As New System.Drawing.PointF(Units.PixelsToDocumentsF(CSng(windowsPoint.X), richEditControl.DpiX), Units.PixelsToDocumentsF(CSng(windowsPoint.Y), richEditControl.DpiY))
			Dim pos As DocumentPosition = richEditControl.GetPositionFromPoint(drawingPoint)

			Return pos
		End Function

		Public Function GetRectangleFromDocumentPosition(ByVal richEditControl As RichEditControl, ByVal pos As DocumentPosition) As System.Drawing.Rectangle
			Return Units.DocumentsToPixels(richEditControl.GetBoundsFromPosition(pos), richEditControl.DpiX, richEditControl.DpiY)
		End Function

	End Module
End Namespace
