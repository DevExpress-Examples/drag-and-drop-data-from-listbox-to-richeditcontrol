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
Imports DevExpress.Portable

Namespace ListBoxDragAndDrop

    Public Partial Class MainWindow
        Inherits System.Windows.Window

        Private isDragStarted As Boolean

        Private rectangle As System.Windows.Shapes.Rectangle

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub MainWindow_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.richEditControl1.Views.SimpleView.Padding = New DevExpress.Portable.PortablePadding(0)
            Me.listBoxEdit1.ItemsSource = ListBoxDragAndDrop.DataHelper.GenerateCustomers()
        End Sub

#Region "ListBoxDrag"
        Private Sub list_PreviewMouseMove(ByVal sender As Object, ByVal e As System.Windows.Input.MouseEventArgs)
            If e.LeftButton = System.Windows.Input.MouseButtonState.Pressed Then
                If Me.isDragStarted Then
                    Dim listBoxEdit As DevExpress.Xpf.Editors.ListBoxEdit = CType(sender, DevExpress.Xpf.Editors.ListBoxEdit)
                    Dim item As ListBoxDragAndDrop.Customer = CType(listBoxEdit.SelectedItem, ListBoxDragAndDrop.Customer)
                    If item IsNot Nothing Then
                        Dim data As System.Windows.DataObject = Me.CreateDataObject(item)
                        data.SetData("dragSource", listBoxEdit)
                        Call System.Windows.DragDrop.DoDragDrop(listBoxEdit, data, System.Windows.DragDropEffects.Copy)
                        Me.isDragStarted = False
                    End If
                End If
            End If
        End Sub

        Private Sub list_MouseLeftButtonDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs)
            Dim listBoxEdit As DevExpress.Xpf.Editors.ListBoxEdit = CType(sender, DevExpress.Xpf.Editors.ListBoxEdit)
            Dim hitObject As System.Windows.DependencyObject = TryCast(listBoxEdit.InputHitTest(e.GetPosition(listBoxEdit)), System.Windows.DependencyObject)
            Dim hitItem As System.Windows.FrameworkElement = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject(Of DevExpress.Xpf.Editors.ListBoxEditItem)(hitObject)
            If hitItem IsNot Nothing Then
                Me.isDragStarted = True
            End If
        End Sub

        Private Function CreateDataObject(ByVal item As ListBoxDragAndDrop.Customer) As DataObject
            Dim data As System.Windows.DataObject = New System.Windows.DataObject()
            data.SetData(GetType(ListBoxDragAndDrop.Customer), item)
            Return data
        End Function

#End Region
#Region "RichEditDrop"
        Private Sub richEditControl1_PreviewDragEnter(ByVal sender As Object, ByVal e As System.Windows.DragEventArgs)
            If e.Data.GetDataPresent(System.Windows.DataFormats.StringFormat) Then e.Effects = System.Windows.DragDropEffects.Copy
        End Sub

        Private Sub richEditControl1_PreviewDragOver(ByVal sender As Object, ByVal e As System.Windows.DragEventArgs)
            Dim windowsPoint As System.Windows.Point = e.GetPosition(CType(Me.richEditControl1, System.Windows.UIElement))
            Dim pos As DevExpress.XtraRichEdit.API.Native.DocumentPosition = ListBoxDragAndDrop.RichEditHelper.GetDocumentPositionFromWindowsPoint(Me.richEditControl1, windowsPoint)
            If pos Is Nothing Then Return
            Me.DrawRectange(pos)
            Me.richEditControl1.Document.CaretPosition = pos
            Me.richEditControl1.ScrollToCaret()
        End Sub

        Private Sub richEditControl1_PreviewDrop(ByVal sender As Object, ByVal e As System.Windows.DragEventArgs)
            Dim item As ListBoxDragAndDrop.Customer = CType(e.Data.GetData(GetType(ListBoxDragAndDrop.Customer)), ListBoxDragAndDrop.Customer)
            Me.richEditControl1.Document.InsertText(Me.richEditControl1.Document.CaretPosition, item.FirstName & " " & item.LastName)
            Me.canvas.Children.Remove(Me.rectangle)
        End Sub

        Public Sub DrawRectange(ByVal pos As DevExpress.XtraRichEdit.API.Native.DocumentPosition)
            If Me.canvas.Children.Contains(Me.rectangle) Then Me.canvas.Children.Remove(Me.rectangle)
            Dim drawingRectange As System.Drawing.RectangleF = ListBoxDragAndDrop.RichEditHelper.GetRectangleFromDocumentPosition(Me.richEditControl1, pos)
            Me.rectangle = New System.Windows.Shapes.Rectangle() With {.Stroke = System.Windows.Media.Brushes.Blue, .StrokeThickness = 1, .Width = 2, .Height = drawingRectange.Height}
            Me.canvas.Children.Add(Me.rectangle)
            Call System.Windows.Controls.Canvas.SetLeft(Me.rectangle, drawingRectange.X)
            Call System.Windows.Controls.Canvas.SetTop(Me.rectangle, drawingRectange.Y)
        End Sub
#End Region
    End Class
End Namespace
