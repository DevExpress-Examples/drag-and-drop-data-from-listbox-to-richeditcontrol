// Developer Express Code Central Example:
// How to do drag and drop items from one ListBoxEdit to another
// 
// This example shows how to implement the drag-and-drop functionality for
// ListBoxEdit.
// 
// First of all, you need to set the AllowDrop
// (http://documentation.devexpress.dev/#WPF/DevExpressXpfCoreDXFrameworkContentElement_AllowDroptopic)
// property to true, in order to let your editor to accept dropping. Next, you'll
// need implement four event handlers for the editor to manage the drag-and-drop
// process:
// 
// 1. MouseLeftButtonDown event handler. Within this handler, it is
// necessary to find out if the click occurred on a certain item. If so, the
// isDragStarted flag is set to True, to allow all the following processing.
// 
// 2.
// PreviewMouseMove event handler. If the isDragStarted flag is set to True, it
// then defines a dragged item and the dragging source object. Then, the
// DragDrop.DoDragDrop() method that initiates a drag-and-drop operation is
// invoked.
// 
// 3. DragOver event handler. Defines the behavior of a drag-and-drop
// operation: if the event's source object can accept a dragged object, the
// e.Effects property is set to the appropriate DragDropEffect value. Otherwise, it
// is set to the DragDropEffects.None value.
// 
// 4. Drop event handler. Handles
// accepting the dragged item by dragging the destination object. Note, that it is
// necessary to create a clone of the copying item to avoid collisions. Do not add
// a dragged item to the items collection itself, until you are sure that this is
// appropriate for your task.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E3122

using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("ListBoxDragAndDrop")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Microsoft")]
[assembly: AssemblyProduct("ListBoxDragAndDrop")]
[assembly: AssemblyCopyright("Copyright © Microsoft 2011")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

//In order to begin building localizable applications, set 
//<UICulture>CultureYouAreCodingWith</UICulture> in your .csproj file
//inside a <PropertyGroup>.  For example, if you are using US english
//in your source files, set the <UICulture> to en-US.  Then uncomment
//the NeutralResourceLanguage attribute below.  Update the "en-US" in
//the line below to match the UICulture setting in the project file.

//[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)]


[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
    //(used if a resource is not found in the page, 
    // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
    //(used if a resource is not found in the page, 
    // app, or any theme specific resource dictionaries)
)]


// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
