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
        Inherits Window

        Private isDragStarted As Boolean

        Private rectangle As Rectangle

        Private surface As Canvas = Nothing

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub richEditControl1_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.surface = TryCast(richEditControl1.Template.FindName("Surface", richEditControl1), Canvas)
        End Sub

        Private Sub MainWindow_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            richEditControl1.Views.SimpleView.Padding = New DevExpress.Portable.PortablePadding(0)
            listBoxEdit1.ItemsSource = ListBoxDragAndDrop.DataHelper.GenerateCustomers()
        End Sub

#Region "ListBoxDrag"
        Private Sub list_PreviewMouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
            If e.LeftButton Is MouseButtonState.Pressed Then
                If Me.isDragStarted Then
                    Dim listBoxEdit As DevExpress.Xpf.Editors.ListBoxEdit = CType(sender, DevExpress.Xpf.Editors.ListBoxEdit)
                    Dim item As ListBoxDragAndDrop.Customer = CType(listBoxEdit.SelectedItem, ListBoxDragAndDrop.Customer)
                    If item IsNot Nothing Then
                        Dim data As DataObject = Me.CreateDataObject(item)
                        data.SetData("dragSource", listBoxEdit)
                        DragDrop.DoDragDrop(listBoxEdit, data, DragDropEffects.Copy)
                        Me.isDragStarted = False
                    End If
                End If
            End If
        End Sub

        Private Sub list_MouseLeftButtonDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            Dim listBoxEdit As DevExpress.Xpf.Editors.ListBoxEdit = CType(sender, DevExpress.Xpf.Editors.ListBoxEdit)
            Dim hitObject As DependencyObject = TryCast(listBoxEdit.InputHitTest(e.GetPosition(listBoxEdit)), DependencyObject)
            Dim hitItem As FrameworkElement = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject(Of DevExpress.Xpf.Editors.ListBoxEditItem)(hitObject)
            If hitItem IsNot Nothing Then
                Me.isDragStarted = True
            End If
        End Sub

        Private Function CreateDataObject(ByVal item As ListBoxDragAndDrop.Customer) As DataObject
            Dim data As DataObject = New DataObject()
            data.SetData(GetType(ListBoxDragAndDrop.Customer), item)
            Return data
        End Function

#End Region
#Region "RichEditDrop"
        Private Sub richEditControl1_PreviewDragEnter(ByVal sender As Object, ByVal e As DragEventArgs)
            If e.Data.GetDataPresent(DataFormats.StringFormat) Then e.Effects = DragDropEffects.Copy
        End Sub

        Private Sub richEditControl1_PreviewDragOver(ByVal sender As Object, ByVal e As DragEventArgs)
            Dim windowsPoint As Point = e.GetPosition(CType(richEditControl1, UIElement))
            Dim pos As DevExpress.XtraRichEdit.API.Native.DocumentPosition = ListBoxDragAndDrop.RichEditHelper.GetDocumentPositionFromWindowsPoint(richEditControl1, windowsPoint)
            If pos Is Nothing Then Return
            Me.DrawRectange(pos)
            richEditControl1.Document.CaretPosition = pos
            richEditControl1.ScrollToCaret()
        End Sub

        Private Sub richEditControl1_PreviewDrop(ByVal sender As Object, ByVal e As DragEventArgs)
            Dim item As ListBoxDragAndDrop.Customer = CType(e.Data.GetData(GetType(ListBoxDragAndDrop.Customer)), ListBoxDragAndDrop.Customer)
            richEditControl1.Document.InsertText(richEditControl1.Document.CaretPosition, item.FirstName & " " & item.LastName)
            Me.surface.Children.Remove(Me.rectangle)
        End Sub

        Public Sub DrawRectange(ByVal pos As DevExpress.XtraRichEdit.API.Native.DocumentPosition)
            If Me.surface.Children.Contains(Me.rectangle) Then Me.surface.Children.Remove(Me.rectangle)
            Dim drawingRectange As System.Drawing.RectangleF = ListBoxDragAndDrop.RichEditHelper.GetRectangleFromDocumentPosition(richEditControl1, pos)
            Me.rectangle = New Rectangle() With {.Stroke = Brushes.Blue, .StrokeThickness = 1, .Width = 2, .Height = drawingRectange.Height}
            Me.surface.Children.Add(Me.rectangle)
            Canvas.SetLeft(Me.rectangle, drawingRectange.X)
            Canvas.SetTop(Me.rectangle, drawingRectange.Y)
        End Sub
#End Region
    End Class
End Namespace
