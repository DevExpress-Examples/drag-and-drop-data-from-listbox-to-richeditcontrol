Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Collections.ObjectModel
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Office.Utils
Imports DevExpress.XtraRichEdit.API.Native

Namespace ListBoxDragAndDrop
	Partial Public Class MainWindow
		Inherits Window

		Private isDragStarted As Boolean
		Private rectangle As Rectangle

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub MainWindow_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
			richEditControl1.Views.SimpleView.Padding = New System.Windows.Forms.Padding(0)
			listBoxEdit1.ItemsSource = DataHelper.GenerateCustomers()
		End Sub

		#Region "ListBoxDrag"
		Private Sub list_PreviewMouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
			If e.LeftButton = MouseButtonState.Pressed Then
				If isDragStarted Then
					Dim listBoxEdit As ListBoxEdit = CType(sender, ListBoxEdit)
					Dim item As Customer = CType(listBoxEdit.SelectedItem, Customer)

					If item IsNot Nothing Then
						Dim data As DataObject = CreateDataObject(item)
						data.SetData("dragSource", listBoxEdit)
						DragDrop.DoDragDrop(listBoxEdit, data, DragDropEffects.Copy)
						isDragStarted = False
					End If
				End If
			End If
		End Sub

		Private Sub list_MouseLeftButtonDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
			Dim listBoxEdit As ListBoxEdit = CType(sender, ListBoxEdit)
			Dim hitObject As DependencyObject = TryCast(listBoxEdit.InputHitTest(e.GetPosition(listBoxEdit)), DependencyObject)
			Dim hitItem As FrameworkElement = LayoutHelper.FindParentObject(Of ListBoxEditItem)(hitObject)

			If hitItem IsNot Nothing Then
				isDragStarted = True
			End If
		End Sub

		Private Function CreateDataObject(ByVal item As Customer) As DataObject
			Dim data As New DataObject()
			data.SetData(GetType(Customer), item)
			Return data
		End Function
		#End Region


		#Region "RichEditDrop"
		Private Sub richEditControl1_PreviewDragEnter(ByVal sender As Object, ByVal e As DragEventArgs)
			If e.Data.GetDataPresent(DataFormats.StringFormat) Then
				e.Effects = DragDropEffects.Copy
			End If
		End Sub

		Private Sub richEditControl1_PreviewDragOver(ByVal sender As Object, ByVal e As DragEventArgs)
			Dim windowsPoint As Point = e.GetPosition(CType(richEditControl1, UIElement))
			Dim pos As DocumentPosition = RichEditHelper.GetDocumentPositionFromWindowsPoint(richEditControl1, windowsPoint)

			If pos Is Nothing Then
				Return
			End If

			DrawRectange(pos)

			richEditControl1.Document.CaretPosition = pos
			richEditControl1.ScrollToCaret()
		End Sub

		Private Sub richEditControl1_PreviewDrop(ByVal sender As Object, ByVal e As DragEventArgs)
			Dim item As Customer = CType(e.Data.GetData(GetType(Customer)), Customer)

			richEditControl1.Document.InsertText(richEditControl1.Document.CaretPosition, item.FirstName & " " & item.LastName)

			canvas.Children.Remove(rectangle)
		End Sub

		Public Sub DrawRectange(ByVal pos As DocumentPosition)
			If canvas.Children.Contains(rectangle) Then
				canvas.Children.Remove(rectangle)
			End If

			Dim drawingRectange As System.Drawing.RectangleF = RichEditHelper.GetRectangleFromDocumentPosition(richEditControl1, pos)

			rectangle = New Rectangle() With {.Stroke = Brushes.Blue, .StrokeThickness = 1, .Width = 2, .Height = drawingRectange.Height}

			canvas.Children.Add(rectangle)

			Canvas.SetLeft(rectangle, drawingRectange.X)
			Canvas.SetTop(rectangle, drawingRectange.Y)
		End Sub
		#End Region
	End Class
End Namespace