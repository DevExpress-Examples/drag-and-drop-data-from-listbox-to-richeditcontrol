<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128607607/20.1.2%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4488)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [Customer.cs](./CS/ListBoxDragAndDrop/Customer.cs) (VB: [Customer.vb](./VB/ListBoxDragAndDrop/Customer.vb))
* [Helpers.cs](./CS/ListBoxDragAndDrop/Helpers.cs) (VB: [Helpers.vb](./VB/ListBoxDragAndDrop/Helpers.vb))
* [MainWindow.xaml](./CS/ListBoxDragAndDrop/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/ListBoxDragAndDrop/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/ListBoxDragAndDrop/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/ListBoxDragAndDrop/MainWindow.xaml.vb))
<!-- default file list end -->
# How to drag-and-drop data from ListBox to RichEditControl


<p>This example illustrates how to perform the drag-and-drop operation in case of a ListBox drag-and-drop source. Since there is no built-in capability to accomplish this task, you should perform the drag-and-drop operation in a custom manner. To do this, use the following RichEditControl events: PreviewDragEnter,  PreviewDragOver, PreviewDrop.</p><p>Most part of the work is performed in the PreviewDragOver event handler. Here, you should update the RichEditControl.Document.CaretPosition property value according to the current mouse position. Also, you might want to draw a custom insertion marker in this event handler. Finally, note how the RichEditControl.ScrollToCaret Method is used to scroll the document to the caret position if you move the caret outside of the visible document area.</p><p><strong>See </strong><strong>also:</strong><strong><br />
</strong><a href="https://www.devexpress.com/Support/Center/p/E2765">DXRichEdit for WPF: How to use hit testing</a><br />
<a href="https://www.devexpress.com/Support/Center/p/E3122">How to do drag and drop items from one ListBoxEdit to another</a></p>

<br/>


