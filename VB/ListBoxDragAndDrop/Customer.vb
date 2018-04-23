Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace ListBoxDragAndDrop
	Public Class Customer
		Private privateFirstName As String
		Public Property FirstName() As String
			Get
				Return privateFirstName
			End Get
			Set(ByVal value As String)
				privateFirstName = value
			End Set
		End Property
		Private privateLastName As String
		Public Property LastName() As String
			Get
				Return privateLastName
			End Get
			Set(ByVal value As String)
				privateLastName = value
			End Set
		End Property
	End Class
End Namespace
