using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Core.Native;
using DevExpress.Office.Utils;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.Portable;

namespace ListBoxDragAndDrop {
    public partial class MainWindow : Window {

        bool isDragStarted;
        private Rectangle rectangle;

        public MainWindow() {
            InitializeComponent();
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            richEditControl1.Views.SimpleView.Padding = new PortablePadding(0);
            listBoxEdit1.ItemsSource = DataHelper.GenerateCustomers();
        }

        #region ListBoxDrag
        void list_PreviewMouseMove(object sender, MouseEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                if (isDragStarted) {
                    ListBoxEdit listBoxEdit = (ListBoxEdit)sender;
                    Customer item = (Customer)listBoxEdit.SelectedItem;

                    if (item != null) {
                        DataObject data = CreateDataObject(item);
                        data.SetData("dragSource", listBoxEdit);
                        DragDrop.DoDragDrop(listBoxEdit, data, DragDropEffects.Copy);
                        isDragStarted = false;
                    }
                }
            }
        }

        void list_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            ListBoxEdit listBoxEdit = (ListBoxEdit)sender;
            DependencyObject hitObject = listBoxEdit.InputHitTest(e.GetPosition(listBoxEdit)) as DependencyObject;
            FrameworkElement hitItem = LayoutHelper.FindParentObject<ListBoxEditItem>(hitObject);

            if (hitItem != null) {
                isDragStarted = true;
            }
        }

        private DataObject CreateDataObject(Customer item) {
            DataObject data = new DataObject();
            data.SetData(typeof(Customer), item);
            return data;
        }
        #endregion


        #region RichEditDrop
        private void richEditControl1_PreviewDragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
                e.Effects = DragDropEffects.Copy;
        }

        private void richEditControl1_PreviewDragOver(object sender, DragEventArgs e) {
            Point windowsPoint = e.GetPosition((UIElement)richEditControl1);
            DocumentPosition pos = RichEditHelper.GetDocumentPositionFromWindowsPoint(richEditControl1, windowsPoint);

            if (pos == null)
                return;

            DrawRectange(pos);

            richEditControl1.Document.CaretPosition = pos;
            richEditControl1.ScrollToCaret();
        }

        private void richEditControl1_PreviewDrop(object sender, DragEventArgs e) {
            Customer item = (Customer)e.Data.GetData(typeof(Customer));

            richEditControl1.Document.InsertText(
                richEditControl1.Document.CaretPosition, item.FirstName + " " + item.LastName);

            canvas.Children.Remove(rectangle);
        }

        public void DrawRectange(DocumentPosition pos) {
            if (canvas.Children.Contains(rectangle))
                canvas.Children.Remove(rectangle);

            System.Drawing.RectangleF drawingRectange = RichEditHelper.GetRectangleFromDocumentPosition(richEditControl1, pos);

            rectangle = new Rectangle() {
                Stroke = Brushes.Blue,
                StrokeThickness = 1,
                Width = 2,
                Height = drawingRectange.Height
            };

            canvas.Children.Add(rectangle);

            Canvas.SetLeft(rectangle, drawingRectange.X);
            Canvas.SetTop(rectangle, drawingRectange.Y);
        }
        #endregion
    }
}