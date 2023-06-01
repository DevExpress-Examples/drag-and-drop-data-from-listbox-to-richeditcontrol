<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128607607/21.1.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4488)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# How to drag-and-drop data from ListBox to RichEditControl

This example illustrates how to perform the drag-and-drop operation in case of a ListBox drag-and-drop source. Since there is no built-in capability to accomplish this task, you should perform the drag-and-drop operation in a custom manner. To do this, use the following RichEditControl events:

* PreviewDragEnter
* PreviewDragOver
* PreviewDrop

Most part of the work is performed in the `PreviewDragOver` event handler. Here, you should update the `RichEditControl.Document.CaretPosition` property value according to the current mouse position. Also, you might want to draw a custom insertion marker in this event handler. Finally, note how the `RichEditControl.ScrollToCaret` method is used to scroll the document to the caret position if you move the caret outside of the visible document area.

## Files to Review

* [Customer.cs](./CS/ListBoxDragAndDrop/Customer.cs) (VB: [Customer.vb](./VB/ListBoxDragAndDrop/Customer.vb))
* [Helpers.cs](./CS/ListBoxDragAndDrop/Helpers.cs) (VB: [Helpers.vb](./VB/ListBoxDragAndDrop/Helpers.vb))
* [MainWindow.xaml](./CS/ListBoxDragAndDrop/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/ListBoxDragAndDrop/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/ListBoxDragAndDrop/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/ListBoxDragAndDrop/MainWindow.xaml.vb))
## More Examples

* [DXRichEdit for WPF: How to use hit testing](https://github.com/DevExpress-Examples/dxrichedit-for-wpf-how-to-use-hit-testing-e2765)
* [How to do drag and drop items from one ListBoxEdit to another](https://github.com/DevExpress-Examples/how-to-do-drag-and-drop-items-from-one-listboxedit-to-another-e3122)