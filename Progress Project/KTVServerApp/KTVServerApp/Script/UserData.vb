Public Class UserData
    Private m_id As Integer
    Private m_fname As String
    Private m_lname As String
    Private m_user As String
    Private m_gender As String
    Private m_usertype As String

    Private Shared instance As New UserData()
    Public Sub New()

    End Sub
    Private Shared Sub SetData(id As Integer, fname As String, lname As String, user As String, gender As String, type As String)
        instance.m_id = id
        instance.m_fname = fname
        instance.m_lname = lname
        instance.m_user = user
        instance.m_gender = gender
        instance.m_usertype = type
    End Sub
    Public Shared Sub Clear()
        If (instance IsNot Nothing) Then
            instance.m_id = 0
            instance.m_fname = ""
            instance.m_lname = ""
            instance.m_user = ""
            instance.m_usertype = ""
            instance.m_gender = ""
        End If
    End Sub
    Public Shared Function GetUser() As UserData
        Return instance
    End Function
    Public Shared Function GetInstance(id As Integer, fname As String, lname As String, user As String, gender As String, type As String)
        SetData(id, fname, lname, user, gender, type)
        Return instance
    End Function

    Public Shared Property ID As String
        Get
            Return instance.m_id
        End Get
        Set(value As String)
            instance.m_id = value
        End Set
    End Property
    Public Shared Property FirstName As String
        Get
            Return instance.m_fname
        End Get
        Set(value As String)
            instance.m_fname = value
        End Set
    End Property
    Public Shared Property LastName As String
        Get
            Return instance.m_lname
        End Get
        Set(value As String)
            instance.m_lname = value
        End Set
    End Property
    Public Shared Property UserName As String
        Get
            Return instance.m_user
        End Get
        Set(value As String)
            instance.m_user = value
        End Set
    End Property
    Public Shared Property Gender As String
        Get
            Return instance.m_gender
        End Get
        Set(value As String)
            instance.m_gender = value
        End Set
    End Property
    Public Shared Property UserType As String
        Get
            Return instance.m_usertype
        End Get
        Set(value As String)
            instance.m_usertype = value
        End Set
    End Property
End Class
